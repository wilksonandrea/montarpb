using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_GIVEUPBATTLE_REQ : GameClientPacket
{
	private bool bool_0;

	private long long_0;

	public override void Read()
	{
		long_0 = ReadD();
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
			if (room == null || room.State < RoomState.LOADING || !room.GetSlot(player.SlotId, out var Slot) || Slot.State < SlotState.LOAD)
			{
				return;
			}
			bool flag = room.IsBotMode();
			AllUtils.FreepassEffect(player, Slot, room, flag);
			if (room.VoteTime.IsTimer() && room.VoteKick != null && room.VoteKick.VictimIdx == Slot.Id)
			{
				room.VoteTime.StopJob();
				room.VoteKick = null;
				using PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK packet = new PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK();
				room.SendPacketToPlayers(packet, SlotState.BATTLE, 0, Slot.Id);
			}
			AllUtils.ResetSlotInfo(room, Slot, UpdateInfo: true);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			SlotModel[] slots = room.Slots;
			foreach (SlotModel slotModel in slots)
			{
				if (slotModel.State < SlotState.LOAD)
				{
					continue;
				}
				if (slotModel.Team == TeamEnum.FR_TEAM)
				{
					num3++;
				}
				else
				{
					num4++;
				}
				if (slotModel.State == SlotState.BATTLE)
				{
					if (slotModel.Team == TeamEnum.FR_TEAM)
					{
						num++;
					}
					else
					{
						num2++;
					}
				}
			}
			if (Slot.Id == room.LeaderSlot)
			{
				if (flag)
				{
					if (num <= 0 && num2 <= 0)
					{
						AllUtils.LeaveHostEndBattlePVE(room, player);
					}
					else
					{
						AllUtils.LeaveHostGiveBattlePVE(room, player);
					}
				}
				else if ((room.State == RoomState.BATTLE && (num == 0 || num2 == 0)) || (room.State <= RoomState.PRE_BATTLE && (num3 == 0 || num4 == 0)))
				{
					AllUtils.LeaveHostEndBattlePVP(room, player, num, num2, out bool_0);
				}
				else
				{
					AllUtils.LeaveHostGiveBattlePVP(room, player);
				}
			}
			else if (!flag)
			{
				if ((room.State == RoomState.BATTLE && (num == 0 || num2 == 0)) || (room.State <= RoomState.PRE_BATTLE && (num3 == 0 || num4 == 0)))
				{
					AllUtils.LeavePlayerEndBattlePVP(room, player, num, num2, out bool_0);
				}
				else
				{
					AllUtils.LeavePlayerQuitBattle(room, player);
				}
			}
			else
			{
				AllUtils.LeavePlayerQuitBattle(room, player);
			}
			Client.SendPacket(new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(player, 0));
			if (!bool_0 && room.State == RoomState.BATTLE)
			{
				AllUtils.BattleEndRoundPlayersCount(room);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_GIVEUPBATTLE_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
