namespace Server.Match.Data.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Server.Match.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class ItemStatisticXML
    {
        public static List<ItemsStatistic> Stats = new List<ItemsStatistic>();

        public static ItemsStatistic GetItemStats(int ItemId)
        {
            List<ItemsStatistic> stats = Stats;
            lock (stats)
            {
                using (List<ItemsStatistic>.Enumerator enumerator = Stats.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        ItemsStatistic current = enumerator.Current;
                        if (current.Id == ItemId)
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
            string path = "Data/Match/ItemStatistics.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
        }

        public static void Reload()
        {
            Stats.Clear();
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
                                    if ("Statistic".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        ItemsStatistic statistic1 = new ItemsStatistic();
                                        statistic1.Id = int.Parse(attributes.GetNamedItem("Id").Value);
                                        statistic1.Name = attributes.GetNamedItem("Name").Value;
                                        statistic1.BulletLoaded = int.Parse(attributes.GetNamedItem("LoadedBullet").Value);
                                        statistic1.BulletTotal = int.Parse(attributes.GetNamedItem("TotalBullet").Value);
                                        statistic1.Damage = int.Parse(attributes.GetNamedItem("Damage").Value);
                                        statistic1.FireDelay = float.Parse(attributes.GetNamedItem("FireDelay").Value);
                                        statistic1.HelmetPenetrate = int.Parse(attributes.GetNamedItem("HelmetPenetrate").Value);
                                        statistic1.Range = float.Parse(attributes.GetNamedItem("Range").Value);
                                        ItemsStatistic item = statistic1;
                                        Stats.Add(item);
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

