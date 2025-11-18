using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML;

public class EventBoostXML
{
	public static List<EventBoostModel> Events = new List<EventBoostModel>();

	public static void Load()
	{
		string text = "Data/Events/Boost.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Events.Count} Event Boost Bonus", LoggerType.Info);
	}

	public static void Reload()
	{
		Events.Clear();
		Load();
	}

	public static EventBoostModel GetRunningEvent()
	{
		lock (Events)
		{
			uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			foreach (EventBoostModel @event in Events)
			{
				if (@event.BeginDate <= num && num < @event.EndedDate)
				{
					return @event;
				}
			}
			return null;
		}
	}

	public static EventBoostModel GetEvent(int EventId)
	{
		lock (Events)
		{
			foreach (EventBoostModel @event in Events)
			{
				if (@event.Id == EventId)
				{
					return @event;
				}
			}
			return null;
		}
	}

	public static bool EventIsValid(EventBoostModel Event, PortalBoostEvent BoostType, int BoostValue)
	{
		if (Event != null)
		{
			if (Event.BoostType != BoostType)
			{
				return Event.BoostValue == BoostValue;
			}
			return true;
		}
		return false;
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
								EventBoostModel item = new EventBoostModel
								{
									Id = int.Parse(attributes.GetNamedItem("Id").Value),
									BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value),
									EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value),
									BoostType = ComDiv.ParseEnum<PortalBoostEvent>(attributes.GetNamedItem("BoostType").Value),
									BoostValue = int.Parse(attributes.GetNamedItem("BoostValue").Value),
									BonusExp = int.Parse(attributes.GetNamedItem("BonusExp").Value),
									BonusGold = int.Parse(attributes.GetNamedItem("BonusGold").Value),
									Percent = int.Parse(attributes.GetNamedItem("Percent").Value),
									Name = attributes.GetNamedItem("Name").Value,
									Description = attributes.GetNamedItem("Description").Value,
									Period = bool.Parse(attributes.GetNamedItem("Period").Value),
									Priority = bool.Parse(attributes.GetNamedItem("Priority").Value)
								};
								Events.Add(item);
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
