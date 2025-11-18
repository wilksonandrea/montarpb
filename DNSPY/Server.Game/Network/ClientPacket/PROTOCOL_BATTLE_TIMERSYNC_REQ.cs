using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000175 RID: 373
	public class PROTOCOL_BATTLE_TIMERSYNC_REQ : GameClientPacket
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x0001D890 File Offset: 0x0001BA90
		public override void Read()
		{
			this.uint_0 = base.ReadUD();
			this.float_0 = base.ReadT();
			this.int_3 = (int)base.ReadC();
			this.int_0 = (int)base.ReadC();
			this.int_1 = (int)base.ReadC();
			this.int_2 = (int)base.ReadH();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001D8E8 File Offset: 0x0001BAE8
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null)
				{
					RoomModel room = player.Room;
					SlotModel slotModel;
					if (room != null && room.GetSlot(player.SlotId, out slotModel))
					{
						bool flag = room.IsBotMode();
						if (slotModel != null)
						{
							if (slotModel.State == SlotState.BATTLE)
							{
								if (this.float_0 != 0f || this.int_1 != 0)
								{
									AllUtils.ValidateBanPlayer(player, string.Format("Using an illegal program! ({0}/{1})", this.float_0, this.int_1));
								}
								this.method_0(player, room, slotModel, flag);
								room.TimeRoom = this.uint_0;
								if ((this.uint_0 == 0U || this.uint_0 > 2147483648U) && room.Rounds == this.int_3 && room.State == RoomState.BATTLE)
								{
									this.method_1(room, flag);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BATTLE_TIMERSYNC_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001DA00 File Offset: 0x0001BC00
		private void method_0(Account account_0, RoomModel roomModel_0, SlotModel slotModel_0, bool bool_0)
		{
			if (bool_0)
			{
				return;
			}
			slotModel_0.Latency = this.int_2;
			slotModel_0.Ping = this.int_0;
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
				account_0.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 5, false, string.Format("{0}ms ({1} bar)", this.int_2, this.int_0)));
			}
			if (slotModel_0.FailLatencyTimes >= ConfigLoader.MaxRepeatLatency)
			{
				CLogger.Print(string.Format("Player: '{0}' (Id: {1}) kicked due to high latency. ({2}/{3}ms)", new object[]
				{
					account_0.Nickname,
					slotModel_0.PlayerId,
					slotModel_0.Latency,
					ConfigLoader.MaxLatency
				}), LoggerType.Warning, null);
				this.Client.Close(500, true, false);
				return;
			}
			AllUtils.RoomPingSync(roomModel_0);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001DB18 File Offset: 0x0001BD18
		private void method_1(RoomModel roomModel_0, bool bool_0)
		{
			PROTOCOL_BATTLE_TIMERSYNC_REQ.Class4 @class = new PROTOCOL_BATTLE_TIMERSYNC_REQ.Class4();
			@class.roomModel_0 = roomModel_0;
			try
			{
				if (@class.roomModel_0.IsDinoMode(""))
				{
					if (@class.roomModel_0.Rounds == 1)
					{
						@class.roomModel_0.Rounds = 2;
						foreach (SlotModel slotModel in @class.roomModel_0.Slots)
						{
							if (slotModel.State == SlotState.BATTLE)
							{
								slotModel.KillsOnLife = 0;
								slotModel.LastKillState = 0;
								slotModel.RepeatLastState = false;
							}
						}
						List<int> dinossaurs = AllUtils.GetDinossaurs(@class.roomModel_0, true, -2);
						using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK protocol_BATTLE_MISSION_ROUND_END_ACK = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(@class.roomModel_0, 2, RoundEndType.TimeOut))
						{
							using (PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK protocol_BATTLE_MISSION_ROUND_PRE_START_ACK = new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(@class.roomModel_0, dinossaurs))
							{
								@class.roomModel_0.SendPacketToPlayers(protocol_BATTLE_MISSION_ROUND_END_ACK, protocol_BATTLE_MISSION_ROUND_PRE_START_ACK, SlotState.BATTLE, 0);
							}
						}
						@class.roomModel_0.RoundTime.StartJob(5250, new TimerCallback(@class.method_0));
					}
					else if (@class.roomModel_0.Rounds == 2)
					{
						AllUtils.EndBattle(@class.roomModel_0, bool_0);
					}
				}
				else if (@class.roomModel_0.ThisModeHaveRounds())
				{
					TeamEnum teamEnum;
					if (@class.roomModel_0.RoomType != RoomCondition.Destroy)
					{
						if (@class.roomModel_0.SwapRound)
						{
							teamEnum = TeamEnum.FR_TEAM;
							@class.roomModel_0.FRRounds++;
						}
						else
						{
							teamEnum = TeamEnum.CT_TEAM;
							@class.roomModel_0.CTRounds++;
						}
					}
					else if (@class.roomModel_0.Bar1 > @class.roomModel_0.Bar2)
					{
						if (@class.roomModel_0.SwapRound)
						{
							teamEnum = TeamEnum.CT_TEAM;
							@class.roomModel_0.CTRounds++;
						}
						else
						{
							teamEnum = TeamEnum.FR_TEAM;
							@class.roomModel_0.FRRounds++;
						}
					}
					else if (@class.roomModel_0.Bar1 < @class.roomModel_0.Bar2)
					{
						if (@class.roomModel_0.SwapRound)
						{
							teamEnum = TeamEnum.FR_TEAM;
							@class.roomModel_0.FRRounds++;
						}
						else
						{
							teamEnum = TeamEnum.CT_TEAM;
							@class.roomModel_0.CTRounds++;
						}
					}
					else
					{
						teamEnum = TeamEnum.TEAM_DRAW;
					}
					AllUtils.BattleEndRound(@class.roomModel_0, teamEnum, RoundEndType.TimeOut);
				}
				else if (@class.roomModel_0.RoomType == RoomCondition.Ace)
				{
					SlotModel[] array = new SlotModel[]
					{
						@class.roomModel_0.GetSlot(0),
						@class.roomModel_0.GetSlot(1)
					};
					if (array[0] == null || array[0].State != SlotState.BATTLE || array[1] == null || array[1].State != SlotState.BATTLE)
					{
						AllUtils.EndBattleNoPoints(@class.roomModel_0);
					}
				}
				else
				{
					List<Account> allPlayers = @class.roomModel_0.GetAllPlayers(SlotState.READY, 1);
					if (allPlayers.Count > 0)
					{
						TeamEnum winnerTeam = AllUtils.GetWinnerTeam(@class.roomModel_0);
						@class.roomModel_0.CalculateResult(winnerTeam, bool_0);
						using (PROTOCOL_BATTLE_MISSION_ROUND_END_ACK protocol_BATTLE_MISSION_ROUND_END_ACK2 = new PROTOCOL_BATTLE_MISSION_ROUND_END_ACK(@class.roomModel_0, winnerTeam, RoundEndType.TimeOut))
						{
							int num;
							int num2;
							byte[] array2;
							AllUtils.GetBattleResult(@class.roomModel_0, out num, out num2, out array2);
							byte[] completeBytes = protocol_BATTLE_MISSION_ROUND_END_ACK2.GetCompleteBytes("PROTOCOL_BATTLE_TIMERSYNC_REQ");
							foreach (Account account in allPlayers)
							{
								if (@class.roomModel_0.Slots[account.SlotId].State == SlotState.BATTLE)
								{
									account.SendCompletePacket(completeBytes, protocol_BATTLE_MISSION_ROUND_END_ACK2.GetType().Name);
								}
								account.SendPacket(new PROTOCOL_BATTLE_ENDBATTLE_ACK(account, winnerTeam, num2, num, bool_0, array2));
								AllUtils.UpdateSeasonPass(account);
							}
						}
					}
					AllUtils.ResetBattleInfo(@class.roomModel_0);
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BATTLE_TIMERSYNC_REQ()
		{
		}

		// Token: 0x040002A8 RID: 680
		private float float_0;

		// Token: 0x040002A9 RID: 681
		private uint uint_0;

		// Token: 0x040002AA RID: 682
		private int int_0;

		// Token: 0x040002AB RID: 683
		private int int_1;

		// Token: 0x040002AC RID: 684
		private int int_2;

		// Token: 0x040002AD RID: 685
		private int int_3;

		// Token: 0x02000176 RID: 374
		[CompilerGenerated]
		private sealed class Class4
		{
			// Token: 0x060003C6 RID: 966 RVA: 0x000025DF File Offset: 0x000007DF
			public Class4()
			{
			}

			// Token: 0x060003C7 RID: 967 RVA: 0x0001DF6C File Offset: 0x0001C16C
			internal void method_0(object object_0)
			{
				if (this.roomModel_0.State == RoomState.BATTLE)
				{
					this.roomModel_0.BattleStart = DateTimeUtil.Now().AddSeconds(5.0);
					using (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK protocol_BATTLE_MISSION_ROUND_START_ACK = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(this.roomModel_0))
					{
						this.roomModel_0.SendPacketToPlayers(protocol_BATTLE_MISSION_ROUND_START_ACK, SlotState.BATTLE, 0);
					}
				}
				lock (object_0)
				{
					this.roomModel_0.RoundTime.StopJob();
				}
			}

			// Token: 0x040002AE RID: 686
			public RoomModel roomModel_0;
		}
	}
}
