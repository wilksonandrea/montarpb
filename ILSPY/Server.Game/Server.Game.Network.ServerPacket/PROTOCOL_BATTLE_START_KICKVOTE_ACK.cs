using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_START_KICKVOTE_ACK : GameServerPacket
{
	private readonly VoteKickModel voteKickModel_0;

	public PROTOCOL_BATTLE_START_KICKVOTE_ACK(VoteKickModel voteKickModel_1)
	{
		voteKickModel_0 = voteKickModel_1;
	}

	public override void Write()
	{
		WriteH(3399);
		WriteC((byte)voteKickModel_0.CreatorIdx);
		WriteC((byte)voteKickModel_0.VictimIdx);
		WriteC((byte)voteKickModel_0.Motive);
	}
}
