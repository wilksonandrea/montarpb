namespace Server.Auth.Data.XML
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Utility;
    using Server.Auth.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public static class ChannelsXML
    {
        public static List<ChannelModel> Channels = new List<ChannelModel>();

        public static ChannelModel GetChannel(int ServerId, int Id)
        {
            List<ChannelModel> channels = Channels;
            lock (channels)
            {
                using (List<ChannelModel>.Enumerator enumerator = Channels.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        ChannelModel current = enumerator.Current;
                        if ((current.ServerId == ServerId) && (current.Id == Id))
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public static List<ChannelModel> GetChannels(int ServerId)
        {
            List<ChannelModel> list = new List<ChannelModel>(11);
            for (int i = 0; i < Channels.Count; i++)
            {
                ChannelModel item = Channels[i];
                if (item.ServerId == ServerId)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public static void Load()
        {
            string path = "Data/Server/Channels.xml";
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
            Channels.Clear();
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
                                    if ("Channel".Equals(node2.Name))
                                    {
                                        smethod_1(node2, int.Parse(node2.Attributes.GetNamedItem("ServerId").Value));
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
                if ("Count".Equals(node.Name))
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if ("Setting".Equals(node2.Name))
                        {
                            XmlNamedNodeMap attributes = node2.Attributes;
                            ChannelModel model1 = new ChannelModel(int_0);
                            model1.Id = int.Parse(attributes.GetNamedItem("Id").Value);
                            model1.Type = ComDiv.ParseEnum<ChannelType>(attributes.GetNamedItem("Type").Value);
                            model1.MaxRooms = int.Parse(attributes.GetNamedItem("MaxRooms").Value);
                            model1.ExpBonus = int.Parse(attributes.GetNamedItem("ExpBonus").Value);
                            model1.GoldBonus = int.Parse(attributes.GetNamedItem("GoldBonus").Value);
                            model1.CashBonus = int.Parse(attributes.GetNamedItem("CashBonus").Value);
                            ChannelModel item = model1;
                            try
                            {
                                if (item.Type == ChannelType.CH_PW)
                                {
                                    item.Password = attributes.GetNamedItem("Password").Value;
                                }
                            }
                            catch (XmlException exception)
                            {
                                CLogger.Print(exception.Message, LoggerType.Error, exception);
                            }
                            ChannelModel channel = GetChannel(item.ServerId, item.Id);
                            if (channel == null)
                            {
                                Channels.Add(item);
                            }
                            else
                            {
                                List<ChannelModel> channels = Channels;
                                lock (channels)
                                {
                                    channel.Type = item.Type;
                                    channel.MaxRooms = item.MaxRooms;
                                    channel.ExpBonus = item.ExpBonus;
                                    channel.GoldBonus = item.GoldBonus;
                                    channel.CashBonus = item.CashBonus;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

