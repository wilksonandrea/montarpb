using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000D2 RID: 210
	public class PROTOCOL_GMCHAT_APPLY_PENALTY_ACK : GameServerPacket
	{
		// Token: 0x06000206 RID: 518 RVA: 0x00004259 File Offset: 0x00002459
		public PROTOCOL_GMCHAT_APPLY_PENALTY_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00004268 File Offset: 0x00002468
		public override void Write()
		{
			base.WriteH(6664);
		}

		// Token: 0x0400017C RID: 380
		private readonly uint uint_0;
	}
}
