using System;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000BB RID: 187
	public class PROTOCOL_CS_MEMBER_INFO_INSERT_ACK : GameServerPacket
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x00003EDD File Offset: 0x000020DD
		public PROTOCOL_CS_MEMBER_INFO_INSERT_ACK(Account account_1)
		{
			this.account_0 = account_1;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00010434 File Offset: 0x0000E634
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

		// Token: 0x04000156 RID: 342
		private readonly Account account_0;
	}
}
