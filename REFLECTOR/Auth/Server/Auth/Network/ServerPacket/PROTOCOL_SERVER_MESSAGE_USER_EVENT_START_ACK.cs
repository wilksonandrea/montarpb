namespace Server.Auth.Network.ServerPacket
{
    using Server.Auth.Network;
    using System;

    public class PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK : AuthServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0xc0e);
        }
    }
}

