using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML;

public class ClanRankXML
{
	private static List<RankModel> list_0 = new List<RankModel>();

	public static void Load()
	{
		string text = "Data/Ranks/Clan.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {list_0.Count} Clan Ranks", LoggerType.Info);
	}

	public static void Reload()
	{
		list_0.Clear();
		Load();
	}

	public static RankModel GetRank(int Id)
	{
		lock (list_0)
		{
			foreach (RankModel item in list_0)
			{
				if (item.Id == Id)
				{
					return item;
				}
			}
			return null;
		}
	}

	private static void smethod_0(string string_0)
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
							if ("Rank".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								RankModel item = new RankModel(byte.Parse(attributes.GetNamedItem("Id").Value))
								{
									Title = attributes.GetNamedItem("Title").Value,
									OnNextLevel = int.Parse(attributes.GetNamedItem("OnNextLevel").Value),
									OnGoldUp = 0,
									OnAllExp = int.Parse(attributes.GetNamedItem("OnAllExp").Value)
								};
								list_0.Add(item);
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
