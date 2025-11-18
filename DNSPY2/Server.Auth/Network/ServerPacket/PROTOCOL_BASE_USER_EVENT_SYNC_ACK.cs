using System;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x0200002A RID: 42
	public class PROTOCOL_BASE_USER_EVENT_SYNC_ACK : AuthServerPacket
	{
		// Token: 0x06000091 RID: 145 RVA: 0x0000249C File Offset: 0x0000069C
		public PROTOCOL_BASE_USER_EVENT_SYNC_ACK()
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002881 File Offset: 0x00000A81
		public override void Write()
		{
			base.WriteH(2473);
			base.WriteH(0);
		}
	}
}
