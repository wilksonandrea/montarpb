using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML;

public class EventVisitXML
{
	public static readonly List<EventVisitModel> Events = new List<EventVisitModel>();

	public static void Load()
	{
		string text = "Data/Events/Visit.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Events.Count} Event Visit", LoggerType.Info);
	}

	public static void Reload()
	{
		Events.Clear();
		Load();
	}

	public static EventVisitModel GetEvent(int EventId)
	{
		lock (Events)
		{
			foreach (EventVisitModel @event in Events)
			{
				if (@event.Id == EventId)
				{
					return @event;
				}
			}
			return null;
		}
	}

	public static EventVisitModel GetRunningEvent()
	{
		lock (Events)
		{
			uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			foreach (EventVisitModel @event in Events)
			{
				if (@event.BeginDate <= num && num < @event.EndedDate)
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
			ComDiv.UpdateDB("player_events", "owner_id", PlayerId, new string[3] { "last_visit_check_day", "last_visit_seq_type", "last_visit_date" }, Event.LastVisitCheckDay, Event.LastVisitSeqType, (long)Event.LastVisitDate);
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
								EventVisitModel eventVisitModel = new EventVisitModel
								{
									Id = int.Parse(attributes.GetNamedItem("Id").Value),
									BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value),
									EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value),
									Title = attributes.GetNamedItem("Title").Value,
									Checks = int.Parse(attributes.GetNamedItem("Days").Value),
									Boxes = new List<VisitBoxModel>()
								};
								for (int i = 0; i < 31; i++)
								{
									eventVisitModel.Boxes.Add(new VisitBoxModel());
								}
								smethod_1(xmlNode2, eventVisitModel);
								eventVisitModel.SetBoxCounts();
								Events.Add(eventVisitModel);
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

	private static void smethod_1(XmlNode xmlNode_0, EventVisitModel eventVisitModel_0)
	{
		for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
		{
			if ("Rewards".Equals(xmlNode.Name))
			{
				for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
				{
					if ("Box".Equals(xmlNode2.Name))
					{
						XmlNamedNodeMap attributes = xmlNode2.Attributes;
						int index = int.Parse(attributes.GetNamedItem("Day").Value) - 1;
						eventVisitModel_0.Boxes[index].Reward1.SetGoodId(int.Parse(attributes.GetNamedItem("GoodId1").Value));
						eventVisitModel_0.Boxes[index].Reward2.SetGoodId(int.Parse(attributes.GetNamedItem("GoodId2").Value));
						eventVisitModel_0.Boxes[index].IsBothReward = bool.Parse(attributes.GetNamedItem("Both").Value);
					}
				}
			}
		}
	}
}
