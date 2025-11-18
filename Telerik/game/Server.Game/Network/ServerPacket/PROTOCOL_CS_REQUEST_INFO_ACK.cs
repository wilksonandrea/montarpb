using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_REQUEST_INFO_ACK : GameServerPacket
	{
		private readonly string string_0;

		private readonly uint uint_0;

		private readonly Account account_0;

		public PROTOCOL_CS_REQUEST_INFO_ACK(long long_0, string string_1)
		{
			this.string_0 = string_1;
			this.account_0 = AccountManager.GetAccount(long_0, 31);
			if (this.account_0 == null || string_1 == null)
			{
				this.uint_0 = -2147483648;
			}
		}

		public override void Write()
		{
			base.WriteH(821);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0)
			{
				base.WriteQ(this.account_0.PlayerId);
				base.WriteU(this.account_0.Nickname, 66);
				base.WriteC((byte)this.account_0.Rank);
				base.WriteD(this.account_0.Statistic.Basic.KillsCount);
				base.WriteD(this.account_0.Statistic.Basic.DeathsCount);
				base.WriteD(this.account_0.Statistic.Basic.Matches);
				base.WriteD(this.account_0.Statistic.Basic.MatchWins);
				base.WriteD(this.account_0.Statistic.Basic.MatchLoses);
				base.WriteN(this.string_0, this.string_0.Length + 2, "UTF-16LE");
			}
		}
	}
}