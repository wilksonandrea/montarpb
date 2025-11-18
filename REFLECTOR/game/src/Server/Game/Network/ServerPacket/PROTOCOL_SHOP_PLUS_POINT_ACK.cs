namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SHOP_PLUS_POINT_ACK : GameServerPacket
    {
        private readonly int int_0;
        private readonly int int_1;
        private readonly int int_2;

        public PROTOCOL_SHOP_PLUS_POINT_ACK(int int_3, int int_4, int int_5)
        {
            this.int_1 = int_3;
            this.int_0 = int_4;
            this.int_2 = int_5;
        }

        public override void Write()
        {
            base.WriteH((short) 0x430);
            base.WriteH((short) 0);
            base.WriteC((byte) this.int_2);
            base.WriteD(this.int_0);
            base.WriteD(this.int_1);
        }
    }
}

