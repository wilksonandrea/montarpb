using System;
using System.Collections.Generic;
using Plugin.Core.Utility;
using Server.Auth.Data.Models;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x0200002D RID: 45
	public class PROTOCOL_CS_MEMBER_INFO_ACK : AuthServerPacket
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000028DA File Offset: 0x00000ADA
		public PROTOCOL_CS_MEMBER_INFO_ACK(List<Account> list_1)
		{
			this.list_0 = list_1;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00006C9C File Offset: 0x00004E9C
		public override void Write()
		{
			base.WriteH(845);
			base.WriteC((byte)this.list_0.Count);
			foreach (Account account in this.list_0)
			{
				base.WriteC((byte)(account.Nickname.Length + 1));
				base.WriteN(account.Nickname, account.Nickname.Length + 2, "UTF-16LE");
				base.WriteQ(account.PlayerId);
				base.WriteQ(ComDiv.GetClanStatus(account.Status, account.IsOnline));
				base.WriteC((byte)account.Rank);
				base.WriteD(account.Statistic.Clan.MatchWins);
				base.WriteD(account.Statistic.Clan.MatchLoses);
				base.WriteD(account.Equipment.NameCardId);
				base.WriteC((byte)account.Bonus.NickBorderColor);
				base.WriteD(0);
				base.WriteD(0);
				base.WriteD(0);
			}
		}

		// Token: 0x0400005D RID: 93
		private readonly List<Account> list_0;
	}
}
