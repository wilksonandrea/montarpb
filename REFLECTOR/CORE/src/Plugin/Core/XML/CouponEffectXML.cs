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

    public static class CouponEffectXML
    {
        private static List<CouponFlag> list_0 = new List<CouponFlag>();

        public static CouponFlag GetCouponEffect(int id)
        {
            CouponFlag flag3;
            List<CouponFlag> list = list_0;
            lock (list)
            {
                int num = 0;
                while (true)
                {
                    if (num >= list_0.Count)
                    {
                        flag3 = null;
                    }
                    else
                    {
                        CouponFlag flag2 = list_0[num];
                        if (flag2.ItemId != id)
                        {
                            num++;
                            continue;
                        }
                        flag3 = flag2;
                    }
                    break;
                }
            }
            return flag3;
        }

        public static void Load()
        {
            string path = "Data/CouponFlags.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {list_0.Count} Coupon Effects", LoggerType.Info, null);
        }

        public static void Reload()
        {
            list_0.Clear();
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
                                    if ("Coupon".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        CouponFlag flag1 = new CouponFlag();
                                        flag1.ItemId = int.Parse(attributes.GetNamedItem("ItemId").Value);
                                        flag1.EffectFlag = ComDiv.ParseEnum<CouponEffects>(attributes.GetNamedItem("EffectFlag").Value);
                                        CouponFlag item = flag1;
                                        list_0.Add(item);
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

