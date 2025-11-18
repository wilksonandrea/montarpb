namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Managers;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Plugin.Core.XML;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_SHOP_GET_SAILLIST_REQ : GameClientPacket
    {
        private string string_0;

        public override void Read()
        {
            this.string_0 = base.ReadS(0x20);
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    if (!player.LoadedShop)
                    {
                        player.LoadedShop = true;
                        foreach (ShopData data in ShopManager.ShopDataItems)
                        {
                            base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEMLIST_ACK(data, ShopManager.TotalItems));
                        }
                        foreach (ShopData data2 in ShopManager.ShopDataGoods)
                        {
                            base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_GOODSLIST_ACK(data2, ShopManager.TotalGoods));
                        }
                        foreach (ShopData data3 in ShopManager.ShopDataItemRepairs)
                        {
                            base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK(data3, ShopManager.TotalRepairs));
                        }
                        foreach (ShopData data4 in BattleBoxXML.ShopDataBattleBoxes)
                        {
                            base.Client.SendPacket(new PROTOCOL_BATTLEBOX_GET_LIST_ACK(data4, BattleBoxXML.TotalBoxes));
                        }
                        if (player.CafePC == CafeEnum.None)
                        {
                            foreach (ShopData data5 in ShopManager.ShopDataMt1)
                            {
                                base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK(data5, ShopManager.TotalMatching1));
                            }
                        }
                        else
                        {
                            foreach (ShopData data6 in ShopManager.ShopDataMt2)
                            {
                                base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK(data6, ShopManager.TotalMatching2));
                            }
                        }
                    }
                    base.Client.SendPacket(new PROTOCOL_SHOP_TAG_INFO_ACK());
                    if (Bitwise.ReadFile(Environment.CurrentDirectory + "/Data/Raws/Shop.dat") == this.string_0)
                    {
                        base.Client.SendPacket(new PROTOCOL_SHOP_GET_SAILLIST_ACK(false));
                    }
                    else
                    {
                        base.Client.SendPacket(new PROTOCOL_SHOP_GET_SAILLIST_ACK(true));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_SHOP_GET_SAILLIST_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

