using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK : GameServerPacket
{
	private readonly VoteKickModel voteKickModel_0;

	private readonly uint uint_0;

	public PROTOCOL_BATTLE_NOTIFY_KICKVOTE_RESULT_ACK(uint uint_1, VoteKickModel voteKickModel_1)
	{
		uint_0 = uint_1;
		voteKickModel_0 = voteKickModel_1;
	}

	public override void Write()
	{
		WriteH(3403);
		WriteC((byte)voteKickModel_0.VictimIdx);
		WriteC((byte)voteKickModel_0.Accept);
		WriteC((byte)voteKickModel_0.Denie);
		WriteD(uint_0);
	}
}
