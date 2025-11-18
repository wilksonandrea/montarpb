namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_LOBBY_ENTER_ACK : GameServerPacket
    {
        private readonly uint uint_0;

        public PROTOCOL_LOBBY_ENTER_ACK(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0xa18);
            base.WriteH((short) 0);
            base.WriteD(this.uint_0);
            base.WriteC(0);
            base.WriteQ((long) 0L);
        }
    }
}

