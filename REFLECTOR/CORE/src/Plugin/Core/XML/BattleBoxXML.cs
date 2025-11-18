namespace Plugin.Core.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class BattleBoxXML
    {
        public static List<BattleBoxModel> BBoxes = new List<BattleBoxModel>();
        public static List<ShopData> ShopDataBattleBoxes = new List<ShopData>();
        public static int TotalBoxes;

        public static BattleBoxModel GetBattleBox(int BattleBoxId)
        {
            BattleBoxModel model2;
            if (BattleBoxId == 0)
            {
                return null;
            }
            List<BattleBoxModel> bBoxes = BBoxes;
            lock (bBoxes)
            {
                using (List<BattleBoxModel>.Enumerator enumerator = BBoxes.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            BattleBoxModel current = enumerator.Current;
                            if (current.CouponId != BattleBoxId)
                            {
                                continue;
                            }
                            model2 = current;
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
            }
            return model2;
        }

        public static void Load()
        {
            DirectoryInfo info = new DirectoryInfo(Directory.GetCurrentDirectory() + @"\Data\BBoxes");
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
                smethod_4();
                CLogger.Print($"Plugin Loaded: {BBoxes.Count} Battle Boxes", LoggerType.Info, null);
            }
        }

        public static void Reload()
        {
            BBoxes.Clear();
            ShopDataBattleBoxes.Clear();
            TotalBoxes = 0;
        }

        private static void smethod_0(int int_0)
        {
            string path = $"Data/BBoxes/{int_0}.xml";
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
                                    if ("BattleBox".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        BattleBoxModel model1 = new BattleBoxModel();
                                        model1.CouponId = int_0;
                                        model1.RequireTags = int.Parse(attributes.GetNamedItem("RequireTags").Value);
                                        model1.Items = new List<BattleBoxItem>();
                                        BattleBoxModel model = model1;
                                        smethod_2(node2, model);
                                        model.InitItemPercentages();
                                        BBoxes.Add(model);
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

        private static void smethod_2(XmlNode xmlNode_0, BattleBoxModel battleBoxModel_0)
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
                            BattleBoxItem item1 = new BattleBoxItem();
                            item1.GoodsId = int.Parse(attributes.GetNamedItem("Id").Value);
                            item1.Percent = int.Parse(attributes.GetNamedItem("Percent").Value);
                            BattleBoxItem item = item1;
                            battleBoxModel_0.Items.Add(item);
                        }
                    }
                }
            }
        }

        private static byte[] smethod_3(int int_0, int int_1, ref int int_2, List<BattleBoxModel> list_0)
        {
            byte[] buffer;
            int_2 = 0;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                int num = int_1 * int_0;
                while (true)
                {
                    if (num < list_0.Count)
                    {
                        smethod_5(list_0[num], packet);
                        int num2 = int_2 + 1;
                        int_2 = num2;
                        if (num2 != int_0)
                        {
                            num++;
                            continue;
                        }
                    }
                    buffer = packet.ToArray();
                    break;
                }
            }
            return buffer;
        }

        private static void smethod_4()
        {
            List<BattleBoxModel> list = new List<BattleBoxModel>();
            List<BattleBoxModel> bBoxes = BBoxes;
            lock (bBoxes)
            {
                foreach (BattleBoxModel model in BBoxes)
                {
                    list.Add(model);
                }
            }
            TotalBoxes = list.Count;
            int num = (int) Math.Ceiling((double) (((double) list.Count) / 100.0));
            int num2 = 0;
            for (int i = 0; i < num; i++)
            {
                byte[] buffer = smethod_3(100, i, ref num2, list);
                ShopData data1 = new ShopData();
                data1.Buffer = buffer;
                data1.ItemsCount = num2;
                data1.Offset = i * 100;
                ShopData item = data1;
                ShopDataBattleBoxes.Add(item);
            }
        }

        private static void smethod_5(BattleBoxModel battleBoxModel_0, SyncServerPacket syncServerPacket_0)
        {
            syncServerPacket_0.WriteD(battleBoxModel_0.CouponId);
            syncServerPacket_0.WriteH((ushort) battleBoxModel_0.RequireTags);
            syncServerPacket_0.WriteH((short) 0);
            syncServerPacket_0.WriteH((short) 0);
            syncServerPacket_0.WriteC(0);
        }
    }
}

