using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML
{
	// Token: 0x02000013 RID: 19
	public class EventXmasXML
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00011084 File Offset: 0x0000F284
		public static void Load()
		{
			string text = "Data/Events/Xmas.xml";
			if (File.Exists(text))
			{
				EventXmasXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Event X-Mas", EventXmasXML.list_0.Count), LoggerType.Info, null);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00002665 File Offset: 0x00000865
		public static void Reload()
		{
			EventXmasXML.list_0.Clear();
			EventXmasXML.Load();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000110DC File Offset: 0x0000F2DC
		public static EventXmasModel GetRunningEvent()
		{
			uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			foreach (EventXmasModel eventXmasModel in EventXmasXML.list_0)
			{
				if (eventXmasModel.BeginDate <= num && num < eventXmasModel.EndedDate)
				{
					return eventXmasModel;
				}
			}
			return null;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00011154 File Offset: 0x0000F354
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
										EventXmasModel eventXmasModel = new EventXmasModel
										{
											BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value),
											EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value),
											GoodId = int.Parse(attributes.GetNamedItem("GoodId").Value)
										};
										EventXmasXML.list_0.Add(eventXmasModel);
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

		// Token: 0x060000DA RID: 218 RVA: 0x00002116 File Offset: 0x00000316
		public EventXmasXML()
		{
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00002676 File Offset: 0x00000876
		// Note: this type is marked as 'beforefieldinit'.
		static EventXmasXML()
		{
		}

		// Token: 0x04000059 RID: 89
		private static List<EventXmasModel> list_0 = new List<EventXmasModel>();
	}
}
