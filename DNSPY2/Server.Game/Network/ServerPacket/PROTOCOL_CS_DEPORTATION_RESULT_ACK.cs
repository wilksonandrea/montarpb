using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000B2 RID: 178
	public class PROTOCOL_CS_DEPORTATION_RESULT_ACK : GameServerPacket
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_CS_DEPORTATION_RESULT_ACK()
		{
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00003D1D File Offset: 0x00001F1D
		public override void Write()
		{
			base.WriteH(832);
		}
	}
}
