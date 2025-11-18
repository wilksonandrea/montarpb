using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class PlayerRankXML
	{
		public readonly static List<RankModel> Ranks;

		static PlayerRankXML()
		{
			PlayerRankXML.Ranks = new List<RankModel>();
		}

		public PlayerRankXML()
		{
		}

		public static RankModel GetRank(int Id)
		{
			RankModel rankModel;
			lock (PlayerRankXML.Ranks)
			{
				foreach (RankModel rank in PlayerRankXML.Ranks)
				{
					if (rank.Id != Id)
					{
						continue;
					}
					rankModel = rank;
					return rankModel;
				}
				rankModel = null;
			}
			return rankModel;
		}

		public static List<int> GetRewards(int RankId)
		{
			List<int> ınt32s;
			List<int> ınt32s1;
			RankModel rank = PlayerRankXML.GetRank(RankId);
			if (rank != null)
			{
				lock (rank.Rewards)
				{
					if (!rank.Rewards.TryGetValue(RankId, out ınt32s))
					{
						return new List<int>();
					}
					else
					{
						ınt32s1 = ınt32s;
					}
				}
				return ınt32s1;
			}
			return new List<int>();
		}

		public static void Load()
		{
			string str = "Data/Ranks/Player.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				PlayerRankXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Player Ranks", PlayerRankXML.Ranks.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			PlayerRankXML.Ranks.Clear();
			PlayerRankXML.Load();
		}

		private static void smethod_0(string string_0)
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
									if ("Rank".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										RankModel rankModel = new RankModel(int.Parse(attributes.GetNamedItem("Id").Value))
										{
											Title = attributes.GetNamedItem("Title").Value,
											OnNextLevel = int.Parse(attributes.GetNamedItem("OnNextLevel").Value),
											OnGoldUp = int.Parse(attributes.GetNamedItem("OnGoldUp").Value),
											OnAllExp = int.Parse(attributes.GetNamedItem("OnAllExp").Value),
											Rewards = new SortedList<int, List<int>>()
										};
										PlayerRankXML.smethod_1(j, rankModel);
										PlayerRankXML.Ranks.Add(rankModel);
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

		private static void smethod_1(XmlNode xmlNode_0, RankModel rankModel_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Rewards".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Good".Equals(j.Name))
						{
							int ınt32 = int.Parse(j.Attributes.GetNamedItem("Id").Value);
							lock (rankModel_0.Rewards)
							{
								if (!rankModel_0.Rewards.ContainsKey(rankModel_0.Id))
								{
									rankModel_0.Rewards.Add(rankModel_0.Id, new List<int>()
									{
										ınt32
									});
								}
								else
								{
									rankModel_0.Rewards[rankModel_0.Id].Add(ınt32);
								}
							}
						}
					}
				}
			}
		}
	}
}