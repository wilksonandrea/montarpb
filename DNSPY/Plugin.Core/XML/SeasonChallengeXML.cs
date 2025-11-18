using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML
{
	// Token: 0x0200001A RID: 26
	public class SeasonChallengeXML
	{
		// Token: 0x0600010B RID: 267 RVA: 0x000125EC File Offset: 0x000107EC
		public static void Load()
		{
			string text = "Data/SeasonChallenges.xml";
			if (File.Exists(text))
			{
				SeasonChallengeXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Season Challenge", SeasonChallengeXML.Seasons.Count), LoggerType.Info, null);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000027B7 File Offset: 0x000009B7
		public static void Reload()
		{
			SeasonChallengeXML.Seasons.Clear();
			SeasonChallengeXML.Load();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00012644 File Offset: 0x00010844
		public static BattlePassModel GetSeasonPass(int SeasonId)
		{
			List<BattlePassModel> seasons = SeasonChallengeXML.Seasons;
			BattlePassModel battlePassModel2;
			lock (seasons)
			{
				foreach (BattlePassModel battlePassModel in SeasonChallengeXML.Seasons)
				{
					if (battlePassModel.Id == SeasonId)
					{
						return battlePassModel;
					}
				}
				battlePassModel2 = null;
			}
			return battlePassModel2;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000126D0 File Offset: 0x000108D0
		public static BattlePassModel GetActiveSeasonPass()
		{
			List<BattlePassModel> seasons = SeasonChallengeXML.Seasons;
			BattlePassModel battlePassModel2;
			lock (seasons)
			{
				uint num = uint.Parse(DateTime.Now.ToString("yyMMddHHmm"));
				foreach (BattlePassModel battlePassModel in SeasonChallengeXML.Seasons)
				{
					if (battlePassModel.BeginDate <= num && num < battlePassModel.EndedDate)
					{
						return battlePassModel;
					}
				}
				battlePassModel2 = null;
			}
			return battlePassModel2;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00012780 File Offset: 0x00010980
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
										SeasonChallengeXML.smethod_1(xmlNode2, battlePassModel);
										battlePassModel.SetBoxCounts();
										SeasonChallengeXML.Seasons.Add(battlePassModel);
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

		// Token: 0x06000110 RID: 272 RVA: 0x0001297C File Offset: 0x00010B7C
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

		// Token: 0x06000111 RID: 273 RVA: 0x00002116 File Offset: 0x00000316
		public SeasonChallengeXML()
		{
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000027C8 File Offset: 0x000009C8
		// Note: this type is marked as 'beforefieldinit'.
		static SeasonChallengeXML()
		{
		}

		// Token: 0x04000064 RID: 100
		public static List<BattlePassModel> Seasons = new List<BattlePassModel>();
	}
}
