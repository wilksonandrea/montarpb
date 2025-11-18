using Plugin.Core.Enums;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_MISSION_ROUND_END_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	private readonly int int_0;

	private readonly RoundEndType roundEndType_0;

	public PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(RoomModel roomModel_1, int int_1, RoundEndType roundEndType_1)
	{
		roomModel_0 = roomModel_1;
		int_0 = int_1;
		roundEndType_0 = roundEndType_1;
	}

	public PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(RoomModel roomModel_1, TeamEnum teamEnum_0, RoundEndType roundEndType_1)
	{
		roomModel_0 = roomModel_1;
		int_0 = (int)teamEnum_0;
		roundEndType_0 = roundEndType_1;
	}

	public override void Write()
	{
		WriteH(5155);
		WriteC((byte)int_0);
		WriteC((byte)roundEndType_0);
		if (roomModel_0.IsDinoMode("DE"))
		{
			WriteH((ushort)roomModel_0.FRDino);
			WriteH((ushort)roomModel_0.CTDino);
		}
		else if (roomModel_0.RoomType != RoomCondition.DeathMatch && roomModel_0.RoomType != RoomCondition.FreeForAll)
		{
			WriteH((ushort)roomModel_0.FRRounds);
			WriteH((ushort)roomModel_0.CTRounds);
		}
		else
		{
			WriteH((ushort)roomModel_0.FRKills);
			WriteH((ushort)roomModel_0.CTKills);
		}
	}
}
