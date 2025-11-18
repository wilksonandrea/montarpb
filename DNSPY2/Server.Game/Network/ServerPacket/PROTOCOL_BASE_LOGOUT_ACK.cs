using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000057 RID: 87
	public class PROTOCOL_BASE_LOGOUT_ACK : GameServerPacket
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00002672 File Offset: 0x00000872
		public PROTOCOL_BASE_LOGOUT_ACK()
		{
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000030FB File Offset: 0x000012FB
		public override void Write()
		{
			base.WriteH(2308);
			base.WriteH(0);
		}
	}
}
