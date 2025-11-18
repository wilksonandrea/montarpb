namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_CHANGE_CLAN_EXP_RANK_ACK : GameServerPacket
    {
        private readonly int int_0;

        public PROTOCOL_CS_CHANGE_CLAN_EXP_RANK_ACK(int int_1)
        {
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 880);
            base.WriteC((byte) this.int_0);
        }
    }
}

