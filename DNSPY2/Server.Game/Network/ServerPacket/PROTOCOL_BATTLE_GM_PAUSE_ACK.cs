using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000029 RID: 41
	public class PROTOCOL_BATTLE_GM_PAUSE_ACK : GameServerPacket
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00002AE6 File Offset: 0x00000CE6
		public PROTOCOL_BATTLE_GM_PAUSE_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002AF5 File Offset: 0x00000CF5
		public override void Write()
		{
			base.WriteH(5206);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteD(1);
			}
		}

		// Token: 0x04000053 RID: 83
		private readonly uint uint_0;
	}
}
