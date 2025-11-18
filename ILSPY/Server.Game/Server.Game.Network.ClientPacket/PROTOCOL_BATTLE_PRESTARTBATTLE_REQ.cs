using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_PRESTARTBATTLE_REQ : GameClientPacket
{
	private StageOptions stageOptions_0;

	private MapRules mapRules_0;

	private MapIdEnum mapIdEnum_0;

	private RoomCondition roomCondition_0;

	public override void Read()
	{
		mapIdEnum_0 = (MapIdEnum)ReadC();
		mapRules_0 = (MapRules)ReadC();
		stageOptions_0 = (StageOptions)ReadC();
		roomCondition_0 = (RoomCondition)ReadC();
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
			if (room == null)
			{
				return;
			}
			if (room.Stage == stageOptions_0 && room.RoomType == roomCondition_0 && room.MapId == mapIdEnum_0 && room.Rule == mapRules_0)
			{
				SlotModel slot = room.GetSlot(player.SlotId);
				if (slot != null && room.IsPreparing() && room.UdpServer != null && slot.State >= SlotState.LOAD)
				{
					Account leader = room.GetLeader();
					if (leader == null)
					{
						Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK(EventErrorEnum.BATTLE_FIRST_HOLE));
						Client.SendPacket(new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(player, 0));
						room.ChangeSlotState(slot, SlotState.NORMAL, SendInfo: true);
						AllUtils.BattleEndPlayersCount(room, room.IsBotMode());
						slot.StopTiming();
						return;
					}
					if (string.IsNullOrEmpty(player.PublicIP.ToString()))
					{
						Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK(EventErrorEnum.BATTLE_NO_REAL_IP));
						Client.SendPacket(new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(player, 0));
						room.ChangeSlotState(slot, SlotState.NORMAL, SendInfo: true);
						AllUtils.BattleEndPlayersCount(room, room.IsBotMode());
						slot.StopTiming();
						return;
					}
					slot.PreStartDate = DateTimeUtil.Now();
					if (slot.Id == room.LeaderSlot)
					{
						room.State = RoomState.PRE_BATTLE;
						room.UpdateRoomInfo();
					}
					room.ChangeSlotState(slot, SlotState.PRESTART, SendInfo: true);
					Client.SendPacket(new PROTOCOL_BATTLE_PRESTARTBATTLE_ACK(player, bool_2: true));
					if (slot.Id != room.LeaderSlot)
					{
						leader.SendPacket(new PROTOCOL_BATTLE_PRESTARTBATTLE_ACK(player, bool_2: false));
					}
					room.StartCounter(1, player, slot);
				}
				else
				{
					Client.SendPacket(new PROTOCOL_BATTLE_STARTBATTLE_ACK());
					room.ChangeSlotState(slot, SlotState.NORMAL, SendInfo: true);
					AllUtils.BattleEndPlayersCount(room, room.IsBotMode());
					slot.StopTiming();
				}
			}
			else
			{
				Client.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK(EventErrorEnum.BATTLE_FIRST_MAINLOAD));
				Client.SendPacket(new PROTOCOL_BATTLE_PRESTARTBATTLE_ACK());
				room.ChangeSlotState(player.SlotId, SlotState.NORMAL, SendInfo: true);
				AllUtils.BattleEndPlayersCount(room, room.IsBotMode());
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_PRESTARTBATTLE_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
