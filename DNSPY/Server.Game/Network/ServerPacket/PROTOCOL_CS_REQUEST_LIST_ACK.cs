using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000CC RID: 204
	public class PROTOCOL_CS_REQUEST_LIST_ACK : GameServerPacket
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x00004161 File Offset: 0x00002361
		public PROTOCOL_CS_REQUEST_LIST_ACK(int int_3, int int_4, int int_5, byte[] byte_1)
		{
			this.int_0 = int_3;
			this.int_2 = int_4;
			this.int_1 = int_5;
			this.byte_0 = byte_1;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00004186 File Offset: 0x00002386
		public PROTOCOL_CS_REQUEST_LIST_ACK(int int_3)
		{
			this.int_0 = int_3;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00010890 File Offset: 0x0000EA90
		public override void Write()
		{
			base.WriteH(819);
			base.WriteD(this.int_0);
			if (this.int_0 >= 0)
			{
				base.WriteC((byte)this.int_1);
				base.WriteC((byte)this.int_2);
				base.WriteB(this.byte_0);
			}
		}

		// Token: 0x04000171 RID: 369
		private readonly int int_0;

		// Token: 0x04000172 RID: 370
		private readonly int int_1;

		// Token: 0x04000173 RID: 371
		private readonly int int_2;

		// Token: 0x04000174 RID: 372
		private readonly byte[] byte_0;
	}
}
