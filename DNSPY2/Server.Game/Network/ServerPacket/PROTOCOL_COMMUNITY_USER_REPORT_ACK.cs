using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000023 RID: 35
	public class PROTOCOL_COMMUNITY_USER_REPORT_ACK : GameServerPacket
	{
		// Token: 0x06000082 RID: 130 RVA: 0x000029B7 File Offset: 0x00000BB7
		public PROTOCOL_COMMUNITY_USER_REPORT_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000029C6 File Offset: 0x00000BC6
		public override void Write()
		{
			base.WriteD(3851);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000046 RID: 70
		private readonly uint uint_0;
	}
}
