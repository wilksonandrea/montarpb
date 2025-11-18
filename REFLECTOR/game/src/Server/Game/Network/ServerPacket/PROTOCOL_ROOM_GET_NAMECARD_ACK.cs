namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_GET_NAMECARD_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0xe35);
        }
    }
}

