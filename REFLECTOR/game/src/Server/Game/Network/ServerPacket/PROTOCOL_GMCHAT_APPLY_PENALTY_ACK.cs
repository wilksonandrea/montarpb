namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_GMCHAT_APPLY_PENALTY_ACK : GameServerPacket
    {
        private readonly uint uint_0;

        public PROTOCOL_GMCHAT_APPLY_PENALTY_ACK(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1a08);
        }
    }
}

