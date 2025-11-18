using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200005C RID: 92
	public class PROTOCOL_BASE_USER_ENTER_ACK : GameServerPacket
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00003200 File Offset: 0x00001400
		public PROTOCOL_BASE_USER_ENTER_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000320F File Offset: 0x0000140F
		public override void Write()
		{
			base.WriteH(2331);
			base.WriteD(this.uint_0);
		}

		// Token: 0x040000B6 RID: 182
		private readonly uint uint_0;
	}
}
