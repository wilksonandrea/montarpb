using System;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x0200002C RID: 44
	public class PROTOCOL_BASE_USER_LEAVE_ACK : AuthServerPacket
	{
		// Token: 0x06000095 RID: 149 RVA: 0x000028AB File Offset: 0x00000AAB
		public PROTOCOL_BASE_USER_LEAVE_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000028BA File Offset: 0x00000ABA
		public override void Write()
		{
			base.WriteH(2329);
			base.WriteD(this.int_0);
			base.WriteH(0);
		}

		// Token: 0x0400005C RID: 92
		private readonly int int_0;
	}
}
