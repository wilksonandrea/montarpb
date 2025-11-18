using System;
using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000053 RID: 83
	public class PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK : GameServerPacket
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x0000306B File Offset: 0x0000126B
		public PROTOCOL_BASE_GET_RECORD_INFO_DB_ACK(Account account_0)
		{
			this.playerStatistic_0 = account_0.Statistic;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000BCFC File Offset: 0x00009EFC
		public override void Write()
		{
			base.WriteH(2351);
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

		// Token: 0x040000AA RID: 170
		private readonly PlayerStatistic playerStatistic_0;
	}
}
