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

    public class BattleRewardXML
    {
        public static List<BattleRewardModel> Rewards = new List<BattleRewardModel>();

        public static BattleRewardModel GetRewardType(BattleRewardType RewardType)
        {
            List<BattleRewardModel> rewards = Rewards;
            lock (rewards)
            {
                using (List<BattleRewardModel>.Enumerator enumerator = Rewards.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        BattleRewardModel current = enumerator.Current;
                        if (current.Type == RewardType)
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
            string path = "Data/BattleRewards.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Rewards.Count} Battle Rewards", LoggerType.Info, null);
        }

        public static void Reload()
        {
            Rewards.Clear();
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
                                    if ("Item".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        int num = int.Parse(attributes.GetNamedItem("Count").Value);
                                        BattleRewardModel model1 = new BattleRewardModel();
                                        model1.Type = ComDiv.ParseEnum<BattleRewardType>(attributes.GetNamedItem("Type").Value);
                                        model1.Percentage = int.Parse(attributes.GetNamedItem("Percentage").Value);
                                        model1.Enable = bool.Parse(attributes.GetNamedItem("Enable").Value);
                                        model1.Rewards = new int[num];
                                        BattleRewardModel model = model1;
                                        smethod_1(node2, model);
                                        Rewards.Add(model);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        CLogger.Print(exception.Message, LoggerType.Error, exception);
                    }
                }
                stream.Dispose();
                stream.Close();
            }
        }

        private static void smethod_1(XmlNode xmlNode_0, BattleRewardModel battleRewardModel_0)
        {
            for (XmlNode node = xmlNode_0.FirstChild; node != null; node = node.NextSibling)
            {
                if ("Rewards".Equals(node.Name))
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if ("Good".Equals(node2.Name))
                        {
                            XmlNamedNodeMap attributes = node2.Attributes;
                            BattleRewardItem item1 = new BattleRewardItem();
                            item1.Index = int.Parse(attributes.GetNamedItem("Index").Value);
                            item1.GoodId = int.Parse(attributes.GetNamedItem("Id").Value);
                            BattleRewardItem item = item1;
                            battleRewardModel_0.Rewards[item.Index] = item.GoodId;
                        }
                    }
                }
            }
        }
    }
}

