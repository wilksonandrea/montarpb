using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x020001F8 RID: 504
	public static class RoomBombC4
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x0002FC24 File Offset: 0x0002DE24
		public static void Load(SyncClientPacket C)
		{
			int num = (int)C.ReadH();
			int num2 = (int)C.ReadH();
			int num3 = (int)C.ReadH();
			int num4 = (int)C.ReadC();
			int num5 = (int)C.ReadC();
			byte b = 0;
			ushort num6 = 0;
			float num7 = 0f;
			float num8 = 0f;
			float num9 = 0f;
			if (num4 == 0)
			{
				b = C.ReadC();
				num7 = C.ReadT();
				num8 = C.ReadT();
				num9 = C.ReadT();
				num6 = C.ReadUH();
				if (C.ToArray().Length > 25)
				{
					CLogger.Print(string.Format("Invalid Bomb (Length > 25): {0}", C.ToArray().Length), LoggerType.Warning, null);
				}
			}
			else if (num4 == 1 && C.ToArray().Length > 10)
			{
				CLogger.Print(string.Format("Invalid Bomb Type[1] (Length > 10): {0}", C.ToArray().Length), LoggerType.Warning, null);
			}
			ChannelModel channel = ChannelsXML.GetChannel(num3, num2);
			if (channel == null)
			{
				return;
			}
			RoomModel room = channel.GetRoom(num);
			if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE)
			{
				SlotModel slot = room.GetSlot(num5);
				if (slot != null)
				{
					if (slot.State == SlotState.BATTLE)
					{
						if (num4 == 0)
						{
							RoomBombC4.InstallBomb(room, slot, b, num6, num7, num8, num9);
							return;
						}
						if (num4 == 1)
						{
							RoomBombC4.UninstallBomb(room, slot);
							return;
						}
						return;
					}
				}
				return;
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0002FD68 File Offset: 0x0002DF68
		public static void InstallBomb(RoomModel Room, SlotModel Slot, byte Zone, ushort Unk, float X, float Y, float Z)
		{
			if (Room.ActiveC4)
			{
				return;
			}
			using (PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK protocol_BATTLE_MISSION_BOMB_INSTALL_ACK = new PROTOCOL_BATTLE_MISSION_BOMB_INSTALL_ACK(Slot.Id, Zone, Unk, X, Y, Z))
			{
				Room.SendPacketToPlayers(protocol_BATTLE_MISSION_BOMB_INSTALL_ACK, SlotState.BATTLE, 0);
			}
			if (Room.RoomType != RoomCondition.Tutorial)
			{
				Room.ActiveC4 = true;
				Slot.Objects++;
				AllUtils.CompleteMission(Room, Slot, MissionType.C4_PLANT, 0);
				Room.StartBomb();
				return;
			}
			Room.ActiveC4 = true;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0002FDF0 File Offset: 0x0002DFF0
		public static void UninstallBomb(RoomModel Room, SlotModel Slot)
		{
			if (!Room.ActiveC4)
			{
				return;
			}
			using (PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK protocol_BATTLE_MISSION_BOMB_UNINSTALL_ACK = new PROTOCOL_BATTLE_MISSION_BOMB_UNINSTALL_ACK(Slot.Id))
			{
				Room.SendPacketToPlayers(protocol_BATTLE_MISSION_BOMB_UNINSTALL_ACK, SlotState.BATTLE, 0);
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
				AllUtils.BattleEndRound(Room, Room.SwapRound ? TeamEnum.FR_TEAM : TeamEnum.CT_TEAM, RoundEndType.Uninstall);
				return;
			}
			Room.ActiveC4 = false;
		}
	}
}
