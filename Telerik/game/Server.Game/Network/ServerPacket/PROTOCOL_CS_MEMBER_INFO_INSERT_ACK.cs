using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_CS_MEMBER_INFO_INSERT_ACK : GameServerPacket
	{
		private readonly Account account_0;

		public PROTOCOL_CS_MEMBER_INFO_INSERT_ACK(Account account_1)
		{
			this.account_0 = account_1;
		}

		public override void Write()
		{
			base.WriteH(847);
			base.WriteC((byte)(this.account_0.Nickname.Length + 1));
			base.WriteN(this.account_0.Nickname, this.account_0.Nickname.Length + 2, "UTF-16LE");
			base.WriteQ(this.account_0.PlayerId);
			base.WriteQ(ComDiv.GetClanStatus(this.account_0.Status, this.account_0.IsOnline));
			base.WriteC((byte)this.account_0.Rank);
			base.WriteC((byte)this.account_0.NickColor);
			base.WriteD(this.account_0.Statistic.Clan.MatchWins);
			base.WriteD(this.account_0.Statistic.Clan.MatchLoses);
			base.WriteD(this.account_0.Equipment.NameCardId);
			base.WriteC((byte)this.account_0.Bonus.NickBorderColor);
			base.WriteD(10);
			base.WriteD(20);
			base.WriteD(30);
		}
	}
}