using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML
{
	// Token: 0x0200000E RID: 14
	public class EventBoostXML
	{
		// Token: 0x060000AC RID: 172 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
		public static void Load()
		{
			string text = "Data/Events/Boost.xml";
			if (File.Exists(text))
			{
				EventBoostXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Event Boost Bonus", EventBoostXML.Events.Count), LoggerType.Info, null);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000025B9 File Offset: 0x000007B9
		public static void Reload()
		{
			EventBoostXML.Events.Clear();
			EventBoostXML.Load();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000FBF8 File Offset: 0x0000DDF8
		public static EventBoostModel GetRunningEvent()
		{
			List<EventBoostModel> events = EventBoostXML.Events;
			EventBoostModel eventBoostModel2;
			lock (events)
			{
				uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
				foreach (EventBoostModel eventBoostModel in EventBoostXML.Events)
				{
					if (eventBoostModel.BeginDate <= num && num < eventBoostModel.EndedDate)
					{
						return eventBoostModel;
					}
				}
				eventBoostModel2 = null;
			}
			return eventBoostModel2;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000FC9C File Offset: 0x0000DE9C
		public static EventBoostModel GetEvent(int EventId)
		{
			List<EventBoostModel> events = EventBoostXML.Events;
			EventBoostModel eventBoostModel2;
			lock (events)
			{
				foreach (EventBoostModel eventBoostModel in EventBoostXML.Events)
				{
					if (eventBoostModel.Id == EventId)
					{
						return eventBoostModel;
					}
				}
				eventBoostModel2 = null;
			}
			return eventBoostModel2;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000025CA File Offset: 0x000007CA
		public static bool EventIsValid(EventBoostModel Event, PortalBoostEvent BoostType, int BoostValue)
		{
			return Event != null && (Event.BoostType == BoostType || Event.BoostValue == BoostValue);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000FD28 File Offset: 0x0000DF28
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
										EventBoostModel eventBoostModel = new EventBoostModel
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
					catch (XmlException ex)
					{
						CLogger.Print(ex.Message, LoggerType.Error, ex);
					}
				}
				fileStream.Dispose();
				fileStream.Close();
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002116 File Offset: 0x00000316
		public EventBoostXML()
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000025E5 File Offset: 0x000007E5
		// Note: this type is marked as 'beforefieldinit'.
		static EventBoostXML()
		{
		}

		// Token: 0x04000054 RID: 84
		public static List<EventBoostModel> Events = new List<EventBoostModel>();
	}
}
