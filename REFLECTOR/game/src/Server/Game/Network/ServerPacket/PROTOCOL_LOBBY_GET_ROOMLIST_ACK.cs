namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_LOBBY_GET_ROOMLIST_ACK : GameServerPacket
    {
        private readonly int int_0;
        private readonly int int_1;
        private readonly int int_2;
        private readonly int int_3;
        private readonly int int_4;
        private readonly int int_5;
        private readonly byte[] byte_0;
        private readonly byte[] byte_1;

        public PROTOCOL_LOBBY_GET_ROOMLIST_ACK(int int_6, int int_7, int int_8, int int_9, int int_10, int int_11, byte[] byte_2, byte[] byte_3)
        {
            this.int_3 = int_6;
            this.int_2 = int_7;
            this.int_0 = int_8;
            this.int_1 = int_9;
            this.byte_0 = byte_2;
            this.byte_1 = byte_3;
            this.int_4 = int_10;
            this.int_5 = int_11;
        }

        public override void Write()
        {
            base.WriteH((short) 0xa1c);
            base.WriteD(this.int_3);
            base.WriteD(this.int_0);
            base.WriteD(this.int_4);
            base.WriteB(this.byte_0);
            base.WriteD(this.int_2);
            base.WriteD(this.int_1);
            base.WriteD(this.int_5);
            base.WriteB(this.byte_1);
        }
    }
}

