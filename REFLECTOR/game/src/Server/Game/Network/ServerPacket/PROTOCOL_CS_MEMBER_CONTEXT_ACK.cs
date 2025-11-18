namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Utility;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_MEMBER_CONTEXT_ACK : GameServerPacket
    {
        private readonly int int_0;
        private readonly int int_1;

        public PROTOCOL_CS_MEMBER_CONTEXT_ACK(int int_2)
        {
            this.int_0 = int_2;
        }

        public PROTOCOL_CS_MEMBER_CONTEXT_ACK(int int_2, int int_3)
        {
            this.int_0 = int_2;
            this.int_1 = int_3;
        }

        public override void Write()
        {
            base.WriteH((short) 0x323);
            base.WriteD(this.int_0);
            if (this.int_0 == 0)
            {
                base.WriteC((byte) this.int_1);
                base.WriteC(14);
                base.WriteC((byte) Math.Ceiling((double) (((double) this.int_1) / 14.0)));
                base.WriteD(uint.Parse(DateTimeUtil.Now("MMddHHmmss")));
            }
        }
    }
}

