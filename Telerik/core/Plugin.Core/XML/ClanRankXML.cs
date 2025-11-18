using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class ClanRankXML
	{
		private static List<RankModel> list_0;

		static ClanRankXML()
		{
			ClanRankXML.list_0 = new List<RankModel>();
		}

		public ClanRankXML()
		{
		}

		public static RankModel GetRank(int Id)
		{
			RankModel rankModel;
			lock (ClanRankXML.list_0)
			{
				foreach (RankModel list0 in ClanRankXML.list_0)
				{
					if (list0.Id != Id)
					{
						continue;
					}
					rankModel = list0;
					return rankModel;
				}
				rankModel = null;
			}
			return rankModel;
		}

		public static void Load()
		{
			string str = "Data/Ranks/Clan.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				ClanRankXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Clan Ranks", ClanRankXML.list_0.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			ClanRankXML.list_0.Clear();
			ClanRankXML.Load();
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
	}
}