using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	public PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK(RoomModel roomModel_1)
	{
		roomModel_0 = roomModel_1;
	}

	public override void Write()
	{
		WriteH(2568);
		WriteC(0);
		WriteU(roomModel_0.LeaderName, 66);
		WriteC((byte)roomModel_0.KillTime);
		WriteC((byte)(roomModel_0.Rounds - 1));
		WriteH((ushort)roomModel_0.GetInBattleTime());
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
