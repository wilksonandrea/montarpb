using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000C5 RID: 197
	public class PROTOCOL_CS_REPLACE_MARK_RESULT_ACK : GameServerPacket
	{
		// Token: 0x060001EB RID: 491 RVA: 0x0000403E File Offset: 0x0000223E
		public PROTOCOL_CS_REPLACE_MARK_RESULT_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000404D File Offset: 0x0000224D
		public override void Write()
		{
			base.WriteH(867);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000166 RID: 358
		private readonly uint uint_0;
	}
}
