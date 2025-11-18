namespace Plugin.Core.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class CompetitiveXML
    {
        public static List<CompetitiveRank> Ranks = new List<CompetitiveRank>();

        public static CompetitiveRank GetRank(int Level)
        {
            List<CompetitiveRank> ranks = Ranks;
            lock (ranks)
            {
                using (List<CompetitiveRank>.Enumerator enumerator = Ranks.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        CompetitiveRank current = enumerator.Current;
                        if (current.Id == Level)
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
            string path = "Data/Competitions.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Ranks.Count} Competitive Ranks", LoggerType.Info, null);
        }

        public static void Reload()
        {
            Ranks.Clear();
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
                                    if ("Competitive".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        CompetitiveRank rank1 = new CompetitiveRank();
                                        rank1.Id = int.Parse(attributes.GetNamedItem("Id").Value);
                                        rank1.TourneyLevel = int.Parse(attributes.GetNamedItem("TourneyLevel").Value);
                                        rank1.Points = int.Parse(attributes.GetNamedItem("Points").Value);
                                        rank1.Name = attributes.GetNamedItem("Name").Value;
                                        CompetitiveRank item = rank1;
                                        Ranks.Add(item);
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

