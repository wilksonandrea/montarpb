using System;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000CB RID: 203
	public class PROTOCOL_CS_REQUEST_INFO_ACK : GameServerPacket
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x0000412E File Offset: 0x0000232E
		public PROTOCOL_CS_REQUEST_INFO_ACK(long long_0, string string_1)
		{
			this.string_0 = string_1;
			this.account_0 = AccountManager.GetAccount(long_0, 31);
			if (this.account_0 == null || string_1 == null)
			{
				this.uint_0 = 2147483648U;
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00010784 File Offset: 0x0000E984
		public override void Write()
		{
			base.WriteH(821);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
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

		// Token: 0x0400016E RID: 366
		private readonly string string_0;

		// Token: 0x0400016F RID: 367
		private readonly uint uint_0;

		// Token: 0x04000170 RID: 368
		private readonly Account account_0;
	}
}
