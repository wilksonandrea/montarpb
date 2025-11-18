using System;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Auth.Data.Models;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x0200002E RID: 46
	public class PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK : AuthServerPacket
	{
		// Token: 0x06000099 RID: 153 RVA: 0x000028E9 File Offset: 0x00000AE9
		public PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(Account account_1)
		{
			this.account_0 = account_1;
			this.ulong_0 = ComDiv.GetClanStatus(account_1.Status, account_1.IsOnline);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000290F File Offset: 0x00000B0F
		public PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(Account account_1, FriendState friendState_0)
		{
			this.account_0 = account_1;
			this.ulong_0 = ((friendState_0 == FriendState.None) ? ComDiv.GetClanStatus(account_1.Status, account_1.IsOnline) : ComDiv.GetClanStatus(friendState_0));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00002940 File Offset: 0x00000B40
		public override void Write()
		{
			base.WriteH(851);
			base.WriteQ(this.account_0.PlayerId);
			base.WriteQ(this.ulong_0);
		}

		// Token: 0x0400005E RID: 94
		private readonly ulong ulong_0;

		// Token: 0x0400005F RID: 95
		private readonly Account account_0;
	}
}
