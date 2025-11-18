using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BASE_QUEST_CHANGE_ACK : GameServerPacket
{
	private readonly int int_0;

	private readonly int int_1;

	public PROTOCOL_BASE_QUEST_CHANGE_ACK(int int_2, MissionCardModel missionCardModel_0)
	{
		int_0 = missionCardModel_0.MissionBasicId;
		if (missionCardModel_0.MissionLimit == int_2)
		{
			int_0 += 240;
		}
		int_1 = int_2;
	}

	public override void Write()
	{
		WriteH(2359);
		WriteC((byte)int_0);
		WriteC((byte)int_1);
	}
}
