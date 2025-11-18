using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	public PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK(RoomModel roomModel_1)
	{
		roomModel_0 = roomModel_1;
	}

	public override void Write()
	{
		WriteH(5173);
		WriteC(roomModel_0.IngameAiLevel);
		for (int i = 0; i < 18; i++)
		{
			WriteD(roomModel_0.Slots[i].AiLevel);
		}
	}
}
