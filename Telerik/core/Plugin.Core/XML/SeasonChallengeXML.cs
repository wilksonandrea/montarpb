using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class SeasonChallengeXML
	{
		public static List<BattlePassModel> Seasons;

		static SeasonChallengeXML()
		{
			SeasonChallengeXML.Seasons = new List<BattlePassModel>();
		}

		public SeasonChallengeXML()
		{
		}

		public static BattlePassModel GetActiveSeasonPass()
		{
			BattlePassModel battlePassModel;
			lock (SeasonChallengeXML.Seasons)
			{
				DateTime now = DateTime.Now;
				uint uInt32 = uint.Parse(now.ToString("yyMMddHHmm"));
				foreach (BattlePassModel season in SeasonChallengeXML.Seasons)
				{
					if (season.BeginDate > uInt32 || uInt32 >= season.EndedDate)
					{
						continue;
					}
					battlePassModel = season;
					return battlePassModel;
				}
				battlePassModel = null;
			}
			return battlePassModel;
		}

		public static BattlePassModel GetSeasonPass(int SeasonId)
		{
			BattlePassModel battlePassModel;
			lock (SeasonChallengeXML.Seasons)
			{
				foreach (BattlePassModel season in SeasonChallengeXML.Seasons)
				{
					if (season.Id != SeasonId)
					{
						continue;
					}
					battlePassModel = season;
					return battlePassModel;
				}
				battlePassModel = null;
			}
			return battlePassModel;
		}

		public static void Load()
		{
			string str = "Data/SeasonChallenges.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				SeasonChallengeXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Season Challenge", SeasonChallengeXML.Seasons.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			SeasonChallengeXML.Seasons.Clear();
			SeasonChallengeXML.Load();
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
									if ("Season".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										BattlePassModel battlePassModel = new BattlePassModel()
										{
											Id = int.Parse(attributes.GetNamedItem("Id").Value),
											MaxDailyPoints = int.Parse(attributes.GetNamedItem("MaxDailyPoints").Value),
											Name = attributes.GetNamedItem("Name").Value,
											BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value),
											EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value),
											Enable = bool.Parse(attributes.GetNamedItem("Enable").Value),
											Cards = new List<PassBoxModel>()
										};
										for (int k = 0; k < 99; k++)
										{
											battlePassModel.Cards.Add(new PassBoxModel());
										}
										SeasonChallengeXML.smethod_1(j, battlePassModel);
										battlePassModel.SetBoxCounts();
										SeasonChallengeXML.Seasons.Add(battlePassModel);
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

		private static void smethod_1(XmlNode xmlNode_0, BattlePassModel battlePassModel_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Rewards".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Card".Equals(j.Name))
						{
							XmlNamedNodeMap attributes = j.Attributes;
							int ınt32 = int.Parse(attributes.GetNamedItem("Level").Value) - 1;
							battlePassModel_0.Cards[ınt32].Card = ınt32 + 1;
							battlePassModel_0.Cards[ınt32].Normal.SetGoodId(int.Parse(attributes.GetNamedItem("Normal").Value));
							battlePassModel_0.Cards[ınt32].PremiumA.SetGoodId(int.Parse(attributes.GetNamedItem("PremiumA").Value));
							battlePassModel_0.Cards[ınt32].PremiumB.SetGoodId(int.Parse(attributes.GetNamedItem("PremiumB").Value));
							battlePassModel_0.Cards[ınt32].RequiredPoints = int.Parse(attributes.GetNamedItem("ReqPoints").Value);
						}
					}
				}
			}
		}
	}
}