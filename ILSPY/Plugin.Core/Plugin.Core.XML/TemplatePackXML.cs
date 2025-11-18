using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML;

public class TemplatePackXML
{
	public static List<ItemsModel> Basics = new List<ItemsModel>();

	public static List<ItemsModel> Awards = new List<ItemsModel>();

	public static List<PCCafeModel> Cafes = new List<PCCafeModel>();

	public static void Load()
	{
		smethod_0();
		smethod_1();
		smethod_2();
	}

	public static void Reload()
	{
		Basics.Clear();
		Awards.Clear();
		Cafes.Clear();
		Load();
	}

	private static void smethod_0()
	{
		string text = "Data/Temps/Basic.xml";
		if (File.Exists(text))
		{
			smethod_3(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Basics.Count} Basic Templates", LoggerType.Info);
	}

	private static void smethod_1()
	{
		string text = "Data/Temps/CafePC.xml";
		if (File.Exists(text))
		{
			smethod_4(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Cafes.Count} PC Cafes", LoggerType.Info);
	}

	private static void smethod_2()
	{
		string text = "Data/Temps/Award.xml";
		if (File.Exists(text))
		{
			smethod_7(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Awards.Count} Award Templates", LoggerType.Info);
	}

	public static PCCafeModel GetPCCafe(CafeEnum Type)
	{
		lock (Cafes)
		{
			foreach (PCCafeModel cafe in Cafes)
			{
				if (cafe.Type == Type)
				{
					return cafe;
				}
			}
			return null;
		}
	}

	public static List<ItemsModel> GetPCCafeRewards(CafeEnum Type)
	{
		PCCafeModel pCCafe = GetPCCafe(Type);
		if (pCCafe != null)
		{
			lock (pCCafe.Rewards)
			{
				if (pCCafe.Rewards.TryGetValue(Type, out var value))
				{
					return value;
				}
			}
		}
		return new List<ItemsModel>();
	}

	private static void smethod_3(string string_0)
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
							if ("Item".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								int num = int.Parse(attributes.GetNamedItem("Id").Value);
								ItemsModel item = new ItemsModel(num)
								{
									ObjectId = ComDiv.ValidateStockId(num),
									Name = attributes.GetNamedItem("Name").Value,
									Count = 1u,
									Equip = ItemEquipType.Permanent
								};
								Basics.Add(item);
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

	private static void smethod_4(string string_0)
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
							if ("Cafe".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								PCCafeModel pCCafeModel = new PCCafeModel(ComDiv.ParseEnum<CafeEnum>(attributes.GetNamedItem("Type").Value))
								{
									ExpUp = int.Parse(attributes.GetNamedItem("ExpUp").Value),
									PointUp = int.Parse(attributes.GetNamedItem("PointUp").Value),
									Rewards = new SortedList<CafeEnum, List<ItemsModel>>()
								};
								smethod_5(xmlNode2, pCCafeModel);
								Cafes.Add(pCCafeModel);
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
						ItemsModel itemsModel_ = new ItemsModel(num)
						{
							ObjectId = ComDiv.ValidateStockId(num),
							Name = attributes.GetNamedItem("Name").Value,
							Count = 1u,
							Equip = ItemEquipType.CafePC
						};
						smethod_6(pccafeModel_0, itemsModel_);
					}
				}
			}
		}
	}

	private static void smethod_6(PCCafeModel pccafeModel_0, ItemsModel itemsModel_0)
	{
		lock (pccafeModel_0.Rewards)
		{
			if (pccafeModel_0.Rewards.ContainsKey(pccafeModel_0.Type))
			{
				pccafeModel_0.Rewards[pccafeModel_0.Type].Add(itemsModel_0);
				return;
			}
			pccafeModel_0.Rewards.Add(pccafeModel_0.Type, new List<ItemsModel> { itemsModel_0 });
		}
	}

	private static void smethod_7(string string_0)
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
							if ("Item".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								ItemsModel item = new ItemsModel(int.Parse(attributes.GetNamedItem("Id").Value))
								{
									Name = attributes.GetNamedItem("Name").Value,
									Count = uint.Parse(attributes.GetNamedItem("Count").Value),
									Equip = ItemEquipType.Durable
								};
								Awards.Add(item);
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
