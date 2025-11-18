namespace Plugin.Core.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Models.Map;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public static class SystemMapXML
    {
        public static List<MapRule> Rules = new List<MapRule>();
        public static List<MapMatch> Matches = new List<MapMatch>();

        public static MapMatch GetMapLimit(int MapId, int RuleId)
        {
            List<MapRule> rules = Rules;
            lock (rules)
            {
                using (List<MapMatch>.Enumerator enumerator = Matches.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        MapMatch current = enumerator.Current;
                        if ((current.Id == MapId) && (GetMapRule(current.Mode).Rule == RuleId))
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public static MapRule GetMapRule(int RuleId)
        {
            List<MapRule> rules = Rules;
            lock (rules)
            {
                using (List<MapRule>.Enumerator enumerator = Rules.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        MapRule current = enumerator.Current;
                        if (current.Id == RuleId)
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
            smethod_0();
            smethod_1();
        }

        public static void Reload()
        {
            Rules.Clear();
            Matches.Clear();
            Load();
        }

        private static void smethod_0()
        {
            string path = "Data/Maps/Rules.xml";
            if (File.Exists(path))
            {
                smethod_2(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Rules.Count} Map Rules", LoggerType.Info, null);
        }

        private static void smethod_1()
        {
            string path = "Data/Maps/Matches.xml";
            if (File.Exists(path))
            {
                smethod_3(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Matches.Count} Map Matches", LoggerType.Info, null);
        }

        private static void smethod_2(string string_0)
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
                                    if ("Mode".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        MapRule rule1 = new MapRule();
                                        rule1.Id = int.Parse(attributes.GetNamedItem("Id").Value);
                                        rule1.Rule = byte.Parse(attributes.GetNamedItem("Rule").Value);
                                        rule1.StageOptions = byte.Parse(attributes.GetNamedItem("StageOptions").Value);
                                        rule1.Conditions = byte.Parse(attributes.GetNamedItem("Conditions").Value);
                                        rule1.Name = attributes.GetNamedItem("Name").Value;
                                        MapRule item = rule1;
                                        Rules.Add(item);
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

        private static void smethod_3(string string_0)
        {
            XmlDocument document = new XmlDocument();
            using (FileStream stream = new FileStream(string_0, FileMode.Open))
            {
                if (stream.Length != 0)
                {
                    try
                    {
                        document.Load(stream);
                        XmlNode firstChild = document.FirstChild;
                        while (true)
                        {
                            if (firstChild == null)
                            {
                                break;
                            }
                            if ("List".Equals(firstChild.Name))
                            {
                                XmlNode nextSibling = firstChild.FirstChild;
                                while (true)
                                {
                                    if (nextSibling == null)
                                    {
                                        break;
                                    }
                                    if ("Match".Equals(nextSibling.Name))
                                    {
                                        XmlAttributeCollection attributes = nextSibling.Attributes;
                                        int num = int.Parse(attributes.GetNamedItem("Rule").Value);
                                        string str = attributes.GetNamedItem("Mode").Value;
                                        if ((num == 0) || string.IsNullOrEmpty(str))
                                        {
                                            CLogger.Print($"Invalid Mode: {num}; Mode Name: {str}; Please check and try again!", LoggerType.Warning, null);
                                            return;
                                        }
                                        else
                                        {
                                            smethod_4(nextSibling, num);
                                        }
                                    }
                                    nextSibling = nextSibling.NextSibling;
                                }
                            }
                            firstChild = firstChild.NextSibling;
                        }
                    }
                    catch (XmlException exception)
                    {
                        CLogger.Print(exception.Message, LoggerType.Error, exception);
                    }
                }
                else
                {
                    CLogger.Print("File is empty: " + string_0, LoggerType.Warning, null);
                }
                stream.Dispose();
                stream.Close();
            }
        }

        private static void smethod_4(XmlNode xmlNode_0, int int_0)
        {
            for (XmlNode node = xmlNode_0.FirstChild; node != null; node = node.NextSibling)
            {
                if ("Count".Equals(node.Name))
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if ("Map".Equals(node2.Name))
                        {
                            XmlNamedNodeMap attributes = node2.Attributes;
                            MapMatch match1 = new MapMatch(int_0);
                            match1.Id = byte.Parse(attributes.GetNamedItem("Id").Value);
                            match1.Limit = byte.Parse(attributes.GetNamedItem("Limit").Value);
                            match1.Tag = byte.Parse(attributes.GetNamedItem("Tag").Value);
                            match1.Name = attributes.GetNamedItem("Name").Value;
                            MapMatch item = match1;
                            Matches.Add(item);
                        }
                    }
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int limit)
        {
            Class3<T> class2 = new Class3<T> {
                int_0 = limit
            };
            Func<T, int, Class0<T, int>> selector = Class2<T>.<>9__2_0;
            if (Class2<T>.<>9__2_0 == null)
            {
                Func<T, int, Class0<T, int>> local1 = Class2<T>.<>9__2_0;
                selector = Class2<T>.<>9__2_0 = new Func<T, int, Class0<T, int>>(Class2<T>.<>9.method_0);
            }
            Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>> func2 = Class2<T>.<>9__2_2;
            if (Class2<T>.<>9__2_2 == null)
            {
                Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>> local2 = Class2<T>.<>9__2_2;
                func2 = Class2<T>.<>9__2_2 = new Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>>(Class2<T>.<>9.method_1);
            }
            return list.Select<T, Class0<T, int>>(selector).GroupBy<Class0<T, int>, int>(new Func<Class0<T, int>, int>(class2.method_0)).Select<IGrouping<int, Class0<T, int>>, IEnumerable<T>>(func2);
        }

        [Serializable, CompilerGenerated]
        private sealed class Class2<T>
        {
            public static readonly SystemMapXML.Class2<T> <>9;
            public static Func<T, int, Class0<T, int>> <>9__2_0;
            public static Func<Class0<T, int>, T> <>9__2_3;
            public static Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>> <>9__2_2;

            static Class2()
            {
                SystemMapXML.Class2<T>.<>9 = new SystemMapXML.Class2<T>();
            }

            internal Class0<T, int> method_0(T gparam_0, int int_0) => 
                new Class0<T, int>(gparam_0, int_0);

            internal IEnumerable<T> method_1(IGrouping<int, Class0<T, int>> igrouping_0)
            {
                Func<Class0<T, int>, T> selector = SystemMapXML.Class2<T>.<>9__2_3;
                if (SystemMapXML.Class2<T>.<>9__2_3 == null)
                {
                    Func<Class0<T, int>, T> local1 = SystemMapXML.Class2<T>.<>9__2_3;
                    selector = SystemMapXML.Class2<T>.<>9__2_3 = new Func<Class0<T, int>, T>(SystemMapXML.Class2<T>.<>9.method_2);
                }
                return igrouping_0.Select<Class0<T, int>, T>(selector);
            }

            internal T method_2(Class0<T, int> class0_0) => 
                class0_0.item;
        }

        [CompilerGenerated]
        private sealed class Class3<T>
        {
            public int int_0;

            internal int method_0(Class0<T, int> class0_0) => 
                class0_0.inx / this.int_0;
        }
    }
}

