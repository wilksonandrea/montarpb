namespace Server.Auth.Network.ServerPacket
{
    using Server.Auth.Network;
    using System;

    public class PROTOCOL_BASE_OPTION_SAVE_ACK : AuthServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x913);
            base.WriteD(0);
        }
    }
}

