using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class EventVisitXML
	{
		public readonly static List<EventVisitModel> Events;

		static EventVisitXML()
		{
			EventVisitXML.Events = new List<EventVisitModel>();
		}

		public EventVisitXML()
		{
		}

		public static EventVisitModel GetEvent(int EventId)
		{
			EventVisitModel eventVisitModel;
			lock (EventVisitXML.Events)
			{
				foreach (EventVisitModel @event in EventVisitXML.Events)
				{
					if (@event.Id != EventId)
					{
						continue;
					}
					eventVisitModel = @event;
					return eventVisitModel;
				}
				eventVisitModel = null;
			}
			return eventVisitModel;
		}

		public static EventVisitModel GetRunningEvent()
		{
			EventVisitModel eventVisitModel;
			lock (EventVisitXML.Events)
			{
				uint uInt32 = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
				foreach (EventVisitModel @event in EventVisitXML.Events)
				{
					if (@event.BeginDate > uInt32 || uInt32 >= @event.EndedDate)
					{
						continue;
					}
					eventVisitModel = @event;
					return eventVisitModel;
				}
				eventVisitModel = null;
			}
			return eventVisitModel;
		}

		public static void Load()
		{
			string str = "Data/Events/Visit.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				EventVisitXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Event Visit", EventVisitXML.Events.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			EventVisitXML.Events.Clear();
			EventVisitXML.Load();
		}

		public static void ResetPlayerEvent(long PlayerId, PlayerEvent Event)
		{
			if (PlayerId == 0)
			{
				return;
			}
			ComDiv.UpdateDB("player_events", "owner_id", PlayerId, new string[] { "last_visit_check_day", "last_visit_seq_type", "last_visit_date" }, new object[] { Event.LastVisitCheckDay, Event.LastVisitSeqType, (long)((ulong)Event.LastVisitDate) });
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
									if ("Event".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										EventVisitModel eventVisitModel = new EventVisitModel()
										{
											Id = int.Parse(attributes.GetNamedItem("Id").Value),
											BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value),
											EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value),
											Title = attributes.GetNamedItem("Title").Value,
											Checks = int.Parse(attributes.GetNamedItem("Days").Value),
											Boxes = new List<VisitBoxModel>()
										};
										for (int k = 0; k < 31; k++)
										{
											eventVisitModel.Boxes.Add(new VisitBoxModel());
										}
										EventVisitXML.smethod_1(j, eventVisitModel);
										eventVisitModel.SetBoxCounts();
										EventVisitXML.Events.Add(eventVisitModel);
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

		private static void smethod_1(XmlNode xmlNode_0, EventVisitModel eventVisitModel_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Rewards".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Box".Equals(j.Name))
						{
							XmlNamedNodeMap attributes = j.Attributes;
							int ınt32 = int.Parse(attributes.GetNamedItem("Day").Value) - 1;
							eventVisitModel_0.Boxes[ınt32].Reward1.SetGoodId(int.Parse(attributes.GetNamedItem("GoodId1").Value));
							eventVisitModel_0.Boxes[ınt32].Reward2.SetGoodId(int.Parse(attributes.GetNamedItem("GoodId2").Value));
							eventVisitModel_0.Boxes[ınt32].IsBothReward = bool.Parse(attributes.GetNamedItem("Both").Value);
						}
					}
				}
			}
		}
	}
}