using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_TIMERSYNC_REQ : GameClientPacket
{
	[CompilerGenerated]
	private sealed class Class4
	{
		public RoomModel roomModel_0;

		internal void method_0(object object_0)
		{
			if (roomModel_0.State == RoomState.BATTLE)
			{
				roomModel_0.BattleStart = DateTimeUtil.Now().AddSeconds(5.0);
				using PROTOCOL_BATTLE_MISSION_ROUND_START_ACK packet = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(roomModel_0);
				roomModel_0.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
			}
			lock (object_0)
			{
				roomModel_0.RoundTime.StopJob();
			}
		}
	}

	private float float_0;

	private uint uint_0;

	private int int_0;

	private int int_1;

	private int int_2;

	private int int_3;

	public override void Read()
	{
		uint_0 = ReadUD();
		float_0 = ReadT();
		int_3 = ReadC();
		int_0 = ReadC();
		int_1 = ReadC();
		int_2 = ReadH();
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
			if (room == null || !room.GetSlot(player.SlotId, out var Slot))
			{
				return;
			}
			bool bool_ = room.IsBotMode();
			if (Slot != null && Slot.State == SlotState.BATTLE)
			{
				if (float_0 != 0f || int_1 != 0)
				{
					AllUtils.ValidateBanPlayer(player, $"Using an illegal program! ({float_0}/{int_1})");
				}
				method_0(player, room, Slot, bool_);
				room.TimeRoom = uint_0;
				if ((uint_0 == 0 || uint_0 > 2147483648u) && room.Rounds == int_3 && room.State == RoomState.BATTLE)
				{
					method_1(room, bool_);
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_TIMERSYNC_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}

	private void method_0(Account account_0, RoomModel roomModel_0, SlotModel slotModel_0, bool bool_0)
	{
		if (!bool_0)
		{
			slotModel_0.Latency = int_2;
			slotModel_0.Ping = int_0;
			if (slotModel_0.Latency >= ConfigLoader.MaxLatency)
			{
				slotModel_0.FailLatencyTimes++;
			}
			else
			{
				slotModel_0.FailLatencyTimes = 0;
			}
			if (ConfigLoader.IsDebugPing && ComDiv.GetDuration(account_0.LastPingDebug) >= (double)ConfigLoader.PingUpdateTimeSeconds)
			{
				account_0.LastPingDebug = DateTimeUtil.Now();
				account_0.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 5, bool_1: false, $"{int_2}ms ({int_0} bar)"));
			}
			if (slotModel_0.FailLatencyTimes >= ConfigLoader.MaxRepeatLatency)
			{
				CLogger.Print($"Player: '{account_0.Nickname}' (Id: {slotModel_0.PlayerId}) kicked due to high latency. ({slotModel_0.Latency}/{ConfigLoader.MaxLatency}ms)", LoggerType.Warning);
				Client.Close(500, DestroyConnection: true);
			}
			else
			{
				AllUtils.RoomPingSync(roomModel_0);
			}
		}
	}

	private void method_1(RoomModel roomModel_0, bool bool_0)
	{
		try
		{
			if (roomModel_0.IsDinoMode())
			{
				if (roomModel_0.Rounds == 1)
				{
					roomModel_0.Rounds = 2;
					SlotModel[] slots = roomModel_0.Slots;
					foreach (SlotModel slotModel in slots)
					{
						if (slotModel.State == SlotState.BATTLE)
						{
							slotModel.KillsOnLife = 0;
							slotModel.LastKillState = 0;
							slotModel.RepeatLastState = false;
						}
					}
					List<int> dinossaurs = AllUtils.GetDinossaurs(roomModel_0, ForceNewTRex: true, -2);
					using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK packet = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, 2, RoundEndType.TimeOut))
					{
						using PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK packet2 = new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(roomModel_0, dinossaurs);
						roomModel_0.SendPacketToPlayers(packet, packet2, SlotState.BATTLE, 0);
					}
					roomModel_0.RoundTime.StartJob(5250, delegate(object object_0)
					{
						if (roomModel_0.State == RoomState.BATTLE)
						{
							roomModel_0.BattleStart = DateTimeUtil.Now().AddSeconds(5.0);
							using PROTOCOL_BATTLE_MISSION_ROUND_START_ACK packet3 = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(roomModel_0);
							roomModel_0.SendPacketToPlayers(packet3, SlotState.BATTLE, 0);
						}
						lock (object_0)
						{
							roomModel_0.RoundTime.StopJob();
						}
					});
				}
				else if (roomModel_0.Rounds == 2)
				{
					AllUtils.EndBattle(roomModel_0, bool_0);
				}
				return;
			}
			if (roomModel_0.ThisModeHaveRounds())
			{
				TeamEnum teamEnum = TeamEnum.TEAM_DRAW;
				if (roomModel_0.RoomType != RoomCondition.Destroy)
				{
					if (roomModel_0.SwapRound)
					{
						teamEnum = TeamEnum.FR_TEAM;
						roomModel_0.FRRounds++;
					}
					else
					{
						teamEnum = TeamEnum.CT_TEAM;
						roomModel_0.CTRounds++;
					}
				}
				else if (roomModel_0.Bar1 > roomModel_0.Bar2)
				{
					if (roomModel_0.SwapRound)
					{
						teamEnum = TeamEnum.CT_TEAM;
						roomModel_0.CTRounds++;
					}
					else
					{
						teamEnum = TeamEnum.FR_TEAM;
						roomModel_0.FRRounds++;
					}
				}
				else if (roomModel_0.Bar1 < roomModel_0.Bar2)
				{
					if (roomModel_0.SwapRound)
					{
						teamEnum = TeamEnum.FR_TEAM;
						roomModel_0.FRRounds++;
					}
					else
					{
						teamEnum = TeamEnum.CT_TEAM;
						roomModel_0.CTRounds++;
					}
				}
				else
				{
					teamEnum = TeamEnum.TEAM_DRAW;
				}
				AllUtils.BattleEndRound(roomModel_0, teamEnum, RoundEndType.TimeOut);
				return;
			}
			if (roomModel_0.RoomType == RoomCondition.Ace)
			{
				SlotModel[] array = new SlotModel[2]
				{
					roomModel_0.GetSlot(0),
					roomModel_0.GetSlot(1)
				};
				if (array[0] == null || array[0].State != SlotState.BATTLE || array[1] == null || array[1].State != SlotState.BATTLE)
				{
					AllUtils.EndBattleNoPoints(roomModel_0);
				}
				return;
			}
			List<Account> allPlayers = roomModel_0.GetAllPlayers(SlotState.READY, 1);
			if (allPlayers.Count > 0)
			{
				TeamEnum winnerTeam = AllUtils.GetWinnerTeam(roomModel_0);
				roomModel_0.CalculateResult(winnerTeam, bool_0);
				using PROTOCOL_BATTLE_MISSION_ROUND_END_ACK pROTOCOL_BATTLE_MISSION_ROUND_END_ACK = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, winnerTeam, RoundEndType.TimeOut);
				AllUtils.GetBattleResult(roomModel_0, out var MissionFlag, out var SlotFlag, out var Data);
				byte[] completeBytes = pROTOCOL_BATTLE_MISSION_ROUND_END_ACK.GetCompleteBytes("PROTOCOL_BATTLE_TIMERSYNC_REQ");
				foreach (Account item in allPlayers)
				{
					if (roomModel_0.Slots[item.SlotId].State == SlotState.BATTLE)
					{
						item.SendCompletePacket(completeBytes, pROTOCOL_BATTLE_MISSION_ROUND_END_ACK.GetType().Name);
					}
					item.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(item, winnerTeam, SlotFlag, MissionFlag, bool_0, Data));
					AllUtils.UpdateSeasonPass(item);
				}
			}
			AllUtils.ResetBattleInfo(roomModel_0);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
