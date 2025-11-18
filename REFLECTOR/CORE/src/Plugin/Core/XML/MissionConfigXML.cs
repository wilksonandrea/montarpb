namespace Plugin.Core.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class MissionConfigXML
    {
        public static uint MissionPage1;
        public static uint MissionPage2;
        private static List<MissionStore> list_0 = new List<MissionStore>();

        public static MissionStore GetMission(int MissionId)
        {
            List<MissionStore> list = list_0;
            lock (list)
            {
                using (List<MissionStore>.Enumerator enumerator = list_0.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        MissionStore current = enumerator.Current;
                        if (current.Id == MissionId)
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
            string path = "Data/MissionConfig.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {list_0.Count} Mission Stores", LoggerType.Info, null);
        }

        public static void Reload()
        {
            MissionPage1 = 0;
            MissionPage2 = 0;
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
                                    if ("Mission".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        MissionStore store1 = new MissionStore();
                                        store1.Id = int.Parse(attributes.GetNamedItem("Id").Value);
                                        store1.ItemId = int.Parse(attributes.GetNamedItem("ItemId").Value);
                                        store1.Enable = bool.Parse(attributes.GetNamedItem("Enable").Value);
                                        MissionStore item = store1;
                                        uint num = (uint) (1 << (item.Id & 0x1f));
                                        int num2 = (int) Math.Ceiling((double) (((double) item.Id) / 32.0));
                                        if (item.Enable)
                                        {
                                            if (num2 == 1)
                                            {
                                                MissionPage1 += num;
                                            }
                                            else if (num2 == 2)
                                            {
                                                MissionPage2 += num;
                                            }
                                        }
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

