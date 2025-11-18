using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_QUEST_GET_INFO_ACK : GameServerPacket
{
	private readonly Account account_0;

	public PROTOCOL_BASE_QUEST_GET_INFO_ACK(Account account_1)
	{
		account_0 = account_1;
	}

	public override void Write()
	{
		WriteH(2355);
		WriteC((byte)account_0.Mission.ActualMission);
		WriteC((byte)account_0.Mission.ActualMission);
		WriteC((byte)account_0.Mission.Card1);
		WriteC((byte)account_0.Mission.Card2);
		WriteC((byte)account_0.Mission.Card3);
		WriteC((byte)account_0.Mission.Card4);
		WriteB(ComDiv.GetMissionCardFlags(account_0.Mission.Mission1, account_0.Mission.List1));
		WriteB(ComDiv.GetMissionCardFlags(account_0.Mission.Mission2, account_0.Mission.List2));
		WriteB(ComDiv.GetMissionCardFlags(account_0.Mission.Mission3, account_0.Mission.List3));
		WriteB(ComDiv.GetMissionCardFlags(account_0.Mission.Mission4, account_0.Mission.List4));
		WriteC((byte)account_0.Mission.Mission1);
		WriteC((byte)account_0.Mission.Mission2);
		WriteC((byte)account_0.Mission.Mission3);
		WriteC((byte)account_0.Mission.Mission4);
	}
}
