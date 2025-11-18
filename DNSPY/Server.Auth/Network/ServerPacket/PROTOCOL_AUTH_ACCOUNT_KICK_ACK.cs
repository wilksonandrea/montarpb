using System;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x0200000F RID: 15
	public class PROTOCOL_AUTH_ACCOUNT_KICK_ACK : AuthServerPacket
	{
		// Token: 0x06000050 RID: 80 RVA: 0x0000258D File Offset: 0x0000078D
		public PROTOCOL_AUTH_ACCOUNT_KICK_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000259C File Offset: 0x0000079C
		public override void Write()
		{
			base.WriteH(1989);
			base.WriteC((byte)this.int_0);
		}

		// Token: 0x04000022 RID: 34
		private readonly int int_0;
	}
}
