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

    public class EventBoostXML
    {
        public static List<EventBoostModel> Events = new List<EventBoostModel>();

        public static bool EventIsValid(EventBoostModel Event, PortalBoostEvent BoostType, int BoostValue) => 
            (Event != null) && ((Event.BoostType == BoostType) || (Event.BoostValue == BoostValue));

        public static EventBoostModel GetEvent(int EventId)
        {
            List<EventBoostModel> events = Events;
            lock (events)
            {
                using (List<EventBoostModel>.Enumerator enumerator = Events.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        EventBoostModel current = enumerator.Current;
                        if (current.Id == EventId)
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public static EventBoostModel GetRunningEvent()
        {
            List<EventBoostModel> events = Events;
            lock (events)
            {
                uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
                using (List<EventBoostModel>.Enumerator enumerator = Events.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        EventBoostModel current = enumerator.Current;
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
            string path = "Data/Events/Boost.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Events.Count} Event Boost Bonus", LoggerType.Info, null);
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
                                        EventBoostModel model1 = new EventBoostModel();
                                        model1.Id = int.Parse(attributes.GetNamedItem("Id").Value);
                                        model1.BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value);
                                        model1.EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value);
                                        model1.BoostType = ComDiv.ParseEnum<PortalBoostEvent>(attributes.GetNamedItem("BoostType").Value);
                                        model1.BoostValue = int.Parse(attributes.GetNamedItem("BoostValue").Value);
                                        model1.BonusExp = int.Parse(attributes.GetNamedItem("BonusExp").Value);
                                        model1.BonusGold = int.Parse(attributes.GetNamedItem("BonusGold").Value);
                                        model1.Percent = int.Parse(attributes.GetNamedItem("Percent").Value);
                                        model1.Name = attributes.GetNamedItem("Name").Value;
                                        model1.Description = attributes.GetNamedItem("Description").Value;
                                        model1.Period = bool.Parse(attributes.GetNamedItem("Period").Value);
                                        model1.Priority = bool.Parse(attributes.GetNamedItem("Priority").Value);
                                        EventBoostModel item = model1;
                                        Events.Add(item);
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

