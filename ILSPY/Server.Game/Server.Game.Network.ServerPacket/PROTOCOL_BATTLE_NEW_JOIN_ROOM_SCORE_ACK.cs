using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK : GameServerPacket
{
	private readonly RoomModel roomModel_0;

	public PROTOCOL_BATTLE_NEW_JOIN_ROOM_SCORE_ACK(RoomModel roomModel_1)
	{
		roomModel_0 = roomModel_1;
	}

	public override void Write()
	{
		WriteH(5263);
		WriteD(method_1());
		WriteD(roomModel_0.GetTimeByMask() * 60 - roomModel_0.GetInBattleTime());
		WriteB(method_0(roomModel_0));
	}

	private byte[] method_0(RoomModel roomModel_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		if (roomModel_1.IsDinoMode("DE"))
		{
			syncServerPacket.WriteD(roomModel_1.FRDino);
			syncServerPacket.WriteD(roomModel_1.CTDino);
		}
		else if (roomModel_1.RoomType == RoomCondition.DeathMatch && !roomModel_1.IsBotMode())
		{
			syncServerPacket.WriteD(roomModel_1.FRKills);
			syncServerPacket.WriteD(roomModel_1.CTKills);
		}
		else if (roomModel_1.RoomType == RoomCondition.FreeForAll)
		{
			syncServerPacket.WriteD(GetSlotKill());
			syncServerPacket.WriteD(0);
		}
		else if (roomModel_1.IsBotMode())
		{
			syncServerPacket.WriteD(roomModel_1.IngameAiLevel);
			syncServerPacket.WriteD(0);
		}
		else
		{
			syncServerPacket.WriteD(roomModel_1.FRRounds);
			syncServerPacket.WriteD(roomModel_1.CTRounds);
		}
		return syncServerPacket.ToArray();
	}

	private int method_1()
	{
		if (roomModel_0.IsBotMode())
		{
			return 3;
		}
		if (roomModel_0.RoomType == RoomCondition.DeathMatch && !roomModel_0.IsBotMode())
		{
			return 1;
		}
		if (roomModel_0.IsDinoMode())
		{
			return 4;
		}
		if (roomModel_0.RoomType == RoomCondition.FreeForAll)
		{
			return 5;
		}
		return 2;
	}

	public int GetSlotKill()
	{
		int[] array = new int[18];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = roomModel_0.Slots[i].AllKills;
		}
		int num = 0;
		for (int j = 0; j < array.Length; j++)
		{
			if (array[j] > array[num])
			{
				num = j;
			}
		}
		return num;
	}
}
