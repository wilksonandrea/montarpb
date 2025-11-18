namespace Plugin.Core.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public static class InternetCafeXML
    {
        public static readonly List<InternetCafe> Cafes = new List<InternetCafe>();

        public static InternetCafe GetICafe(int ConfigId)
        {
            List<InternetCafe> cafes = Cafes;
            lock (cafes)
            {
                using (List<InternetCafe>.Enumerator enumerator = Cafes.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        InternetCafe current = enumerator.Current;
                        if (current.ConfigId == ConfigId)
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public static bool IsValidAddress(string PlayerAddress, string RegisteredAddress) => 
            PlayerAddress.Equals(RegisteredAddress);

        public static void Load()
        {
            string path = "Data/InternetCafe.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Cafes.Count} Internet Cafe Bonuses", LoggerType.Info, null);
        }

        public static void Reload()
        {
            Cafes.Clear();
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
                                    if ("Bonus".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        InternetCafe cafe1 = new InternetCafe(int.Parse(attributes.GetNamedItem("Id").Value));
                                        cafe1.BasicExp = int.Parse(attributes.GetNamedItem("BasicExp").Value);
                                        cafe1.BasicGold = int.Parse(attributes.GetNamedItem("BasicGold").Value);
                                        cafe1.PremiumExp = int.Parse(attributes.GetNamedItem("PremiumExp").Value);
                                        cafe1.PremiumGold = int.Parse(attributes.GetNamedItem("PremiumGold").Value);
                                        InternetCafe item = cafe1;
                                        Cafes.Add(item);
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

