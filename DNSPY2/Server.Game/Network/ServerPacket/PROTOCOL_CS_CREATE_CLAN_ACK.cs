using System;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000AE RID: 174
	public class PROTOCOL_CS_CREATE_CLAN_ACK : GameServerPacket
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x00003C8C File Offset: 0x00001E8C
		public PROTOCOL_CS_CREATE_CLAN_ACK(uint uint_1, ClanModel clanModel_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.clanModel_0 = clanModel_1;
			this.account_0 = account_1;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000FA38 File Offset: 0x0000DC38
		public override void Write()
		{
			base.WriteH(807);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteD(this.clanModel_0.Id);
				base.WriteU(this.clanModel_0.Name, 34);
				base.WriteC((byte)this.clanModel_0.Rank);
				base.WriteC((byte)DaoManagerSQL.GetClanPlayers(this.clanModel_0.Id));
				base.WriteC((byte)this.clanModel_0.MaxPlayers);
				base.WriteD(this.clanModel_0.CreationDate);
				base.WriteD(this.clanModel_0.Logo);
				base.WriteB(new byte[11]);
				base.WriteQ(this.clanModel_0.OwnerId);
				base.WriteS(this.account_0.Nickname, 66);
				base.WriteC((byte)this.account_0.NickColor);
				base.WriteC((byte)this.account_0.Rank);
				base.WriteU(this.clanModel_0.Info, 510);
				base.WriteU("Temp", 42);
				base.WriteC((byte)this.clanModel_0.RankLimit);
				base.WriteC((byte)this.clanModel_0.MinAgeLimit);
				base.WriteC((byte)this.clanModel_0.MaxAgeLimit);
				base.WriteC((byte)this.clanModel_0.Authority);
				base.WriteU("", 510);
				base.WriteB(new byte[44]);
				base.WriteF((double)this.clanModel_0.Points);
				base.WriteF(60.0);
				base.WriteB(new byte[16]);
				base.WriteF((double)this.clanModel_0.Points);
				base.WriteF(60.0);
				base.WriteB(new byte[80]);
				base.WriteB(new byte[66]);
				base.WriteD(this.account_0.Gold);
			}
		}

		// Token: 0x04000143 RID: 323
		private readonly Account account_0;

		// Token: 0x04000144 RID: 324
		private readonly ClanModel clanModel_0;

		// Token: 0x04000145 RID: 325
		private readonly uint uint_0;
	}
}
