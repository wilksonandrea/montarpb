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
	public class EventRankUpXML
	{
		public static List<EventRankUpModel> Events;

		static EventRankUpXML()
		{
			EventRankUpXML.Events = new List<EventRankUpModel>();
		}

		public EventRankUpXML()
		{
		}

		public static EventRankUpModel GetEvent(int EventId)
		{
			EventRankUpModel eventRankUpModel;
			lock (EventRankUpXML.Events)
			{
				foreach (EventRankUpModel @event in EventRankUpXML.Events)
				{
					if (@event.Id != EventId)
					{
						continue;
					}
					eventRankUpModel = @event;
					return eventRankUpModel;
				}
				eventRankUpModel = null;
			}
			return eventRankUpModel;
		}

		public static EventRankUpModel GetRunningEvent()
		{
			EventRankUpModel eventRankUpModel;
			lock (EventRankUpXML.Events)
			{
				uint uInt32 = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
				foreach (EventRankUpModel @event in EventRankUpXML.Events)
				{
					if (@event.BeginDate > uInt32 || uInt32 >= @event.EndedDate)
					{
						continue;
					}
					eventRankUpModel = @event;
					return eventRankUpModel;
				}
				eventRankUpModel = null;
			}
			return eventRankUpModel;
		}

		public static void Load()
		{
			string str = "Data/Events/Rank.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				EventRankUpXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Event Rank Up", EventRankUpXML.Events.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			EventRankUpXML.Events.Clear();
			EventRankUpXML.Load();
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
										EventRankUpModel eventRankUpModel = new EventRankUpModel()
										{
											Id = int.Parse(attributes.GetNamedItem("Id").Value),
											BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value),
											EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value),
											Name = attributes.GetNamedItem("Name").Value,
											Description = attributes.GetNamedItem("Description").Value,
											Period = bool.Parse(attributes.GetNamedItem("Period").Value),
											Priority = bool.Parse(attributes.GetNamedItem("Priority").Value),
											Ranks = new List<int[]>()
										};
										EventRankUpXML.smethod_1(j, eventRankUpModel);
										EventRankUpXML.Events.Add(eventRankUpModel);
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

		private static void smethod_1(XmlNode xmlNode_0, EventRankUpModel eventRankUpModel_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Rewards".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Rank".Equals(j.Name))
						{
							XmlNamedNodeMap attributes = j.Attributes;
							int[] ınt32Array = new int[] { int.Parse(attributes.GetNamedItem("UpId").Value), int.Parse(attributes.GetNamedItem("BonusExp").Value), int.Parse(attributes.GetNamedItem("BonusPoint").Value), int.Parse(attributes.GetNamedItem("Percent").Value) };
							eventRankUpModel_0.Ranks.Add(ınt32Array);
						}
					}
				}
			}
		}
	}
}