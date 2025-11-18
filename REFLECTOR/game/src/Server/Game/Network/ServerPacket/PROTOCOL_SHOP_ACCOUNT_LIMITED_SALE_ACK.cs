namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SHOP_ACCOUNT_LIMITED_SALE_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x44d);
            base.WriteD(1);
            base.WriteD(1);
            base.WriteD(1);
            base.WriteD(1);
        }
    }
}

