namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_GET_LOBBY_USER_LIST_ACK : GameServerPacket
    {
        private readonly uint uint_0;

        public PROTOCOL_ROOM_GET_LOBBY_USER_LIST_ACK(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0xe2e);
            base.WriteD(this.uint_0);
        }
    }
}

