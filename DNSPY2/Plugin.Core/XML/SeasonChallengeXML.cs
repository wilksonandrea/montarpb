using Plugin.Core.Enums;
using Plugin.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
    public class SeasonChallengeXML
    {
        public static List<BattlePassModel> Seasons = new List<BattlePassModel>();
        public static void Load()
        {
            string Path = "Data/SeasonChallenges.xml";
            if (File.Exists(Path))
            {
                Parse(Path);
            }
            else
            {
                CLogger.Print($"File not found: {Path}", LoggerType.Warning);
            }
            CLogger.Print($"Plugin Loaded: {Seasons.Count} Season Challenge", LoggerType.Info);
        }
        public static void Reload()
        {
            Seasons.Clear();
            Load();
        }
        public static BattlePassModel GetSeasonPass(int SeasonId)
        {
            lock (Seasons)
            {
                foreach (BattlePassModel Season in Seasons)
                {
                    if (Season.Id == SeasonId)
                    {
                        return Season;
                    }
                }
                return null;
            }
        }
        public static BattlePassModel GetActiveSeasonPass()
        {
            lock (Seasons)
            {
                uint Date = uint.Parse(DateTime.Now.ToString("yyMMddHHmm"));
                foreach (BattlePassModel Event in Seasons)
                {
                    if (Event.BeginDate <= Date && Date < Event.EndedDate)
                    {
                        return Event;
                    }
                }
                return null;
            }
        }
        private static void Parse(string Path)
        {
            XmlDocument Document = new XmlDocument();
            using (FileStream Stream = new FileStream(Path, FileMode.Open))
            {
                if (Stream.Length == 0)
                {
                    CLogger.Print($"File is empty: {Path}", LoggerType.Warning);
                }
                else
                {
                    try
                    {
                        Document.Load(Stream);
                        for (XmlNode Node1 = Document.FirstChild; Node1 != null; Node1 = Node1.NextSibling)
                        {
                            if ("List".Equals(Node1.Name))
                            {
                                for (XmlNode Node2 = Node1.FirstChild; Node2 != null; Node2 = Node2.NextSibling)
                                {
                                    if ("Season".Equals(Node2.Name))
                                    {
                                        XmlNamedNodeMap xml = Node2.Attributes;
                                        BattlePassModel Pass = new BattlePassModel()
                                        {
                                            Id = int.Parse(xml.GetNamedItem("Id").Value),
                                            Name = xml.GetNamedItem("Name").Value,
                                            BeginDate = uint.Parse(xml.GetNamedItem("Begin").Value),
                                            EndedDate = uint.Parse(xml.GetNamedItem("Ended").Value),
                                            Enable = bool.Parse(xml.GetNamedItem("Enable").Value),
                                            Cards = new List<PassBoxModel>()
                                        };
                                        for (int i = 0; i < 99; i++)
                                        {
                                            Pass.Cards.Add(new PassBoxModel());
                                        }
                                        RewardXML(Node2, Pass);
                                        Pass.SetBoxCounts();
                                        Seasons.Add(Pass);
                                    }
                                }
                            }
                        }
                    }
                    catch (XmlException Ex)
                    {
                        CLogger.Print(Ex.Message, LoggerType.Error, Ex);
                    }
                }
                Stream.Dispose();
                Stream.Close();
            }
        }
        private static void RewardXML(XmlNode xmlNode, BattlePassModel Pass)
        {
            for (XmlNode xmlNode3 = xmlNode.FirstChild; xmlNode3 != null; xmlNode3 = xmlNode3.NextSibling)
            {
                if ("Rewards".Equals(xmlNode3.Name))
                {
                    for (XmlNode xmlNode4 = xmlNode3.FirstChild; xmlNode4 != null; xmlNode4 = xmlNode4.NextSibling)
                    {
                        if ("Card".Equals(xmlNode4.Name))
                        {
                            XmlNamedNodeMap xml4 = xmlNode4.Attributes;
                            int Level = int.Parse(xml4.GetNamedItem("Level").Value) - 1;
                            Pass.Cards[Level].Card = Level + 1;
                            Pass.Cards[Level].Normal.SetGoodId(int.Parse(xml4.GetNamedItem("Normal").Value));
                            Pass.Cards[Level].PremiumA.SetGoodId(int.Parse(xml4.GetNamedItem("PremiumA").Value));
                            Pass.Cards[Level].PremiumB.SetGoodId(int.Parse(xml4.GetNamedItem("PremiumB").Value));
                            Pass.Cards[Level].RequiredPoints = int.Parse(xml4.GetNamedItem("ReqPoints").Value);
                        }
                    }
                }
            }
        }
    }
}
