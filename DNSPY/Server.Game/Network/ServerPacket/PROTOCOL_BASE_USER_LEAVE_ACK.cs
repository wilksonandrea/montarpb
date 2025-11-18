using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200005D RID: 93
	public class PROTOCOL_BASE_USER_LEAVE_ACK : GameServerPacket
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00003228 File Offset: 0x00001428
		public PROTOCOL_BASE_USER_LEAVE_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003237 File Offset: 0x00001437
		public override void Write()
		{
			base.WriteH(2329);
			base.WriteD(this.int_0);
			base.WriteH(0);
		}

		// Token: 0x040000B7 RID: 183
		private readonly int int_0;
	}
}
