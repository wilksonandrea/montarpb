using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML
{
	// Token: 0x0200001E RID: 30
	public class ClanRankXML
	{
		// Token: 0x0600012C RID: 300 RVA: 0x00013570 File Offset: 0x00011770
		public static void Load()
		{
			string text = "Data/Ranks/Clan.xml";
			if (File.Exists(text))
			{
				ClanRankXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Clan Ranks", ClanRankXML.list_0.Count), LoggerType.Info, null);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00002864 File Offset: 0x00000A64
		public static void Reload()
		{
			ClanRankXML.list_0.Clear();
			ClanRankXML.Load();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000135C8 File Offset: 0x000117C8
		public static RankModel GetRank(int Id)
		{
			List<RankModel> list = ClanRankXML.list_0;
			RankModel rankModel2;
			lock (list)
			{
				foreach (RankModel rankModel in ClanRankXML.list_0)
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

		// Token: 0x0600012F RID: 303 RVA: 0x00013654 File Offset: 0x00011854
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
										RankModel rankModel = new RankModel((int)byte.Parse(attributes.GetNamedItem("Id").Value))
										{
											Title = attributes.GetNamedItem("Title").Value,
											OnNextLevel = int.Parse(attributes.GetNamedItem("OnNextLevel").Value),
											OnGoldUp = 0,
											OnAllExp = int.Parse(attributes.GetNamedItem("OnAllExp").Value)
										};
										ClanRankXML.list_0.Add(rankModel);
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

		// Token: 0x06000130 RID: 304 RVA: 0x00002116 File Offset: 0x00000316
		public ClanRankXML()
		{
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00002875 File Offset: 0x00000A75
		// Note: this type is marked as 'beforefieldinit'.
		static ClanRankXML()
		{
		}

		// Token: 0x0400006A RID: 106
		private static List<RankModel> list_0 = new List<RankModel>();
	}
}
