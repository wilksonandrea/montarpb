using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Sync.Client;

public static class RoomBombC4
{
	public static void Load(SyncClientPacket C)
	{
		int id = C.ReadH();
		int ıd = C.ReadH();
		short serverId = C.ReadH();
		int num = C.ReadC();
		int slotIdx = C.ReadC();
		byte zone = 0;
		ushort unk = 0;
		float x = 0f;
		float y = 0f;
		float z = 0f;
		switch (num)
		{
		case 0:
			zone = C.ReadC();
			x = C.ReadT();
			y = C.ReadT();
			z = C.ReadT();
			unk = C.ReadUH();
			if (C.ToArray().Length > 25)
			{
				CLogger.Print($"Invalid Bomb (Length > 25): {C.ToArray().Length}", LoggerType.Warning);
			}
			break;
		case 1:
			if (C.ToArray().Length > 10)
			{
				CLogger.Print($"Invalid Bomb Type[1] (Length > 10): {C.ToArray().Length}", LoggerType.Warning);
			}
			break;
		}
		ChannelModel channel = ChannelsXML.GetChannel(serverId, ıd);
		if (channel == null)
		{
			return;
		}
		RoomModel room = channel.GetRoom(id);
		if (room == null || room.RoundTime.IsTimer() || room.State != RoomState.BATTLE)
		{
			return;
		}
		SlotModel slot = room.GetSlot(slotIdx);
		if (slot != null && slot.State == SlotState.BATTLE)
		{
			switch (num)
			{
			case 0:
				InstallBomb(room, slot, zone, unk, x, y, z);
				break;
			case 1:
				UninstallBomb(room, slot);
				break;
			}
		}
	}

	public static void InstallBomb(RoomModel Room, SlotModel Slot, byte Zone, ushort Unk, float X, float Y, float Z)
	{
		if (!Room.ActiveC4)
		{
			using (PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK packet = new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(Slot.Id, Zone, Unk, X, Y, Z))
			{
				Room.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
			}
			if (Room.RoomType != RoomCondition.Tutorial)
			{
				Room.ActiveC4 = true;
				Slot.Objects++;
				AllUtils.CompleteMission(Room, Slot, MissionType.C4_PLANT, 0);
				Room.StartBomb();
			}
			else
			{
				Room.ActiveC4 = true;
			}
		}
	}

	public static void UninstallBomb(RoomModel Room, SlotModel Slot)
	{
		if (!Room.ActiveC4)
		{
			return;
		}
		using (PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK packet = new PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK(Slot.Id))
		{
			Room.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
		}
		if (Room.RoomType != RoomCondition.Tutorial)
		{
			Slot.Objects++;
			if (Room.SwapRound)
			{
				Room.FRRounds++;
			}
			else
			{
				Room.CTRounds++;
			}
			AllUtils.CompleteMission(Room, Slot, MissionType.C4_DEFUSE, 0);
			AllUtils.BattleEndRound(Room, (!Room.SwapRound) ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM, RoundEndType.Uninstall);
		}
		else
		{
			Room.ActiveC4 = false;
		}
	}
}
