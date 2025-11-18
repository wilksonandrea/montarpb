namespace Plugin.Core.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class MissionAwardXML
    {
        private static List<MissionAwards> list_0 = new List<MissionAwards>();

        public static MissionAwards GetAward(int MissionId)
        {
            List<MissionAwards> list = list_0;
            lock (list)
            {
                using (List<MissionAwards>.Enumerator enumerator = list_0.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        MissionAwards current = enumerator.Current;
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
            string path = "Data/Cards/MissionAwards.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {list_0.Count} Mission Awards", LoggerType.Info, null);
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
                                    if ("Mission".Equals(node2.Name))
                                    {
                                        XmlAttributeCollection attributes = node2.Attributes;
                                        int num = int.Parse(attributes.GetNamedItem("Id").Value);
                                        list_0.Add(new MissionAwards(num, int.Parse(attributes.GetNamedItem("MasterMedal").Value), int.Parse(attributes.GetNamedItem("Exp").Value), int.Parse(attributes.GetNamedItem("Point").Value)));
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

