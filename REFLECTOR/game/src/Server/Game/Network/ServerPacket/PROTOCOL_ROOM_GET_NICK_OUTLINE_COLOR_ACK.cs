namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK : GameServerPacket
    {
        private readonly int int_0;
        private readonly int int_1;

        public PROTOCOL_ROOM_GET_NICK_OUTLINE_COLOR_ACK(int int_2, int int_3)
        {
            this.int_0 = int_2;
            this.int_1 = int_3;
        }

        public override void Write()
        {
            base.WriteH((short) 0xe36);
            base.WriteD(this.int_0);
            base.WriteC((byte) this.int_1);
        }
    }
}

