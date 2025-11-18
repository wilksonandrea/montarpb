namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SHOP_TAG_INFO_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x44b);
            base.WriteH((short) 0);
            base.WriteC(7);
            base.WriteC(5);
            base.WriteH((short) 0);
            base.WriteC(0);
            base.WriteD(0);
            base.WriteH((short) 0);
            base.WriteC(3);
            base.WriteQ((long) 0L);
            base.WriteC(0);
            base.WriteC(4);
            base.WriteQ((long) 0L);
            base.WriteC(0);
            base.WriteC(2);
            base.WriteQ((long) 0L);
            base.WriteC(0);
            base.WriteC(6);
            base.WriteQ((long) 0L);
            base.WriteC(0);
            base.WriteC(1);
            base.WriteQ((long) 0L);
            base.WriteD(0);
            base.WriteC(0);
            base.WriteC(0xff);
            base.WriteC(0xff);
            base.WriteC(0xff);
            base.WriteC(0);
            base.WriteC(0xff);
            base.WriteC(1);
            base.WriteC(7);
            base.WriteC(2);
        }
    }
}

