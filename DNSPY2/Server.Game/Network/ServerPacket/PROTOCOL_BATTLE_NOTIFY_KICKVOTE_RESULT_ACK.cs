using System;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000074 RID: 116
	public class PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK : GameServerPacket
	{
		// Token: 0x06000138 RID: 312 RVA: 0x000034FC File Offset: 0x000016FC
		public PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK(uint uint_1, VoteKickModel voteKickModel_1)
		{
			this.uint_0 = uint_1;
			this.voteKickModel_0 = voteKickModel_1;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000D774 File Offset: 0x0000B974
		public override void Write()
		{
			base.WriteH(3403);
			base.WriteC((byte)this.voteKickModel_0.VictimIdx);
			base.WriteC((byte)this.voteKickModel_0.Accept);
			base.WriteC((byte)this.voteKickModel_0.Denie);
			base.WriteD(this.uint_0);
		}

		// Token: 0x040000E0 RID: 224
		private readonly VoteKickModel voteKickModel_0;

		// Token: 0x040000E1 RID: 225
		private readonly uint uint_0;
	}
}
