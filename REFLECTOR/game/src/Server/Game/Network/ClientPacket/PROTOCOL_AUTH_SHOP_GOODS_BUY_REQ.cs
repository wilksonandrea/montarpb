namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Managers;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Data.Utils;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_AUTH_SHOP_GOODS_BUY_REQ : GameClientPacket
    {
        private List<CartGoods> list_0 = new List<CartGoods>();

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
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    if (player.Inventory.Items.Count >= 500)
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
                            foreach (GoodsItem item in list)
                            {
                                if (ComDiv.GetIdStatics(item.Item.Id, 1) != 0x24)
                                {
                                    base.Client.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, player, item.Item));
                                    continue;
                                }
                                AllUtils.ProcessBattlepassPremiumBuy(player);
                                player.UpdateSeasonpass = false;
                                player.SendPacket(new PROTOCOL_SEASON_CHALLENGE_BUY_SEASON_PASS_ACK(0, player));
                            }
                            base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(1, list, player));
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

