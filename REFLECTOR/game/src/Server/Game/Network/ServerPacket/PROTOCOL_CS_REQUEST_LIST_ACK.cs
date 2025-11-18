namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_REQUEST_LIST_ACK : GameServerPacket
    {
        private readonly int int_0;
        private readonly int int_1;
        private readonly int int_2;
        private readonly byte[] byte_0;

        public PROTOCOL_CS_REQUEST_LIST_ACK(int int_3)
        {
            this.int_0 = int_3;
        }

        public PROTOCOL_CS_REQUEST_LIST_ACK(int int_3, int int_4, int int_5, byte[] byte_1)
        {
            this.int_0 = int_3;
            this.int_2 = int_4;
            this.int_1 = int_5;
            this.byte_0 = byte_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x333);
            base.WriteD(this.int_0);
            if (this.int_0 >= 0)
            {
                base.WriteC((byte) this.int_1);
                base.WriteC((byte) this.int_2);
                base.WriteB(this.byte_0);
            }
        }
    }
}

