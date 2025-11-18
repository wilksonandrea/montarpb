namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK : GameServerPacket
    {
        private readonly int int_0;

        public PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK(int int_1)
        {
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x7a3);
            base.WriteC((byte) this.int_0);
            base.WriteC(13);
            base.WriteC((byte) Math.Ceiling((double) (((double) this.int_0) / 13.0)));
        }
    }
}

