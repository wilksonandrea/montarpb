namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SERVER_MESSAGE_KICK_PLAYER_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0xc03);
            base.WriteC(0);
        }
    }
}

