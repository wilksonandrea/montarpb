using System;
using System.Collections.Generic;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000B8 RID: 184
	public class PROTOCOL_CS_MEMBER_INFO_ACK : GameServerPacket
	{
		// Token: 0x060001CF RID: 463 RVA: 0x00003E1F File Offset: 0x0000201F
		public PROTOCOL_CS_MEMBER_INFO_ACK(List<Account> list_1)
		{
			this.list_0 = list_1;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000102F0 File Offset: 0x0000E4F0
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
				base.WriteC((byte)account.NickColor);
				base.WriteD(account.Statistic.Clan.MatchWins);
				base.WriteD(account.Statistic.Clan.MatchLoses);
				base.WriteD(account.Equipment.NameCardId);
				base.WriteC((byte)account.Bonus.NickBorderColor);
				base.WriteD(0);
				base.WriteD(0);
				base.WriteD(0);
			}
		}

		// Token: 0x04000152 RID: 338
		private readonly List<Account> list_0;
	}
}
