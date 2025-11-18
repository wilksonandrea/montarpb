namespace Server.Auth.Network.ServerPacket
{
    using Server.Auth.Network;
    using System;

    public class PROTOCOL_AUTH_ACCOUNT_KICK_ACK : AuthServerPacket
    {
        private readonly int int_0;

        public PROTOCOL_AUTH_ACCOUNT_KICK_ACK(int int_1)
        {
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x7c5);
            base.WriteC((byte) this.int_0);
        }
    }
}

