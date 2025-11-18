namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK : GameServerPacket
    {
        private readonly MatchModel matchModel_0;
        private readonly int int_0;
        private readonly int int_1;

        public PROTOCOL_CLAN_WAR_JOIN_ROOM_ACK(MatchModel matchModel_1, int int_2, int int_3)
        {
            this.matchModel_0 = matchModel_1;
            this.int_0 = int_2;
            this.int_1 = int_3;
        }

        public override void Write()
        {
            base.WriteH((short) 0x61e);
            base.WriteD(this.int_0);
            base.WriteH((ushort) this.int_1);
            base.WriteH((ushort) this.matchModel_0.GetServerInfo());
        }
    }
}

