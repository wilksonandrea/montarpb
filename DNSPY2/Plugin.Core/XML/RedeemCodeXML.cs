using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;

namespace Plugin.Core.XML
{
	// Token: 0x02000019 RID: 25
	public class RedeemCodeXML
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00012278 File Offset: 0x00010478
		public static void Load()
		{
			string text = "Data/RedeemCodes.xml";
			if (File.Exists(text))
			{
				RedeemCodeXML.smethod_0(text);
			}
			else
			{
				CLogger.Print("File not found: " + text, LoggerType.Warning, null);
			}
			CLogger.Print(string.Format("Plugin Loaded: {0} Redeem Codes", RedeemCodeXML.Tickets.Count), LoggerType.Info, null);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000279A File Offset: 0x0000099A
		public static void Reload()
		{
			RedeemCodeXML.Tickets.Clear();
			RedeemCodeXML.Load();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000122D0 File Offset: 0x000104D0
		public static TicketModel GetTicket(string Token, TicketType Type)
		{
			List<TicketModel> tickets = RedeemCodeXML.Tickets;
			TicketModel ticketModel2;
			lock (tickets)
			{
				foreach (TicketModel ticketModel in RedeemCodeXML.Tickets)
				{
					if (ticketModel.Token == Token && ticketModel.Type == Type)
					{
						return ticketModel;
					}
				}
				ticketModel2 = null;
			}
			return ticketModel2;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00012368 File Offset: 0x00010568
		private static void smethod_0(string string_0)
		{
			XmlDocument xmlDocument = new XmlDocument();
			using (FileStream fileStream = new FileStream(string_0, FileMode.Open))
			{
				if (fileStream.Length == 0L)
				{
					CLogger.Print("File is empty: " + string_0, LoggerType.Warning, null);
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
											RedeemCodeXML.smethod_1(xmlNode2, ticketModel);
										}
										RedeemCodeXML.Tickets.Add(ticketModel);
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
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0001256C File Offset: 0x0001076C
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

		// Token: 0x06000109 RID: 265 RVA: 0x00002116 File Offset: 0x00000316
		public RedeemCodeXML()
		{
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000027AB File Offset: 0x000009AB
		// Note: this type is marked as 'beforefieldinit'.
		static RedeemCodeXML()
		{
		}

		// Token: 0x04000063 RID: 99
		public static List<TicketModel> Tickets = new List<TicketModel>();
	}
}
