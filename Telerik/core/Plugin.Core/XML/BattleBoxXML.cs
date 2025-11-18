using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class BattleBoxXML
	{
		public static List<BattleBoxModel> BBoxes;

		public static List<ShopData> ShopDataBattleBoxes;

		public static int TotalBoxes;

		static BattleBoxXML()
		{
			BattleBoxXML.BBoxes = new List<BattleBoxModel>();
			BattleBoxXML.ShopDataBattleBoxes = new List<ShopData>();
		}

		public BattleBoxXML()
		{
		}

		public static BattleBoxModel GetBattleBox(int BattleBoxId)
		{
			BattleBoxModel battleBoxModel;
			if (BattleBoxId == 0)
			{
				return null;
			}
			lock (BattleBoxXML.BBoxes)
			{
				List<BattleBoxModel>.Enumerator enumerator = BattleBoxXML.BBoxes.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						BattleBoxModel current = enumerator.Current;
						if (current.CouponId != BattleBoxId)
						{
							continue;
						}
						battleBoxModel = current;
						return battleBoxModel;
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
			}
			return battleBoxModel;
		}

		public static void Load()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(string.Concat(Directory.GetCurrentDirectory(), "\\Data\\BBoxes"));
			if (!directoryInfo.Exists)
			{
				return;
			}
			FileInfo[] files = directoryInfo.GetFiles();
			for (int i = 0; i < (int)files.Length; i++)
			{
				FileInfo fileInfo = files[i];
				try
				{
					BattleBoxXML.smethod_0(int.Parse(fileInfo.Name.Substring(0, fileInfo.Name.Length - 4)));
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					CLogger.Print(exception.Message, LoggerType.Error, exception);
				}
			}
			BattleBoxXML.smethod_4();
			CLogger.Print(string.Format("Plugin Loaded: {0} Battle Boxes", BattleBoxXML.BBoxes.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			BattleBoxXML.BBoxes.Clear();
			BattleBoxXML.ShopDataBattleBoxes.Clear();
			BattleBoxXML.TotalBoxes = 0;
		}

		private static void smethod_0(int int_0)
		{
			string str = string.Format("Data/BBoxes/{0}.xml", int_0);
			if (File.Exists(str))
			{
				BattleBoxXML.smethod_1(str, int_0);
				return;
			}
			CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
		}

		private static void smethod_1(string string_0, int int_0)
		{
			XmlDocument xmlDocument = new XmlDocument();
			using (FileStream fileStream = new FileStream(string_0, FileMode.Open))
			{
				if (fileStream.Length != 0)
				{
					try
					{
						xmlDocument.Load(fileStream);
						for (XmlNode i = xmlDocument.FirstChild; i != null; i = i.NextSibling)
						{
							if ("List".Equals(i.Name))
							{
								for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
								{
									if ("BattleBox".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										BattleBoxModel battleBoxModel = new BattleBoxModel()
										{
											CouponId = int_0,
											RequireTags = int.Parse(attributes.GetNamedItem("RequireTags").Value),
											Items = new List<BattleBoxItem>()
										};
										BattleBoxXML.smethod_2(j, battleBoxModel);
										battleBoxModel.InitItemPercentages();
										BattleBoxXML.BBoxes.Add(battleBoxModel);
									}
								}
							}
						}
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						CLogger.Print(exception.Message, LoggerType.Error, exception);
					}
				}
				else
				{
					CLogger.Print(string.Concat("File is empty: ", string_0), LoggerType.Warning, null);
				}
				fileStream.Dispose();
				fileStream.Close();
			}
		}

		private static void smethod_2(XmlNode xmlNode_0, BattleBoxModel battleBoxModel_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Rewards".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Good".Equals(j.Name))
						{
							XmlNamedNodeMap attributes = j.Attributes;
							BattleBoxItem battleBoxItem = new BattleBoxItem()
							{
								GoodsId = int.Parse(attributes.GetNamedItem("Id").Value),
								Percent = int.Parse(attributes.GetNamedItem("Percent").Value)
							};
							battleBoxModel_0.Items.Add(battleBoxItem);
						}
					}
				}
			}
		}

		private static byte[] smethod_3(int int_0, int int_1, ref int int_2, List<BattleBoxModel> list_0)
		{
			byte[] array;
			int_2 = 0;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_1 * int_0; i < list_0.Count; i++)
				{
					BattleBoxXML.smethod_5(list_0[i], syncServerPacket);
					int int2 = int_2 + 1;
					int_2 = int2;
					if (int2 == int_0)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private static void smethod_4()
		{
			List<BattleBoxModel> battleBoxModels = new List<BattleBoxModel>();
			lock (BattleBoxXML.BBoxes)
			{
				foreach (BattleBoxModel bBox in BattleBoxXML.BBoxes)
				{
					battleBoxModels.Add(bBox);
				}
			}
			BattleBoxXML.TotalBoxes = battleBoxModels.Count;
			int ınt32 = (int)Math.Ceiling((double)battleBoxModels.Count / 100);
			int ınt321 = 0;
			for (int i = 0; i < ınt32; i++)
			{
				byte[] numArray = BattleBoxXML.smethod_3(100, i, ref ınt321, battleBoxModels);
				ShopData shopDatum = new ShopData()
				{
					Buffer = numArray,
					ItemsCount = ınt321,
					Offset = i * 100
				};
				BattleBoxXML.ShopDataBattleBoxes.Add(shopDatum);
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
	}
}