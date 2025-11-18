using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;
using System;

namespace Server.Game.Data.Sync.Client
{
	public static class RoomBombC4
	{
		public static void InstallBomb(RoomModel Room, SlotModel Slot, byte Zone, ushort Unk, float X, float Y, float Z)
		{
			if (Room.ActiveC4)
			{
				return;
			}
			using (PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK pROTOCOLBATTLEMISSIONBOMBINSTALLACK = new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(Slot.Id, Zone, Unk, X, Y, Z))
			{
				Room.SendPacketToPlayers(pROTOCOLBATTLEMISSIONBOMBINSTALLACK, SlotState.BATTLE, 0);
			}
			if (Room.RoomType == RoomCondition.Tutorial)
			{
				Room.ActiveC4 = true;
				return;
			}
			Room.ActiveC4 = true;
			Slot.Objects++;
			AllUtils.CompleteMission(Room, Slot, MissionType.C4_PLANT, 0);
			Room.StartBomb();
		}

		public static void Load(SyncClientPacket C)
		{
			int ınt32 = C.ReadH();
			int ınt321 = C.ReadH();
			short ınt16 = C.ReadH();
			int ınt322 = C.ReadC();
			int ınt323 = C.ReadC();
			byte num = 0;
			ushort uInt16 = 0;
			float single = 0f;
			float single1 = 0f;
			float single2 = 0f;
			if (ınt322 == 0)
			{
				num = C.ReadC();
				single = C.ReadT();
				single1 = C.ReadT();
				single2 = C.ReadT();
				uInt16 = C.ReadUH();
				if ((int)C.ToArray().Length > 25)
				{
					CLogger.Print(string.Format("Invalid Bomb (Length > 25): {0}", (int)C.ToArray().Length), LoggerType.Warning, null);
				}
			}
			else if (ınt322 == 1 && (int)C.ToArray().Length > 10)
			{
				CLogger.Print(string.Format("Invalid Bomb Type[1] (Length > 10): {0}", (int)C.ToArray().Length), LoggerType.Warning, null);
			}
			ChannelModel channel = ChannelsXML.GetChannel(ınt16, ınt321);
			if (channel == null)
			{
				return;
			}
			RoomModel room = channel.GetRoom(ınt32);
			if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE)
			{
				SlotModel slot = room.GetSlot(ınt323);
				if (slot != null)
				{
					if (slot.State != SlotState.BATTLE)
					{
						return;
					}
					if (ınt322 == 0)
					{
						RoomBombC4.InstallBomb(room, slot, num, uInt16, single, single1, single2);
						return;
					}
					if (ınt322 == 1)
					{
						RoomBombC4.UninstallBomb(room, slot);
						return;
					}
					else
					{
						return;
					}
				}
				return;
			}
		}

		public static void UninstallBomb(RoomModel Room, SlotModel Slot)
		{
			if (!Room.ActiveC4)
			{
				return;
			}
			using (PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK pROTOCOLBATTLEMISSIONBOMBUNINSTALLACK = new PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK(Slot.Id))
			{
				Room.SendPacketToPlayers(pROTOCOLBATTLEMISSIONBOMBUNINSTALLACK, SlotState.BATTLE, 0);
			}
			if (Room.RoomType == RoomCondition.Tutorial)
			{
				Room.ActiveC4 = false;
				return;
			}
			Slot.Objects++;
			if (!Room.SwapRound)
			{
				Room.CTRounds++;
			}
			else
			{
				Room.FRRounds++;
			}
			AllUtils.CompleteMission(Room, Slot, MissionType.C4_DEFUSE, 0);
			AllUtils.BattleEndRound(Room, (Room.SwapRound ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM), RoundEndType.Uninstall);
		}
	}
}