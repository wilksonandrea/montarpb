namespace Plugin.Core.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class SeasonChallengeXML
    {
        public static List<BattlePassModel> Seasons = new List<BattlePassModel>();

        public static BattlePassModel GetActiveSeasonPass()
        {
            List<BattlePassModel> seasons = Seasons;
            lock (seasons)
            {
                uint num = uint.Parse(DateTime.Now.ToString("yyMMddHHmm"));
                using (List<BattlePassModel>.Enumerator enumerator = Seasons.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        BattlePassModel current = enumerator.Current;
                        if ((current.BeginDate <= num) && (num < current.EndedDate))
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public static BattlePassModel GetSeasonPass(int SeasonId)
        {
            List<BattlePassModel> seasons = Seasons;
            lock (seasons)
            {
                using (List<BattlePassModel>.Enumerator enumerator = Seasons.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        BattlePassModel current = enumerator.Current;
                        if (current.Id == SeasonId)
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
            string path = "Data/SeasonChallenges.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Seasons.Count} Season Challenge", LoggerType.Info, null);
        }

        public static void Reload()
        {
            Seasons.Clear();
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
                                    if ("Season".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        BattlePassModel model1 = new BattlePassModel();
                                        model1.Id = int.Parse(attributes.GetNamedItem("Id").Value);
                                        model1.MaxDailyPoints = int.Parse(attributes.GetNamedItem("MaxDailyPoints").Value);
                                        model1.Name = attributes.GetNamedItem("Name").Value;
                                        model1.BeginDate = uint.Parse(attributes.GetNamedItem("Begin").Value);
                                        model1.EndedDate = uint.Parse(attributes.GetNamedItem("Ended").Value);
                                        model1.Enable = bool.Parse(attributes.GetNamedItem("Enable").Value);
                                        model1.Cards = new List<PassBoxModel>();
                                        BattlePassModel model = model1;
                                        int num = 0;
                                        while (true)
                                        {
                                            if (num >= 0x63)
                                            {
                                                smethod_1(node2, model);
                                                model.SetBoxCounts();
                                                Seasons.Add(model);
                                                break;
                                            }
                                            model.Cards.Add(new PassBoxModel());
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

        private static void smethod_1(XmlNode xmlNode_0, BattlePassModel battlePassModel_0)
        {
            for (XmlNode node = xmlNode_0.FirstChild; node != null; node = node.NextSibling)
            {
                if ("Rewards".Equals(node.Name))
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if ("Card".Equals(node2.Name))
                        {
                            XmlNamedNodeMap attributes = node2.Attributes;
                            int num = int.Parse(attributes.GetNamedItem("Level").Value) - 1;
                            battlePassModel_0.Cards[num].Card = num + 1;
                            battlePassModel_0.Cards[num].Normal.SetGoodId(int.Parse(attributes.GetNamedItem("Normal").Value));
                            battlePassModel_0.Cards[num].PremiumA.SetGoodId(int.Parse(attributes.GetNamedItem("PremiumA").Value));
                            battlePassModel_0.Cards[num].PremiumB.SetGoodId(int.Parse(attributes.GetNamedItem("PremiumB").Value));
                            battlePassModel_0.Cards[num].RequiredPoints = int.Parse(attributes.GetNamedItem("ReqPoints").Value);
                        }
                    }
                }
            }
        }
    }
}

