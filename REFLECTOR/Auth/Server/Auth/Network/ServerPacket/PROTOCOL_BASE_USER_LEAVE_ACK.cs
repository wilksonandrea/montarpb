namespace Server.Auth.Network.ServerPacket
{
    using Server.Auth.Network;
    using System;

    public class PROTOCOL_BASE_USER_LEAVE_ACK : AuthServerPacket
    {
        private readonly int int_0;

        public PROTOCOL_BASE_USER_LEAVE_ACK(int int_1)
        {
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x919);
            base.WriteD(this.int_0);
            base.WriteH((short) 0);
        }
    }
}

