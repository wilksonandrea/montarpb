using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000120 RID: 288
	public class PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_ACK : GameServerPacket
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_ACK()
		{
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00004C86 File Offset: 0x00002E86
		public override void Write()
		{
			base.WriteH(6658);
			base.WriteH(0);
		}
	}
}
