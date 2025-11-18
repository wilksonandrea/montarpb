using System.Collections.Generic;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK : GameServerPacket
{
	private readonly List<MatchModel> list_0;

	private readonly int int_0;

	private readonly int int_1;

	public PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK(List<MatchModel> list_1, int int_2)
	{
		int_0 = int_2;
		list_0 = list_1;
		int_1 = list_1.Count - 1;
	}

	public override void Write()
	{
		WriteH(6917);
		WriteH((ushort)int_1);
		if (int_1 <= 0)
		{
			return;
		}
		WriteH(1);
		WriteH(0);
		WriteC((byte)int_1);
		foreach (MatchModel item in list_0)
		{
			if (item.MatchId != int_0)
			{
				WriteH((short)item.MatchId);
				WriteH((short)item.GetServerInfo());
				WriteH((short)item.GetServerInfo());
				WriteC((byte)item.State);
				WriteC((byte)item.FriendId);
				WriteC((byte)item.Training);
				WriteC((byte)item.GetCountPlayers());
				WriteD(item.Leader);
				WriteC(0);
				WriteD(item.Clan.Id);
				WriteC((byte)item.Clan.Rank);
				WriteD(item.Clan.Logo);
				WriteS(item.Clan.Name, 17);
				WriteT(item.Clan.Points);
				WriteC((byte)item.Clan.NameColor);
				Account leader = item.GetLeader();
				if (leader != null)
				{
					WriteC((byte)leader.Rank);
					WriteS(leader.Nickname, 33);
					WriteQ(leader.PlayerId);
					WriteC((byte)item.Slots[item.Leader].State);
				}
				else
				{
					WriteB(new byte[43]);
				}
			}
		}
	}
}
