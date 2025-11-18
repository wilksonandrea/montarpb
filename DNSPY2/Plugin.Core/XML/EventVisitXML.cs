using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML
{
	// Token: 0x02000012 RID: 18
	public class EventVisitXML
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00010BA0 File Offset: 0x0000EDA0
		public static void Load()
		{
			string text = "Data/Events/Visit.xml";
			if (File.Exists(text))
			{
				EventVisitXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Event Visit", EventVisitXML.Events.Count), LoggerType.Info, null);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00002648 File Offset: 0x00000848
		public static void Reload()
		{
			EventVisitXML.Events.Clear();
			EventVisitXML.Load();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00010BF8 File Offset: 0x0000EDF8
		public static EventVisitModel GetEvent(int EventId)
		{
			List<EventVisitModel> events = EventVisitXML.Events;
			EventVisitModel eventVisitModel2;
			lock (events)
			{
				foreach (EventVisitModel eventVisitModel in EventVisitXML.Events)
				{
					if (eventVisitModel.Id == EventId)
					{
						return eventVisitModel;
					}
				}
				eventVisitModel2 = null;
			}
			return eventVisitModel2;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00010C84 File Offset: 0x0000EE84
		public static EventVisitModel GetRunningEvent()
		{
			List<EventVisitModel> events = EventVisitXML.Events;
			EventVisitModel eventVisitModel2;
			lock (events)
			{
				uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
				foreach (EventVisitModel eventVisitModel in EventVisitXML.Events)
				{
					if (eventVisitModel.BeginDate <= num && num < eventVisitModel.EndedDate)
					{
						return eventVisitModel;
					}
				}
				eventVisitModel2 = null;
			}
			return eventVisitModel2;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00010D28 File Offset: 0x0000EF28
		public static void ResetPlayerEvent(long PlayerId, PlayerEvent Event)
		{
			if (PlayerId == 0L)
			{
				return;
			}
			ComDiv.UpdateDB("player_events", "owner_id", PlayerId, new string[] { "last_visit_check_day", "last_visit_seq_type", "last_visit_date" }, new object[]
			{
				Event.LastVisitCheckDay,
				Event.LastVisitSeqType,
				(long)((ulong)Event.LastVisitDate)
			});
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00010DA0 File Offset: 0x0000EFA0
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
										EventVisitXML.smethod_1(xmlNode2, eventVisitModel);
										eventVisitModel.SetBoxCounts();
										EventVisitXML.Events.Add(eventVisitModel);
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

		// Token: 0x060000D3 RID: 211 RVA: 0x00010F80 File Offset: 0x0000F180
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
							int num = int.Parse(attributes.GetNamedItem("Day").Value) - 1;
							eventVisitModel_0.Boxes[num].Reward1.SetGoodId(int.Parse(attributes.GetNamedItem("GoodId1").Value));
							eventVisitModel_0.Boxes[num].Reward2.SetGoodId(int.Parse(attributes.GetNamedItem("GoodId2").Value));
							eventVisitModel_0.Boxes[num].IsBothReward = bool.Parse(attributes.GetNamedItem("Both").Value);
						}
					}
				}
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00002116 File Offset: 0x00000316
		public EventVisitXML()
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00002659 File Offset: 0x00000859
		// Note: this type is marked as 'beforefieldinit'.
		static EventVisitXML()
		{
		}

		// Token: 0x04000058 RID: 88
		public static readonly List<EventVisitModel> Events = new List<EventVisitModel>();
	}
}
