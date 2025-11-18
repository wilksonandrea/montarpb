using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Plugin.Core.XML
{
	public class RedeemCodeXML
	{
		public static List<TicketModel> Tickets;

		static RedeemCodeXML()
		{
			RedeemCodeXML.Tickets = new List<TicketModel>();
		}

		public RedeemCodeXML()
		{
		}

		public static TicketModel GetTicket(string Token, TicketType Type)
		{
			TicketModel ticketModel;
			lock (RedeemCodeXML.Tickets)
			{
				foreach (TicketModel ticket in RedeemCodeXML.Tickets)
				{
					if (!(ticket.Token == Token) || ticket.Type != Type)
					{
						continue;
					}
					ticketModel = ticket;
					return ticketModel;
				}
				ticketModel = null;
			}
			return ticketModel;
		}

		public static void Load()
		{
			string str = "Data/RedeemCodes.xml";
			if (!File.Exists(str))
			{
				CLogger.Print(string.Concat("File not found: ", str), LoggerType.Warning, null);
			}
			else
			{
				RedeemCodeXML.smethod_0(str);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Redeem Codes", RedeemCodeXML.Tickets.Count), LoggerType.Info, null);
		}

		public static void Reload()
		{
			RedeemCodeXML.Tickets.Clear();
			RedeemCodeXML.Load();
		}

		private static void smethod_0(string string_0)
		{
			XmlDocument xmlDocument = new XmlDocument();
			using (FileStream fileStream = new FileStream(string_0, FileMode.Open))
			{
				if (fileStream.Length != 0)
				{
					try
					{
						xmlDocument.Load(fileStream);
						for (XmlNode i = xmlDocument.FirstChild; i != null; i = i.NextSibling)
						{
							if ("List".Equals(i.Name))
							{
								for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
								{
									if ("Ticket".Equals(j.Name))
									{
										XmlNamedNodeMap attributes = j.Attributes;
										TicketModel ticketModel = new TicketModel()
										{
											Token = attributes.GetNamedItem("Token").Value,
											Type = ComDiv.ParseEnum<TicketType>(attributes.GetNamedItem("Type").Value),
											TicketCount = uint.Parse(attributes.GetNamedItem("Count").Value),
											PlayerRation = uint.Parse(attributes.GetNamedItem("PlayerRation").Value),
											Rewards = new List<int>()
										};
										if (ticketModel.Type == TicketType.VOUCHER)
										{
											ticketModel.GoldReward = int.Parse(attributes.GetNamedItem("GoldReward").Value);
											ticketModel.CashReward = int.Parse(attributes.GetNamedItem("CashReward").Value);
											ticketModel.TagsReward = int.Parse(attributes.GetNamedItem("TagsReward").Value);
										}
										if (ticketModel.Type == TicketType.COUPON)
										{
											RedeemCodeXML.smethod_1(j, ticketModel);
										}
										RedeemCodeXML.Tickets.Add(ticketModel);
									}
								}
							}
						}
					}
					catch (XmlException xmlException1)
					{
						XmlException xmlException = xmlException1;
						CLogger.Print(xmlException.Message, LoggerType.Error, xmlException);
					}
				}
				else
				{
					CLogger.Print(string.Concat("File is empty: ", string_0), LoggerType.Warning, null);
				}
				fileStream.Dispose();
				fileStream.Close();
			}
		}

		private static void smethod_1(XmlNode xmlNode_0, TicketModel ticketModel_0)
		{
			for (XmlNode i = xmlNode_0.FirstChild; i != null; i = i.NextSibling)
			{
				if ("Rewards".Equals(i.Name))
				{
					for (XmlNode j = i.FirstChild; j != null; j = j.NextSibling)
					{
						if ("Goods".Equals(j.Name))
						{
							XmlNamedNodeMap attributes = j.Attributes;
							ticketModel_0.Rewards.Add(int.Parse(attributes.GetNamedItem("Id").Value));
						}
					}
				}
			}
		}
	}
}