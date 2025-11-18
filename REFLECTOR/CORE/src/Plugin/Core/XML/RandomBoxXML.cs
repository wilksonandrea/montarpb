namespace Plugin.Core.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class RandomBoxXML
    {
        public static SortedList<int, RandomBoxModel> RBoxes = new SortedList<int, RandomBoxModel>();

        public static bool ContainsBox(int Id) => 
            RBoxes.ContainsKey(Id);

        public static RandomBoxModel GetBox(int Id)
        {
            try
            {
                return RBoxes[Id];
            }
            catch
            {
                return null;
            }
        }

        public static void Load()
        {
            DirectoryInfo info = new DirectoryInfo(Directory.GetCurrentDirectory() + @"\Data\RBoxes");
            if (info.Exists)
            {
                foreach (FileInfo info2 in info.GetFiles())
                {
                    try
                    {
                        smethod_0(int.Parse(info2.Name.Substring(0, info2.Name.Length - 4)));
                    }
                    catch (Exception exception)
                    {
                        CLogger.Print(exception.Message, LoggerType.Error, exception);
                    }
                }
                CLogger.Print($"Plugin Loaded: {RBoxes.Count} Random Boxes", LoggerType.Info, null);
            }
        }

        public static void Reload()
        {
            RBoxes.Clear();
            Load();
        }

        private static void smethod_0(int int_0)
        {
            string path = $"Data/RBoxes/{int_0}.xml";
            if (File.Exists(path))
            {
                smethod_1(path, int_0);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
        }

        private static void smethod_1(string string_0, int int_0)
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
                                        RandomBoxModel model1 = new RandomBoxModel();
                                        model1.ItemsCount = int.Parse(attributes.GetNamedItem("Count").Value);
                                        model1.Items = new List<RandomBoxItem>();
                                        RandomBoxModel model = model1;
                                        smethod_2(node2, model);
                                        model.SetTopPercent();
                                        RBoxes.Add(int_0, model);
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

        private static void smethod_2(XmlNode xmlNode_0, RandomBoxModel randomBoxModel_0)
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
                            RandomBoxItem item1 = new RandomBoxItem();
                            item1.Index = int.Parse(attributes.GetNamedItem("Index").Value);
                            item1.GoodsId = int.Parse(attributes.GetNamedItem("Id").Value);
                            item1.Percent = int.Parse(attributes.GetNamedItem("Percent").Value);
                            item1.Special = bool.Parse(attributes.GetNamedItem("Special").Value);
                            RandomBoxItem item = item1;
                            randomBoxModel_0.Items.Add(item);
                        }
                    }
                }
            }
        }
    }
}

