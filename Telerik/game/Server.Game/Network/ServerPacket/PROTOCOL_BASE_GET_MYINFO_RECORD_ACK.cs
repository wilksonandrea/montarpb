using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BASE_GET_MYINFO_RECORD_ACK : GameServerPacket
	{
		private readonly PlayerStatistic playerStatistic_0;

		public PROTOCOL_BASE_GET_MYINFO_RECORD_ACK(PlayerStatistic playerStatistic_1)
		{
			this.playerStatistic_0 = playerStatistic_1;
		}

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
	}
}