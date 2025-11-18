namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_CLIENT_LEAVE_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x304);
            base.WriteD(0);
        }
    }
}

