using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML;

public class TitleAwardXML
{
	public static List<TitleAward> Awards = new List<TitleAward>();

	public static void Load()
	{
		string text = "Data/Titles/Rewards.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Awards.Count} Title Awards", LoggerType.Info);
	}

	public static void Reload()
	{
		Awards.Clear();
		Load();
	}

	public static List<ItemsModel> GetAwards(int titleId)
	{
		List<ItemsModel> list = new List<ItemsModel>();
		lock (Awards)
		{
			foreach (TitleAward award in Awards)
			{
				if (award.Id == titleId)
				{
					list.Add(award.Item);
				}
			}
			return list;
		}
	}

	public static bool Contains(int TitleId, int ItemId)
	{
		if (ItemId == 0)
		{
			return false;
		}
		foreach (TitleAward award in Awards)
		{
			if (award.Id == TitleId && award.Item.Id == ItemId)
			{
				return true;
			}
		}
		return false;
	}

	private static void smethod_0(string string_0)
	{
		XmlDocument xmlDocument = new XmlDocument();
		using FileStream fileStream = new FileStream(string_0, FileMode.Open);
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
								int int_ = int.Parse(xmlNode2.Attributes.GetNamedItem("Id").Value);
								smethod_1(xmlNode2, int_);
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
							ItemsModel ıtemsModel = new ItemsModel(num)
							{
								Name = attributes.GetNamedItem("Name").Value,
								Count = uint.Parse(attributes.GetNamedItem("Count").Value),
								Equip = (ItemEquipType)int.Parse(attributes.GetNamedItem("Equip").Value)
							};
							if (ıtemsModel.Equip == ItemEquipType.Permanent)
							{
								ıtemsModel.ObjectId = ComDiv.ValidateStockId(num);
							}
							titleAward.Item = ıtemsModel;
							Awards.Add(titleAward);
						}
					}
				}
			}
		}
	}
}
