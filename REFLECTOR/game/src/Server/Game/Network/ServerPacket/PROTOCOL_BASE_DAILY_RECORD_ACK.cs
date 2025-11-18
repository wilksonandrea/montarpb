namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_DAILY_RECORD_ACK : GameServerPacket
    {
        private readonly StatisticDaily statisticDaily_0;
        private readonly byte byte_0;
        private readonly uint uint_0;

        public PROTOCOL_BASE_DAILY_RECORD_ACK(StatisticDaily statisticDaily_1, byte byte_1, uint uint_1)
        {
            this.statisticDaily_0 = statisticDaily_1;
            this.byte_0 = byte_1;
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x96f);
            base.WriteH((ushort) this.statisticDaily_0.Matches);
            base.WriteH((ushort) this.statisticDaily_0.MatchWins);
            base.WriteH((ushort) this.statisticDaily_0.MatchLoses);
            base.WriteH((ushort) this.statisticDaily_0.MatchDraws);
            base.WriteH((ushort) this.statisticDaily_0.KillsCount);
            base.WriteH((ushort) this.statisticDaily_0.HeadshotsCount);
            base.WriteH((ushort) this.statisticDaily_0.DeathsCount);
            base.WriteD(this.statisticDaily_0.ExpGained);
            base.WriteD(this.statisticDaily_0.PointGained);
            base.WriteD(this.statisticDaily_0.Playtime);
            base.WriteC(this.byte_0);
            base.WriteD(this.uint_0);
        }
    }
}

