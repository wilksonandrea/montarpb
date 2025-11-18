namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_GET_UID_LOBBY_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x98a);
        }
    }
}

