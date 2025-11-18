namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_CANCEL_REQUEST_ACK : GameServerPacket
    {
        private readonly uint uint_0;

        public PROTOCOL_CS_CANCEL_REQUEST_ACK(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x32f);
            base.WriteD(this.uint_0);
        }
    }
}

