namespace Plugin.Core.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Utility;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class PermissionXML
    {
        private static readonly SortedList<int, string> sortedList_0 = new SortedList<int, string>();
        private static readonly SortedList<AccessLevel, List<string>> sortedList_1 = new SortedList<AccessLevel, List<string>>();
        private static readonly SortedList<int, int> sortedList_2 = new SortedList<int, int>();

        public static int GetFakeRank(int Level)
        {
            SortedList<int, int> list = sortedList_2;
            lock (list)
            {
                return (!sortedList_2.ContainsKey(Level) ? -1 : sortedList_2[Level]);
            }
        }

        public static bool HavePermission(string Permission, AccessLevel Level) => 
            sortedList_1.ContainsKey(Level) && sortedList_1[Level].Contains(Permission);

        public static void Load()
        {
            smethod_0();
            smethod_1();
            smethod_2();
        }

        public static void Reload()
        {
            sortedList_0.Clear();
            sortedList_1.Clear();
            sortedList_2.Clear();
            Load();
        }

        private static void smethod_0()
        {
            string path = "Data/Access/Permission.xml";
            if (File.Exists(path))
            {
                smethod_3(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {sortedList_0.Count} Permissions", LoggerType.Info, null);
        }

        private static void smethod_1()
        {
            string path = "Data/Access/PermissionLevel.xml";
            if (File.Exists(path))
            {
                smethod_4(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {sortedList_2.Count} Permission Ranks", LoggerType.Info, null);
        }

        private static void smethod_2()
        {
            string path = "Data/Access/PermissionRight.xml";
            if (File.Exists(path))
            {
                smethod_5(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {sortedList_1.Count} Level Permission", LoggerType.Info, null);
        }

        private static void smethod_3(string string_0)
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
                                    if ("Permission".Equals(node2.Name))
                                    {
                                        XmlAttributeCollection attributes = node2.Attributes;
                                        int key = int.Parse(attributes.GetNamedItem("Key").Value);
                                        string str = attributes.GetNamedItem("Name").Value;
                                        string text1 = attributes.GetNamedItem("Description").Value;
                                        if (!sortedList_0.ContainsKey(key))
                                        {
                                            sortedList_0.Add(key, str);
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

        private static void smethod_4(string string_0)
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
                                    if ("Permission".Equals(node2.Name))
                                    {
                                        XmlAttributeCollection attributes = node2.Attributes;
                                        int key = int.Parse(attributes.GetNamedItem("Key").Value);
                                        string text1 = attributes.GetNamedItem("Name").Value;
                                        string text2 = attributes.GetNamedItem("Description").Value;
                                        sortedList_2.Add(key, int.Parse(attributes.GetNamedItem("FakeRank").Value));
                                        sortedList_1.Add((AccessLevel) key, new List<string>());
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

        private static void smethod_5(string string_0)
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
                                    if ("Access".Equals(node2.Name))
                                    {
                                        smethod_6(node2, ComDiv.ParseEnum<AccessLevel>(node2.Attributes.GetNamedItem("Level").Value));
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

        private static void smethod_6(XmlNode xmlNode_0, AccessLevel accessLevel_0)
        {
            for (XmlNode node = xmlNode_0.FirstChild; node != null; node = node.NextSibling)
            {
                if ("Permission".Equals(node.Name))
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if ("Right".Equals(node2.Name))
                        {
                            int key = int.Parse(node2.Attributes.GetNamedItem("LevelKey").Value);
                            if (sortedList_0.ContainsKey(key))
                            {
                                sortedList_1[accessLevel_0].Add(sortedList_0[key]);
                            }
                        }
                    }
                }
            }
        }
    }
}

