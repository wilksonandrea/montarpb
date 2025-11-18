using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML;

public class RedeemCodeXML
{
	public static List<TicketModel> Tickets = new List<TicketModel>();

	public static void Load()
	{
		string text = "Data/RedeemCodes.xml";
		if (File.Exists(text))
		{
			smethod_0(text);
		}
		else
		{
			CLogger.Print("File not found: " + text, LoggerType.Warning);
		}
		CLogger.Print($"Plugin Loaded: {Tickets.Count} Redeem Codes", LoggerType.Info);
	}

	public static void Reload()
	{
		Tickets.Clear();
		Load();
	}

	public static TicketModel GetTicket(string Token, TicketType Type)
	{
		lock (Tickets)
		{
			foreach (TicketModel ticket in Tickets)
			{
				if (ticket.Token == Token && ticket.Type == Type)
				{
					return ticket;
				}
			}
			return null;
		}
	}

	private static void smethod_0(string string_0)
	{
		XmlDocument xmlDocument = new XmlDocument();
		using FileStream fileStream = new FileStream(string_0, FileMode.Open);
		if (fileStream.Length == 0L)
		{
			CLogger.Print("File is empty: " + string_0, LoggerType.Warning);
		}
		else
		{
			try
			{
				xmlDocument.Load(fileStream);
				for (XmlNode xmlNode = xmlDocument.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					if ("List".Equals(xmlNode.Name))
					{
						for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
						{
							if ("Ticket".Equals(xmlNode2.Name))
							{
								XmlNamedNodeMap attributes = xmlNode2.Attributes;
								TicketModel ticketModel = new TicketModel
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
									smethod_1(xmlNode2, ticketModel);
								}
								Tickets.Add(ticketModel);
							}
						}
					}
				}
			}
			catch (XmlException ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}
		fileStream.Dispose();
		fileStream.Close();
	}

	private static void smethod_1(XmlNode xmlNode_0, TicketModel ticketModel_0)
	{
		for (XmlNode xmlNode = xmlNode_0.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
		{
			if ("Rewards".Equals(xmlNode.Name))
			{
				for (XmlNode xmlNode2 = xmlNode.FirstChild; xmlNode2 != null; xmlNode2 = xmlNode2.NextSibling)
				{
					if ("Goods".Equals(xmlNode2.Name))
					{
						XmlNamedNodeMap attributes = xmlNode2.Attributes;
						ticketModel_0.Rewards.Add(int.Parse(attributes.GetNamedItem("Id").Value));
					}
				}
			}
		}
	}
}
