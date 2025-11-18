using System;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x0200000E RID: 14
	public class PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK : AuthServerPacket
	{
		// Token: 0x0600004E RID: 78 RVA: 0x0000249C File Offset: 0x0000069C
		public PROTOCOL_SERVER_MESSAGE_USER_EVENT_START_ACK()
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002580 File Offset: 0x00000780
		public override void Write()
		{
			base.WriteH(3086);
		}
	}
}
