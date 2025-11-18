using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BATTLE_TIMERSYNC_REQ : GameClientPacket
	{
		private float float_0;

		private uint uint_0;

		private int int_0;

		private int int_1;

		private int int_2;

		private int int_3;

		public PROTOCOL_BATTLE_TIMERSYNC_REQ()
		{
		}

		private void method_0(Account account_0, RoomModel roomModel_0, SlotModel slotModel_0, bool bool_0)
		{
			if (bool_0)
			{
				return;
			}
			slotModel_0.Latency = this.int_2;
			slotModel_0.Ping = this.int_0;
			if (slotModel_0.Latency < ConfigLoader.MaxLatency)
			{
				slotModel_0.FailLatencyTimes = 0;
			}
			else
			{
				slotModel_0.FailLatencyTimes++;
			}
			if (ConfigLoader.IsDebugPing && ComDiv.GetDuration(account_0.LastPingDebug) >= (double)ConfigLoader.PingUpdateTimeSeconds)
			{
				account_0.LastPingDebug = DateTimeUtil.Now();
				account_0.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 5, false, string.Format("{0}ms ({1} bar)", this.int_2, this.int_0)));
			}
			if (slotModel_0.FailLatencyTimes < ConfigLoader.MaxRepeatLatency)
			{
				AllUtils.RoomPingSync(roomModel_0);
				return;
			}
			CLogger.Print(string.Format("Player: '{0}' (Id: {1}) kicked due to high latency. ({2}/{3}ms)", new object[] { account_0.Nickname, slotModel_0.PlayerId, slotModel_0.Latency, ConfigLoader.MaxLatency }), LoggerType.Warning, null);
			this.Client.Close(500, true, false);
		}

		private void method_1(RoomModel roomModel_0, bool bool_0)
		{
			int ınt32;
			int ınt321;
			byte[] numArray;
			try
			{
				if (roomModel_0.IsDinoMode(""))
				{
					if (roomModel_0.Rounds == 1)
					{
						roomModel_0.Rounds = 2;
						SlotModel[] slots = roomModel_0.Slots;
						for (int i = 0; i < (int)slots.Length; i++)
						{
							SlotModel slotModel = slots[i];
							if (slotModel.State == SlotState.BATTLE)
							{
								slotModel.KillsOnLife = 0;
								slotModel.LastKillState = 0;
								slotModel.RepeatLastState = false;
							}
						}
						List<int> dinossaurs = AllUtils.GetDinossaurs(roomModel_0, true, -2);
						using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK pROTOCOLBATTLEMISSIONROUNDENDACK = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, 2, RoundEndType.TimeOut))
						{
							using (PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK pROTOCOLBATTLEMISSIONROUNDPRESTARTACK = new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(roomModel_0, dinossaurs))
							{
								roomModel_0.SendPacketToPlayers(pROTOCOLBATTLEMISSIONROUNDENDACK, pROTOCOLBATTLEMISSIONROUNDPRESTARTACK, SlotState.BATTLE, 0);
							}
						}
						roomModel_0.RoundTime.StartJob(5250, (object object_0) => {
							if (roomModel_0.State == RoomState.BATTLE)
							{
								roomModel_0.BattleStart = DateTimeUtil.Now().AddSeconds(5);
								using (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK pROTOCOLBATTLEMISSIONROUNDSTARTACK = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(roomModel_0))
								{
									roomModel_0.SendPacketToPlayers(pROTOCOLBATTLEMISSIONROUNDSTARTACK, SlotState.BATTLE, 0);
								}
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
				}
				else if (roomModel_0.ThisModeHaveRounds())
				{
					TeamEnum teamEnum = TeamEnum.TEAM_DRAW;
					if (roomModel_0.RoomType != RoomCondition.Destroy)
					{
						if (!roomModel_0.SwapRound)
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
					else if (roomModel_0.Bar1 > roomModel_0.Bar2)
					{
						if (!roomModel_0.SwapRound)
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
					else if (roomModel_0.Bar1 >= roomModel_0.Bar2)
					{
						teamEnum = TeamEnum.TEAM_DRAW;
					}
					else if (!roomModel_0.SwapRound)
					{
						teamEnum = TeamEnum.CT_TEAM;
						roomModel_0.CTRounds++;
					}
					else
					{
						teamEnum = TeamEnum.FR_TEAM;
						roomModel_0.FRRounds++;
					}
					AllUtils.BattleEndRound(roomModel_0, teamEnum, RoundEndType.TimeOut);
				}
				else if (roomModel_0.RoomType != RoomCondition.Ace)
				{
					List<Account> allPlayers = roomModel_0.GetAllPlayers(SlotState.READY, 1);
					if (allPlayers.Count > 0)
					{
						TeamEnum winnerTeam = AllUtils.GetWinnerTeam(roomModel_0);
						roomModel_0.CalculateResult(winnerTeam, bool_0);
						using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK pROTOCOLBATTLEMISSIONROUNDENDACK1 = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(roomModel_0, winnerTeam, RoundEndType.TimeOut))
						{
							AllUtils.GetBattleResult(roomModel_0, out ınt32, out ınt321, out numArray);
							byte[] completeBytes = pROTOCOLBATTLEMISSIONROUNDENDACK1.GetCompleteBytes("PROTOCOL_BATTLE_TIMERSYNC_REQ");
							foreach (Account allPlayer in allPlayers)
							{
								if (roomModel_0.Slots[allPlayer.SlotId].State == SlotState.BATTLE)
								{
									allPlayer.SendCompletePacket(completeBytes, pROTOCOLBATTLEMISSIONROUNDENDACK1.GetType().Name);
								}
								allPlayer.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(allPlayer, winnerTeam, ınt321, ınt32, bool_0, numArray));
								AllUtils.UpdateSeasonPass(allPlayer);
							}
						}
					}
					AllUtils.ResetBattleInfo(roomModel_0);
				}
				else
				{
					SlotModel[] slot = new SlotModel[] { roomModel_0.GetSlot(0), roomModel_0.GetSlot(1) };
					if (slot[0] == null || slot[0].State != SlotState.BATTLE || slot[1] == null || slot[1].State != SlotState.BATTLE)
					{
						AllUtils.EndBattleNoPoints(roomModel_0);
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}

		public override void Read()
		{
			this.uint_0 = base.ReadUD();
			this.float_0 = base.ReadT();
			this.int_3 = base.ReadC();
			this.int_0 = base.ReadC();
			this.int_1 = base.ReadC();
			this.int_2 = base.ReadH();
		}

		public override void Run()
		{
			SlotModel slotModel;
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					if (room != null && room.GetSlot(player.SlotId, out slotModel))
					{
						bool flag = room.IsBotMode();
						if (slotModel != null)
						{
							if (slotModel.State != SlotState.BATTLE)
							{
								return;
							}
							if (this.float_0 != 0f || this.int_1 != 0)
							{
								AllUtils.ValidateBanPlayer(player, string.Format("Using an illegal program! ({0}/{1})", this.float_0, this.int_1));
							}
							this.method_0(player, room, slotModel, flag);
							room.TimeRoom = this.uint_0;
							if ((this.uint_0 == 0 || this.uint_0 > -2147483648) && room.Rounds == this.int_3 && room.State == RoomState.BATTLE)
							{
								this.method_1(room, flag);
								goto Label1;
							}
							else
							{
								goto Label1;
							}
						}
						return;
					}
				Label1:
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BATTLE_TIMERSYNC_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}