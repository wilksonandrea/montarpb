namespace Plugin.Core.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Managers;
    using Plugin.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class GameRuleXML
    {
        public static List<TRuleModel> GameRules = new List<TRuleModel>();

        public static TRuleModel CheckTRuleByRoomName(string RoomName)
        {
            List<TRuleModel> gameRules = GameRules;
            lock (gameRules)
            {
                using (List<TRuleModel>.Enumerator enumerator = GameRules.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        TRuleModel current = enumerator.Current;
                        if (RoomName.ToLower().Contains(current.Name.ToLower()))
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public static bool IsBlocked(int ListId, int ItemId) => 
            ListId == ItemId;

        public static bool IsBlocked(int ListId, int ItemId, ref List<string> List, string Category)
        {
            if (ListId != ItemId)
            {
                return false;
            }
            List.Add(Category);
            return true;
        }

        public static void Load()
        {
            string path = "Data/ClassicMode.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {GameRules.Count} Game Rules", LoggerType.Info, null);
        }

        public static void Reload()
        {
            GameRules.Clear();
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
                                    if ("Rule".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        TRuleModel model1 = new TRuleModel();
                                        model1.Name = attributes.GetNamedItem("Name").Value;
                                        model1.BanIndexes = new List<int>();
                                        TRuleModel model = model1;
                                        smethod_1(node2, model);
                                        GameRules.Add(model);
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

        private static void smethod_1(XmlNode xmlNode_0, TRuleModel truleModel_0)
        {
            for (XmlNode node = xmlNode_0.FirstChild; node != null; node = node.NextSibling)
            {
                if ("Extensions".Equals(node.Name))
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if ("Ban".Equals(node2.Name))
                        {
                            ShopManager.IsBlocked(node2.Attributes.GetNamedItem("Filter").Value, truleModel_0.BanIndexes);
                        }
                    }
                }
            }
        }
    }
}

