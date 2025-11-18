namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SHOP_LIMITED_SALE_LIST_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x448);
            base.WriteD(1);
            base.WriteD(1);
            base.WriteD(1);
            base.WriteD(1);
            base.WriteC(1);
            base.WriteD(0x3c55cd1);
            base.WriteC(1);
            base.WriteD(0x5a201687);
            base.WriteC(1);
        }
    }
}

