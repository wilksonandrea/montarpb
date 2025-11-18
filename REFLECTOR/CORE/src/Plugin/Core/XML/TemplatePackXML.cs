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

    public class TemplatePackXML
    {
        public static List<ItemsModel> Basics = new List<ItemsModel>();
        public static List<ItemsModel> Awards = new List<ItemsModel>();
        public static List<PCCafeModel> Cafes = new List<PCCafeModel>();

        public static PCCafeModel GetPCCafe(CafeEnum Type)
        {
            List<PCCafeModel> cafes = Cafes;
            lock (cafes)
            {
                using (List<PCCafeModel>.Enumerator enumerator = Cafes.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        PCCafeModel current = enumerator.Current;
                        if (current.Type == Type)
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public static List<ItemsModel> GetPCCafeRewards(CafeEnum Type)
        {
            PCCafeModel pCCafe = GetPCCafe(Type);
            if (pCCafe != null)
            {
                SortedList<CafeEnum, List<ItemsModel>> rewards = pCCafe.Rewards;
                lock (rewards)
                {
                    List<ItemsModel> list2;
                    if (pCCafe.Rewards.TryGetValue(Type, out list2))
                    {
                        return list2;
                    }
                }
            }
            return new List<ItemsModel>();
        }

        public static void Load()
        {
            smethod_0();
            smethod_1();
            smethod_2();
        }

        public static void Reload()
        {
            Basics.Clear();
            Awards.Clear();
            Cafes.Clear();
            Load();
        }

        private static void smethod_0()
        {
            string path = "Data/Temps/Basic.xml";
            if (File.Exists(path))
            {
                smethod_3(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Basics.Count} Basic Templates", LoggerType.Info, null);
        }

        private static void smethod_1()
        {
            string path = "Data/Temps/CafePC.xml";
            if (File.Exists(path))
            {
                smethod_4(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Cafes.Count} PC Cafes", LoggerType.Info, null);
        }

        private static void smethod_2()
        {
            string path = "Data/Temps/Award.xml";
            if (File.Exists(path))
            {
                smethod_7(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Awards.Count} Award Templates", LoggerType.Info, null);
        }

        private static void smethod_3(string string_0)
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
                                        int num = int.Parse(attributes.GetNamedItem("Id").Value);
                                        ItemsModel model1 = new ItemsModel(num);
                                        model1.ObjectId = ComDiv.ValidateStockId(num);
                                        model1.Name = attributes.GetNamedItem("Name").Value;
                                        model1.Count = 1;
                                        model1.Equip = ItemEquipType.Permanent;
                                        ItemsModel item = model1;
                                        Basics.Add(item);
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

        private static void smethod_4(string string_0)
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
                                    if ("Cafe".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        PCCafeModel model1 = new PCCafeModel(ComDiv.ParseEnum<CafeEnum>(attributes.GetNamedItem("Type").Value));
                                        model1.ExpUp = int.Parse(attributes.GetNamedItem("ExpUp").Value);
                                        model1.PointUp = int.Parse(attributes.GetNamedItem("PointUp").Value);
                                        model1.Rewards = new SortedList<CafeEnum, List<ItemsModel>>();
                                        PCCafeModel model = model1;
                                        smethod_5(node2, model);
                                        Cafes.Add(model);
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

        private static void smethod_5(XmlNode xmlNode_0, PCCafeModel pccafeModel_0)
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
                            int num = int.Parse(attributes.GetNamedItem("Id").Value);
                            ItemsModel model1 = new ItemsModel(num);
                            model1.ObjectId = ComDiv.ValidateStockId(num);
                            model1.Name = attributes.GetNamedItem("Name").Value;
                            model1.Count = 1;
                            model1.Equip = ItemEquipType.CafePC;
                            ItemsModel model = model1;
                            smethod_6(pccafeModel_0, model);
                        }
                    }
                }
            }
        }

        private static void smethod_6(PCCafeModel pccafeModel_0, ItemsModel itemsModel_0)
        {
            SortedList<CafeEnum, List<ItemsModel>> rewards = pccafeModel_0.Rewards;
            lock (rewards)
            {
                if (pccafeModel_0.Rewards.ContainsKey(pccafeModel_0.Type))
                {
                    pccafeModel_0.Rewards[pccafeModel_0.Type].Add(itemsModel_0);
                }
                else
                {
                    List<ItemsModel> list1 = new List<ItemsModel>();
                    list1.Add(itemsModel_0);
                    pccafeModel_0.Rewards.Add(pccafeModel_0.Type, list1);
                }
            }
        }

        private static void smethod_7(string string_0)
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
                                        ItemsModel model1 = new ItemsModel(int.Parse(attributes.GetNamedItem("Id").Value));
                                        model1.Name = attributes.GetNamedItem("Name").Value;
                                        model1.Count = uint.Parse(attributes.GetNamedItem("Count").Value);
                                        model1.Equip = ItemEquipType.Durable;
                                        ItemsModel item = model1;
                                        Awards.Add(item);
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

