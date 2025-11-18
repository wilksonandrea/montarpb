using System;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200007F RID: 127
	public class PROTOCOL_BATTLE_START_KICKVOTE_ACK : GameServerPacket
	{
		// Token: 0x06000156 RID: 342 RVA: 0x000035DF File Offset: 0x000017DF
		public PROTOCOL_BATTLE_START_KICKVOTE_ACK(VoteKickModel voteKickModel_1)
		{
			this.voteKickModel_0 = voteKickModel_1;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		public override void Write()
		{
			base.WriteH(3399);
			base.WriteC((byte)this.voteKickModel_0.CreatorIdx);
			base.WriteC((byte)this.voteKickModel_0.VictimIdx);
			base.WriteC((byte)this.voteKickModel_0.Motive);
		}

		// Token: 0x040000FC RID: 252
		private readonly VoteKickModel voteKickModel_0;
	}
}
