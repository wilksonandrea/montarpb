using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML
{
	// Token: 0x0200000D RID: 13
	public class EventLoginXML
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x0000F794 File Offset: 0x0000D994
		public static void Load()
		{
			string text = "Data/Events/Login.xml";
			if (File.Exists(text))
			{
				EventLoginXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Event Login", EventLoginXML.Events.Count), LoggerType.Info, null);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000259C File Offset: 0x0000079C
		public static void Reload()
		{
			EventLoginXML.Events.Clear();
			EventLoginXML.Load();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000F7EC File Offset: 0x0000D9EC
		public static EventLoginModel GetRunningEvent()
		{
			List<EventLoginModel> events = EventLoginXML.Events;
			EventLoginModel eventLoginModel2;
			lock (events)
			{
				uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
				foreach (EventLoginModel eventLoginModel in EventLoginXML.Events)
				{
					if (eventLoginModel.BeginDate <= num && num < eventLoginModel.EndedDate)
					{
						return eventLoginModel;
					}
				}
				eventLoginModel2 = null;
			}
			return eventLoginModel2;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000F890 File Offset: 0x0000DA90
		public static EventLoginModel GetEvent(int EventId)
		{
			List<EventLoginModel> events = EventLoginXML.Events;
			EventLoginModel eventLoginModel2;
			lock (events)
			{
				foreach (EventLoginModel eventLoginModel in EventLoginXML.Events)
				{
					if (eventLoginModel.Id == EventId)
					{
						return eventLoginModel;
					}
				}
				eventLoginModel2 = null;
			}
			return eventLoginModel2;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000F91C File Offset: 0x0000DB1C
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
										EventLoginModel eventLoginModel = new EventLoginModel
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
										EventLoginXML.smethod_1(xmlNode2, eventLoginModel);
										EventLoginXML.Events.Add(eventLoginModel);
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

		// Token: 0x060000A9 RID: 169 RVA: 0x0000FB04 File Offset: 0x0000DD04
		private static void smethod_1(XmlNode xmlNode_0, EventLoginModel eventLoginModel_0)
		{
			for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if ("Rewards".Equals(xmlNode.Name))
				{
					for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
					{
						if ("Item".Equals(xmlNode2.Name))
						{
							int num = int.Parse(xmlNode2.Attributes.GetNamedItem("GoodId").Value);
							if (eventLoginModel_0.Goods.Count > 4)
							{
								CLogger.Print("Max that can be listed on Login Event was 4!", LoggerType.Warning, null);
								return;
							}
							eventLoginModel_0.Goods.Add(num);
						}
					}
				}
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002116 File Offset: 0x00000316
		public EventLoginXML()
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000025AD File Offset: 0x000007AD
		// Note: this type is marked as 'beforefieldinit'.
		static EventLoginXML()
		{
		}

		// Token: 0x04000053 RID: 83
		public static List<EventLoginModel> Events = new List<EventLoginModel>();
	}
}
