using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200002A RID: 42
	public class PROTOCOL_BATTLE_GM_RESUME_ACK : GameServerPacket
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00002B1D File Offset: 0x00000D1D
		public PROTOCOL_BATTLE_GM_RESUME_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002B2C File Offset: 0x00000D2C
		public override void Write()
		{
			base.WriteH(5270);
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000054 RID: 84
		private readonly uint uint_0;
	}
}
