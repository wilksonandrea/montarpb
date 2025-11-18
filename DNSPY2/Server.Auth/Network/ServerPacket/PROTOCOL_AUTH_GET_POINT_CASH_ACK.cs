using System;
using Server.Auth.Data.Models;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000012 RID: 18
	public class PROTOCOL_AUTH_GET_POINT_CASH_ACK : AuthServerPacket
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000025EA File Offset: 0x000007EA
		public PROTOCOL_AUTH_GET_POINT_CASH_ACK(uint uint_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000043F4 File Offset: 0x000025F4
		public override void Write()
		{
			base.WriteH(1058);
			base.WriteD(this.uint_0);
			base.WriteD(this.account_0.Gold);
			base.WriteD(this.account_0.Cash);
			base.WriteD(this.account_0.Tags);
			base.WriteD(0);
		}

		// Token: 0x04000028 RID: 40
		private readonly uint uint_0;

		// Token: 0x04000029 RID: 41
		private readonly Account account_0;
	}
}
