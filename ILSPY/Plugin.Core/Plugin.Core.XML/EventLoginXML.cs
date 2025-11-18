using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML;

public class EventLoginXML
{
	public static List<EventLoginModel> Events = new List<EventLoginModel>();

	public static void Load()
	{
		string text = "Data/Events/Login.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Events.Count} Event Login", LoggerType.Info);
	}

	public static void Reload()
	{
		Events.Clear();
		Load();
	}

	public static EventLoginModel GetRunningEvent()
	{
		lock (Events)
		{
			uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			foreach (EventLoginModel @event in Events)
			{
				if (@event.BeginDate <= num && num < @event.EndedDate)
				{
					return @event;
				}
			}
			return null;
		}
	}

	public static EventLoginModel GetEvent(int EventId)
	{
		lock (Events)
		{
			foreach (EventLoginModel @event in Events)
			{
				if (@event.Id == EventId)
				{
					return @event;
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
							if ("Event".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								EventLoginModel eventLoginModel = new EventLoginModel
								{
									Id = int.Parse(attributes.GetNamedItem("Id").Value),
									BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value),
									EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value),
									Name = attributes.GetNamedItem("Name").Value,
									Description = attributes.GetNamedItem("Description").Value,
									Period = bool.Parse(attributes.GetNamedItem("Period").Value),
									Priority = bool.Parse(attributes.GetNamedItem("Priority").Value),
									Goods = new List<int>()
								};
								smethod_1(xmlNode2, eventLoginModel);
								Events.Add(eventLoginModel);
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

	private static void smethod_1(XmlNode xmlNode_0, EventLoginModel eventLoginModel_0)
	{
		for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
		{
			if ("Rewards".Equals(xmlNode.Name))
			{
				for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
				{
					if ("Item".Equals(xmlNode2.Name))
					{
						int item = int.Parse(xmlNode2.Attributes.GetNamedItem("GoodId").Value);
						if (eventLoginModel_0.Goods.Count > 4)
						{
							CLogger.Print("Max that can be listed on Login Event was 4!", LoggerType.Warning);
							return;
						}
						eventLoginModel_0.Goods.Add(item);
					}
				}
			}
		}
	}
}
