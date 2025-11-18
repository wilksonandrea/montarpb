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

    public class EventPlaytimeXML
    {
        public static readonly List<EventPlaytimeModel> Events = new List<EventPlaytimeModel>();

        public static EventPlaytimeModel GetEvent(int EventId)
        {
            List<EventPlaytimeModel> events = Events;
            lock (events)
            {
                using (List<EventPlaytimeModel>.Enumerator enumerator = Events.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        EventPlaytimeModel current = enumerator.Current;
                        if (current.Id == EventId)
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public static EventPlaytimeModel GetRunningEvent()
        {
            List<EventPlaytimeModel> events = Events;
            lock (events)
            {
                uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
                using (List<EventPlaytimeModel>.Enumerator enumerator = Events.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        EventPlaytimeModel current = enumerator.Current;
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
            string path = "Data/Events/Play.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Events.Count} Event Playtime", LoggerType.Info, null);
        }

        public static void Reload()
        {
            Events.Clear();
            Load();
        }

        public static void ResetPlayerEvent(long PlayerId, PlayerEvent Event)
        {
            if (PlayerId != 0)
            {
                string[] cOLUMNS = new string[] { "last_playtime_value", "last_playtime_finish", "last_playtime_date" };
                object[] vALUES = new object[] { Event.LastPlaytimeValue, Event.LastPlaytimeFinish, Event.LastPlaytimeDate };
                ComDiv.UpdateDB("player_events", "owner_id", PlayerId, cOLUMNS, vALUES);
            }
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
                                        EventPlaytimeModel model1 = new EventPlaytimeModel();
                                        model1.Id = int.Parse(attributes.GetNamedItem("Id").Value);
                                        model1.BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value);
                                        model1.EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value);
                                        model1.Name = attributes.GetNamedItem("Name").Value;
                                        model1.Description = attributes.GetNamedItem("Description").Value;
                                        model1.Period = bool.Parse(attributes.GetNamedItem("Period").Value);
                                        model1.Priority = bool.Parse(attributes.GetNamedItem("Priority").Value);
                                        EventPlaytimeModel model = model1;
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

        private static void smethod_1(XmlNode xmlNode_0, EventPlaytimeModel eventPlaytimeModel_0)
        {
            for (XmlNode node = xmlNode_0.FirstChild; node != null; node = node.NextSibling)
            {
                if ("Minutes".Equals(node.Name))
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if ("Time".Equals(node2.Name))
                        {
                            XmlNamedNodeMap attributes = node2.Attributes;
                            int num = int.Parse(attributes.GetNamedItem("Index").Value);
                            if (num == 1)
                            {
                                eventPlaytimeModel_0.Minutes1 = int.Parse(attributes.GetNamedItem("Play").Value);
                                eventPlaytimeModel_0.Goods1 = smethod_2(node2);
                            }
                            else if (num == 2)
                            {
                                eventPlaytimeModel_0.Minutes2 = int.Parse(attributes.GetNamedItem("Play").Value);
                                eventPlaytimeModel_0.Goods2 = smethod_2(node2);
                            }
                            else if (num == 3)
                            {
                                eventPlaytimeModel_0.Minutes3 = int.Parse(attributes.GetNamedItem("Play").Value);
                                eventPlaytimeModel_0.Goods3 = smethod_2(node2);
                            }
                        }
                    }
                }
            }
        }

        private static List<int> smethod_2(XmlNode xmlNode_0)
        {
            List<int> list = new List<int>();
            for (XmlNode node = xmlNode_0.FirstChild; node != null; node = node.NextSibling)
            {
                if ("Reward".Equals(node.Name))
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if ("Goods".Equals(node2.Name))
                        {
                            list.Add(int.Parse(node2.Attributes.GetNamedItem("Id").Value));
                        }
                    }
                }
            }
            return list;
        }
    }
}

