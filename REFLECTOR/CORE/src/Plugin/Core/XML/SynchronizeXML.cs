namespace Plugin.Core.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class SynchronizeXML
    {
        public static List<Synchronize> Servers = new List<Synchronize>();

        public static Synchronize GetServer(int Port)
        {
            Synchronize synchronize2;
            if (Servers.Count == 0)
            {
                return null;
            }
            try
            {
                List<Synchronize> servers = Servers;
                lock (servers)
                {
                    using (List<Synchronize>.Enumerator enumerator = Servers.GetEnumerator())
                    {
                        while (true)
                        {
                            if (!enumerator.MoveNext())
                            {
                                break;
                            }
                            Synchronize current = enumerator.Current;
                            if (current.RemotePort == Port)
                            {
                                return current;
                            }
                        }
                    }
                    synchronize2 = null;
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
                synchronize2 = null;
            }
            return synchronize2;
        }

        public static void Load()
        {
            string path = "Data/Synchronize.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
        }

        public static void Reload()
        {
            Servers.Clear();
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
                                XmlAttributeCollection attributes = node.Attributes;
                                for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                                {
                                    if ("Sync".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap map = node2.Attributes;
                                        Synchronize synchronize1 = new Synchronize(map.GetNamedItem("Host").Value, int.Parse(map.GetNamedItem("Port").Value));
                                        synchronize1.RemotePort = int.Parse(map.GetNamedItem("RemotePort").Value);
                                        Synchronize item = synchronize1;
                                        Servers.Add(item);
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

