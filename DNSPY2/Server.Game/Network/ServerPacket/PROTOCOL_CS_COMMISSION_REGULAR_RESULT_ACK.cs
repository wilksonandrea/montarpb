using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000AB RID: 171
	public class PROTOCOL_CS_COMMISSION_REGULAR_RESULT_ACK : GameServerPacket
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_CS_COMMISSION_REGULAR_RESULT_ACK()
		{
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00003C4A File Offset: 0x00001E4A
		public override void Write()
		{
			base.WriteH(841);
		}
	}
}
