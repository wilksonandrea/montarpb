using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML;

public class PlayerRankXML
{
	public static readonly List<RankModel> Ranks = new List<RankModel>();

	public static void Load()
	{
		string text = "Data/Ranks/Player.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Ranks.Count} Player Ranks", LoggerType.Info);
	}

	public static void Reload()
	{
		Ranks.Clear();
		Load();
	}

	public static RankModel GetRank(int Id)
	{
		lock (Ranks)
		{
			foreach (RankModel rank in Ranks)
			{
				if (rank.Id == Id)
				{
					return rank;
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
								RankModel rankModel = new RankModel(int.Parse(attributes.GetNamedItem("Id").Value))
								{
									Title = attributes.GetNamedItem("Title").Value,
									OnNextLevel = int.Parse(attributes.GetNamedItem("OnNextLevel").Value),
									OnGoldUp = int.Parse(attributes.GetNamedItem("OnGoldUp").Value),
									OnAllExp = int.Parse(attributes.GetNamedItem("OnAllExp").Value),
									Rewards = new SortedList<int, List<int>>()
								};
								smethod_1(xmlNode2, rankModel);
								Ranks.Add(rankModel);
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

	public static List<int> GetRewards(int RankId)
	{
		RankModel rank = GetRank(RankId);
		if (rank != null)
		{
			lock (rank.Rewards)
			{
				if (rank.Rewards.TryGetValue(RankId, out var value))
				{
					return value;
				}
			}
		}
		return new List<int>();
	}

	private static void smethod_1(XmlNode xmlNode_0, RankModel rankModel_0)
	{
		for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
		{
			if ("Rewards".Equals(xmlNode.Name))
			{
				for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
				{
					if ("Good".Equals(xmlNode2.Name))
					{
						int item = int.Parse(xmlNode2.Attributes.GetNamedItem("Id").Value);
						lock (rankModel_0.Rewards)
						{
							if (rankModel_0.Rewards.ContainsKey(rankModel_0.Id))
							{
								rankModel_0.Rewards[rankModel_0.Id].Add(item);
							}
							else
							{
								rankModel_0.Rewards.Add(rankModel_0.Id, new List<int> { item });
							}
						}
					}
				}
			}
		}
	}
}
