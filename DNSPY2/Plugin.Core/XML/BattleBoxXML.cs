using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;

namespace Plugin.Core.XML
{
	// Token: 0x02000009 RID: 9
	public class BattleBoxXML
	{
		// Token: 0x06000087 RID: 135 RVA: 0x0000EB40 File Offset: 0x0000CD40
		public static void Load()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Data\\BBoxes");
			if (!directoryInfo.Exists)
			{
				return;
			}
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				try
				{
					BattleBoxXML.smethod_0(int.Parse(fileInfo.Name.Substring(0, fileInfo.Name.Length - 4)));
				}
				catch (Exception ex)
				{
					CLogger.Print(ex.Message, LoggerType.Error, ex);
				}
			}
			BattleBoxXML.smethod_4();
			CLogger.Print(string.Format("Plugin Loaded: {0} Battle Boxes", BattleBoxXML.BBoxes.Count), LoggerType.Info, null);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000024E3 File Offset: 0x000006E3
		public static void Reload()
		{
			BattleBoxXML.BBoxes.Clear();
			BattleBoxXML.ShopDataBattleBoxes.Clear();
			BattleBoxXML.TotalBoxes = 0;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000EBF4 File Offset: 0x0000CDF4
		private static void smethod_0(int int_0)
		{
			string text = string.Format("Data/BBoxes/{0}.xml", int_0);
			if (File.Exists(text))
			{
				BattleBoxXML.smethod_1(text, int_0);
				return;
			}
			CLogger.Print("File not found: " + text, LoggerType.Warning, null);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000EC34 File Offset: 0x0000CE34
		private static void smethod_1(string string_0, int int_0)
		{
			XmlDocument xmlDocument = new XmlDocument();
			using (FileStream fileStream = new FileStream(string_0, FileMode.Open))
			{
				if (fileStream.Length == 0L)
				{
					CLogger.Print("File is empty: " + string_0, LoggerType.Warning, null);
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
										BattleBoxXML.smethod_2(xmlNode2, battleBoxModel);
										battleBoxModel.InitItemPercentages();
										BattleBoxXML.BBoxes.Add(battleBoxModel);
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
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000ED68 File Offset: 0x0000CF68
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
							BattleBoxItem battleBoxItem = new BattleBoxItem
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

		// Token: 0x0600008C RID: 140 RVA: 0x0000EE14 File Offset: 0x0000D014
		private static byte[] smethod_3(int int_0, int int_1, ref int int_2, List<BattleBoxModel> list_0)
		{
			int_2 = 0;
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				for (int i = int_1 * int_0; i < list_0.Count; i++)
				{
					BattleBoxXML.smethod_5(list_0[i], syncServerPacket);
					int num = int_2 + 1;
					int_2 = num;
					if (num == int_0)
					{
						break;
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000EE7C File Offset: 0x0000D07C
		private static void smethod_4()
		{
			List<BattleBoxModel> list = new List<BattleBoxModel>();
			List<BattleBoxModel> bboxes = BattleBoxXML.BBoxes;
			lock (bboxes)
			{
				foreach (BattleBoxModel battleBoxModel in BattleBoxXML.BBoxes)
				{
					list.Add(battleBoxModel);
				}
			}
			BattleBoxXML.TotalBoxes = list.Count;
			int num = (int)Math.Ceiling((double)list.Count / 100.0);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				byte[] array = BattleBoxXML.smethod_3(100, i, ref num2, list);
				ShopData shopData = new ShopData
				{
					Buffer = array,
					ItemsCount = num2,
					Offset = i * 100
				};
				BattleBoxXML.ShopDataBattleBoxes.Add(shopData);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000024FF File Offset: 0x000006FF
		private static void smethod_5(BattleBoxModel battleBoxModel_0, SyncServerPacket syncServerPacket_0)
		{
			syncServerPacket_0.WriteD(battleBoxModel_0.CouponId);
			syncServerPacket_0.WriteH((ushort)battleBoxModel_0.RequireTags);
			syncServerPacket_0.WriteH(0);
			syncServerPacket_0.WriteH(0);
			syncServerPacket_0.WriteC(0);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000EF70 File Offset: 0x0000D170
		public static BattleBoxModel GetBattleBox(int BattleBoxId)
		{
			if (BattleBoxId == 0)
			{
				return null;
			}
			List<BattleBoxModel> bboxes = BattleBoxXML.BBoxes;
			lock (bboxes)
			{
				foreach (BattleBoxModel battleBoxModel in BattleBoxXML.BBoxes)
				{
					if (battleBoxModel.CouponId == BattleBoxId)
					{
						return battleBoxModel;
					}
				}
			}
			return null;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002116 File Offset: 0x00000316
		public BattleBoxXML()
		{
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000252F File Offset: 0x0000072F
		// Note: this type is marked as 'beforefieldinit'.
		static BattleBoxXML()
		{
		}

		// Token: 0x0400004D RID: 77
		public static List<BattleBoxModel> BBoxes = new List<BattleBoxModel>();

		// Token: 0x0400004E RID: 78
		public static List<ShopData> ShopDataBattleBoxes = new List<ShopData>();

		// Token: 0x0400004F RID: 79
		public static int TotalBoxes;
	}
}
