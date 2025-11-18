namespace Plugin.Core.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class PlayerRankXML
    {
        public static readonly List<RankModel> Ranks = new List<RankModel>();

        public static RankModel GetRank(int Id)
        {
            List<RankModel> ranks = Ranks;
            lock (ranks)
            {
                using (List<RankModel>.Enumerator enumerator = Ranks.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        RankModel current = enumerator.Current;
                        if (current.Id == Id)
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public static List<int> GetRewards(int RankId)
        {
            RankModel rank = GetRank(RankId);
            if (rank != null)
            {
                SortedList<int, List<int>> rewards = rank.Rewards;
                lock (rewards)
                {
                    List<int> list2;
                    if (rank.Rewards.TryGetValue(RankId, out list2))
                    {
                        return list2;
                    }
                }
            }
            return new List<int>();
        }

        public static void Load()
        {
            string path = "Data/Ranks/Player.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Ranks.Count} Player Ranks", LoggerType.Info, null);
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
                                    if ("Rank".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        RankModel model1 = new RankModel(int.Parse(attributes.GetNamedItem("Id").Value));
                                        model1.Title = attributes.GetNamedItem("Title").Value;
                                        model1.OnNextLevel = int.Parse(attributes.GetNamedItem("OnNextLevel").Value);
                                        model1.OnGoldUp = int.Parse(attributes.GetNamedItem("OnGoldUp").Value);
                                        model1.OnAllExp = int.Parse(attributes.GetNamedItem("OnAllExp").Value);
                                        model1.Rewards = new SortedList<int, List<int>>();
                                        RankModel model = model1;
                                        smethod_1(node2, model);
                                        Ranks.Add(model);
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

        private static void smethod_1(XmlNode xmlNode_0, RankModel rankModel_0)
        {
            for (XmlNode node = xmlNode_0.FirstChild; node != null; node = node.NextSibling)
            {
                if ("Rewards".Equals(node.Name))
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if ("Good".Equals(node2.Name))
                        {
                            int item = int.Parse(node2.Attributes.GetNamedItem("Id").Value);
                            SortedList<int, List<int>> rewards = rankModel_0.Rewards;
                            lock (rewards)
                            {
                                if (rankModel_0.Rewards.ContainsKey(rankModel_0.Id))
                                {
                                    rankModel_0.Rewards[rankModel_0.Id].Add(item);
                                }
                                else
                                {
                                    List<int> list1 = new List<int>();
                                    list1.Add(item);
                                    rankModel_0.Rewards.Add(rankModel_0.Id, list1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

