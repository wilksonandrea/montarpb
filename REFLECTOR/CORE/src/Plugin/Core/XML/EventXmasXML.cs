namespace Plugin.Core.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class EventXmasXML
    {
        private static List<EventXmasModel> list_0 = new List<EventXmasModel>();

        public static EventXmasModel GetRunningEvent()
        {
            EventXmasModel model2;
            uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
            using (List<EventXmasModel>.Enumerator enumerator = list_0.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        EventXmasModel current = enumerator.Current;
                        if ((current.BeginDate > num) || (num >= current.EndedDate))
                        {
                            continue;
                        }
                        model2 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return model2;
        }

        public static void Load()
        {
            string path = "Data/Events/Xmas.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {list_0.Count} Event X-Mas", LoggerType.Info, null);
        }

        public static void Reload()
        {
            list_0.Clear();
            Load();
        }

        private static void smethod_0(string string_0)
        {
            XmlDocument document = new XmlDocument();
            using (FileStream stream = new FileStream(string_0, FileMode.Open))
            {
                if (stream.Length == 0)
                {
                    CLogger.Print("File is empty: " + string_0, LoggerType.Warning, null);
                }
                else
                {
                    try
                    {
                        document.Load(stream);
                        for (XmlNode node = document.FirstChild; node != null; node = node.NextSibling)
                        {
                            if ("List".Equals(node.Name))
                            {
                                for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                                {
                                    if ("Event".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        EventXmasModel model1 = new EventXmasModel();
                                        model1.BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value);
                                        model1.EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value);
                                        model1.GoodId = int.Parse(attributes.GetNamedItem("GoodId").Value);
                                        EventXmasModel item = model1;
                                        list_0.Add(item);
                                    }
                                }
                            }
                        }
                    }
                    catch (XmlException exception)
                    {
                        CLogger.Print(exception.Message, LoggerType.Error, exception);
                    }
                }
                stream.Dispose();
                stream.Close();
            }
        }
    }
}

