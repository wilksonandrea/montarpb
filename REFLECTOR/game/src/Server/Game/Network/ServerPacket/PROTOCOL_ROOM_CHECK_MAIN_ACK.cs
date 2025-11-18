namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_CHECK_MAIN_ACK : GameServerPacket
    {
        private readonly uint uint_0;

        public PROTOCOL_ROOM_CHECK_MAIN_ACK(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0xe22);
            base.WriteD(this.uint_0);
        }
    }
}

