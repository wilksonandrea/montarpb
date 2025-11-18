namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_MEMBER_LIST_ACK : GameServerPacket
    {
        private readonly uint uint_0;
        private readonly byte[] byte_0;
        private readonly byte byte_1;
        private readonly byte byte_2;

        public PROTOCOL_CS_MEMBER_LIST_ACK(uint uint_1, byte byte_3, byte byte_4, byte[] byte_5)
        {
            this.uint_0 = uint_1;
            this.byte_1 = byte_3;
            this.byte_2 = byte_4;
            this.byte_0 = byte_5;
        }

        public override void Write()
        {
            base.WriteH((short) 0x325);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 0)
            {
                base.WriteC(this.byte_1);
                base.WriteC(this.byte_2);
                base.WriteB(this.byte_0);
            }
        }
    }
}

