using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	public PROTOCOL_ROOM_CHANGE_ROOM_OPTIONINFO_ACK(RoomModel roomModel_1)
	{
		roomModel_0 = roomModel_1;
	}

	public override void Write()
	{
		WriteH(3636);
		WriteC(0);
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
