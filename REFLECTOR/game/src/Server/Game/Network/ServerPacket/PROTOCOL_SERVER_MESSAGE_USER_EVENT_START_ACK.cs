namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0xc0e);
        }
    }
}

