using System;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000071 RID: 113
	public class PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK : GameServerPacket
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00003478 File Offset: 0x00001678
		public PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK(VoteKickModel voteKickModel_1)
		{
			this.voteKickModel_0 = voteKickModel_1;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00003487 File Offset: 0x00001687
		public override void Write()
		{
			base.WriteH(3407);
			base.WriteC((byte)this.voteKickModel_0.Accept);
			base.WriteC((byte)this.voteKickModel_0.Denie);
		}

		// Token: 0x040000DE RID: 222
		private readonly VoteKickModel voteKickModel_0;
	}
}
