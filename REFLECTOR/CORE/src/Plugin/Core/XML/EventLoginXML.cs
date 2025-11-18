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

    public class EventLoginXML
    {
        public static List<EventLoginModel> Events = new List<EventLoginModel>();

        public static EventLoginModel GetEvent(int EventId)
        {
            List<EventLoginModel> events = Events;
            lock (events)
            {
                using (List<EventLoginModel>.Enumerator enumerator = Events.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        EventLoginModel current = enumerator.Current;
                        if (current.Id == EventId)
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public static EventLoginModel GetRunningEvent()
        {
            List<EventLoginModel> events = Events;
            lock (events)
            {
                uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
                using (List<EventLoginModel>.Enumerator enumerator = Events.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        EventLoginModel current = enumerator.Current;
                        if ((current.BeginDate <= num) && (num < current.EndedDate))
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public static void Load()
        {
            string path = "Data/Events/Login.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Events.Count} Event Login", LoggerType.Info, null);
        }

        public static void Reload()
        {
            Events.Clear();
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
                                        EventLoginModel model1 = new EventLoginModel();
                                        model1.Id = int.Parse(attributes.GetNamedItem("Id").Value);
                                        model1.BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value);
                                        model1.EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value);
                                        model1.Name = attributes.GetNamedItem("Name").Value;
                                        model1.Description = attributes.GetNamedItem("Description").Value;
                                        model1.Period = bool.Parse(attributes.GetNamedItem("Period").Value);
                                        model1.Priority = bool.Parse(attributes.GetNamedItem("Priority").Value);
                                        model1.Goods = new List<int>();
                                        EventLoginModel model = model1;
                                        smethod_1(node2, model);
                                        Events.Add(model);
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

        private static void smethod_1(XmlNode xmlNode_0, EventLoginModel eventLoginModel_0)
        {
            for (XmlNode node = xmlNode_0.FirstChild; node != null; node = node.NextSibling)
            {
                if ("Rewards".Equals(node.Name))
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if ("Item".Equals(node2.Name))
                        {
                            int item = int.Parse(node2.Attributes.GetNamedItem("GoodId").Value);
                            if (eventLoginModel_0.Goods.Count > 4)
                            {
                                CLogger.Print("Max that can be listed on Login Event was 4!", LoggerType.Warning, null);
                                return;
                            }
                            eventLoginModel_0.Goods.Add(item);
                        }
                    }
                }
            }
        }
    }
}

