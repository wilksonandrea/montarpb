using System;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x0200001A RID: 26
	public class PROTOCOL_BASE_GAMEGUARD_ACK : AuthServerPacket
	{
		// Token: 0x06000067 RID: 103 RVA: 0x000026D2 File Offset: 0x000008D2
		public PROTOCOL_BASE_GAMEGUARD_ACK(int int_1, byte[] byte_1)
		{
			this.int_0 = int_1;
			this.byte_0 = byte_1;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000026E8 File Offset: 0x000008E8
		public override void Write()
		{
			base.WriteH(2312);
			base.WriteB(this.byte_0);
			base.WriteD(this.int_0);
		}

		// Token: 0x04000035 RID: 53
		private readonly int int_0;

		// Token: 0x04000036 RID: 54
		private readonly byte[] byte_0;
	}
}
