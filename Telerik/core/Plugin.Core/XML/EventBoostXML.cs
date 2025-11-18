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
	public class EventBoostXML
	{
		public static List<EventBoostModel> Events;

		static EventBoostXML()
		{
			EventBoostXML.Events = new List<EventBoostModel>();
		}

		public EventBoostXML()
		{
		}

		public static bool EventIsValid(EventBoostModel Event, PortalBoostEvent BoostType, int BoostValue)
		{
			if (Event == null)
			{
				return false;
			}
			if (Event.BoostType == BoostType)
			{
				return true;
			}
			return Event.BoostValue == BoostValue;
		}

		public static EventBoostModel GetEvent(int EventId)
		{
			EventBoostModel eventBoostModel;
			lock (EventBoostXML.Events)
			{
				foreach (EventBoostModel @event in EventBoostXML.Events)
				{
					if (@event.Id != EventId)
					{
						continue;
					}
					eventBoostModel = @event;
					return eventBoostModel;
				}
				eventBoostModel = null;
			}
			return eventBoostModel;
		}

		public static EventBoostModel GetRunningEvent()
		{
			EventBoostModel eventBoostModel;
			lock (EventBoostXML.Events)
			{
				uint uInt32 = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
				foreach (EventBoostModel @event in EventBoostXML.Events)
				{
					if (@event.BeginDate > uInt32 || uInt32 >= @event.EndedDate)
					{
						continue;
					}
					eventBoostModel = @event;
					return eventBoostModel;
				}
				eventBoostModel = null;
			}
			return eventBoostModel;
		}

		public static void Load()
		{
			string str = "Data/Events/Boost.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				EventBoostXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Event Boost Bonus", EventBoostXML.Events.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			EventBoostXML.Events.Clear();
			EventBoostXML.Load();
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
										EventBoostModel eventBoostModel = new EventBoostModel()
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
										EventBoostXML.Events.Add(eventBoostModel);
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
	}
}