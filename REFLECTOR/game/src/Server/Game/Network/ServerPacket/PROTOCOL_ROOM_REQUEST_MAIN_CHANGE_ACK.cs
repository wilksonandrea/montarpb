namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK : GameServerPacket
    {
        private readonly uint uint_0;

        public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(int int_0)
        {
            this.uint_0 = (uint) int_0;
        }

        public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_ACK(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0xe1e);
            base.WriteD(this.uint_0);
        }
    }
}

