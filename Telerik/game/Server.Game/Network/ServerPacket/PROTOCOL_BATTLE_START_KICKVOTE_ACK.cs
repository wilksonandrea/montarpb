using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_START_KICKVOTE_ACK : GameServerPacket
	{
		private readonly VoteKickModel voteKickModel_0;

		public PROTOCOL_BATTLE_START_KICKVOTE_ACK(VoteKickModel voteKickModel_1)
		{
			this.voteKickModel_0 = voteKickModel_1;
		}

		public override void Write()
		{
			base.WriteH(3399);
			base.WriteC((byte)this.voteKickModel_0.CreatorIdx);
			base.WriteC((byte)this.voteKickModel_0.VictimIdx);
			base.WriteC((byte)this.voteKickModel_0.Motive);
		}
	}
}