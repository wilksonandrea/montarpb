namespace Server.Auth.Network.ServerPacket
{
    using Server.Auth.Network;
    using System;

    public class PROTOCOL_SERVER_MESSAGE_ERROR_ACK : AuthServerPacket
    {
        private readonly uint uint_0;

        public PROTOCOL_SERVER_MESSAGE_ERROR_ACK(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0xc06);
            base.WriteD(this.uint_0);
        }
    }
}

