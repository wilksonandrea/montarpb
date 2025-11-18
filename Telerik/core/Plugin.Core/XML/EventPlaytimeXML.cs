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
	public class EventPlaytimeXML
	{
		public readonly static List<EventPlaytimeModel> Events;

		static EventPlaytimeXML()
		{
			EventPlaytimeXML.Events = new List<EventPlaytimeModel>();
		}

		public EventPlaytimeXML()
		{
		}

		public static EventPlaytimeModel GetEvent(int EventId)
		{
			EventPlaytimeModel eventPlaytimeModel;
			lock (EventPlaytimeXML.Events)
			{
				foreach (EventPlaytimeModel @event in EventPlaytimeXML.Events)
				{
					if (@event.Id != EventId)
					{
						continue;
					}
					eventPlaytimeModel = @event;
					return eventPlaytimeModel;
				}
				eventPlaytimeModel = null;
			}
			return eventPlaytimeModel;
		}

		public static EventPlaytimeModel GetRunningEvent()
		{
			EventPlaytimeModel eventPlaytimeModel;
			lock (EventPlaytimeXML.Events)
			{
				uint uInt32 = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
				foreach (EventPlaytimeModel @event in EventPlaytimeXML.Events)
				{
					if (@event.BeginDate > uInt32 || uInt32 >= @event.EndedDate)
					{
						continue;
					}
					eventPlaytimeModel = @event;
					return eventPlaytimeModel;
				}
				eventPlaytimeModel = null;
			}
			return eventPlaytimeModel;
		}

		public static void Load()
		{
			string str = "Data/Events/Play.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				EventPlaytimeXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Event Playtime", EventPlaytimeXML.Events.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			EventPlaytimeXML.Events.Clear();
			EventPlaytimeXML.Load();
		}

		public static void ResetPlayerEvent(long PlayerId, PlayerEvent Event)
		{
			if (PlayerId == 0)
			{
				return;
			}
			ComDiv.UpdateDB("player_events", "owner_id", PlayerId, new string[] { "last_playtime_value", "last_playtime_finish", "last_playtime_date" }, new object[] { Event.LastPlaytimeValue, Event.LastPlaytimeFinish, (long)((ulong)Event.LastPlaytimeDate) });
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
										EventPlaytimeModel eventPlaytimeModel = new EventPlaytimeModel()
										{
											Id = int.Parse(attributes.GetNamedItem("Id").Value),
											BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value),
											EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value),
											Name = attributes.GetNamedItem("Name").Value,
											Description = attributes.GetNamedItem("Description").Value,
											Period = bool.Parse(attributes.GetNamedItem("Period").Value),
											Priority = bool.Parse(attributes.GetNamedItem("Priority").Value)
										};
										EventPlaytimeXML.smethod_1(j, eventPlaytimeModel);
										EventPlaytimeXML.Events.Add(eventPlaytimeModel);
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

		private static void smethod_1(XmlNode xmlNode_0, EventPlaytimeModel eventPlaytimeModel_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Minutes".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Time".Equals(j.Name))
						{
							XmlNamedNodeMap attributes = j.Attributes;
							int ınt32 = int.Parse(attributes.GetNamedItem("Index").Value);
							if (ınt32 == 1)
							{
								eventPlaytimeModel_0.Minutes1 = int.Parse(attributes.GetNamedItem("Play").Value);
								eventPlaytimeModel_0.Goods1 = EventPlaytimeXML.smethod_2(j);
							}
							else if (ınt32 == 2)
							{
								eventPlaytimeModel_0.Minutes2 = int.Parse(attributes.GetNamedItem("Play").Value);
								eventPlaytimeModel_0.Goods2 = EventPlaytimeXML.smethod_2(j);
							}
							else if (ınt32 == 3)
							{
								eventPlaytimeModel_0.Minutes3 = int.Parse(attributes.GetNamedItem("Play").Value);
								eventPlaytimeModel_0.Goods3 = EventPlaytimeXML.smethod_2(j);
							}
						}
					}
				}
			}
		}

		private static List<int> smethod_2(XmlNode xmlNode_0)
		{
			List<int> ınt32s = new List<int>();
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Reward".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Goods".Equals(j.Name))
						{
							XmlNamedNodeMap attributes = j.Attributes;
							ınt32s.Add(int.Parse(attributes.GetNamedItem("Id").Value));
						}
					}
				}
			}
			return ınt32s;
		}
	}
}