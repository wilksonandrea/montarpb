using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000076 RID: 118
	public class PROTOCOL_BATTLE_READYBATTLE_ACK : GameServerPacket
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00003512 File Offset: 0x00001712
		public PROTOCOL_BATTLE_READYBATTLE_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00003521 File Offset: 0x00001721
		public override void Write()
		{
			base.WriteH(5124);
			base.WriteC(0);
			base.WriteH(0);
			base.WriteD(this.uint_0);
		}

		// Token: 0x040000E8 RID: 232
		private readonly uint uint_0;
	}
}
