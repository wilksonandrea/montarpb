namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_JOIN_REQUEST_ACK : GameServerPacket
    {
        private readonly int int_0;
        private readonly uint uint_0;

        public PROTOCOL_CS_JOIN_REQUEST_ACK(uint uint_1, int int_1)
        {
            this.uint_0 = uint_1;
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x32d);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 0)
            {
                base.WriteD(this.int_0);
            }
        }
    }
}

