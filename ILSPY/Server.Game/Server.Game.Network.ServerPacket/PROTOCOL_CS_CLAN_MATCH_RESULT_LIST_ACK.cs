using System.Collections.Generic;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK : GameServerPacket
{
	private readonly List<MatchModel> list_0;

	private readonly int int_0;

	public PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK(int int_1, List<MatchModel> list_1)
	{
		int_0 = int_1;
		list_0 = list_1;
	}

	public override void Write()
	{
		WriteH(1957);
		WriteC((byte)((int_0 == 0) ? list_0.Count : int_0));
		if (int_0 > 0 || list_0.Count == 0)
		{
			return;
		}
		WriteC(1);
		WriteC(0);
		WriteC((byte)list_0.Count);
		for (int i = 0; i < list_0.Count; i++)
		{
			MatchModel matchModel = list_0[i];
			WriteH((short)matchModel.MatchId);
			WriteH((ushort)matchModel.GetServerInfo());
			WriteH((ushort)matchModel.GetServerInfo());
			WriteC((byte)matchModel.State);
			WriteC((byte)matchModel.FriendId);
			WriteC((byte)matchModel.Training);
			WriteC((byte)matchModel.GetCountPlayers());
			WriteC(0);
			WriteD(matchModel.Leader);
			Account leader = matchModel.GetLeader();
			if (leader != null)
			{
				WriteC((byte)leader.Rank);
				WriteU(leader.Nickname, 66);
				WriteQ(leader.PlayerId);
				WriteC((byte)matchModel.Slots[matchModel.Leader].State);
			}
			else
			{
				WriteB(new byte[76]);
			}
		}
	}
}
