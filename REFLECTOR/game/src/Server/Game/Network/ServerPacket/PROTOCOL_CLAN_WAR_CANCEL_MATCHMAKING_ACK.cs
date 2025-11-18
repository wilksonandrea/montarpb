namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CLAN_WAR_CANCEL_MATCHMAKING_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x1b17);
        }
    }
}

