using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK : GameServerPacket
{
	private readonly MatchModel matchModel_0;

	public PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(MatchModel matchModel_1)
	{
		matchModel_0 = matchModel_1;
	}

	public override void Write()
	{
		WriteH(6939);
		WriteH((short)matchModel_0.GetServerInfo());
		WriteC((byte)matchModel_0.State);
		WriteC((byte)matchModel_0.FriendId);
		WriteC((byte)matchModel_0.Training);
		WriteC((byte)matchModel_0.GetCountPlayers());
		WriteD(matchModel_0.Leader);
		WriteC(0);
		SlotMatch[] slots = matchModel_0.Slots;
		foreach (SlotMatch slotMatch in slots)
		{
			Account playerBySlot = matchModel_0.GetPlayerBySlot(slotMatch);
			if (playerBySlot != null)
			{
				WriteC((byte)playerBySlot.Rank);
				WriteS(playerBySlot.Nickname, 33);
				WriteQ(slotMatch.PlayerId);
				WriteC((byte)slotMatch.State);
			}
			else
			{
				WriteB(new byte[43]);
			}
		}
	}
}
