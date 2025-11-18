using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000A9 RID: 169
	public class PROTOCOL_CS_COMMISSION_MASTER_RESULT_ACK : GameServerPacket
	{
		// Token: 0x060001AF RID: 431 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_CS_COMMISSION_MASTER_RESULT_ACK()
		{
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00003C15 File Offset: 0x00001E15
		public override void Write()
		{
			base.WriteH(835);
		}
	}
}
