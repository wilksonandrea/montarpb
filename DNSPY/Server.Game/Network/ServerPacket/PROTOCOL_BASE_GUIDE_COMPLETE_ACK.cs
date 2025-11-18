using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000055 RID: 85
	public class PROTOCOL_BASE_GUIDE_COMPLETE_ACK : GameServerPacket
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_BASE_GUIDE_COMPLETE_ACK()
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000030AE File Offset: 0x000012AE
		public override void Write()
		{
			base.WriteH(2341);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
			base.WriteD(0);
		}
	}
}
