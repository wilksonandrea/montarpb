using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ : GameClientPacket
{
	private uint uint_0;

	private string string_0;

	private TicketType ticketType_0;

	public override void Read()
	{
		string_0 = ReadS(ReadC());
		ticketType_0 = (TicketType)ReadC();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			TicketModel ticket = RedeemCodeXML.GetTicket(string_0, ticketType_0);
			if (ticket != null)
			{
				if (ComDiv.CountDB("SELECT COUNT(used_count) FROM base_redeem_history WHERE used_token = '" + ticket.Token + "'") >= ticket.TicketCount)
				{
					Client.SendPacket(new PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_ACK(2147483648u));
					return;
				}
				int usedTicket = DaoManagerSQL.GetUsedTicket(player.PlayerId, ticket.Token);
				if (usedTicket < ticket.PlayerRation)
				{
					usedTicket++;
					if (ticket.Type == TicketType.COUPON)
					{
						List<GoodsItem> goods = GetGoods(ticket);
						if (goods.Count > 0 && method_0(player, ticket, usedTicket))
						{
							foreach (GoodsItem item in goods)
							{
								if (ComDiv.GetIdStatics(item.Item.Id, 1) == 6 && player.Character.GetCharacter(item.Item.Id) == null)
								{
									AllUtils.CreateCharacter(player, item.Item);
								}
								else
								{
									player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, item.Item));
								}
							}
						}
					}
					else if (ticket.Type == TicketType.VOUCHER && (ticket.GoldReward != 0 || ticket.CashReward != 0 || ticket.TagsReward != 0) && method_0(player, ticket, usedTicket))
					{
						if (DaoManagerSQL.UpdateAccountValuable(player.PlayerId, player.Gold + ticket.GoldReward, player.Cash + ticket.CashReward, player.Tags + ticket.TagsReward))
						{
							player.Gold += ticket.GoldReward;
							player.Cash += ticket.CashReward;
							player.Tags += ticket.TagsReward;
						}
						Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0u, player));
					}
				}
				else
				{
					uint_0 = 2147483648u;
				}
			}
			else
			{
				uint_0 = 2147483648u;
			}
			Client.SendPacket(new PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_ACK(uint_0));
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}

	public List<GoodsItem> GetGoods(TicketModel Ticket)
	{
		List<GoodsItem> list = new List<GoodsItem>();
		if (Ticket.Rewards.Count == 0)
		{
			return list;
		}
		foreach (int reward in Ticket.Rewards)
		{
			GoodsItem good = ShopManager.GetGood(reward);
			if (good != null)
			{
				list.Add(good);
			}
		}
		return list;
	}

	private bool method_0(Account account_0, TicketModel ticketModel_0, int int_0)
	{
		if (!DaoManagerSQL.IsTicketUsedByPlayer(account_0.PlayerId, ticketModel_0.Token))
		{
			return DaoManagerSQL.CreatePlayerRedeemHistory(account_0.PlayerId, ticketModel_0.Token, int_0);
		}
		return ComDiv.UpdateDB("base_redeem_history", "owner_id", account_0.PlayerId, "used_token", ticketModel_0.Token, new string[1] { "used_count" }, int_0);
	}
}
