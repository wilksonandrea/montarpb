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
	public class EventXmasXML
	{
		private static List<EventXmasModel> list_0;

		static EventXmasXML()
		{
			EventXmasXML.list_0 = new List<EventXmasModel>();
		}

		public EventXmasXML()
		{
		}

		public static EventXmasModel GetRunningEvent()
		{
			EventXmasModel eventXmasModel;
			uint uInt32 = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			List<EventXmasModel>.Enumerator enumerator = EventXmasXML.list_0.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					EventXmasModel current = enumerator.Current;
					if (current.BeginDate > uInt32 || uInt32 >= current.EndedDate)
					{
						continue;
					}
					eventXmasModel = current;
					return eventXmasModel;
				}
				return null;
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return eventXmasModel;
		}

		public static void Load()
		{
			string str = "Data/Events/Xmas.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				EventXmasXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Event X-Mas", EventXmasXML.list_0.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			EventXmasXML.list_0.Clear();
			EventXmasXML.Load();
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
										EventXmasModel eventXmasModel = new EventXmasModel()
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