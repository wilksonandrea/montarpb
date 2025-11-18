using System;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x02000032 RID: 50
	public class PROTOCOL_SERVER_MESSAGE_ERROR_ACK : AuthServerPacket
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x000029F3 File Offset: 0x00000BF3
		public PROTOCOL_SERVER_MESSAGE_ERROR_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002A02 File Offset: 0x00000C02
		public override void Write()
		{
			base.WriteH(3078);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000067 RID: 103
		private readonly uint uint_0;
	}
}
