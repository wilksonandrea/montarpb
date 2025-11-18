using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML
{
	// Token: 0x0200000F RID: 15
	public class EventPlaytimeXML
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x0000FF88 File Offset: 0x0000E188
		public static void Load()
		{
			string text = "Data/Events/Play.xml";
			if (File.Exists(text))
			{
				EventPlaytimeXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Event Playtime", EventPlaytimeXML.Events.Count), LoggerType.Info, null);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000025F1 File Offset: 0x000007F1
		public static void Reload()
		{
			EventPlaytimeXML.Events.Clear();
			EventPlaytimeXML.Load();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		public static EventPlaytimeModel GetRunningEvent()
		{
			List<EventPlaytimeModel> events = EventPlaytimeXML.Events;
			EventPlaytimeModel eventPlaytimeModel2;
			lock (events)
			{
				uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
				foreach (EventPlaytimeModel eventPlaytimeModel in EventPlaytimeXML.Events)
				{
					if (eventPlaytimeModel.BeginDate <= num && num < eventPlaytimeModel.EndedDate)
					{
						return eventPlaytimeModel;
					}
				}
				eventPlaytimeModel2 = null;
			}
			return eventPlaytimeModel2;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00010084 File Offset: 0x0000E284
		public static EventPlaytimeModel GetEvent(int EventId)
		{
			List<EventPlaytimeModel> events = EventPlaytimeXML.Events;
			EventPlaytimeModel eventPlaytimeModel2;
			lock (events)
			{
				foreach (EventPlaytimeModel eventPlaytimeModel in EventPlaytimeXML.Events)
				{
					if (eventPlaytimeModel.Id == EventId)
					{
						return eventPlaytimeModel;
					}
				}
				eventPlaytimeModel2 = null;
			}
			return eventPlaytimeModel2;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00010110 File Offset: 0x0000E310
		public static void ResetPlayerEvent(long PlayerId, PlayerEvent Event)
		{
			if (PlayerId == 0L)
			{
				return;
			}
			ComDiv.UpdateDB("player_events", "owner_id", PlayerId, new string[] { "last_playtime_value", "last_playtime_finish", "last_playtime_date" }, new object[]
			{
				Event.LastPlaytimeValue,
				Event.LastPlaytimeFinish,
				(long)((ulong)Event.LastPlaytimeDate)
			});
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00010188 File Offset: 0x0000E388
		private static void smethod_0(string string_0)
		{
			XmlDocument xmlDocument = new XmlDocument();
			using (FileStream fileStream = new FileStream(string_0, FileMode.Open))
			{
				if (fileStream.Length == 0L)
				{
					CLogger.Print("File is empty: " + string_0, LoggerType.Warning, null);
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
										EventPlaytimeXML.smethod_1(xmlNode2, eventPlaytimeModel);
										EventPlaytimeXML.Events.Add(eventPlaytimeModel);
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

		// Token: 0x060000BA RID: 186 RVA: 0x00010364 File Offset: 0x0000E564
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
							int num = int.Parse(attributes.GetNamedItem("Index").Value);
							if (num == 1)
							{
								eventPlaytimeModel_0.Minutes1 = int.Parse(attributes.GetNamedItem("Play").Value);
								eventPlaytimeModel_0.Goods1 = EventPlaytimeXML.smethod_2(xmlNode2);
							}
							else if (num == 2)
							{
								eventPlaytimeModel_0.Minutes2 = int.Parse(attributes.GetNamedItem("Play").Value);
								eventPlaytimeModel_0.Goods2 = EventPlaytimeXML.smethod_2(xmlNode2);
							}
							else if (num == 3)
							{
								eventPlaytimeModel_0.Minutes3 = int.Parse(attributes.GetNamedItem("Play").Value);
								eventPlaytimeModel_0.Goods3 = EventPlaytimeXML.smethod_2(xmlNode2);
							}
						}
					}
				}
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00010470 File Offset: 0x0000E670
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

		// Token: 0x060000BC RID: 188 RVA: 0x00002116 File Offset: 0x00000316
		public EventPlaytimeXML()
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00002602 File Offset: 0x00000802
		// Note: this type is marked as 'beforefieldinit'.
		static EventPlaytimeXML()
		{
		}

		// Token: 0x04000055 RID: 85
		public static readonly List<EventPlaytimeModel> Events = new List<EventPlaytimeModel>();
	}
}
