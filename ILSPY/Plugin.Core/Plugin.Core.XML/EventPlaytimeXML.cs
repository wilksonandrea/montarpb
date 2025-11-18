using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML;

public class EventPlaytimeXML
{
	public static readonly List<EventPlaytimeModel> Events = new List<EventPlaytimeModel>();

	public static void Load()
	{
		string text = "Data/Events/Play.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Events.Count} Event Playtime", LoggerType.Info);
	}

	public static void Reload()
	{
		Events.Clear();
		Load();
	}

	public static EventPlaytimeModel GetRunningEvent()
	{
		lock (Events)
		{
			uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			foreach (EventPlaytimeModel @event in Events)
			{
				if (@event.BeginDate <= num && num < @event.EndedDate)
				{
					return @event;
				}
			}
			return null;
		}
	}

	public static EventPlaytimeModel GetEvent(int EventId)
	{
		lock (Events)
		{
			foreach (EventPlaytimeModel @event in Events)
			{
				if (@event.Id == EventId)
				{
					return @event;
				}
			}
			return null;
		}
	}

	public static void ResetPlayerEvent(long PlayerId, PlayerEvent Event)
	{
		if (PlayerId != 0L)
		{
			ComDiv.UpdateDB("player_events", "owner_id", PlayerId, new string[3] { "last_playtime_value", "last_playtime_finish", "last_playtime_date" }, Event.LastPlaytimeValue, Event.LastPlaytimeFinish, (long)Event.LastPlaytimeDate);
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
								EventPlaytimeModel eventPlaytimeModel = new EventPlaytimeModel
								{
									Id = int.Parse(attributes.GetNamedItem("Id").Value),
									BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value),
									EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value),
									Name = attributes.GetNamedItem("Name").Value,
									Description = attributes.GetNamedItem("Description").Value,
									Period = bool.Parse(attributes.GetNamedItem("Period").Value),
									Priority = bool.Parse(attributes.GetNamedItem("Priority").Value)
								};
								smethod_1(xmlNode2, eventPlaytimeModel);
								Events.Add(eventPlaytimeModel);
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

	private static void smethod_1(XmlNode xmlNode_0, EventPlaytimeModel eventPlaytimeModel_0)
	{
		for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
		{
			if ("Minutes".Equals(xmlNode.Name))
			{
				for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
				{
					if ("Time".Equals(xmlNode2.Name))
					{
						XmlNamedNodeMap attributes = xmlNode2.Attributes;
						switch (int.Parse(attributes.GetNamedItem("Index").Value))
						{
						case 1:
							eventPlaytimeModel_0.Minutes1 = int.Parse(attributes.GetNamedItem("Play").Value);
							eventPlaytimeModel_0.Goods1 = smethod_2(xmlNode2);
							break;
						case 2:
							eventPlaytimeModel_0.Minutes2 = int.Parse(attributes.GetNamedItem("Play").Value);
							eventPlaytimeModel_0.Goods2 = smethod_2(xmlNode2);
							break;
						case 3:
							eventPlaytimeModel_0.Minutes3 = int.Parse(attributes.GetNamedItem("Play").Value);
							eventPlaytimeModel_0.Goods3 = smethod_2(xmlNode2);
							break;
						}
					}
				}
			}
		}
	}

	private static List<int> smethod_2(XmlNode xmlNode_0)
	{
		List<int> list = new List<int>();
		for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
		{
			if ("Reward".Equals(xmlNode.Name))
			{
				for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
				{
					if ("Goods".Equals(xmlNode2.Name))
					{
						XmlNamedNodeMap attributes = xmlNode2.Attributes;
						list.Add(int.Parse(attributes.GetNamedItem("Id").Value));
					}
				}
			}
		}
		return list;
	}
}
