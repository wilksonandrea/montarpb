using System;
using Plugin.Core.Models;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200009A RID: 154
	public class PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK : GameServerPacket
	{
		// Token: 0x0600018E RID: 398 RVA: 0x000039A2 File Offset: 0x00001BA2
		public PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK(ClanModel clanModel_1, Account account_1, int int_1)
		{
			this.clanModel_0 = clanModel_1;
			this.account_0 = account_1;
			this.int_0 = int_1;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000039BF File Offset: 0x00001BBF
		public PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK(ClanModel clanModel_1, int int_1)
		{
			this.clanModel_0 = clanModel_1;
			this.account_0 = AccountManager.GetAccount(clanModel_1.OwnerId, 31);
			this.int_0 = int_1;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000F370 File Offset: 0x0000D570
		public override void Write()
		{
			base.WriteH(824);
			base.WriteD(this.clanModel_0.Id);
			base.WriteU(this.clanModel_0.Name, 34);
			base.WriteC((byte)this.clanModel_0.Rank);
			base.WriteC((byte)this.int_0);
			base.WriteC((byte)this.clanModel_0.MaxPlayers);
			base.WriteD(this.clanModel_0.CreationDate);
			base.WriteD(this.clanModel_0.Logo);
			base.WriteC((byte)this.clanModel_0.NameColor);
			base.WriteC((byte)this.clanModel_0.Effect);
			base.WriteC((byte)this.clanModel_0.GetClanUnit());
			base.WriteD(this.clanModel_0.Exp);
			base.WriteD(10);
			base.WriteQ(this.clanModel_0.OwnerId);
			base.WriteU((this.account_0 != null) ? this.account_0.Nickname : "", 66);
			base.WriteC((byte)((this.account_0 != null) ? this.account_0.NickColor : 0));
			base.WriteC((byte)((this.account_0 != null) ? this.account_0.Rank : 0));
			base.WriteU(this.clanModel_0.Info, 510);
			base.WriteU("Temp", 42);
			base.WriteC((byte)this.clanModel_0.RankLimit);
			base.WriteC((byte)this.clanModel_0.MinAgeLimit);
			base.WriteC((byte)this.clanModel_0.MaxAgeLimit);
			base.WriteC((byte)this.clanModel_0.Authority);
			base.WriteU(this.clanModel_0.News, 510);
			base.WriteD(this.clanModel_0.Matches);
			base.WriteD(this.clanModel_0.MatchWins);
			base.WriteD(this.clanModel_0.MatchLoses);
			base.WriteD(this.clanModel_0.Matches);
			base.WriteD(this.clanModel_0.MatchWins);
			base.WriteD(this.clanModel_0.MatchLoses);
		}

		// Token: 0x04000127 RID: 295
		private readonly ClanModel clanModel_0;

		// Token: 0x04000128 RID: 296
		private readonly Account account_0;

		// Token: 0x04000129 RID: 297
		private readonly int int_0;
	}
}
