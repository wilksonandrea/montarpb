namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_TICKET_UPDATE_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x9cd);
            base.WriteH((short) 0);
        }
    }
}

