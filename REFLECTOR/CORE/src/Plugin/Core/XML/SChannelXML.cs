namespace Plugin.Core.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Xml;

    public class SChannelXML
    {
        public static List<SChannelModel> Servers = new List<SChannelModel>();

        public static SChannelModel GetServer(int id)
        {
            List<SChannelModel> servers = Servers;
            lock (servers)
            {
                using (List<SChannelModel>.Enumerator enumerator = Servers.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        SChannelModel current = enumerator.Current;
                        if (current.Id == id)
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public static void Load(bool Update = false)
        {
            string path = "Data/Server/SChannels.xml";
            if (File.Exists(path))
            {
                smethod_0(path, Update);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Servers.Count} Server Channel", LoggerType.Info, null);
        }

        public static void Reload()
        {
            Servers.Clear();
            Load(true);
        }

        private static void smethod_0(string string_0, bool bool_0)
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
                                    if ("Server".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        SChannelModel model1 = new SChannelModel(attributes.GetNamedItem("Host").Value, ushort.Parse(attributes.GetNamedItem("Port").Value));
                                        model1.Id = int.Parse(attributes.GetNamedItem("Id").Value);
                                        model1.State = bool.Parse(attributes.GetNamedItem("State").Value);
                                        model1.Type = ComDiv.ParseEnum<SChannelType>(attributes.GetNamedItem("Type").Value);
                                        model1.IsMobile = bool.Parse(attributes.GetNamedItem("Mobile").Value);
                                        model1.MaxPlayers = int.Parse(attributes.GetNamedItem("MaxPlayers").Value);
                                        model1.ChannelPlayers = int.Parse(attributes.GetNamedItem("MaxPlayers").Value);
                                        SChannelModel item = model1;
                                        if (!bool_0)
                                        {
                                            Servers.Add(item);
                                        }
                                        else
                                        {
                                            SChannelModel server = GetServer(item.Id);
                                            if (server == null)
                                            {
                                                Servers.Add(item);
                                            }
                                            else
                                            {
                                                List<SChannelModel> servers = Servers;
                                                lock (servers)
                                                {
                                                    server.State = bool.Parse(attributes.GetNamedItem("State").Value);
                                                    server.Host = attributes.GetNamedItem("Host").Value;
                                                    server.Port = ushort.Parse(attributes.GetNamedItem("Port").Value);
                                                    server.Type = ComDiv.ParseEnum<SChannelType>(attributes.GetNamedItem("Type").Value);
                                                    server.IsMobile = bool.Parse(attributes.GetNamedItem("Mobile").Value);
                                                    server.MaxPlayers = int.Parse(attributes.GetNamedItem("MaxPlayers").Value);
                                                    server.ChannelPlayers = int.Parse(attributes.GetNamedItem("ChannelPlayers").Value);
                                                }
                                            }
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
                                    if ("Server".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        SChannelModel server = GetServer(int_0);
                                        if (server != null)
                                        {
                                            server.State = bool.Parse(attributes.GetNamedItem("State").Value);
                                            server.Host = attributes.GetNamedItem("Host").Value;
                                            server.Port = ushort.Parse(attributes.GetNamedItem("Port").Value);
                                            server.Type = ComDiv.ParseEnum<SChannelType>(attributes.GetNamedItem("Type").Value);
                                            server.IsMobile = bool.Parse(attributes.GetNamedItem("Mobile").Value);
                                            server.MaxPlayers = int.Parse(attributes.GetNamedItem("MaxPlayers").Value);
                                            server.ChannelPlayers = int.Parse(attributes.GetNamedItem("ChannelPlayers").Value);
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

        public static void UpdateServer(int ServerId)
        {
            string path = "Data/Server/SChannels.xml";
            if (File.Exists(path))
            {
                smethod_1(path, ServerId);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
        }
    }
}

