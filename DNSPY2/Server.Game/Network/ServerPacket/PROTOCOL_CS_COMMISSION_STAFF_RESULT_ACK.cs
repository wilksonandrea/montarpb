using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000AD RID: 173
	public class PROTOCOL_CS_COMMISSION_STAFF_RESULT_ACK : GameServerPacket
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_CS_COMMISSION_STAFF_RESULT_ACK()
		{
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00003C7F File Offset: 0x00001E7F
		public override void Write()
		{
			base.WriteH(838);
		}
	}
}
