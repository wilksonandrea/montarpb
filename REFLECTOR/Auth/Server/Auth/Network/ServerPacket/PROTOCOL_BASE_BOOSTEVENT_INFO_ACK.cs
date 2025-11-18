namespace Server.Auth.Network.ServerPacket
{
    using Server.Auth.Network;
    using System;

    public class PROTOCOL_BASE_BOOSTEVENT_INFO_ACK : AuthServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x9a5);
            base.WriteD(1);
            base.WriteD(0);
            base.WriteC(0);
        }
    }
}

