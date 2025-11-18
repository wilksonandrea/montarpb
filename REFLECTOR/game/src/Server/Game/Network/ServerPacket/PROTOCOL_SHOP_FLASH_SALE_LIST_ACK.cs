namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SHOP_FLASH_SALE_LIST_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x457);
            base.WriteC(1);
            base.WriteD(1);
            base.WriteC(1);
        }
    }
}

