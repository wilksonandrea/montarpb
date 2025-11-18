using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000028 RID: 40
	public class PROTOCOL_BATTLEBOX_AUTH_ACK : GameServerPacket
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00002AC9 File Offset: 0x00000CC9
		public PROTOCOL_BATTLEBOX_AUTH_ACK(uint uint_1, Account account_1 = null, int int_1 = 0)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
			this.int_0 = int_1;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000A3C8 File Offset: 0x000085C8
		public override void Write()
		{
			base.WriteH(7430);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteD(this.int_0);
				base.WriteB(new byte[5]);
				base.WriteD(this.account_0.Tags);
			}
		}

		// Token: 0x04000050 RID: 80
		private readonly uint uint_0;

		// Token: 0x04000051 RID: 81
		private readonly Account account_0;

		// Token: 0x04000052 RID: 82
		private readonly int int_0;
	}
}
