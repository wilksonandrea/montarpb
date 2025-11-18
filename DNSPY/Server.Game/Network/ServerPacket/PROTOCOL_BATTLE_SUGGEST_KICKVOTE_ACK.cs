using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000080 RID: 128
	public class PROTOCOL_BATTLE_SUGGEST_KICKVOTE_ACK : GameServerPacket
	{
		// Token: 0x06000158 RID: 344 RVA: 0x000035EE File Offset: 0x000017EE
		public PROTOCOL_BATTLE_SUGGEST_KICKVOTE_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000035FD File Offset: 0x000017FD
		public override void Write()
		{
			base.WriteH(3397);
			base.WriteD(this.uint_0);
		}

		// Token: 0x040000FD RID: 253
		private readonly uint uint_0;
	}
}
