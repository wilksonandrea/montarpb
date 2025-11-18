namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_GAMEGUARD_ACK : GameServerPacket
    {
        private readonly int int_0;
        private readonly byte[] byte_0;

        public PROTOCOL_BASE_GAMEGUARD_ACK(int int_1, byte[] byte_1)
        {
            this.int_0 = int_1;
            this.byte_0 = byte_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x908);
            base.WriteB(this.byte_0);
            base.WriteD(this.int_0);
        }
    }
}

