using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	public PROTOCOL_BATTLE_MISSION_TUTORIAL_ROUND_END_ACK(RoomModel roomModel_1)
	{
		roomModel_0 = roomModel_1;
	}

	public override void Write()
	{
		WriteH(5189);
		WriteC(3);
		WriteH((short)(roomModel_0.GetTimeByMask() * 60 - roomModel_0.GetInBattleTime()));
	}
}
