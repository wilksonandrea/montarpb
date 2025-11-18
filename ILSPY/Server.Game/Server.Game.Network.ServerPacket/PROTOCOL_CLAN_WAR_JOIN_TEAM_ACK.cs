using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK : GameServerPacket
{
	private readonly MatchModel matchModel_0;

	private readonly uint uint_0;

	public PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(uint uint_1, MatchModel matchModel_1 = null)
	{
		uint_0 = uint_1;
		matchModel_0 = matchModel_1;
	}

	public override void Write()
	{
		WriteH(6921);
		WriteD(uint_0);
		if (uint_0 != 0)
		{
			return;
		}
		WriteH((short)matchModel_0.MatchId);
		WriteH((ushort)matchModel_0.GetServerInfo());
		WriteH((ushort)matchModel_0.GetServerInfo());
		WriteC((byte)matchModel_0.State);
		WriteC((byte)matchModel_0.FriendId);
		WriteC((byte)matchModel_0.Training);
		WriteC((byte)matchModel_0.GetCountPlayers());
		WriteD(matchModel_0.Leader);
		WriteC(0);
		WriteD(matchModel_0.Clan.Id);
		WriteC((byte)matchModel_0.Clan.Rank);
		WriteD(matchModel_0.Clan.Logo);
		WriteS(matchModel_0.Clan.Name, 17);
		WriteT(matchModel_0.Clan.Points);
		WriteC((byte)matchModel_0.Clan.NameColor);
		for (int i = 0; i < matchModel_0.Training; i++)
		{
			SlotMatch slotMatch = matchModel_0.Slots[i];
			Account playerBySlot = matchModel_0.GetPlayerBySlot(slotMatch);
			if (playerBySlot != null)
			{
				WriteC((byte)playerBySlot.Rank);
				WriteS(playerBySlot.Nickname, 33);
				WriteQ(playerBySlot.PlayerId);
				WriteC((byte)slotMatch.State);
			}
			else
			{
				WriteB(new byte[43]);
			}
		}
	}
}
