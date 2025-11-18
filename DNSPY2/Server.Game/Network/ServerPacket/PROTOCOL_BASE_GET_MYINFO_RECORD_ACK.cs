using System;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000052 RID: 82
	public class PROTOCOL_BASE_GET_MYINFO_RECORD_ACK : GameServerPacket
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x0000305C File Offset: 0x0000125C
		public PROTOCOL_BASE_GET_MYINFO_RECORD_ACK(PlayerStatistic playerStatistic_1)
		{
			this.playerStatistic_0 = playerStatistic_1;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		public override void Write()
		{
			base.WriteH(2369);
			base.WriteD(this.playerStatistic_0.Season.Matches);
			base.WriteD(this.playerStatistic_0.Season.MatchWins);
			base.WriteD(this.playerStatistic_0.Season.MatchLoses);
			base.WriteD(this.playerStatistic_0.Season.MatchDraws);
			base.WriteD(this.playerStatistic_0.Season.KillsCount);
			base.WriteD(this.playerStatistic_0.Season.HeadshotsCount);
			base.WriteD(this.playerStatistic_0.Season.DeathsCount);
			base.WriteD(this.playerStatistic_0.Season.TotalMatchesCount);
			base.WriteD(this.playerStatistic_0.Season.TotalKillsCount);
			base.WriteD(this.playerStatistic_0.Season.EscapesCount);
			base.WriteD(this.playerStatistic_0.Season.AssistsCount);
			base.WriteD(this.playerStatistic_0.Season.MvpCount);
			base.WriteD(this.playerStatistic_0.Basic.Matches);
			base.WriteD(this.playerStatistic_0.Basic.MatchWins);
			base.WriteD(this.playerStatistic_0.Basic.MatchLoses);
			base.WriteD(this.playerStatistic_0.Basic.MatchDraws);
			base.WriteD(this.playerStatistic_0.Basic.KillsCount);
			base.WriteD(this.playerStatistic_0.Basic.HeadshotsCount);
			base.WriteD(this.playerStatistic_0.Basic.DeathsCount);
			base.WriteD(this.playerStatistic_0.Basic.TotalMatchesCount);
			base.WriteD(this.playerStatistic_0.Basic.TotalKillsCount);
			base.WriteD(this.playerStatistic_0.Basic.EscapesCount);
			base.WriteD(this.playerStatistic_0.Basic.AssistsCount);
			base.WriteD(this.playerStatistic_0.Basic.MvpCount);
		}

		// Token: 0x040000A9 RID: 169
		private readonly PlayerStatistic playerStatistic_0;
	}
}
