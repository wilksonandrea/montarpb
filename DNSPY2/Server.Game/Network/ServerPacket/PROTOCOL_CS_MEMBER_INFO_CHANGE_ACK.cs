using System;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000B9 RID: 185
	public class PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK : GameServerPacket
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00003E2E File Offset: 0x0000202E
		public PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(Account account_1)
		{
			this.account_0 = account_1;
			if (account_1 != null)
			{
				this.ulong_0 = ComDiv.GetClanStatus(account_1.Status, account_1.IsOnline);
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00003E57 File Offset: 0x00002057
		public PROTOCOL_CS_MEMBER_INFO_CHANGE_ACK(Account account_1, FriendState friendState_0)
		{
			this.account_0 = account_1;
			if (account_1 != null)
			{
				this.ulong_0 = ((friendState_0 == FriendState.None) ? ComDiv.GetClanStatus(account_1.Status, account_1.IsOnline) : ComDiv.GetClanStatus(friendState_0));
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00003E8B File Offset: 0x0000208B
		public override void Write()
		{
			base.WriteH(851);
			base.WriteQ(this.account_0.PlayerId);
			base.WriteQ(this.ulong_0);
		}

		// Token: 0x04000153 RID: 339
		private readonly Account account_0;

		// Token: 0x04000154 RID: 340
		private readonly ulong ulong_0;
	}
}
