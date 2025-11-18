using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML;

public class BattleRewardXML
{
	public static List<BattleRewardModel> Rewards = new List<BattleRewardModel>();

	public static void Load()
	{
		string text = "Data/BattleRewards.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Rewards.Count} Battle Rewards", LoggerType.Info);
	}

	public static void Reload()
	{
		Rewards.Clear();
		Load();
	}

	public static BattleRewardModel GetRewardType(BattleRewardType RewardType)
	{
		lock (Rewards)
		{
			foreach (BattleRewardModel reward in Rewards)
			{
				if (reward.Type == RewardType)
				{
					return reward;
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
							if ("Item".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								int num = int.Parse(attributes.GetNamedItem("Count").Value);
								BattleRewardModel battleRewardModel = new BattleRewardModel
								{
									Type = ComDiv.ParseEnum<BattleRewardType>(attributes.GetNamedItem("Type").Value),
									Percentage = int.Parse(attributes.GetNamedItem("Percentage").Value),
									Enable = bool.Parse(attributes.GetNamedItem("Enable").Value),
									Rewards = new int[num]
								};
								smethod_1(xmlNode2, battleRewardModel);
								Rewards.Add(battleRewardModel);
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

	private static void smethod_1(XmlNode xmlNode_0, BattleRewardModel battleRewardModel_0)
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
						BattleRewardItem battleRewardItem = new BattleRewardItem
						{
							Index = int.Parse(attributes.GetNamedItem("Index").Value),
							GoodId = int.Parse(attributes.GetNamedItem("Id").Value)
						};
						battleRewardModel_0.Rewards[battleRewardItem.Index] = battleRewardItem.GoodId;
					}
				}
			}
		}
	}
}
