using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.SQL;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK : GameServerPacket
	{
		private readonly ClanModel clanModel_0;

		private readonly int int_0;

		private readonly Account account_0;

		private readonly int int_1;

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

		private byte[] method_0(Account account_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				if (account_1 == null)
				{
					syncServerPacket.WriteC(0);
				}
				else
				{
					syncServerPacket.WriteC((byte)(account_1.Nickname.Length + 1));
					syncServerPacket.WriteN(account_1.Nickname, account_1.Nickname.Length + 2, "UTF-16LE");
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

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
	}
}