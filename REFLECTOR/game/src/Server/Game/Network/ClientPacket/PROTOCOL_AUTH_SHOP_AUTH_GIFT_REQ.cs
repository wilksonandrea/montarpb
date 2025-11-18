namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Managers;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ : GameClientPacket
    {
        private long long_0;

        public override void Read()
        {
            this.long_0 = base.ReadUD();
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
                        base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(0x80001029, null, null));
                        base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(0x80000000, null, null));
                    }
                    else
                    {
                        MessageModel message = DaoManagerSQL.GetMessage(this.long_0, player.PlayerId);
                        if ((message == null) || (message.Type != NoteMessageType.Gift))
                        {
                            base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(0x80000000, null, null));
                        }
                        else
                        {
                            GoodsItem good = ShopManager.GetGood((int) message.SenderId);
                            if (good != null)
                            {
                                base.Client.SendPacket(new PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(1, good.Item, player));
                                DaoManagerSQL.DeleteMessage(this.long_0, player.PlayerId);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_AUTH_SHOP_AUTH_GIFT_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

