using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000DC RID: 220
	public class PROTOCOL_LOBBY_GET_ROOMLIST_ACK : GameServerPacket
	{
		// Token: 0x0600021D RID: 541 RVA: 0x00010DCC File Offset: 0x0000EFCC
		public PROTOCOL_LOBBY_GET_ROOMLIST_ACK(int int_6, int int_7, int int_8, int int_9, int int_10, int int_11, byte[] byte_2, byte[] byte_3)
		{
			this.int_3 = int_6;
			this.int_2 = int_7;
			this.int_0 = int_8;
			this.int_1 = int_9;
			this.byte_0 = byte_2;
			this.byte_1 = byte_3;
			this.int_4 = int_10;
			this.int_5 = int_11;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00010E1C File Offset: 0x0000F01C
		public override void Write()
		{
			base.WriteH(2588);
			base.WriteD(this.int_3);
			base.WriteD(this.int_0);
			base.WriteD(this.int_4);
			base.WriteB(this.byte_0);
			base.WriteD(this.int_2);
			base.WriteD(this.int_1);
			base.WriteD(this.int_5);
			base.WriteB(this.byte_1);
		}

		// Token: 0x0400018B RID: 395
		private readonly int int_0;

		// Token: 0x0400018C RID: 396
		private readonly int int_1;

		// Token: 0x0400018D RID: 397
		private readonly int int_2;

		// Token: 0x0400018E RID: 398
		private readonly int int_3;

		// Token: 0x0400018F RID: 399
		private readonly int int_4;

		// Token: 0x04000190 RID: 400
		private readonly int int_5;

		// Token: 0x04000191 RID: 401
		private readonly byte[] byte_0;

		// Token: 0x04000192 RID: 402
		private readonly byte[] byte_1;
	}
}
