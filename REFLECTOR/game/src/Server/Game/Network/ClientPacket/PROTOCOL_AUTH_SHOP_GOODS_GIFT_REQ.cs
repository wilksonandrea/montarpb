namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Managers;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_AUTH_SHOP_GOODS_GIFT_REQ : GameClientPacket
    {
        private string string_0;
        private string string_1;
        private List<CartGoods> list_0 = new List<CartGoods>();

        private MessageModel method_0(string string_2, long long_0, long long_1)
        {
            MessageModel model1 = new MessageModel(15.0);
            model1.SenderName = string_2;
            model1.SenderId = long_1;
            model1.Text = this.string_0;
            model1.State = NoteMessageState.Unreaded;
            MessageModel message = model1;
            if (DaoManagerSQL.CreateMessage(long_0, message))
            {
                return message;
            }
            base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(0x80000000));
            return null;
        }

        public override void Read()
        {
            byte num = base.ReadC();
            for (byte i = 0; i < num; i = (byte) (i + 1))
            {
                base.ReadC();
                CartGoods goods1 = new CartGoods();
                goods1.GoodId = base.ReadD();
                goods1.BuyType = base.ReadC();
                CartGoods item = goods1;
                this.list_0.Add(item);
                base.ReadC();
                base.ReadQ();
            }
            this.string_0 = base.ReadU(base.ReadC() * 2);
            this.string_1 = base.ReadU(base.ReadC() * 2);
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    Account account2 = AccountManager.GetAccount(this.string_1, 1, 0);
                    if ((account2 == null) || (!account2.IsOnline || (player.Nickname == this.string_1)))
                    {
                        base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(0x80001019));
                    }
                    else if (account2.Inventory.Items.Count >= 500)
                    {
                        base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(0x800010b9));
                    }
                    else
                    {
                        int num;
                        int num2;
                        int num3;
                        List<GoodsItem> list = ShopManager.GetGoods(this.list_0, out num, out num2, out num3);
                        if (list.Count == 0)
                        {
                            base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(0x80001017));
                        }
                        else if ((0 > (player.Gold - num)) || ((0 > (player.Cash - num2)) || (0 > (player.Tags - num3))))
                        {
                            base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(0x80001018));
                        }
                        else if (!DaoManagerSQL.UpdateAccountValuable(player.PlayerId, player.Gold - num, player.Cash - num2, player.Tags - num3))
                        {
                            base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(0x80001019));
                        }
                        else
                        {
                            player.Gold -= num;
                            player.Cash -= num2;
                            player.Tags -= num3;
                            if (DaoManagerSQL.GetMessagesCount(account2.PlayerId) >= 100)
                            {
                                base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(0x800010b9));
                            }
                            else
                            {
                                MessageModel model = this.method_0(player.Nickname, account2.PlayerId, base.Client.PlayerId);
                                if (model != null)
                                {
                                    account2.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(model), false);
                                }
                                account2.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account2, list), false);
                                base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_GIFT_ACK(1, list, account2));
                                base.Client.SendPacket(new PROTOCOL_AUTH_GET_POINT_CASH_ACK(0, player));
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

