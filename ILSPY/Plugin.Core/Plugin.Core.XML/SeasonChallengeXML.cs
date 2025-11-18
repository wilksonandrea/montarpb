using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML;

public class SeasonChallengeXML
{
	public static List<BattlePassModel> Seasons = new List<BattlePassModel>();

	public static void Load()
	{
		string text = "Data/SeasonChallenges.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Seasons.Count} Season Challenge", LoggerType.Info);
	}

	public static void Reload()
	{
		Seasons.Clear();
		Load();
	}

	public static BattlePassModel GetSeasonPass(int SeasonId)
	{
		lock (Seasons)
		{
			foreach (BattlePassModel season in Seasons)
			{
				if (season.Id == SeasonId)
				{
					return season;
				}
			}
			return null;
		}
	}

	public static BattlePassModel GetActiveSeasonPass()
	{
		lock (Seasons)
		{
			uint num = uint.Parse(DateTime.Now.ToString("yyMMddHHmm"));
			foreach (BattlePassModel season in Seasons)
			{
				if (season.BeginDate <= num && num < season.EndedDate)
				{
					return season;
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
							if ("Season".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								BattlePassModel battlePassModel = new BattlePassModel
								{
									Id = int.Parse(attributes.GetNamedItem("Id").Value),
									MaxDailyPoints = int.Parse(attributes.GetNamedItem("MaxDailyPoints").Value),
									Name = attributes.GetNamedItem("Name").Value,
									BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value),
									EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value),
									Enable = bool.Parse(attributes.GetNamedItem("Enable").Value),
									Cards = new List<PassBoxModel>()
								};
								for (int i = 0; i < 99; i++)
								{
									battlePassModel.Cards.Add(new PassBoxModel());
								}
								smethod_1(xmlNode2, battlePassModel);
								battlePassModel.SetBoxCounts();
								Seasons.Add(battlePassModel);
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

	private static void smethod_1(XmlNode xmlNode_0, BattlePassModel battlePassModel_0)
	{
		for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
		{
			if ("Rewards".Equals(xmlNode.Name))
			{
				for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
				{
					if ("Card".Equals(xmlNode2.Name))
					{
						XmlNamedNodeMap attributes = xmlNode2.Attributes;
						int num = int.Parse(attributes.GetNamedItem("Level").Value) - 1;
						battlePassModel_0.Cards[num].Card = num + 1;
						battlePassModel_0.Cards[num].Normal.SetGoodId(int.Parse(attributes.GetNamedItem("Normal").Value));
						battlePassModel_0.Cards[num].PremiumA.SetGoodId(int.Parse(attributes.GetNamedItem("PremiumA").Value));
						battlePassModel_0.Cards[num].PremiumB.SetGoodId(int.Parse(attributes.GetNamedItem("PremiumB").Value));
						battlePassModel_0.Cards[num].RequiredPoints = int.Parse(attributes.GetNamedItem("ReqPoints").Value);
					}
				}
			}
		}
	}
}
