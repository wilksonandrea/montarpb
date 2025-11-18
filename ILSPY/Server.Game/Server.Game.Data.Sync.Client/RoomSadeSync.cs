using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Sync.Client;

public class RoomSadeSync
{
	public static void Load(SyncClientPacket C)
	{
		int id = C.ReadH();
		int ıd = C.ReadH();
		short serverId = C.ReadH();
		byte slotId = C.ReadC();
		ushort bar = C.ReadUH();
		ushort bar2 = C.ReadUH();
		int num = C.ReadC();
		ushort num2 = C.ReadUH();
		if (C.ToArray().Length > 16)
		{
			CLogger.Print($"Invalid Sabotage (Length > 16): {C.ToArray().Length}", LoggerType.Warning);
		}
		ChannelModel channel = ChannelsXML.GetChannel(serverId, ıd);
		if (channel == null)
		{
			return;
		}
		RoomModel room = channel.GetRoom(id);
		if (room == null || room.RoundTime.IsTimer() || room.State != RoomState.BATTLE || !room.GetSlot(slotId, out var Slot))
		{
			return;
		}
		room.Bar1 = bar;
		room.Bar2 = bar2;
		RoomCondition roomType = room.RoomType;
		int num3 = 0;
		switch (num)
		{
		case 1:
			Slot.DamageBar1 += num2;
			num3 += (int)Slot.DamageBar1 / 600;
			break;
		case 2:
			Slot.DamageBar2 += num2;
			num3 += (int)Slot.DamageBar2 / 600;
			break;
		}
		Slot.EarnedEXP = num3;
		switch (roomType)
		{
		case RoomCondition.Destroy:
		{
			using (PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK packet2 = new PROTOCOL_BATTLE_MISSION_GENERATOR_INFO_ACK(room))
			{
				room.SendPacketToPlayers(packet2, SlotState.BATTLE, 0);
			}
			if (room.Bar1 == 0)
			{
				EndRound(room, (!room.SwapRound) ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM);
			}
			else if (room.Bar2 == 0)
			{
				EndRound(room, room.SwapRound ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM);
			}
			break;
		}
		case RoomCondition.Defense:
		{
			using (PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK packet = new PROTOCOL_BATTLE_MISSION_DEFENCE_INFO_ACK(room))
			{
				room.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
			}
			if (room.Bar1 == 0 && room.Bar2 == 0)
			{
				EndRound(room, room.SwapRound ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM);
			}
			break;
		}
		}
	}

	public static void EndRound(RoomModel Room, TeamEnum Winner)
	{
		switch (Winner)
		{
		case TeamEnum.FR_TEAM:
			Room.FRRounds++;
			break;
		case TeamEnum.CT_TEAM:
			Room.CTRounds++;
			break;
		}
		AllUtils.BattleEndRound(Room, Winner, RoundEndType.Normal);
	}
}
