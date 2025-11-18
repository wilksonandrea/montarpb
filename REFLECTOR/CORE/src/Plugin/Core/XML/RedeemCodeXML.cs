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

    public class RedeemCodeXML
    {
        public static List<TicketModel> Tickets = new List<TicketModel>();

        public static TicketModel GetTicket(string Token, TicketType Type)
        {
            List<TicketModel> tickets = Tickets;
            lock (tickets)
            {
                using (List<TicketModel>.Enumerator enumerator = Tickets.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        TicketModel current = enumerator.Current;
                        if ((current.Token == Token) && (current.Type == Type))
                        {
                            return current;
                        }
                    }
                }
                return null;
            }
        }

        public static void Load()
        {
            string path = "Data/RedeemCodes.xml";
            if (File.Exists(path))
            {
                smethod_0(path);
            }
            else
            {
                CLogger.Print("File not found: " + path, LoggerType.Warning, null);
            }
            CLogger.Print($"Plugin Loaded: {Tickets.Count} Redeem Codes", LoggerType.Info, null);
        }

        public static void Reload()
        {
            Tickets.Clear();
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
                                    if ("Ticket".Equals(node2.Name))
                                    {
                                        XmlNamedNodeMap attributes = node2.Attributes;
                                        TicketModel model1 = new TicketModel();
                                        model1.Token = attributes.GetNamedItem("Token").Value;
                                        model1.Type = ComDiv.ParseEnum<TicketType>(attributes.GetNamedItem("Type").Value);
                                        model1.TicketCount = uint.Parse(attributes.GetNamedItem("Count").Value);
                                        model1.PlayerRation = uint.Parse(attributes.GetNamedItem("PlayerRation").Value);
                                        model1.Rewards = new List<int>();
                                        TicketModel model = model1;
                                        if (model.Type == TicketType.VOUCHER)
                                        {
                                            model.GoldReward = int.Parse(attributes.GetNamedItem("GoldReward").Value);
                                            model.CashReward = int.Parse(attributes.GetNamedItem("CashReward").Value);
                                            model.TagsReward = int.Parse(attributes.GetNamedItem("TagsReward").Value);
                                        }
                                        if (model.Type == TicketType.COUPON)
                                        {
                                            smethod_1(node2, model);
                                        }
                                        Tickets.Add(model);
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

        private static void smethod_1(XmlNode xmlNode_0, TicketModel ticketModel_0)
        {
            for (XmlNode node = xmlNode_0.FirstChild; node != null; node = node.NextSibling)
            {
                if ("Rewards".Equals(node.Name))
                {
                    for (XmlNode node2 = node.FirstChild; node2 != null; node2 = node2.NextSibling)
                    {
                        if ("Goods".Equals(node2.Name))
                        {
                            XmlNamedNodeMap attributes = node2.Attributes;
                            ticketModel_0.Rewards.Add(int.Parse(attributes.GetNamedItem("Id").Value));
                        }
                    }
                }
            }
        }
    }
}

