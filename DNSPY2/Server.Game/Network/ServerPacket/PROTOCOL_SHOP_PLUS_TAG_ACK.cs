using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200011D RID: 285
	public class PROTOCOL_SHOP_PLUS_TAG_ACK : GameServerPacket
	{
		// Token: 0x060002AF RID: 687 RVA: 0x00004C2E File Offset: 0x00002E2E
		public PROTOCOL_SHOP_PLUS_TAG_ACK(int int_2, int int_3)
		{
			this.int_0 = int_2;
			this.int_1 = int_3;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00004C44 File Offset: 0x00002E44
		public override void Write()
		{
			base.WriteH(3097);
			base.WriteD(this.int_0);
			base.WriteD(this.int_1);
		}

		// Token: 0x04000202 RID: 514
		private readonly int int_0;

		// Token: 0x04000203 RID: 515
		private readonly int int_1;
	}
}
