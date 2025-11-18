using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000A6 RID: 166
	public class PROTOCOL_CS_CLIENT_LEAVE_ACK : GameServerPacket
	{
		// Token: 0x060001A9 RID: 425 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_CS_CLIENT_LEAVE_ACK()
		{
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00003BB1 File Offset: 0x00001DB1
		public override void Write()
		{
			base.WriteH(772);
			base.WriteD(0);
		}
	}
}
