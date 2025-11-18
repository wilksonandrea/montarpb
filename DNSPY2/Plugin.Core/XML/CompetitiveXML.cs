using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML
{
	// Token: 0x0200000B RID: 11
	public class CompetitiveXML
	{
		// Token: 0x06000099 RID: 153 RVA: 0x0000F330 File Offset: 0x0000D530
		public static void Load()
		{
			string text = "Data/Competitions.xml";
			if (File.Exists(text))
			{
				CompetitiveXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Competitive Ranks", CompetitiveXML.Ranks.Count), LoggerType.Info, null);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002562 File Offset: 0x00000762
		public static void Reload()
		{
			CompetitiveXML.Ranks.Clear();
			CompetitiveXML.Load();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000F388 File Offset: 0x0000D588
		public static CompetitiveRank GetRank(int Level)
		{
			List<CompetitiveRank> ranks = CompetitiveXML.Ranks;
			CompetitiveRank competitiveRank2;
			lock (ranks)
			{
				foreach (CompetitiveRank competitiveRank in CompetitiveXML.Ranks)
				{
					if (competitiveRank.Id == Level)
					{
						return competitiveRank;
					}
				}
				competitiveRank2 = null;
			}
			return competitiveRank2;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000F414 File Offset: 0x0000D614
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
									if ("Competitive".Equals(xmlNode2.Name))
									{
										XmlNamedNodeMap attributes = xmlNode2.Attributes;
										CompetitiveRank competitiveRank = new CompetitiveRank
										{
											Id = int.Parse(attributes.GetNamedItem("Id").Value),
											TourneyLevel = int.Parse(attributes.GetNamedItem("TourneyLevel").Value),
											Points = int.Parse(attributes.GetNamedItem("Points").Value),
											Name = attributes.GetNamedItem("Name").Value
										};
										CompetitiveXML.Ranks.Add(competitiveRank);
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

		// Token: 0x0600009D RID: 157 RVA: 0x00002116 File Offset: 0x00000316
		public CompetitiveXML()
		{
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00002573 File Offset: 0x00000773
		// Note: this type is marked as 'beforefieldinit'.
		static CompetitiveXML()
		{
		}

		// Token: 0x04000051 RID: 81
		public static List<CompetitiveRank> Ranks = new List<CompetitiveRank>();
	}
}
