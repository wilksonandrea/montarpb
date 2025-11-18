using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000083 RID: 131
	public class PROTOCOL_BATTLE_VOTE_KICKVOTE_ACK : GameServerPacket
	{
		// Token: 0x0600015E RID: 350 RVA: 0x00003669 File Offset: 0x00001869
		public PROTOCOL_BATTLE_VOTE_KICKVOTE_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00003678 File Offset: 0x00001878
		public override void Write()
		{
			base.WriteH(5195);
			base.WriteD(this.uint_0);
		}

		// Token: 0x040000FF RID: 255
		private readonly uint uint_0;
	}
}
