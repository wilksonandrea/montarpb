namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_USER_TITLE_CHANGE_ACK : GameServerPacket
    {
        private readonly int int_0;
        private readonly uint uint_0;

        public PROTOCOL_BASE_USER_TITLE_CHANGE_ACK(uint uint_1, int int_1)
        {
            this.uint_0 = uint_1;
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x949);
            base.WriteD(this.uint_0);
            base.WriteD(this.int_0);
        }
    }
}

