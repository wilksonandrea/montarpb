using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_CREATE_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	private readonly uint uint_0;

	public PROTOCOL_ROOM_CREATE_ACK(uint uint_1, RoomModel roomModel_1)
	{
		uint_0 = uint_1;
		roomModel_0 = roomModel_1;
	}

	public override void Write()
	{
		WriteH(3593);
		WriteD((uint_0 == 0) ? ((uint)roomModel_0.RoomId) : uint_0);
		if (uint_0 == 0)
		{
			WriteD(roomModel_0.RoomId);
			WriteU(roomModel_0.Name, 46);
			WriteC((byte)roomModel_0.MapId);
			WriteC((byte)roomModel_0.Rule);
			WriteC((byte)roomModel_0.Stage);
			WriteC((byte)roomModel_0.RoomType);
			WriteC((byte)roomModel_0.State);
			WriteC((byte)roomModel_0.GetCountPlayers());
			WriteC((byte)roomModel_0.GetSlotCount());
			WriteC((byte)roomModel_0.Ping);
			WriteH((ushort)roomModel_0.WeaponsFlag);
			WriteD(roomModel_0.GetFlag());
			WriteH(0);
			WriteD(roomModel_0.NewInt);
			WriteH(0);
			WriteU(roomModel_0.LeaderName, 66);
			WriteD(roomModel_0.KillTime);
			WriteC(roomModel_0.Limit);
			WriteC(roomModel_0.WatchRuleFlag);
			WriteH((ushort)roomModel_0.BalanceType);
			WriteB(roomModel_0.RandomMaps);
			WriteC(roomModel_0.CountdownIG);
			WriteB(roomModel_0.LeaderAddr);
			WriteC(roomModel_0.KillCam);
			WriteH(0);
		}
	}
}
