using System;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000028 RID: 40
	public class PROTOCOL_BASE_OPTION_SAVE_ACK : AuthServerPacket
	{
		// Token: 0x0600008D RID: 141 RVA: 0x0000249C File Offset: 0x0000069C
		public PROTOCOL_BASE_OPTION_SAVE_ACK()
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000285E File Offset: 0x00000A5E
		public override void Write()
		{
			base.WriteH(2323);
			base.WriteD(0);
		}
	}
}
