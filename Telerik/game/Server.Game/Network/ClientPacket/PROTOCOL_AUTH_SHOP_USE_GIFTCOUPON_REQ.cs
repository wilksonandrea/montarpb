using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ : GameClientPacket
	{
		private uint uint_0;

		private string string_0;

		private TicketType ticketType_0;

		public PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ()
		{
		}

		public List<GoodsItem> GetGoods(TicketModel Ticket)
		{
			List<GoodsItem> goodsItems = new List<GoodsItem>();
			if (Ticket.Rewards.Count == 0)
			{
				return goodsItems;
			}
			foreach (int reward in Ticket.Rewards)
			{
				GoodsItem good = ShopManager.GetGood(reward);
				if (good == null)
				{
					continue;
				}
				goodsItems.Add(good);
			}
			return goodsItems;
		}

		private bool method_0(Account account_0, TicketModel ticketModel_0, int int_0)
		{
			if (!DaoManagerSQL.IsTicketUsedByPlayer(account_0.PlayerId, ticketModel_0.Token))
			{
				return DaoManagerSQL.CreatePlayerRedeemHistory(account_0.PlayerId, ticketModel_0.Token, int_0);
			}
			return ComDiv.UpdateDB("base_redeem_history", "owner_id", account_0.PlayerId, "used_token", ticketModel_0.Token, new string[] { "used_count" }, new object[] { int_0 });
		}

		public override void Read()
		{
			this.string_0 = base.ReadS((int)base.ReadC());
			this.ticketType_0 = (TicketType)base.ReadC();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					TicketModel ticket = RedeemCodeXML.GetTicket(this.string_0, this.ticketType_0);
					if (ticket == null)
					{
						this.uint_0 = -2147483648;
					}
					else if ((long)ComDiv.CountDB(string.Concat("SELECT COUNT(used_count) FROM base_redeem_history WHERE used_token = '", ticket.Token, "'")) < (ulong)ticket.TicketCount)
					{
						int usedTicket = DaoManagerSQL.GetUsedTicket(player.PlayerId, ticket.Token);
						if ((long)usedTicket >= (ulong)ticket.PlayerRation)
						{
							this.uint_0 = -2147483648;
						}
						else
						{
							usedTicket++;
							if (ticket.Type == TicketType.COUPON)
							{
								List<GoodsItem> goods = this.GetGoods(ticket);
								if (goods.Count > 0 && this.method_0(player, ticket, usedTicket))
								{
									foreach (GoodsItem good in goods)
									{
										if (ComDiv.GetIdStatics(good.Item.Id, 1) != 6 || player.Character.GetCharacter(good.Item.Id) != null)
										{
											player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, good.Item));
										}
										else
										{
											AllUtils.CreateCharacter(player, good.Item);
										}
									}
								}
							}
							else if (ticket.Type == TicketType.VOUCHER && (ticket.GoldReward != 0 || ticket.CashReward != 0 || ticket.TagsReward != 0) && this.method_0(player, ticket, usedTicket))
							{
								if (DaoManagerSQL.UpdateAccountValuable(player.PlayerId, player.Gold + ticket.GoldReward, player.Cash + ticket.CashReward, player.Tags + ticket.TagsReward))
								{
									player.Gold += ticket.GoldReward;
									player.Cash += ticket.CashReward;
									player.Tags += ticket.TagsReward;
								}
								this.Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, player));
							}
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_ACK(-2147483648));
						return;
					}
					this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_ACK(this.uint_0));
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}