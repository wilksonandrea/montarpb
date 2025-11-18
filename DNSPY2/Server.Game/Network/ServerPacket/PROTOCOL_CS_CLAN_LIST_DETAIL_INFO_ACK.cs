using System;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000A1 RID: 161
	public class PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK : GameServerPacket
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00003ADD File Offset: 0x00001CDD
		public PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK(int int_2, ClanModel clanModel_1)
		{
			this.int_0 = int_2;
			this.clanModel_0 = clanModel_1;
			if (clanModel_1 != null)
			{
				this.account_0 = AccountManager.GetAccount(clanModel_1.OwnerId, 31);
				this.int_1 = DaoManagerSQL.GetClanPlayers(clanModel_1.Id);
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000F670 File Offset: 0x0000D870
		public override void Write()
		{
			base.WriteH(1000);
			base.WriteH(0);
			base.WriteD(0);
			base.WriteD((int)this.clanModel_0.Points);
			base.WriteC(0);
			base.WriteD(0);
			base.WriteD(this.clanModel_0.MatchLoses);
			base.WriteD(this.clanModel_0.MatchWins);
			base.WriteD(this.clanModel_0.Matches);
			base.WriteC((byte)this.clanModel_0.MaxPlayers);
			base.WriteC((byte)this.int_1);
			base.WriteC((byte)this.clanModel_0.GetClanUnit());
			base.WriteB(this.method_0(this.account_0));
			base.WriteD(this.clanModel_0.Exp);
			base.WriteC((byte)this.clanModel_0.Rank);
			base.WriteQ(0L);
			base.WriteC(0);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000F768 File Offset: 0x0000D968
		private byte[] method_0(Account account_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (account_1 != null)
				{
					syncServerPacket.WriteC((byte)(account_1.Nickname.Length + 1));
					syncServerPacket.WriteN(account_1.Nickname, account_1.Nickname.Length + 2, "UTF-16LE");
				}
				else
				{
					syncServerPacket.WriteC(0);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x04000133 RID: 307
		private readonly ClanModel clanModel_0;

		// Token: 0x04000134 RID: 308
		private readonly int int_0;

		// Token: 0x04000135 RID: 309
		private readonly Account account_0;

		// Token: 0x04000136 RID: 310
		private readonly int int_1;
	}
}
