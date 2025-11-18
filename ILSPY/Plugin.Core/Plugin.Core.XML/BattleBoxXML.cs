using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;

namespace Plugin.Core.XML;

public class BattleBoxXML
{
	public static List<BattleBoxModel> BBoxes = new List<BattleBoxModel>();

	public static List<ShopData> ShopDataBattleBoxes = new List<ShopData>();

	public static int TotalBoxes;

	public static void Load()
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Data\\BBoxes");
		if (!directoryInfo.Exists)
		{
			return;
		}
		FileInfo[] files = directoryInfo.GetFiles();
		foreach (FileInfo fileInfo in files)
		{
			try
			{
				smethod_0(int.Parse(fileInfo.Name.Substring(0, fileInfo.Name.Length - 4)));
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}
		smethod_4();
		CLogger.Print($"Plugin Loaded: {BBoxes.Count} Battle Boxes", LoggerType.Info);
	}

	public static void Reload()
	{
		BBoxes.Clear();
		ShopDataBattleBoxes.Clear();
		TotalBoxes = 0;
	}

	private static void smethod_0(int int_0)
	{
		string text = $"Data/BBoxes/{int_0}.xml";
		if (File.Exists(text))
		{
			smethod_1(text, int_0);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
	}

	private static void smethod_1(string string_0, int int_0)
	{
		XmlDocument xmlDocument = new XmlDocument();
		using FileStream fileStream = new FileStream(string_0, FileMode.Open);
		if (fileStream.Length == 0L)
		{
			CLogger.Print("File is empty: " + string_0, LoggerType.Warning);
		}
		else
		{
			try
			{
				xmlDocument.Load(fileStream);
				for (XmlNode xmlNode = xmlDocument.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					if ("List".Equals(xmlNode.Name))
					{
						for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
						{
							if ("BattleBox".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								BattleBoxModel battleBoxModel = new BattleBoxModel
								{
									CouponId = int_0,
									RequireTags = int.Parse(attributes.GetNamedItem("RequireTags").Value),
									Items = new List<BattleBoxItem>()
								};
								smethod_2(xmlNode2, battleBoxModel);
								battleBoxModel.InitItemPercentages();
								BBoxes.Add(battleBoxModel);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}
		fileStream.Dispose();
		fileStream.Close();
	}

	private static void smethod_2(XmlNode xmlNode_0, BattleBoxModel battleBoxModel_0)
	{
		for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
		{
			if ("Rewards".Equals(xmlNode.Name))
			{
				for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
				{
					if ("Good".Equals(xmlNode2.Name))
					{
						XmlNamedNodeMap attributes = xmlNode2.Attributes;
						BattleBoxItem item = new BattleBoxItem
						{
							GoodsId = int.Parse(attributes.GetNamedItem("Id").Value),
							Percent = int.Parse(attributes.GetNamedItem("Percent").Value)
						};
						battleBoxModel_0.Items.Add(item);
					}
				}
			}
		}
	}

	private static byte[] smethod_3(int int_0, int int_1, ref int int_2, List<BattleBoxModel> list_0)
	{
		int_2 = 0;
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		for (int i = int_1 * int_0; i < list_0.Count; i++)
		{
			smethod_5(list_0[i], syncServerPacket);
			if (++int_2 == int_0)
			{
				break;
			}
		}
		return syncServerPacket.ToArray();
	}

	private static void smethod_4()
	{
		List<BattleBoxModel> list = new List<BattleBoxModel>();
		lock (BBoxes)
		{
			foreach (BattleBoxModel bBox in BBoxes)
			{
				list.Add(bBox);
			}
		}
		TotalBoxes = list.Count;
		int num = (int)Math.Ceiling((double)list.Count / 100.0);
		int int_ = 0;
		for (int i = 0; i < num; i++)
		{
			byte[] buffer = smethod_3(100, i, ref int_, list);
			ShopData item = new ShopData
			{
				Buffer = buffer,
				ItemsCount = int_,
				Offset = i * 100
			};
			ShopDataBattleBoxes.Add(item);
		}
	}

	private static void smethod_5(BattleBoxModel battleBoxModel_0, SyncServerPacket syncServerPacket_0)
	{
		syncServerPacket_0.WriteD(battleBoxModel_0.CouponId);
		syncServerPacket_0.WriteH((ushort)battleBoxModel_0.RequireTags);
		syncServerPacket_0.WriteH(0);
		syncServerPacket_0.WriteH(0);
		syncServerPacket_0.WriteC(0);
	}

	public static BattleBoxModel GetBattleBox(int BattleBoxId)
	{
		if (BattleBoxId == 0)
		{
			return null;
		}
		lock (BBoxes)
		{
			foreach (BattleBoxModel bBox in BBoxes)
			{
				if (bBox.CouponId == BattleBoxId)
				{
					return bBox;
				}
			}
		}
		return null;
	}
}
