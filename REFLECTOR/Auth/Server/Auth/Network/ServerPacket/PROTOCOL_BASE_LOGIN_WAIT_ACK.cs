namespace Server.Auth.Network.ServerPacket
{
    using Server.Auth.Network;
    using System;

    public class PROTOCOL_BASE_LOGIN_WAIT_ACK : AuthServerPacket
    {
        private readonly int int_0;

        public PROTOCOL_BASE_LOGIN_WAIT_ACK(int int_1)
        {
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x909);
            base.WriteC(3);
            base.WriteH((short) 0x44);
            base.WriteD(this.int_0);
        }
    }
}

