namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;
    using System.Runtime.InteropServices;

    public class PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK : GameServerPacket
    {
        private readonly int int_0;
        private readonly uint uint_0;

        public PROTOCOL_CLAN_WAR_INVITE_MERCENARY_RECEIVER_ACK(uint uint_1, int int_1 = 0)
        {
            this.uint_0 = uint_1;
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x624);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 0)
            {
                base.WriteC((byte) this.int_0);
            }
        }
    }
}

