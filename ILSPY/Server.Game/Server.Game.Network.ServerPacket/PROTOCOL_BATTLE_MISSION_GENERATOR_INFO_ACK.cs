using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	public PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK(RoomModel roomModel_1)
	{
		roomModel_0 = roomModel_1;
	}

	public override void Write()
	{
		WriteH(5167);
		WriteH((ushort)roomModel_0.Bar1);
		WriteH((ushort)roomModel_0.Bar2);
		for (int i = 0; i < 18; i++)
		{
			WriteH(roomModel_0.Slots[i].DamageBar1);
		}
	}
}
