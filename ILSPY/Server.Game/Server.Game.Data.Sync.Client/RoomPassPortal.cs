using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Sync.Client;

public static class RoomPassPortal
{
	public static void Load(SyncClientPacket C)
	{
		int id = C.ReadH();
		int ıd = C.ReadH();
		short serverId = C.ReadH();
		int slotIdx = C.ReadC();
		C.ReadC();
		if (C.ToArray().Length > 10)
		{
			CLogger.Print($"Invalid Portal (Length > 10): {C.ToArray().Length}", LoggerType.Warning);
		}
		ChannelModel channel = ChannelsXML.GetChannel(serverId, ıd);
		if (channel == null)
		{
			return;
		}
		RoomModel room = channel.GetRoom(id);
		if (room == null || room.RoundTime.IsTimer() || room.State != RoomState.BATTLE || !room.IsDinoMode("DE"))
		{
			return;
		}
		SlotModel slot = room.GetSlot(slotIdx);
		if (slot == null || slot.State != SlotState.BATTLE)
		{
			return;
		}
		slot.PassSequence++;
		if (slot.Team == TeamEnum.FR_TEAM)
		{
			room.FRDino += 5;
		}
		else
		{
			room.CTDino += 5;
		}
		smethod_0(room, slot);
		using PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK packet = new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_ACK(room, slot);
		using PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK packet2 = new PROTOCOL_BATTLE_MISSION_TOUCHDOWN_COUNT_ACK(room);
		room.SendPacketToPlayers(packet, packet2, SlotState.BATTLE, 0);
	}

	private static void smethod_0(RoomModel roomModel_0, SlotModel slotModel_0)
	{
		MissionType missionType = MissionType.NA;
		if (slotModel_0.PassSequence == 1)
		{
			missionType = MissionType.TOUCHDOWN;
		}
		else if (slotModel_0.PassSequence == 2)
		{
			missionType = MissionType.TOUCHDOWN_ACE_ATTACKER;
		}
		else if (slotModel_0.PassSequence == 3)
		{
			missionType = MissionType.TOUCHDOWN_HATTRICK;
		}
		else if (slotModel_0.PassSequence >= 4)
		{
			missionType = MissionType.TOUCHDOWN_GAME_MAKER;
		}
		if (missionType != 0)
		{
			AllUtils.CompleteMission(roomModel_0, slotModel_0, missionType, 0);
		}
	}
}
