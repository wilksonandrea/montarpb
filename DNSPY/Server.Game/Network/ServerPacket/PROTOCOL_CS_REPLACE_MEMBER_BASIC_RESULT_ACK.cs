using System;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000C6 RID: 198
	public class PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK : GameServerPacket
	{
		// Token: 0x060001ED RID: 493 RVA: 0x00004066 File Offset: 0x00002266
		public PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK(Account account_1)
		{
			this.account_0 = account_1;
			this.ulong_0 = ComDiv.GetClanStatus(account_1.Status, account_1.IsOnline);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00010670 File Offset: 0x0000E870
		public override void Write()
		{
			base.WriteH(876);
			base.WriteQ(this.account_0.PlayerId);
			base.WriteU(this.account_0.Nickname, 66);
			base.WriteC((byte)this.account_0.Rank);
			base.WriteC((byte)this.account_0.ClanAccess);
			base.WriteQ(this.ulong_0);
			base.WriteD(this.account_0.ClanDate);
			base.WriteC((byte)this.account_0.NickColor);
			base.WriteD(0);
			base.WriteD(0);
		}

		// Token: 0x04000167 RID: 359
		private readonly Account account_0;

		// Token: 0x04000168 RID: 360
		private readonly ulong ulong_0;
	}
}
