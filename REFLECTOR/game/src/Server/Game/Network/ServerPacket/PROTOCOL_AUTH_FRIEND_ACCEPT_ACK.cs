namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_AUTH_FRIEND_ACCEPT_ACK : GameServerPacket
    {
        private readonly uint uint_0;

        public PROTOCOL_AUTH_FRIEND_ACCEPT_ACK(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x719);
            base.WriteD(this.uint_0);
        }
    }
}

