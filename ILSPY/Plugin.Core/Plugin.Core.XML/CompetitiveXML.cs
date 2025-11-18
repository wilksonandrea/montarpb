using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML;

public class CompetitiveXML
{
	public static List<CompetitiveRank> Ranks = new List<CompetitiveRank>();

	public static void Load()
	{
		string text = "Data/Competitions.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Ranks.Count} Competitive Ranks", LoggerType.Info);
	}

	public static void Reload()
	{
		Ranks.Clear();
		Load();
	}

	public static CompetitiveRank GetRank(int Level)
	{
		lock (Ranks)
		{
			foreach (CompetitiveRank rank in Ranks)
			{
				if (rank.Id == Level)
				{
					return rank;
				}
			}
			return null;
		}
	}

	private static void smethod_0(string string_0)
	{
		XmlDocument xmlDocument = new XmlDocument();
		using FileStream fileStream = new FileStream(string_0, FileMode.Open);
		if (fileStream.Length == 0L)
		{
			CLogger.Print("File is empty: " + string_0, LoggerType.Warning);
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
								CompetitiveRank item = new CompetitiveRank
								{
									Id = int.Parse(attributes.GetNamedItem("Id").Value),
									TourneyLevel = int.Parse(attributes.GetNamedItem("TourneyLevel").Value),
									Points = int.Parse(attributes.GetNamedItem("Points").Value),
									Name = attributes.GetNamedItem("Name").Value
								};
								Ranks.Add(item);
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
