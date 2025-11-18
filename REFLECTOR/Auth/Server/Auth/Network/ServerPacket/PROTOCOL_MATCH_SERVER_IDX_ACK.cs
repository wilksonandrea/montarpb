namespace Server.Auth.Network.ServerPacket
{
    using Server.Auth.Network;
    using System;

    public class PROTOCOL_MATCH_SERVER_IDX_ACK : AuthServerPacket
    {
        private readonly short short_0;

        public PROTOCOL_MATCH_SERVER_IDX_ACK(short short_1)
        {
            this.short_0 = short_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1e02);
            base.WriteH((short) 0);
        }
    }
}

