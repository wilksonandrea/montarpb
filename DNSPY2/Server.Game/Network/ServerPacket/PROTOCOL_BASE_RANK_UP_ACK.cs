using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200005A RID: 90
	public class PROTOCOL_BASE_RANK_UP_ACK : GameServerPacket
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x0000315B File Offset: 0x0000135B
		public PROTOCOL_BASE_RANK_UP_ACK(int int_2, int int_3)
		{
			this.int_0 = int_2;
			this.int_1 = int_3;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003171 File Offset: 0x00001371
		public override void Write()
		{
			base.WriteH(2343);
			base.WriteD(this.int_0);
			base.WriteD(this.int_0);
			base.WriteD(this.int_1);
		}

		// Token: 0x040000B1 RID: 177
		private readonly int int_0;

		// Token: 0x040000B2 RID: 178
		private readonly int int_1;
	}
}
