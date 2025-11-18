namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Managers;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ : GameClientPacket
    {
        private uint uint_0;
        private string string_0;
        private TicketType ticketType_0;

        public List<GoodsItem> GetGoods(TicketModel Ticket)
        {
            List<GoodsItem> list = new List<GoodsItem>();
            if (Ticket.Rewards.Count != 0)
            {
                using (List<int>.Enumerator enumerator = Ticket.Rewards.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        GoodsItem good = ShopManager.GetGood(enumerator.Current);
                        if (good != null)
                        {
                            list.Add(good);
                        }
                    }
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
            string[] cOLUMNS = new string[] { "used_count" };
            object[] vALUES = new object[] { int_0 };
            return ComDiv.UpdateDB("base_redeem_history", "owner_id", account_0.PlayerId, "used_token", ticketModel_0.Token, cOLUMNS, vALUES);
        }

        public override void Read()
        {
            this.string_0 = base.ReadS(base.ReadC());
            this.ticketType_0 = (TicketType) base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    TicketModel ticket = RedeemCodeXML.GetTicket(this.string_0, this.ticketType_0);
                    if (ticket == null)
                    {
                        this.uint_0 = 0x80000000;
                    }
                    else if (ComDiv.CountDB("SELECT COUNT(used_count) FROM base_redeem_history WHERE used_token = '" + ticket.Token + "'") < ticket.TicketCount)
                    {
                        int usedTicket = DaoManagerSQL.GetUsedTicket(player.PlayerId, ticket.Token);
                        if (usedTicket >= ticket.PlayerRation)
                        {
                            this.uint_0 = 0x80000000;
                        }
                        else
                        {
                            usedTicket++;
                            if (ticket.Type != TicketType.COUPON)
                            {
                                if (((ticket.Type == TicketType.VOUCHER) && ((ticket.GoldReward != 0) || ((ticket.CashReward != 0) || (ticket.TagsReward != 0)))) && this.method_0(player, ticket, usedTicket))
                                {
                                    if (DaoManagerSQL.UpdateAccountValuable(player.PlayerId, player.Gold + ticket.GoldReward, player.Cash + ticket.CashReward, player.Tags + ticket.TagsReward))
                                    {
                                        player.Gold += ticket.GoldReward;
                                        player.Cash += ticket.CashReward;
                                        player.Tags += ticket.TagsReward;
                                    }
                                    base.Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, player));
                                }
                            }
                            else
                            {
                                List<GoodsItem> goods = this.GetGoods(ticket);
                                if ((goods.Count > 0) && this.method_0(player, ticket, usedTicket))
                                {
                                    foreach (GoodsItem item in goods)
                                    {
                                        if ((ComDiv.GetIdStatics(item.Item.Id, 1) == 6) && (player.Character.GetCharacter(item.Item.Id) == null))
                                        {
                                            AllUtils.CreateCharacter(player, item.Item);
                                            continue;
                                        }
                                        player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, item.Item));
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_ACK(0x80000000));
                        return;
                    }
                    base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_ACK(this.uint_0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_AUTH_SHOP_USE_GIFTCOUPON_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

