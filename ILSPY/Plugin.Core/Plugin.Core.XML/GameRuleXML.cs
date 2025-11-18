using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;

namespace Plugin.Core.XML;

public class GameRuleXML
{
	public static List<TRuleModel> GameRules = new List<TRuleModel>();

	public static void Load()
	{
		string text = "Data/ClassicMode.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {GameRules.Count} Game Rules", LoggerType.Info);
	}

	public static void Reload()
	{
		GameRules.Clear();
		Load();
	}

	public static TRuleModel CheckTRuleByRoomName(string RoomName)
	{
		lock (GameRules)
		{
			foreach (TRuleModel gameRule in GameRules)
			{
				if (RoomName.ToLower().Contains(gameRule.Name.ToLower()))
				{
					return gameRule;
				}
			}
			return null;
		}
	}

	public static bool IsBlocked(int ListId, int ItemId)
	{
		return ListId == ItemId;
	}

	public static bool IsBlocked(int ListId, int ItemId, ref List<string> List, string Category)
	{
		if (ListId == ItemId)
		{
			List.Add(Category);
			return true;
		}
		return false;
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
							if ("Rule".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								TRuleModel tRuleModel = new TRuleModel
								{
									Name = attributes.GetNamedItem("Name").Value,
									BanIndexes = new List<int>()
								};
								smethod_1(xmlNode2, tRuleModel);
								GameRules.Add(tRuleModel);
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

	private static void smethod_1(XmlNode xmlNode_0, TRuleModel truleModel_0)
	{
		for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
		{
			if ("Extensions".Equals(xmlNode.Name))
			{
				for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
				{
					if ("Ban".Equals(xmlNode2.Name))
					{
						ShopManager.IsBlocked(xmlNode2.Attributes.GetNamedItem("Filter").Value, truleModel_0.BanIndexes);
					}
				}
			}
		}
	}
}
