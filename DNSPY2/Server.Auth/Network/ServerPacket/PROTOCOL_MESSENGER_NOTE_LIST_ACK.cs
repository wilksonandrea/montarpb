using System;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x0200002F RID: 47
	public class PROTOCOL_MESSENGER_NOTE_LIST_ACK : AuthServerPacket
	{
		// Token: 0x0600009C RID: 156 RVA: 0x0000296A File Offset: 0x00000B6A
		public PROTOCOL_MESSENGER_NOTE_LIST_ACK(int int_2, int int_3, byte[] byte_2, byte[] byte_3)
		{
			this.int_1 = int_2;
			this.int_0 = int_3;
			this.byte_0 = byte_2;
			this.byte_1 = byte_3;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000298F File Offset: 0x00000B8F
		public override void Write()
		{
			base.WriteH(1925);
			base.WriteC((byte)this.int_0);
			base.WriteC((byte)this.int_1);
			base.WriteB(this.byte_0);
			base.WriteB(this.byte_1);
		}

		// Token: 0x04000060 RID: 96
		private readonly int int_0;

		// Token: 0x04000061 RID: 97
		private readonly int int_1;

		// Token: 0x04000062 RID: 98
		private readonly byte[] byte_0;

		// Token: 0x04000063 RID: 99
		private readonly byte[] byte_1;
	}
}
