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

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000134 RID: 308
	public class PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ : GameClientPacket
	{
		// Token: 0x060002F7 RID: 759 RVA: 0x00004E60 File Offset: 0x00003060
		public override void Read()
		{
			this.string_0 = base.ReadS((int)base.ReadC());
			this.ticketType_0 = (TicketType)base.ReadC();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00017CC8 File Offset: 0x00015EC8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					TicketModel ticket = RedeemCodeXML.GetTicket(this.string_0, this.ticketType_0);
					if (ticket != null)
					{
						if ((long)ComDiv.CountDB("SELECT COUNT(used_count) FROM base_redeem_history WHERE used_token = '" + ticket.Token + "'") >= (long)((ulong)ticket.TicketCount))
						{
							this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_ACK(2147483648U));
							return;
						}
						int num = DaoManagerSQL.GetUsedTicket(player.PlayerId, ticket.Token);
						if ((long)num < (long)((ulong)ticket.PlayerRation))
						{
							num++;
							if (ticket.Type == TicketType.COUPON)
							{
								List<GoodsItem> goods = this.GetGoods(ticket);
								if (goods.Count <= 0 || !this.method_0(player, ticket, num))
								{
									goto IL_20A;
								}
								using (List<GoodsItem>.Enumerator enumerator = goods.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										GoodsItem goodsItem = enumerator.Current;
										if (ComDiv.GetIdStatics(goodsItem.Item.Id, 1) == 6 && player.Character.GetCharacter(goodsItem.Item.Id) == null)
										{
											AllUtils.CreateCharacter(player, goodsItem.Item);
										}
										else
										{
											player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, goodsItem.Item));
										}
									}
									goto IL_20A;
								}
							}
							if (ticket.Type == TicketType.VOUCHER && (ticket.GoldReward != 0 || ticket.CashReward != 0 || ticket.TagsReward != 0) && this.method_0(player, ticket, num))
							{
								if (DaoManagerSQL.UpdateAccountValuable(player.PlayerId, player.Gold + ticket.GoldReward, player.Cash + ticket.CashReward, player.Tags + ticket.TagsReward))
								{
									player.Gold += ticket.GoldReward;
									player.Cash += ticket.CashReward;
									player.Tags += ticket.TagsReward;
								}
								this.Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0U, player));
							}
						}
						else
						{
							this.uint_0 = 2147483648U;
						}
					}
					else
					{
						this.uint_0 = 2147483648U;
					}
					IL_20A:
					this.Client.SendPacket(new PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_ACK(this.uint_0));
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00017F48 File Offset: 0x00016148
		public List<GoodsItem> GetGoods(TicketModel Ticket)
		{
			List<GoodsItem> list = new List<GoodsItem>();
			if (Ticket.Rewards.Count == 0)
			{
				return list;
			}
			foreach (int num in Ticket.Rewards)
			{
				GoodsItem good = ShopManager.GetGood(num);
				if (good != null)
				{
					list.Add(good);
				}
			}
			return list;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00017FBC File Offset: 0x000161BC
		private bool method_0(Account account_0, TicketModel ticketModel_0, int int_0)
		{
			if (!DaoManagerSQL.IsTicketUsedByPlayer(account_0.PlayerId, ticketModel_0.Token))
			{
				return DaoManagerSQL.CreatePlayerRedeemHistory(account_0.PlayerId, ticketModel_0.Token, int_0);
			}
			return ComDiv.UpdateDB("base_redeem_history", "owner_id", account_0.PlayerId, "used_token", ticketModel_0.Token, new string[] { "used_count" }, new object[] { int_0 });
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ()
		{
		}

		// Token: 0x0400022E RID: 558
		private uint uint_0;

		// Token: 0x0400022F RID: 559
		private string string_0;

		// Token: 0x04000230 RID: 560
		private TicketType ticketType_0;
	}
}
