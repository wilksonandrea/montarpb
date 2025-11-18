using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML
{
	// Token: 0x02000024 RID: 36
	public class PlayerRankXML
	{
		// Token: 0x06000153 RID: 339 RVA: 0x000142C8 File Offset: 0x000124C8
		public static void Load()
		{
			string text = "Data/Ranks/Player.xml";
			if (File.Exists(text))
			{
				PlayerRankXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Player Ranks", PlayerRankXML.Ranks.Count), LoggerType.Info, null);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00002958 File Offset: 0x00000B58
		public static void Reload()
		{
			PlayerRankXML.Ranks.Clear();
			PlayerRankXML.Load();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00014320 File Offset: 0x00012520
		public static RankModel GetRank(int Id)
		{
			List<RankModel> ranks = PlayerRankXML.Ranks;
			RankModel rankModel2;
			lock (ranks)
			{
				foreach (RankModel rankModel in PlayerRankXML.Ranks)
				{
					if (rankModel.Id == Id)
					{
						return rankModel;
					}
				}
				rankModel2 = null;
			}
			return rankModel2;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000143AC File Offset: 0x000125AC
		private static void smethod_0(string string_0)
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
										PlayerRankXML.smethod_1(xmlNode2, rankModel);
										PlayerRankXML.Ranks.Add(rankModel);
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

		// Token: 0x06000157 RID: 343 RVA: 0x0001455C File Offset: 0x0001275C
		public static List<int> GetRewards(int RankId)
		{
			RankModel rank = PlayerRankXML.GetRank(RankId);
			if (rank != null)
			{
				SortedList<int, List<int>> rewards = rank.Rewards;
				List<int> list2;
				lock (rewards)
				{
					List<int> list;
					if (!rank.Rewards.TryGetValue(RankId, out list))
					{
						goto IL_3F;
					}
					list2 = list;
				}
				return list2;
			}
			IL_3F:
			return new List<int>();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000145C0 File Offset: 0x000127C0
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
							int num = int.Parse(xmlNode2.Attributes.GetNamedItem("Id").Value);
							SortedList<int, List<int>> rewards = rankModel_0.Rewards;
							lock (rewards)
							{
								if (rankModel_0.Rewards.ContainsKey(rankModel_0.Id))
								{
									rankModel_0.Rewards[rankModel_0.Id].Add(num);
									goto IL_D3;
								}
								rankModel_0.Rewards.Add(rankModel_0.Id, new List<int> { num });
								goto IL_D3;
							}
							break;
						}
						IL_D3:;
					}
				}
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00002116 File Offset: 0x00000316
		public PlayerRankXML()
		{
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00002969 File Offset: 0x00000B69
		// Note: this type is marked as 'beforefieldinit'.
		static PlayerRankXML()
		{
		}

		// Token: 0x04000074 RID: 116
		public static readonly List<RankModel> Ranks = new List<RankModel>();
	}
}
