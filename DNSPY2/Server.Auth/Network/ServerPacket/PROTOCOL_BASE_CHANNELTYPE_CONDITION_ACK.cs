using System;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000017 RID: 23
	public class PROTOCOL_BASE_CHANNELTYPE_CONDITION_ACK : AuthServerPacket
	{
		// Token: 0x06000061 RID: 97 RVA: 0x0000249C File Offset: 0x0000069C
		public PROTOCOL_BASE_CHANNELTYPE_CONDITION_ACK()
		{
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000265B File Offset: 0x0000085B
		public override void Write()
		{
			base.WriteH(2486);
			base.WriteB(new byte[888]);
		}
	}
}
