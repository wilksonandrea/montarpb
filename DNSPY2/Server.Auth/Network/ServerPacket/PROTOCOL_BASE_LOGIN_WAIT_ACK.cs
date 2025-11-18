using System;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000023 RID: 35
	public class PROTOCOL_BASE_LOGIN_WAIT_ACK : AuthServerPacket
	{
		// Token: 0x06000083 RID: 131 RVA: 0x000027C9 File Offset: 0x000009C9
		public PROTOCOL_BASE_LOGIN_WAIT_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000027D8 File Offset: 0x000009D8
		public override void Write()
		{
			base.WriteH(2313);
			base.WriteC(3);
			base.WriteH(68);
			base.WriteD(this.int_0);
		}

		// Token: 0x04000053 RID: 83
		private readonly int int_0;
	}
}
