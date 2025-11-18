using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML
{
	// Token: 0x0200001D RID: 29
	public class TemplatePackXML
	{
		// Token: 0x0600011E RID: 286 RVA: 0x0000280E File Offset: 0x00000A0E
		public static void Load()
		{
			TemplatePackXML.smethod_0();
			TemplatePackXML.smethod_1();
			TemplatePackXML.smethod_2();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000281F File Offset: 0x00000A1F
		public static void Reload()
		{
			TemplatePackXML.Basics.Clear();
			TemplatePackXML.Awards.Clear();
			TemplatePackXML.Cafes.Clear();
			TemplatePackXML.Load();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00012DF0 File Offset: 0x00010FF0
		private static void smethod_0()
		{
			string text = "Data/Temps/Basic.xml";
			if (File.Exists(text))
			{
				TemplatePackXML.smethod_3(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Basic Templates", TemplatePackXML.Basics.Count), LoggerType.Info, null);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00012E48 File Offset: 0x00011048
		private static void smethod_1()
		{
			string text = "Data/Temps/CafePC.xml";
			if (File.Exists(text))
			{
				TemplatePackXML.smethod_4(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} PC Cafes", TemplatePackXML.Cafes.Count), LoggerType.Info, null);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00012EA0 File Offset: 0x000110A0
		private static void smethod_2()
		{
			string text = "Data/Temps/Award.xml";
			if (File.Exists(text))
			{
				TemplatePackXML.smethod_7(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Award Templates", TemplatePackXML.Awards.Count), LoggerType.Info, null);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00012EF8 File Offset: 0x000110F8
		public static PCCafeModel GetPCCafe(CafeEnum Type)
		{
			List<PCCafeModel> cafes = TemplatePackXML.Cafes;
			PCCafeModel pccafeModel2;
			lock (cafes)
			{
				foreach (PCCafeModel pccafeModel in TemplatePackXML.Cafes)
				{
					if (pccafeModel.Type == Type)
					{
						return pccafeModel;
					}
				}
				pccafeModel2 = null;
			}
			return pccafeModel2;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00012F84 File Offset: 0x00011184
		public static List<ItemsModel> GetPCCafeRewards(CafeEnum Type)
		{
			PCCafeModel pccafe = TemplatePackXML.GetPCCafe(Type);
			if (pccafe != null)
			{
				SortedList<CafeEnum, List<ItemsModel>> rewards = pccafe.Rewards;
				List<ItemsModel> list2;
				lock (rewards)
				{
					List<ItemsModel> list;
					if (!pccafe.Rewards.TryGetValue(Type, out list))
					{
						goto IL_3F;
					}
					list2 = list;
				}
				return list2;
			}
			IL_3F:
			return new List<ItemsModel>();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00012FE8 File Offset: 0x000111E8
		private static void smethod_3(string string_0)
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
									if ("Item".Equals(xmlNode2.Name))
									{
										XmlNamedNodeMap attributes = xmlNode2.Attributes;
										int num = int.Parse(attributes.GetNamedItem("Id").Value);
										ItemsModel itemsModel = new ItemsModel(num)
										{
											ObjectId = (long)((ulong)ComDiv.ValidateStockId(num)),
											Name = attributes.GetNamedItem("Name").Value,
											Count = 1U,
											Equip = ItemEquipType.Permanent
										};
										TemplatePackXML.Basics.Add(itemsModel);
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

		// Token: 0x06000126 RID: 294 RVA: 0x0001314C File Offset: 0x0001134C
		private static void smethod_4(string string_0)
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
									if ("Cafe".Equals(xmlNode2.Name))
									{
										XmlNamedNodeMap attributes = xmlNode2.Attributes;
										PCCafeModel pccafeModel = new PCCafeModel(ComDiv.ParseEnum<CafeEnum>(attributes.GetNamedItem("Type").Value))
										{
											ExpUp = int.Parse(attributes.GetNamedItem("ExpUp").Value),
											PointUp = int.Parse(attributes.GetNamedItem("PointUp").Value),
											Rewards = new SortedList<CafeEnum, List<ItemsModel>>()
										};
										TemplatePackXML.smethod_5(xmlNode2, pccafeModel);
										TemplatePackXML.Cafes.Add(pccafeModel);
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

		// Token: 0x06000127 RID: 295 RVA: 0x000132C4 File Offset: 0x000114C4
		private static void smethod_5(XmlNode xmlNode_0, PCCafeModel pccafeModel_0)
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
							int num = int.Parse(attributes.GetNamedItem("Id").Value);
							ItemsModel itemsModel = new ItemsModel(num)
							{
								ObjectId = (long)((ulong)ComDiv.ValidateStockId(num)),
								Name = attributes.GetNamedItem("Name").Value,
								Count = 1U,
								Equip = ItemEquipType.CafePC
							};
							TemplatePackXML.smethod_6(pccafeModel_0, itemsModel);
						}
					}
				}
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00013384 File Offset: 0x00011584
		private static void smethod_6(PCCafeModel pccafeModel_0, ItemsModel itemsModel_0)
		{
			SortedList<CafeEnum, List<ItemsModel>> rewards = pccafeModel_0.Rewards;
			lock (rewards)
			{
				if (pccafeModel_0.Rewards.ContainsKey(pccafeModel_0.Type))
				{
					pccafeModel_0.Rewards[pccafeModel_0.Type].Add(itemsModel_0);
				}
				else
				{
					pccafeModel_0.Rewards.Add(pccafeModel_0.Type, new List<ItemsModel> { itemsModel_0 });
				}
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00013408 File Offset: 0x00011608
		private static void smethod_7(string string_0)
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
									if ("Item".Equals(xmlNode2.Name))
									{
										XmlNamedNodeMap attributes = xmlNode2.Attributes;
										ItemsModel itemsModel = new ItemsModel(int.Parse(attributes.GetNamedItem("Id").Value))
										{
											Name = attributes.GetNamedItem("Name").Value,
											Count = uint.Parse(attributes.GetNamedItem("Count").Value),
											Equip = ItemEquipType.Durable
										};
										TemplatePackXML.Awards.Add(itemsModel);
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

		// Token: 0x0600012A RID: 298 RVA: 0x00002116 File Offset: 0x00000316
		public TemplatePackXML()
		{
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00002844 File Offset: 0x00000A44
		// Note: this type is marked as 'beforefieldinit'.
		static TemplatePackXML()
		{
		}

		// Token: 0x04000067 RID: 103
		public static List<ItemsModel> Basics = new List<ItemsModel>();

		// Token: 0x04000068 RID: 104
		public static List<ItemsModel> Awards = new List<ItemsModel>();

		// Token: 0x04000069 RID: 105
		public static List<PCCafeModel> Cafes = new List<PCCafeModel>();
	}
}
