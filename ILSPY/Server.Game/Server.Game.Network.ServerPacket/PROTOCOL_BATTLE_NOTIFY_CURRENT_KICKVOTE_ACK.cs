using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK : GameServerPacket
{
	private readonly VoteKickModel voteKickModel_0;

	public PROTOCOL_BATTLE_NOTIFY_CURRENT_KICKVOTE_ACK(VoteKickModel voteKickModel_1)
	{
		voteKickModel_0 = voteKickModel_1;
	}

	public override void Write()
	{
		WriteH(3407);
		WriteC((byte)voteKickModel_0.Accept);
		WriteC((byte)voteKickModel_0.Denie);
	}
}
