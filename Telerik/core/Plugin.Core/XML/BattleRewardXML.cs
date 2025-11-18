using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class BattleRewardXML
	{
		public static List<BattleRewardModel> Rewards;

		static BattleRewardXML()
		{
			BattleRewardXML.Rewards = new List<BattleRewardModel>();
		}

		public BattleRewardXML()
		{
		}

		public static BattleRewardModel GetRewardType(BattleRewardType RewardType)
		{
			BattleRewardModel battleRewardModel;
			lock (BattleRewardXML.Rewards)
			{
				foreach (BattleRewardModel reward in BattleRewardXML.Rewards)
				{
					if (reward.Type != RewardType)
					{
						continue;
					}
					battleRewardModel = reward;
					return battleRewardModel;
				}
				battleRewardModel = null;
			}
			return battleRewardModel;
		}

		public static void Load()
		{
			string str = "Data/BattleRewards.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				BattleRewardXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Battle Rewards", BattleRewardXML.Rewards.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			BattleRewardXML.Rewards.Clear();
			BattleRewardXML.Load();
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
									if ("Item".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										int ınt32 = int.Parse(attributes.GetNamedItem("Count").Value);
										BattleRewardModel battleRewardModel = new BattleRewardModel()
										{
											Type = ComDiv.ParseEnum<BattleRewardType>(attributes.GetNamedItem("Type").Value),
											Percentage = int.Parse(attributes.GetNamedItem("Percentage").Value),
											Enable = bool.Parse(attributes.GetNamedItem("Enable").Value),
											Rewards = new int[ınt32]
										};
										BattleRewardXML.smethod_1(j, battleRewardModel);
										BattleRewardXML.Rewards.Add(battleRewardModel);
									}
								}
							}
						}
					}
					catch (Exception exception1)
					{
						Exception exception = exception1;
						CLogger.Print(exception.Message, LoggerType.Error, exception);
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

		private static void smethod_1(XmlNode xmlNode_0, BattleRewardModel battleRewardModel_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Rewards".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Good".Equals(j.Name))
						{
							XmlNamedNodeMap attributes = j.Attributes;
							BattleRewardItem battleRewardItem = new BattleRewardItem()
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
}