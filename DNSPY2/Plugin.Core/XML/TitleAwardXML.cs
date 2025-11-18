using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML
{
	// Token: 0x02000026 RID: 38
	public class TitleAwardXML
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00014CC8 File Offset: 0x00012EC8
		public static void Load()
		{
			string text = "Data/Titles/Rewards.xml";
			if (File.Exists(text))
			{
				TitleAwardXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Title Awards", TitleAwardXML.Awards.Count), LoggerType.Info, null);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00002993 File Offset: 0x00000B93
		public static void Reload()
		{
			TitleAwardXML.Awards.Clear();
			TitleAwardXML.Load();
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00014D20 File Offset: 0x00012F20
		public static List<ItemsModel> GetAwards(int titleId)
		{
			List<ItemsModel> list = new List<ItemsModel>();
			List<TitleAward> awards = TitleAwardXML.Awards;
			lock (awards)
			{
				foreach (TitleAward titleAward in TitleAwardXML.Awards)
				{
					if (titleAward.Id == titleId)
					{
						list.Add(titleAward.Item);
					}
				}
			}
			return list;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00014DB4 File Offset: 0x00012FB4
		public static bool Contains(int TitleId, int ItemId)
		{
			if (ItemId == 0)
			{
				return false;
			}
			foreach (TitleAward titleAward in TitleAwardXML.Awards)
			{
				if (titleAward.Id == TitleId && titleAward.Item.Id == ItemId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00014E28 File Offset: 0x00013028
		private static void smethod_0(string string_0)
		{
			XmlDocument xmlDocument = new XmlDocument();
			using (FileStream fileStream = new FileStream(string_0, FileMode.Open))
			{
				if (fileStream.Length > 0L)
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
									if ("Title".Equals(xmlNode2.Name))
									{
										int num = int.Parse(xmlNode2.Attributes.GetNamedItem("Id").Value);
										TitleAwardXML.smethod_1(xmlNode2, num);
									}
								}
							}
						}
					}
					catch (XmlException ex)
					{
						CLogger.Print(ex.Message, LoggerType.Error, ex);
					}
				}
				fileStream.Dispose();
				fileStream.Close();
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00014F18 File Offset: 0x00013118
		private static void smethod_1(XmlNode xmlNode_0, int int_0)
		{
			for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if ("Rewards".Equals(xmlNode.Name))
				{
					for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
					{
						if ("Item".Equals(xmlNode2.Name))
						{
							XmlNamedNodeMap attributes = xmlNode2.Attributes;
							TitleAward titleAward = new TitleAward
							{
								Id = int_0
							};
							if (titleAward != null)
							{
								int num = int.Parse(attributes.GetNamedItem("Id").Value);
								ItemsModel itemsModel = new ItemsModel(num)
								{
									Name = attributes.GetNamedItem("Name").Value,
									Count = uint.Parse(attributes.GetNamedItem("Count").Value),
									Equip = (ItemEquipType)int.Parse(attributes.GetNamedItem("Equip").Value)
								};
								if (itemsModel.Equip == ItemEquipType.Permanent)
								{
									itemsModel.ObjectId = (long)((ulong)ComDiv.ValidateStockId(num));
								}
								titleAward.Item = itemsModel;
								TitleAwardXML.Awards.Add(titleAward);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00002116 File Offset: 0x00000316
		public TitleAwardXML()
		{
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000029A4 File Offset: 0x00000BA4
		// Note: this type is marked as 'beforefieldinit'.
		static TitleAwardXML()
		{
		}

		// Token: 0x04000076 RID: 118
		public static List<TitleAward> Awards = new List<TitleAward>();
	}
}
