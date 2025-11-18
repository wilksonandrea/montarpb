using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;

namespace Plugin.Core.XML;

public class MissionAwardXML
{
	private static List<MissionAwards> list_0 = new List<MissionAwards>();

	public static void Load()
	{
		string text = "Data/Cards/MissionAwards.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {list_0.Count} Mission Awards", LoggerType.Info);
	}

	public static void Reload()
	{
		list_0.Clear();
		Load();
	}

	public static MissionAwards GetAward(int MissionId)
	{
		lock (list_0)
		{
			foreach (MissionAwards item in list_0)
			{
				if (item.Id == MissionId)
				{
					return item;
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
							if ("Mission".Equals(xmlNode2.Name))
							{
								XmlAttributeCollection attributes = xmlNode2.Attributes;
								int int_ = int.Parse(attributes.GetNamedItem("Id").Value);
								int int_2 = int.Parse(attributes.GetNamedItem("MasterMedal").Value);
								int int_3 = int.Parse(attributes.GetNamedItem("Exp").Value);
								int int_4 = int.Parse(attributes.GetNamedItem("Point").Value);
								list_0.Add(new MissionAwards(int_, int_2, int_3, int_4));
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
