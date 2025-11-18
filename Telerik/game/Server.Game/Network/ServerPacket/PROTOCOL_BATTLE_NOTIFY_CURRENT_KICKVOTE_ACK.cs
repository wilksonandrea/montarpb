using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK : GameServerPacket
	{
		private readonly VoteKickModel voteKickModel_0;

		public PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK(VoteKickModel voteKickModel_1)
		{
			this.voteKickModel_0 = voteKickModel_1;
		}

		public override void Write()
		{
			base.WriteH(3407);
			base.WriteC((byte)this.voteKickModel_0.Accept);
			base.WriteC((byte)this.voteKickModel_0.Denie);
		}
	}
}