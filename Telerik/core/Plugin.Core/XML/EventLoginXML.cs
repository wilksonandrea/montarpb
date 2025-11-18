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
	public class EventLoginXML
	{
		public static List<EventLoginModel> Events;

		static EventLoginXML()
		{
			EventLoginXML.Events = new List<EventLoginModel>();
		}

		public EventLoginXML()
		{
		}

		public static EventLoginModel GetEvent(int EventId)
		{
			EventLoginModel eventLoginModel;
			lock (EventLoginXML.Events)
			{
				foreach (EventLoginModel @event in EventLoginXML.Events)
				{
					if (@event.Id != EventId)
					{
						continue;
					}
					eventLoginModel = @event;
					return eventLoginModel;
				}
				eventLoginModel = null;
			}
			return eventLoginModel;
		}

		public static EventLoginModel GetRunningEvent()
		{
			EventLoginModel eventLoginModel;
			lock (EventLoginXML.Events)
			{
				uint uInt32 = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
				foreach (EventLoginModel @event in EventLoginXML.Events)
				{
					if (@event.BeginDate > uInt32 || uInt32 >= @event.EndedDate)
					{
						continue;
					}
					eventLoginModel = @event;
					return eventLoginModel;
				}
				eventLoginModel = null;
			}
			return eventLoginModel;
		}

		public static void Load()
		{
			string str = "Data/Events/Login.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				EventLoginXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Event Login", EventLoginXML.Events.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			EventLoginXML.Events.Clear();
			EventLoginXML.Load();
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
										EventLoginModel eventLoginModel = new EventLoginModel()
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
										EventLoginXML.smethod_1(j, eventLoginModel);
										EventLoginXML.Events.Add(eventLoginModel);
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

		private static void smethod_1(XmlNode xmlNode_0, EventLoginModel eventLoginModel_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Rewards".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Item".Equals(j.Name))
						{
							int ınt32 = int.Parse(j.Attributes.GetNamedItem("GoodId").Value);
							if (eventLoginModel_0.Goods.Count > 4)
							{
								CLogger.Print("Max that can be listed on Login Event was 4!", LoggerType.Warning, null);
								return;
							}
							eventLoginModel_0.Goods.Add(ınt32);
						}
					}
				}
			}
		}
	}
}