using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_STARTBATTLE_REQ : GameClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			RoomModel room = player.Room;
			if (room == null || room.GetLeader() == null || !room.GetChannel(out var Channel))
			{
				return;
			}
			if (!room.IsPreparing())
			{
				Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK(EventErrorEnum.BATTLE_FIRST_HOLE));
				Client.SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK());
				room.ChangeSlotState(player.SlotId, SlotState.NORMAL, SendInfo: true);
				return;
			}
			bool flag = room.IsBotMode();
			SlotModel slot = room.GetSlot(player.SlotId);
			if (slot != null && slot.State == SlotState.PRESTART)
			{
				room.ChangeSlotState(slot, SlotState.BATTLE_READY, SendInfo: true);
				slot.StopTiming();
				if (flag)
				{
					Client.SendPacket(new PROTOCOL_BATTLE_CHANGE_DIFFICULTY_LEVEL_ACK(room));
				}
				Client.SendPacket(new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(room, flag));
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				SlotModel[] slots = room.Slots;
				foreach (SlotModel slotModel in slots)
				{
					if (slotModel.State < SlotState.LOAD)
					{
						continue;
					}
					num4++;
					if (slotModel.Team == TeamEnum.FR_TEAM)
					{
						num5++;
					}
					else
					{
						num6++;
					}
					if (slotModel.State >= SlotState.BATTLE_READY)
					{
						num++;
						if (slotModel.Team == TeamEnum.FR_TEAM)
						{
							num3++;
						}
						else
						{
							num2++;
						}
					}
				}
				bool num7 = room.State == RoomState.BATTLE;
				bool flag2 = room.GetSlot(room.LeaderSlot).State >= SlotState.BATTLE_READY && flag && ((room.LeaderSlot % 2 == 0 && num3 > num5 / 2) || (room.LeaderSlot % 2 == 1 && num2 > num6 / 2));
				bool flag3 = room.GetSlot(room.LeaderSlot).State >= SlotState.BATTLE_READY && num2 > num6 / 2 && num3 > num5 / 2;
				bool flag4 = room.GetSlot(room.LeaderSlot).State >= SlotState.BATTLE_READY && room.RoomType == RoomCondition.FreeForAll && num >= 2 && num4 >= 2;
				bool flag5 = Channel.Type == ChannelType.Clan && num == num3 + num2;
				bool flag6 = room.Competitive && num == num3 + num2;
				if (num7 || flag2 || flag3 || flag4 || flag5 || flag6)
				{
					if (flag5)
					{
						CLogger.Print($"Starting Clan War Match with '{num}' players. FR: {num3} CT: {num2}", LoggerType.Warning);
					}
					if (flag6)
					{
						CLogger.Print($"Starting Competitive Match with '{num}' players. FR: {num3} CT: {num2}", LoggerType.Warning);
					}
					room.SpawnReadyPlayers();
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK(EventErrorEnum.BATTLE_FIRST_HOLE));
				Client.SendPacket(new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(player, 0));
				room.ChangeSlotState(slot, SlotState.NORMAL, SendInfo: true);
				AllUtils.BattleEndPlayersCount(room, flag);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_STARTBATTLE_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
