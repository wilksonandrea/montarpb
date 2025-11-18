using System;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000094 RID: 148
	public class PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK : GameServerPacket
	{
		// Token: 0x06000180 RID: 384 RVA: 0x0000EE6C File Offset: 0x0000D06C
		public PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(uint uint_1, ClanModel clanModel_1)
		{
			this.uint_0 = uint_1;
			this.clanModel_0 = clanModel_1;
			if (this.clanModel_0 != null)
			{
				this.int_0 = DaoManagerSQL.GetClanPlayers(clanModel_1.Id);
				this.account_0 = AccountManager.GetAccount(clanModel_1.OwnerId, 31);
				if (this.account_0 == null)
				{
					this.uint_0 = 2147483648U;
				}
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000038F8 File Offset: 0x00001AF8
		public PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000EECC File Offset: 0x0000D0CC
		public override void Write()
		{
			base.WriteH(1570);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteD(this.clanModel_0.Id);
				base.WriteS(this.clanModel_0.Name, 17);
				base.WriteC((byte)this.clanModel_0.Rank);
				base.WriteC((byte)this.int_0);
				base.WriteC((byte)this.clanModel_0.MaxPlayers);
				base.WriteD(this.clanModel_0.CreationDate);
				base.WriteD(this.clanModel_0.Logo);
				base.WriteC((byte)this.clanModel_0.NameColor);
				base.WriteC((byte)this.clanModel_0.GetClanUnit());
				base.WriteD(this.clanModel_0.Exp);
				base.WriteD(0);
				base.WriteQ(this.clanModel_0.OwnerId);
				base.WriteS(this.account_0.Nickname, 33);
				base.WriteC((byte)this.account_0.Rank);
				base.WriteS("", 255);
			}
		}

		// Token: 0x0400011A RID: 282
		private readonly uint uint_0;

		// Token: 0x0400011B RID: 283
		private readonly ClanModel clanModel_0;

		// Token: 0x0400011C RID: 284
		private readonly Account account_0;

		// Token: 0x0400011D RID: 285
		private readonly int int_0;
	}
}
