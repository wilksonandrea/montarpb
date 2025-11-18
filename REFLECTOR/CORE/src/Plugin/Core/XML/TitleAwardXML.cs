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

    public class TitleAwardXML
    {
        public static List<TitleAward> Awards = new List<TitleAward>();

        public static bool Contains(int TitleId, int ItemId)
        {
            bool flag;
            if (ItemId == 0)
            {
                return false;
            }
            using (List<TitleAward>.Enumerator enumerator = Awards.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        TitleAward current = enumerator.Current;
                        if ((current.Id != TitleId) || (current.Item.Id != ItemId))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public static List<ItemsModel> GetAwards(int titleId)
        {
            List<ItemsModel> list = new List<ItemsModel>();
            List<TitleAward> awards = Awards;
            lock (awards)
            {
                foreach (TitleAward award in Awards)
                {
                    if (award.Id == titleId)
                    {
                        list.Add(award.Item);
                    }
                }
            }
            return list;
        }

        public static void Load()
        {
            string path = "Data/Titles/Rewards.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Awards.Count} Title Awards", LoggerType.Info, null);
        }

        public static void Reload()
        {
            Awards.Clear();
            Load();
        }

        private static void smethod_0(string string_0)
        {
            XmlDocument document = new XmlDocument();
            using (FileStream stream = new FileStream(string_0, FileMode.Open))
            {
                if (stream.Length > 0L)
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
                                    if ("Title".Equals(node2.Name))
                                    {
                                        smethod_1(node2, int.Parse(node2.Attributes.GetNamedItem("Id").Value));
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

        private static void smethod_1(XmlNode xmlNode_0, int int_0)
        {
            for (XmlNode node = xmlNode_0.FirstChild; node != null; node = node.NextSibling)
            {
                if ("Rewards".Equals(node.Name))
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if ("Item".Equals(node2.Name))
                        {
                            XmlNamedNodeMap attributes = node2.Attributes;
                            TitleAward award1 = new TitleAward();
                            award1.Id = int_0;
                            TitleAward item = award1;
                            if (item != null)
                            {
                                int num = int.Parse(attributes.GetNamedItem("Id").Value);
                                ItemsModel model1 = new ItemsModel(num);
                                model1.Name = attributes.GetNamedItem("Name").Value;
                                model1.Count = uint.Parse(attributes.GetNamedItem("Count").Value);
                                model1.Equip = (ItemEquipType) int.Parse(attributes.GetNamedItem("Equip").Value);
                                ItemsModel model = model1;
                                if (model.Equip == ItemEquipType.Permanent)
                                {
                                    model.ObjectId = ComDiv.ValidateStockId(num);
                                }
                                item.Item = model;
                                Awards.Add(item);
                            }
                        }
                    }
                }
            }
        }
    }
}

