using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML;

public class EventXmasXML
{
	private static List<EventXmasModel> list_0 = new List<EventXmasModel>();

	public static void Load()
	{
		string text = "Data/Events/Xmas.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {list_0.Count} Event X-Mas", LoggerType.Info);
	}

	public static void Reload()
	{
		list_0.Clear();
		Load();
	}

	public static EventXmasModel GetRunningEvent()
	{
		uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
		foreach (EventXmasModel item in list_0)
		{
			if (item.BeginDate <= num && num < item.EndedDate)
			{
				return item;
			}
		}
		return null;
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
							if ("Event".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								EventXmasModel item = new EventXmasModel
								{
									BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value),
									EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value),
									GoodId = int.Parse(attributes.GetNamedItem("GoodId").Value)
								};
								list_0.Add(item);
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
