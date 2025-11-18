using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000121 RID: 289
	public class PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_ACK : GameServerPacket
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x00004C9A File Offset: 0x00002E9A
		public PROTOCOL_COMMUNITY_USER_REPORT_CONDITION_CHECK_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00004CA9 File Offset: 0x00002EA9
		public override void Write()
		{
			base.WriteD(3853);
			base.WriteD(this.int_0);
		}

		// Token: 0x04000207 RID: 519
		private readonly int int_0;
	}
}
