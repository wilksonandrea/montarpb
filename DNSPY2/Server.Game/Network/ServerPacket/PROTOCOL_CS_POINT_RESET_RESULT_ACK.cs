using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000BF RID: 191
	public class PROTOCOL_CS_POINT_RESET_RESULT_ACK : GameServerPacket
	{
		// Token: 0x060001DF RID: 479 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_CS_POINT_RESET_RESULT_ACK()
		{
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00003F82 File Offset: 0x00002182
		public override void Write()
		{
			base.WriteH(908);
		}
	}
}
