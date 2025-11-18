using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK : GameServerPacket
	{
		private readonly VoteKickModel voteKickModel_0;

		private readonly uint uint_0;

		public PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK(uint uint_1, VoteKickModel voteKickModel_1)
		{
			this.uint_0 = uint_1;
			this.voteKickModel_0 = voteKickModel_1;
		}

		public override void Write()
		{
			base.WriteH(3403);
			base.WriteC((byte)this.voteKickModel_0.VictimIdx);
			base.WriteC((byte)this.voteKickModel_0.Accept);
			base.WriteC((byte)this.voteKickModel_0.Denie);
			base.WriteD(this.uint_0);
		}
	}
}