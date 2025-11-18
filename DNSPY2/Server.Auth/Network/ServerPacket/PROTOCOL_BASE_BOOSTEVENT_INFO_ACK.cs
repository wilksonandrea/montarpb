using System;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000015 RID: 21
	public class PROTOCOL_BASE_BOOSTEVENT_INFO_ACK : AuthServerPacket
	{
		// Token: 0x0600005C RID: 92 RVA: 0x0000249C File Offset: 0x0000069C
		public PROTOCOL_BASE_BOOSTEVENT_INFO_ACK()
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000261B File Offset: 0x0000081B
		public override void Write()
		{
			base.WriteH(2469);
			base.WriteD(1);
			base.WriteD(0);
			base.WriteC(0);
		}
	}
}
