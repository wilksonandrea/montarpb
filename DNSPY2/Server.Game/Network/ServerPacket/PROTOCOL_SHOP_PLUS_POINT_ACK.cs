using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000119 RID: 281
	public class PROTOCOL_SHOP_PLUS_POINT_ACK : GameServerPacket
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x00004B86 File Offset: 0x00002D86
		public PROTOCOL_SHOP_PLUS_POINT_ACK(int int_3, int int_4, int int_5)
		{
			this.int_1 = int_3;
			this.int_0 = int_4;
			this.int_2 = int_5;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00004BA3 File Offset: 0x00002DA3
		public override void Write()
		{
			base.WriteH(1072);
			base.WriteH(0);
			base.WriteC((byte)this.int_2);
			base.WriteD(this.int_0);
			base.WriteD(this.int_1);
		}

		// Token: 0x040001FF RID: 511
		private readonly int int_0;

		// Token: 0x04000200 RID: 512
		private readonly int int_1;

		// Token: 0x04000201 RID: 513
		private readonly int int_2;
	}
}
