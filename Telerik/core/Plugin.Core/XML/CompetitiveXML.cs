using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class CompetitiveXML
	{
		public static List<CompetitiveRank> Ranks;

		static CompetitiveXML()
		{
			CompetitiveXML.Ranks = new List<CompetitiveRank>();
		}

		public CompetitiveXML()
		{
		}

		public static CompetitiveRank GetRank(int Level)
		{
			CompetitiveRank competitiveRank;
			lock (CompetitiveXML.Ranks)
			{
				foreach (CompetitiveRank rank in CompetitiveXML.Ranks)
				{
					if (rank.Id != Level)
					{
						continue;
					}
					competitiveRank = rank;
					return competitiveRank;
				}
				competitiveRank = null;
			}
			return competitiveRank;
		}

		public static void Load()
		{
			string str = "Data/Competitions.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				CompetitiveXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Competitive Ranks", CompetitiveXML.Ranks.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			CompetitiveXML.Ranks.Clear();
			CompetitiveXML.Load();
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
									if ("Competitive".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										CompetitiveRank competitiveRank = new CompetitiveRank()
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