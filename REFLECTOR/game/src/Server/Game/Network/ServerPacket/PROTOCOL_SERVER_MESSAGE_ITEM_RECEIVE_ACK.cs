namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SERVER_MESSAGE_ITEM_RECEIVE_ACK : GameServerPacket
    {
        private readonly uint uint_0;

        public PROTOCOL_SERVER_MESSAGE_ITEM_RECEIVE_ACK(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0xc0c);
            base.WriteD(this.uint_0);
        }
    }
}

