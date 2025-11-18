using System;
using Plugin.Core.Utility;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000031 RID: 49
	public class PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK : AuthServerPacket
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x000029DD File Offset: 0x00000BDD
		public PROTOCOL_SERVER_MESSAGE_DISCONNECTIONSUCCESS_ACK(uint uint_1, bool bool_1)
		{
			this.uint_0 = uint_1;
			this.bool_0 = bool_1;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00006E30 File Offset: 0x00005030
		public override void Write()
		{
			base.WriteH(3074);
			base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
			base.WriteD(this.uint_0);
			base.WriteD((this.bool_0 > false) ? 1 : 0);
			if (this.bool_0)
			{
				base.WriteD(0);
			}
		}

		// Token: 0x04000065 RID: 101
		private readonly uint uint_0;

		// Token: 0x04000066 RID: 102
		private readonly bool bool_0;
	}
}
