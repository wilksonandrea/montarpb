using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200003D RID: 61
	public class PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK : GameServerPacket
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00002DD7 File Offset: 0x00000FD7
		public PROTOCOL_AUTH_SHOP_DELETE_ITEM_ACK(uint uint_1, long long_1 = 0L)
		{
			this.uint_0 = uint_1;
			if (uint_1 == 1U)
			{
				this.long_0 = long_1;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00002DF1 File Offset: 0x00000FF1
		public override void Write()
		{
			base.WriteH(1056);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 1U)
			{
				base.WriteD((int)this.long_0);
			}
		}

		// Token: 0x0400007B RID: 123
		private readonly long long_0;

		// Token: 0x0400007C RID: 124
		private readonly uint uint_0;
	}
}
