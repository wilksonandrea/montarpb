namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK : GameServerPacket
    {
        private readonly uint uint_0;

        public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(int int_0)
        {
            this.uint_0 = (uint) int_0;
        }

        public PROTOCOL_ROOM_REQUEST_MAIN_CHANGE_WHO_ACK(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0xe20);
            base.WriteD(this.uint_0);
        }
    }
}

