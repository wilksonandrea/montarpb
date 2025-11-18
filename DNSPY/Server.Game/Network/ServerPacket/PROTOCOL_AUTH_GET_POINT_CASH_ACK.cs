using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000037 RID: 55
	public class PROTOCOL_AUTH_GET_POINT_CASH_ACK : GameServerPacket
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00002D5B File Offset: 0x00000F5B
		public PROTOCOL_AUTH_GET_POINT_CASH_ACK(uint uint_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000A660 File Offset: 0x00008860
		public override void Write()
		{
			base.WriteH(1058);
			base.WriteD(this.uint_0);
			base.WriteD(this.account_0.Gold);
			base.WriteD(this.account_0.Cash);
			base.WriteD(this.account_0.Tags);
			base.WriteD(0);
		}

		// Token: 0x04000066 RID: 102
		private readonly uint uint_0;

		// Token: 0x04000067 RID: 103
		private readonly Account account_0;
	}
}
