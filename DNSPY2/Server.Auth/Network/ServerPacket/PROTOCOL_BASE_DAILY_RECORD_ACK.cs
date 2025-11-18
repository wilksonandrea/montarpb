using System;
using Plugin.Core.Models;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000019 RID: 25
	public class PROTOCOL_BASE_DAILY_RECORD_ACK : AuthServerPacket
	{
		// Token: 0x06000065 RID: 101 RVA: 0x000026B5 File Offset: 0x000008B5
		public PROTOCOL_BASE_DAILY_RECORD_ACK(StatisticDaily statisticDaily_1, byte byte_1, uint uint_1)
		{
			this.statisticDaily_0 = statisticDaily_1;
			this.byte_0 = byte_1;
			this.uint_0 = uint_1;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004874 File Offset: 0x00002A74
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

		// Token: 0x04000032 RID: 50
		private readonly StatisticDaily statisticDaily_0;

		// Token: 0x04000033 RID: 51
		private readonly byte byte_0;

		// Token: 0x04000034 RID: 52
		private readonly uint uint_0;
	}
}
