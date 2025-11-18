namespace Server.Auth.Network.ServerPacket
{
    using Server.Auth.Network;
    using System;

    public class PROTOCOL_BASE_LOGOUT_ACK : AuthServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x904);
            base.WriteH((short) 0);
        }
    }
}

