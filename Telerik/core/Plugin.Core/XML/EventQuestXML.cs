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
	public class EventQuestXML
	{
		private static List<EventQuestModel> list_0;

		static EventQuestXML()
		{
			EventQuestXML.list_0 = new List<EventQuestModel>();
		}

		public EventQuestXML()
		{
		}

		public static EventQuestModel GetRunningEvent()
		{
			EventQuestModel eventQuestModel;
			uint uInt32 = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			List<EventQuestModel>.Enumerator enumerator = EventQuestXML.list_0.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					EventQuestModel current = enumerator.Current;
					if (current.BeginDate > uInt32 || uInt32 >= current.EndedDate)
					{
						continue;
					}
					eventQuestModel = current;
					return eventQuestModel;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return eventQuestModel;
		}

		public static void Load()
		{
			string str = "Data/Events/Quest.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				EventQuestXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Event Quest", EventQuestXML.list_0.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			EventQuestXML.list_0.Clear();
			EventQuestXML.Load();
		}

		public static void ResetPlayerEvent(long pId, PlayerEvent pE)
		{
			if (pId == 0)
			{
				return;
			}
			ComDiv.UpdateDB("player_events", "owner_id", pId, new string[] { "last_quest_date", "last_quest_finish" }, new object[] { (long)((ulong)pE.LastQuestDate), pE.LastQuestFinish });
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
										EventQuestModel eventQuestModel = new EventQuestModel()
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