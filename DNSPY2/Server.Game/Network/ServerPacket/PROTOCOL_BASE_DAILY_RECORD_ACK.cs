using System;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200004E RID: 78
	public class PROTOCOL_BASE_DAILY_RECORD_ACK : GameServerPacket
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00002FCB File Offset: 0x000011CB
		public PROTOCOL_BASE_DAILY_RECORD_ACK(StatisticDaily statisticDaily_1, byte byte_1, uint uint_1)
		{
			this.statisticDaily_0 = statisticDaily_1;
			this.byte_0 = byte_1;
			this.uint_0 = uint_1;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000B504 File Offset: 0x00009704
		public override void Write()
		{
			base.WriteH(2415);
			base.WriteH((ushort)this.statisticDaily_0.Matches);
			base.WriteH((ushort)this.statisticDaily_0.MatchWins);
			base.WriteH((ushort)this.statisticDaily_0.MatchLoses);
			base.WriteH((ushort)this.statisticDaily_0.MatchDraws);
			base.WriteH((ushort)this.statisticDaily_0.KillsCount);
			base.WriteH((ushort)this.statisticDaily_0.HeadshotsCount);
			base.WriteH((ushort)this.statisticDaily_0.DeathsCount);
			base.WriteD(this.statisticDaily_0.ExpGained);
			base.WriteD(this.statisticDaily_0.PointGained);
			base.WriteD(this.statisticDaily_0.Playtime);
			base.WriteC(this.byte_0);
			base.WriteD(this.uint_0);
		}

		// Token: 0x040000A0 RID: 160
		private readonly StatisticDaily statisticDaily_0;

		// Token: 0x040000A1 RID: 161
		private readonly byte byte_0;

		// Token: 0x040000A2 RID: 162
		private readonly uint uint_0;
	}
}
