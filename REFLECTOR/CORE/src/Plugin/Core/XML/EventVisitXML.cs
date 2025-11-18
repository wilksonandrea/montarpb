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

    public class EventVisitXML
    {
        public static readonly List<EventVisitModel> Events = new List<EventVisitModel>();

        public static EventVisitModel GetEvent(int EventId)
        {
            List<EventVisitModel> events = Events;
            lock (events)
            {
                using (List<EventVisitModel>.Enumerator enumerator = Events.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        EventVisitModel current = enumerator.Current;
                        if (current.Id == EventId)
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public static EventVisitModel GetRunningEvent()
        {
            List<EventVisitModel> events = Events;
            lock (events)
            {
                uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
                using (List<EventVisitModel>.Enumerator enumerator = Events.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        EventVisitModel current = enumerator.Current;
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
            string path = "Data/Events/Visit.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Events.Count} Event Visit", LoggerType.Info, null);
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
                string[] cOLUMNS = new string[] { "last_visit_check_day", "last_visit_seq_type", "last_visit_date" };
                object[] vALUES = new object[] { Event.LastVisitCheckDay, Event.LastVisitSeqType, Event.LastVisitDate };
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
                                        EventVisitModel model1 = new EventVisitModel();
                                        model1.Id = int.Parse(attributes.GetNamedItem("Id").Value);
                                        model1.BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value);
                                        model1.EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value);
                                        model1.Title = attributes.GetNamedItem("Title").Value;
                                        model1.Checks = int.Parse(attributes.GetNamedItem("Days").Value);
                                        model1.Boxes = new List<VisitBoxModel>();
                                        EventVisitModel model = model1;
                                        int num = 0;
                                        while (true)
                                        {
                                            if (num >= 0x1f)
                                            {
                                                smethod_1(node2, model);
                                                model.SetBoxCounts();
                                                Events.Add(model);
                                                break;
                                            }
                                            model.Boxes.Add(new VisitBoxModel());
                                            num++;
                                        }
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

        private static void smethod_1(XmlNode xmlNode_0, EventVisitModel eventVisitModel_0)
        {
            for (XmlNode node = xmlNode_0.FirstChild; node != null; node = node.NextSibling)
            {
                if ("Rewards".Equals(node.Name))
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if ("Box".Equals(node2.Name))
                        {
                            XmlNamedNodeMap attributes = node2.Attributes;
                            int num = int.Parse(attributes.GetNamedItem("Day").Value) - 1;
                            eventVisitModel_0.Boxes[num].Reward1.SetGoodId(int.Parse(attributes.GetNamedItem("GoodId1").Value));
                            eventVisitModel_0.Boxes[num].Reward2.SetGoodId(int.Parse(attributes.GetNamedItem("GoodId2").Value));
                            eventVisitModel_0.Boxes[num].IsBothReward = bool.Parse(attributes.GetNamedItem("Both").Value);
                        }
                    }
                }
            }
        }
    }
}

