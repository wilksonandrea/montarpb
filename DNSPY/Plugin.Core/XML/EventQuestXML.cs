using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML
{
	// Token: 0x02000010 RID: 16
	public class EventQuestXML
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000104F0 File Offset: 0x0000E6F0
		public static void Load()
		{
			string text = "Data/Events/Quest.xml";
			if (File.Exists(text))
			{
				EventQuestXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Event Quest", EventQuestXML.list_0.Count), LoggerType.Info, null);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000260E File Offset: 0x0000080E
		public static void Reload()
		{
			EventQuestXML.list_0.Clear();
			EventQuestXML.Load();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00010548 File Offset: 0x0000E748
		public static EventQuestModel GetRunningEvent()
		{
			uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			foreach (EventQuestModel eventQuestModel in EventQuestXML.list_0)
			{
				if (eventQuestModel.BeginDate <= num && num < eventQuestModel.EndedDate)
				{
					return eventQuestModel;
				}
			}
			return null;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000105C0 File Offset: 0x0000E7C0
		public static void ResetPlayerEvent(long pId, PlayerEvent pE)
		{
			if (pId == 0L)
			{
				return;
			}
			ComDiv.UpdateDB("player_events", "owner_id", pId, new string[] { "last_quest_date", "last_quest_finish" }, new object[]
			{
				(long)((ulong)pE.LastQuestDate),
				pE.LastQuestFinish
			});
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00010620 File Offset: 0x0000E820
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
										EventQuestModel eventQuestModel = new EventQuestModel
										{
											BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value),
											EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value)
										};
										EventQuestXML.list_0.Add(eventQuestModel);
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

		// Token: 0x060000C3 RID: 195 RVA: 0x00002116 File Offset: 0x00000316
		public EventQuestXML()
		{
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000261F File Offset: 0x0000081F
		// Note: this type is marked as 'beforefieldinit'.
		static EventQuestXML()
		{
		}

		// Token: 0x04000056 RID: 86
		private static List<EventQuestModel> list_0 = new List<EventQuestModel>();
	}
}
