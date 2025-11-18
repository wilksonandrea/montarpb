using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML
{
	// Token: 0x0200000A RID: 10
	public class BattleRewardXML
	{
		// Token: 0x06000092 RID: 146 RVA: 0x0000EFFC File Offset: 0x0000D1FC
		public static void Load()
		{
			string text = "Data/BattleRewards.xml";
			if (File.Exists(text))
			{
				BattleRewardXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Battle Rewards", BattleRewardXML.Rewards.Count), LoggerType.Info, null);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002545 File Offset: 0x00000745
		public static void Reload()
		{
			BattleRewardXML.Rewards.Clear();
			BattleRewardXML.Load();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000F054 File Offset: 0x0000D254
		public static BattleRewardModel GetRewardType(BattleRewardType RewardType)
		{
			List<BattleRewardModel> rewards = BattleRewardXML.Rewards;
			BattleRewardModel battleRewardModel2;
			lock (rewards)
			{
				foreach (BattleRewardModel battleRewardModel in BattleRewardXML.Rewards)
				{
					if (battleRewardModel.Type == RewardType)
					{
						return battleRewardModel;
					}
				}
				battleRewardModel2 = null;
			}
			return battleRewardModel2;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000F0E0 File Offset: 0x0000D2E0
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
										BattleRewardXML.smethod_1(xmlNode2, battleRewardModel);
										BattleRewardXML.Rewards.Add(battleRewardModel);
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
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000F27C File Offset: 0x0000D47C
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

		// Token: 0x06000097 RID: 151 RVA: 0x00002116 File Offset: 0x00000316
		public BattleRewardXML()
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002556 File Offset: 0x00000756
		// Note: this type is marked as 'beforefieldinit'.
		static BattleRewardXML()
		{
		}

		// Token: 0x04000050 RID: 80
		public static List<BattleRewardModel> Rewards = new List<BattleRewardModel>();
	}
}
