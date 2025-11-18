using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class TemplatePackXML
	{
		public static List<ItemsModel> Basics;

		public static List<ItemsModel> Awards;

		public static List<PCCafeModel> Cafes;

		static TemplatePackXML()
		{
			TemplatePackXML.Basics = new List<ItemsModel>();
			TemplatePackXML.Awards = new List<ItemsModel>();
			TemplatePackXML.Cafes = new List<PCCafeModel>();
		}

		public TemplatePackXML()
		{
		}

		public static PCCafeModel GetPCCafe(CafeEnum Type)
		{
			PCCafeModel pCCafeModel;
			lock (TemplatePackXML.Cafes)
			{
				foreach (PCCafeModel cafe in TemplatePackXML.Cafes)
				{
					if (cafe.Type != Type)
					{
						continue;
					}
					pCCafeModel = cafe;
					return pCCafeModel;
				}
				pCCafeModel = null;
			}
			return pCCafeModel;
		}

		public static List<ItemsModel> GetPCCafeRewards(CafeEnum Type)
		{
			List<ItemsModel> ıtemsModels;
			List<ItemsModel> ıtemsModels1;
			PCCafeModel pCCafe = TemplatePackXML.GetPCCafe(Type);
			if (pCCafe != null)
			{
				lock (pCCafe.Rewards)
				{
					if (!pCCafe.Rewards.TryGetValue(Type, out ıtemsModels))
					{
						return new List<ItemsModel>();
					}
					else
					{
						ıtemsModels1 = ıtemsModels;
					}
				}
				return ıtemsModels1;
			}
			return new List<ItemsModel>();
		}

		public static void Load()
		{
			TemplatePackXML.smethod_0();
			TemplatePackXML.smethod_1();
			TemplatePackXML.smethod_2();
		}

		public static void Reload()
		{
			TemplatePackXML.Basics.Clear();
			TemplatePackXML.Awards.Clear();
			TemplatePackXML.Cafes.Clear();
			TemplatePackXML.Load();
		}

		private static void smethod_0()
		{
			string str = "Data/Temps/Basic.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				TemplatePackXML.smethod_3(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Basic Templates", TemplatePackXML.Basics.Count), LoggerType.Info, null);
		}

		private static void smethod_1()
		{
			string str = "Data/Temps/CafePC.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				TemplatePackXML.smethod_4(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} PC Cafes", TemplatePackXML.Cafes.Count), LoggerType.Info, null);
		}

		private static void smethod_2()
		{
			string str = "Data/Temps/Award.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				TemplatePackXML.smethod_7(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Award Templates", TemplatePackXML.Awards.Count), LoggerType.Info, null);
		}

		private static void smethod_3(string string_0)
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
									if ("Item".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										int ınt32 = int.Parse(attributes.GetNamedItem("Id").Value);
										ItemsModel ıtemsModel = new ItemsModel(ınt32)
										{
											ObjectId = (long)ComDiv.ValidateStockId(ınt32),
											Name = attributes.GetNamedItem("Name").Value,
											Count = 1,
											Equip = ItemEquipType.Permanent
										};
										TemplatePackXML.Basics.Add(ıtemsModel);
									}
								}
							}
						}
					}
					catch (XmlException xmlException1)
					{
						XmlException xmlException = xmlException1;
						CLogger.Print(xmlException.Message, LoggerType.Error, xmlException);
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

		private static void smethod_4(string string_0)
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
									if ("Cafe".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										PCCafeModel pCCafeModel = new PCCafeModel(ComDiv.ParseEnum<CafeEnum>(attributes.GetNamedItem("Type").Value))
										{
											ExpUp = int.Parse(attributes.GetNamedItem("ExpUp").Value),
											PointUp = int.Parse(attributes.GetNamedItem("PointUp").Value),
											Rewards = new SortedList<CafeEnum, List<ItemsModel>>()
										};
										TemplatePackXML.smethod_5(j, pCCafeModel);
										TemplatePackXML.Cafes.Add(pCCafeModel);
									}
								}
							}
						}
					}
					catch (XmlException xmlException1)
					{
						XmlException xmlException = xmlException1;
						CLogger.Print(xmlException.Message, LoggerType.Error, xmlException);
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

		private static void smethod_5(XmlNode xmlNode_0, PCCafeModel pccafeModel_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Rewards".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Item".Equals(j.Name))
						{
							XmlNamedNodeMap attributes = j.Attributes;
							int ınt32 = int.Parse(attributes.GetNamedItem("Id").Value);
							TemplatePackXML.smethod_6(pccafeModel_0, new ItemsModel(ınt32)
							{
								ObjectId = (long)ComDiv.ValidateStockId(ınt32),
								Name = attributes.GetNamedItem("Name").Value,
								Count = 1,
								Equip = ItemEquipType.CafePC
							});
						}
					}
				}
			}
		}

		private static void smethod_6(PCCafeModel pccafeModel_0, ItemsModel itemsModel_0)
		{
			lock (pccafeModel_0.Rewards)
			{
				if (!pccafeModel_0.Rewards.ContainsKey(pccafeModel_0.Type))
				{
					pccafeModel_0.Rewards.Add(pccafeModel_0.Type, new List<ItemsModel>()
					{
						itemsModel_0
					});
				}
				else
				{
					pccafeModel_0.Rewards[pccafeModel_0.Type].Add(itemsModel_0);
				}
			}
		}

		private static void smethod_7(string string_0)
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
									if ("Item".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										ItemsModel ıtemsModel = new ItemsModel(int.Parse(attributes.GetNamedItem("Id").Value))
										{
											Name = attributes.GetNamedItem("Name").Value,
											Count = uint.Parse(attributes.GetNamedItem("Count").Value),
											Equip = ItemEquipType.Durable
										};
										TemplatePackXML.Awards.Add(ıtemsModel);
									}
								}
							}
						}
					}
					catch (XmlException xmlException1)
					{
						XmlException xmlException = xmlException1;
						CLogger.Print(xmlException.Message, LoggerType.Error, xmlException);
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
	}
}