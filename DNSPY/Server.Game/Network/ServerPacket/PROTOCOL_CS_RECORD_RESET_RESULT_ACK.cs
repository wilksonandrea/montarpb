using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000C0 RID: 192
	public class PROTOCOL_CS_RECORD_RESET_RESULT_ACK : GameServerPacket
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_CS_RECORD_RESET_RESULT_ACK()
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00003F8F File Offset: 0x0000218F
		public override void Write()
		{
			base.WriteH(907);
		}
	}
}
