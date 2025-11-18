using System;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000024 RID: 36
	public class PROTOCOL_BASE_LOGOUT_ACK : AuthServerPacket
	{
		// Token: 0x06000085 RID: 133 RVA: 0x0000249C File Offset: 0x0000069C
		public PROTOCOL_BASE_LOGOUT_ACK()
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002800 File Offset: 0x00000A00
		public override void Write()
		{
			base.WriteH(2308);
			base.WriteH(0);
		}
	}
}
