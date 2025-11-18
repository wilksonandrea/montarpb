using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK : GameServerPacket
	{
		private readonly ClanModel clanModel_0;

		private readonly Account account_0;

		private readonly int int_0;

		public PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK(ClanModel clanModel_1, Account account_1, int int_1)
		{
			this.clanModel_0 = clanModel_1;
			this.account_0 = account_1;
			this.int_0 = int_1;
		}

		public PROTOCOL_CS_ACCEPT_REQUEST_RESULT_ACK(ClanModel clanModel_1, int int_1)
		{
			this.clanModel_0 = clanModel_1;
			this.account_0 = AccountManager.GetAccount(clanModel_1.OwnerId, 31);
			this.int_0 = int_1;
		}

		public override void Write()
		{
			object nickColor;
			object rank;
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
			base.WriteU((this.account_0 != null ? this.account_0.Nickname : ""), 66);
			if (this.account_0 != null)
			{
				nickColor = this.account_0.NickColor;
			}
			else
			{
				nickColor = null;
			}
			base.WriteC((byte)nickColor);
			if (this.account_0 != null)
			{
				rank = this.account_0.Rank;
			}
			else
			{
				rank = null;
			}
			base.WriteC((byte)rank);
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
	}
}