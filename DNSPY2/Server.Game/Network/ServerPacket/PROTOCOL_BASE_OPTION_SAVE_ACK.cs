using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000059 RID: 89
	public class PROTOCOL_BASE_OPTION_SAVE_ACK : GameServerPacket
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_BASE_OPTION_SAVE_ACK()
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003147 File Offset: 0x00001347
		public override void Write()
		{
			base.WriteH(2323);
			base.WriteD(0);
		}
	}
}
