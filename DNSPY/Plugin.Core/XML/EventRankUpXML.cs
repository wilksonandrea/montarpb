using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML
{
	// Token: 0x02000011 RID: 17
	public class EventRankUpXML
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00010750 File Offset: 0x0000E950
		public static void Load()
		{
			string text = "Data/Events/Rank.xml";
			if (File.Exists(text))
			{
				EventRankUpXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Event Rank Up", EventRankUpXML.Events.Count), LoggerType.Info, null);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000262B File Offset: 0x0000082B
		public static void Reload()
		{
			EventRankUpXML.Events.Clear();
			EventRankUpXML.Load();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000107A8 File Offset: 0x0000E9A8
		public static EventRankUpModel GetRunningEvent()
		{
			List<EventRankUpModel> events = EventRankUpXML.Events;
			EventRankUpModel eventRankUpModel2;
			lock (events)
			{
				uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
				foreach (EventRankUpModel eventRankUpModel in EventRankUpXML.Events)
				{
					if (eventRankUpModel.BeginDate <= num && num < eventRankUpModel.EndedDate)
					{
						return eventRankUpModel;
					}
				}
				eventRankUpModel2 = null;
			}
			return eventRankUpModel2;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0001084C File Offset: 0x0000EA4C
		public static EventRankUpModel GetEvent(int EventId)
		{
			List<EventRankUpModel> events = EventRankUpXML.Events;
			EventRankUpModel eventRankUpModel2;
			lock (events)
			{
				foreach (EventRankUpModel eventRankUpModel in EventRankUpXML.Events)
				{
					if (eventRankUpModel.Id == EventId)
					{
						return eventRankUpModel;
					}
				}
				eventRankUpModel2 = null;
			}
			return eventRankUpModel2;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000108D8 File Offset: 0x0000EAD8
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
										EventRankUpModel eventRankUpModel = new EventRankUpModel
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
										EventRankUpXML.smethod_1(xmlNode2, eventRankUpModel);
										EventRankUpXML.Events.Add(eventRankUpModel);
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

		// Token: 0x060000CA RID: 202 RVA: 0x00010AC0 File Offset: 0x0000ECC0
		private static void smethod_1(XmlNode xmlNode_0, EventRankUpModel eventRankUpModel_0)
		{
			for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if ("Rewards".Equals(xmlNode.Name))
				{
					for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
					{
						if ("Rank".Equals(xmlNode2.Name))
						{
							XmlNamedNodeMap attributes = xmlNode2.Attributes;
							int[] array = new int[]
							{
								int.Parse(attributes.GetNamedItem("UpId").Value),
								int.Parse(attributes.GetNamedItem("BonusExp").Value),
								int.Parse(attributes.GetNamedItem("BonusPoint").Value),
								int.Parse(attributes.GetNamedItem("Percent").Value)
							};
							eventRankUpModel_0.Ranks.Add(array);
						}
					}
				}
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00002116 File Offset: 0x00000316
		public EventRankUpXML()
		{
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000263C File Offset: 0x0000083C
		// Note: this type is marked as 'beforefieldinit'.
		static EventRankUpXML()
		{
		}

		// Token: 0x04000057 RID: 87
		public static List<EventRankUpModel> Events = new List<EventRankUpModel>();
	}
}
