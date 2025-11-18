namespace Server.Auth.Network.ServerPacket
{
    using Server.Auth.Network;
    using System;

    public class PROTOCOL_BASE_CHANNELTYPE_CONDITION_ACK : AuthServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x9b6);
            base.WriteB(new byte[0x378]);
        }
    }
}

