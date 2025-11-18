using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	public PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK(RoomModel roomModel_1)
	{
		roomModel_0 = roomModel_1;
	}

	public override void Write()
	{
		WriteH(5185);
		WriteH((ushort)roomModel_0.FRDino);
		WriteH((ushort)roomModel_0.CTDino);
	}
}
