namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_ENTER_PASS_ACK : GameServerPacket
    {
        private readonly uint uint_0;

        public PROTOCOL_BASE_ENTER_PASS_ACK(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x962);
            base.WriteH((short) 0);
            base.WriteD(this.uint_0);
        }
    }
}

