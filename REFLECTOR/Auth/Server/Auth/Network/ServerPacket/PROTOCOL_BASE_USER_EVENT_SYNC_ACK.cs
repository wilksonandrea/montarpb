namespace Server.Auth.Network.ServerPacket
{
    using Server.Auth.Network;
    using System;

    public class PROTOCOL_BASE_USER_EVENT_SYNC_ACK : AuthServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x9a9);
            base.WriteH((short) 0);
        }
    }
}

