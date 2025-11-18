using System;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x0200000A RID: 10
	public class PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK : AuthServerPacket
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000024D8 File Offset: 0x000006D8
		public PROTOCOL_BASE_MISSION_CARD_INFO_STREAM_ACK(byte[] byte_2, short short_1, byte byte_3)
		{
			this.byte_0 = byte_2;
			this.short_0 = short_1;
			this.byte_1 = byte_3;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000024F5 File Offset: 0x000006F5
		public override void Write()
		{
			base.WriteH(2517);
			base.WriteH(this.short_0);
			base.WriteC(this.byte_1);
			base.WriteB(this.byte_0);
		}

		// Token: 0x0400001D RID: 29
		private readonly byte[] byte_0;

		// Token: 0x0400001E RID: 30
		private readonly byte byte_1;

		// Token: 0x0400001F RID: 31
		private readonly short short_0;
	}
}
