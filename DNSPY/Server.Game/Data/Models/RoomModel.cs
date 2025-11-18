using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Game.Data.Managers;
using Server.Game.Data.Sync.Server;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Models
{
	// Token: 0x02000204 RID: 516
	public class RoomModel
	{
		// Token: 0x0600065E RID: 1630 RVA: 0x00032558 File Offset: 0x00030758
		public RoomModel(int int_0, ChannelModel channelModel_0)
		{
			this.RoomId = int_0;
			for (int i = 0; i < this.Slots.Length; i++)
			{
				this.Slots[i] = new SlotModel(i);
			}
			this.ChannelId = channelModel_0.Id;
			this.ChannelType = channelModel_0.Type;
			this.ServerId = channelModel_0.ServerId;
			this.Rounds = 1;
			this.AiCount = 1;
			this.AiLevel = 1;
			this.TRex = -1;
			this.Ping = 5;
			this.Name = "";
			this.Password = "";
			this.MapName = "";
			this.LeaderName = "";
			this.method_0();
			DateTime dateTime = DateTimeUtil.Now();
			this.LastChangeTeam = dateTime;
			this.LastPingSync = dateTime;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00005ECF File Offset: 0x000040CF
		private void method_0()
		{
			this.UniqueRoomId = (uint)(((this.ServerId & 255) << 20) | ((this.ChannelId & 255) << 12) | (this.RoomId & 4095));
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00005F03 File Offset: 0x00004103
		public void GenerateSeed()
		{
			this.Seed = (uint)(((uint)(this.MapId & (MapIdEnum)255) << 20) | (MapIdEnum)((uint)(this.Rule & (MapRules)255) << 12) | (MapIdEnum)(this.RoomType & (RoomCondition)4095));
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00032780 File Offset: 0x00030980
		public bool ThisModeHaveCD()
		{
			return this.RoomType == RoomCondition.Bomb || this.RoomType == RoomCondition.Annihilation || this.RoomType == RoomCondition.Boss || this.RoomType == RoomCondition.CrossCounter || this.RoomType == RoomCondition.Destroy || this.RoomType == RoomCondition.Ace || this.RoomType == RoomCondition.Escape || this.RoomType == RoomCondition.Glass;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x000327E0 File Offset: 0x000309E0
		public bool ThisModeHaveRounds()
		{
			return this.RoomType == RoomCondition.Bomb || this.RoomType == RoomCondition.Destroy || this.RoomType == RoomCondition.Annihilation || this.RoomType == RoomCondition.Defense || this.RoomType == RoomCondition.Destroy || this.RoomType == RoomCondition.Ace || this.RoomType == RoomCondition.Escape || this.RoomType == RoomCondition.Glass;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00032840 File Offset: 0x00030A40
		public int GetFlag()
		{
			int num = 0;
			if (this.Flag.HasFlag(RoomStageFlag.TEAM_SWAP))
			{
				num++;
			}
			if (this.Flag.HasFlag(RoomStageFlag.RANDOM_MAP))
			{
				num += 2;
			}
			if (this.Flag.HasFlag(RoomStageFlag.PASSWORD) || this.Password.Length > 0)
			{
				num += 4;
			}
			if (this.Flag.HasFlag(RoomStageFlag.OBSERVER_MODE))
			{
				num += 8;
			}
			if (this.Flag.HasFlag(RoomStageFlag.REAL_IP))
			{
				num += 16;
			}
			if (this.Flag.HasFlag(RoomStageFlag.TEAM_BALANCE) || this.BalanceType == TeamBalance.Count)
			{
				num += 32;
			}
			if (this.Flag.HasFlag(RoomStageFlag.OBSERVER))
			{
				num += 64;
			}
			if (this.Flag.HasFlag(RoomStageFlag.INTER_ENTER) || (this.Limit > 0 && this.IsStartingMatch()))
			{
				num += 128;
			}
			this.Flag = (RoomStageFlag)num;
			return num;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00005F37 File Offset: 0x00004137
		public bool IsBotMode()
		{
			return this.Stage == StageOptions.AI || this.Stage == StageOptions.DieHard || this.Stage == StageOptions.Infection;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00032970 File Offset: 0x00030B70
		public void SetBotLevel()
		{
			if (!this.IsBotMode())
			{
				return;
			}
			this.IngameAiLevel = this.AiLevel;
			for (int i = 0; i < 18; i++)
			{
				this.Slots[i].AiLevel = (int)this.IngameAiLevel;
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x000329B4 File Offset: 0x00030BB4
		private void method_1()
		{
			if (this.RoomType == RoomCondition.Defense)
			{
				if (this.MapId == MapIdEnum.BlackPanther)
				{
					this.Bar1 = 6000;
					this.Bar2 = 9000;
					return;
				}
			}
			else if (this.RoomType == RoomCondition.Destroy)
			{
				if (this.MapId == MapIdEnum.Helispot)
				{
					this.Bar1 = 12000;
					this.Bar2 = 12000;
					return;
				}
				if (this.MapId == MapIdEnum.BreakDown)
				{
					this.Bar1 = 6000;
					this.Bar2 = 6000;
				}
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00005F56 File Offset: 0x00004156
		public void ChangeRounds()
		{
			this.Rounds++;
			if (this.method_2() && !this.SwapRound)
			{
				this.SwapRound = true;
			}
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00032A38 File Offset: 0x00030C38
		private bool method_2()
		{
			if (!this.Flag.HasFlag(RoomStageFlag.TEAM_SWAP))
			{
				return false;
			}
			if (this.IsDinoMode(""))
			{
				return this.Rounds == 2;
			}
			int num = this.GetRoundsByMask() - 1;
			return (this.FRRounds == num && this.CTRounds == 0) || (this.CTRounds == num && this.FRRounds == 0);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00032AA4 File Offset: 0x00030CA4
		public int GetInBattleTime()
		{
			int num = 0;
			if (this.BattleStart != default(DateTime) && (this.State == RoomState.BATTLE || this.State == RoomState.PRE_BATTLE))
			{
				num = (int)ComDiv.GetDuration(this.BattleStart);
				if (num < 0)
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00005F7D File Offset: 0x0000417D
		public int GetInBattleTimeLeft()
		{
			return this.GetTimeByMask() * 60 - this.GetInBattleTime();
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00005F8F File Offset: 0x0000418F
		public ChannelModel GetChannel()
		{
			return ChannelsXML.GetChannel(this.ServerId, this.ChannelId);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00005FA2 File Offset: 0x000041A2
		public bool GetChannel(out ChannelModel Channel)
		{
			Channel = ChannelsXML.GetChannel(this.ServerId, this.ChannelId);
			return Channel != null;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00032AF0 File Offset: 0x00030CF0
		public bool GetSlot(int SlotId, out SlotModel Slot)
		{
			Slot = null;
			SlotModel[] slots = this.Slots;
			bool flag2;
			lock (slots)
			{
				if (SlotId >= 0 && SlotId <= 17)
				{
					Slot = this.Slots[SlotId];
				}
				flag2 = Slot != null;
			}
			return flag2;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00032B48 File Offset: 0x00030D48
		public SlotModel GetSlot(int SlotIdx)
		{
			SlotModel[] slots = this.Slots;
			SlotModel slotModel;
			lock (slots)
			{
				if (SlotIdx >= 0 && SlotIdx <= 17)
				{
					slotModel = this.Slots[SlotIdx];
				}
				else
				{
					slotModel = null;
				}
			}
			return slotModel;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00032B9C File Offset: 0x00030D9C
		public void StartCounter(int Type, Account Player, SlotModel Slot)
		{
			RoomModel.Class12 @class = new RoomModel.Class12();
			@class.slotModel_0 = Slot;
			@class.roomModel_0 = this;
			@class.account_0 = Player;
			int num = 0;
			@class.eventErrorEnum_0 = EventErrorEnum.SUCCESS;
			if (Type == 0)
			{
				@class.eventErrorEnum_0 = (EventErrorEnum)2147487754U;
				num = 90000;
			}
			else if (Type == 1)
			{
				@class.eventErrorEnum_0 = (EventErrorEnum)2147487755U;
				num = 30000;
			}
			if (num > 0)
			{
				@class.slotModel_0.FirstInactivityOff = true;
			}
			@class.slotModel_0.Timing.StartJob(num, new TimerCallback(@class.method_0));
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00005FBC File Offset: 0x000041BC
		private void method_3(EventErrorEnum eventErrorEnum_0, Account account_0, SlotModel slotModel_0)
		{
			account_0.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK(eventErrorEnum_0));
			account_0.SendPacket(new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(account_0, 0));
			this.ChangeSlotState(slotModel_0.Id, SlotState.NORMAL, true);
			AllUtils.BattleEndPlayersCount(this, this.IsBotMode());
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00005FF2 File Offset: 0x000041F2
		public void StartBomb()
		{
			this.BombTime.StartJob(42000, new TimerCallback(this.method_19));
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00006010 File Offset: 0x00004210
		public void StartVote()
		{
			if (this.VoteKick == null)
			{
				return;
			}
			this.VoteTime.StartJob(20000, new TimerCallback(this.method_20));
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00032C28 File Offset: 0x00030E28
		public void RoundRestart()
		{
			this.StopBomb();
			foreach (SlotModel slotModel in this.Slots)
			{
				if (slotModel.PlayerId > 0L && slotModel.State == SlotState.BATTLE)
				{
					if (!slotModel.DeathState.HasFlag(DeadEnum.UseChat))
					{
						slotModel.DeathState |= DeadEnum.UseChat;
					}
					if (slotModel.Spectator)
					{
						slotModel.Spectator = false;
					}
					if (slotModel.KillsOnLife >= 3 && (this.RoomType == RoomCondition.Annihilation || this.RoomType == RoomCondition.Ace))
					{
						slotModel.Objects++;
					}
					slotModel.KillsOnLife = 0;
					slotModel.LastKillState = 0;
					slotModel.RepeatLastState = false;
					slotModel.DamageBar1 = 0;
					slotModel.DamageBar2 = 0;
				}
			}
			this.RoundTime.StartJob(8250, new TimerCallback(this.method_21));
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00032D1C File Offset: 0x00030F1C
		private void method_4()
		{
			foreach (SlotModel slotModel in this.Slots)
			{
				if (slotModel.PlayerId > 0L)
				{
					if (!slotModel.DeathState.HasFlag(DeadEnum.UseChat))
					{
						slotModel.DeathState |= DeadEnum.UseChat;
					}
					if (slotModel.Spectator)
					{
						slotModel.Spectator = false;
					}
				}
			}
			this.StopBomb();
			DateTime dateTime = DateTimeUtil.Now();
			if (this.State == RoomState.BATTLE)
			{
				this.BattleStart = (this.IsDinoMode("") ? dateTime.AddSeconds(5.0) : dateTime);
			}
			List<int> dinossaurs = AllUtils.GetDinossaurs(this, false, -1);
			using (PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK protocol_BATTLE_MISSION_ROUND_PRE_START_ACK = new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(this, dinossaurs))
			{
				this.SendPacketToPlayers(protocol_BATTLE_MISSION_ROUND_PRE_START_ACK, SlotState.BATTLE, 0);
			}
			if (this.method_2())
			{
				this.RoundTeamSwap.StartJob(5250, new TimerCallback(this.method_22));
			}
			else
			{
				this.method_5();
			}
			this.StopBomb();
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00032E38 File Offset: 0x00031038
		private void method_5()
		{
			using (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK protocol_BATTLE_MISSION_ROUND_START_ACK = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(this))
			{
				this.SendPacketToPlayers(protocol_BATTLE_MISSION_ROUND_START_ACK, SlotState.BATTLE, 0);
			}
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00006037 File Offset: 0x00004237
		public void StopBomb()
		{
			if (!this.ActiveC4)
			{
				return;
			}
			this.ActiveC4 = false;
			if (this.BombTime != null)
			{
				this.BombTime.StopJob();
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00032E74 File Offset: 0x00031074
		public void StartBattle(bool UpdateInfo)
		{
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				this.State = RoomState.LOADING;
				this.RequestRoomMaster.Clear();
				this.SetBotLevel();
				AllUtils.CheckClanMatchRestrict(this);
				this.StartTick = DateTimeUtil.Now().Ticks;
				this.StartDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
				using (PROTOCOL_BATTLE_START_GAME_ACK protocol_BATTLE_START_GAME_ACK = new PROTOCOL_BATTLE_START_GAME_ACK(this))
				{
					byte[] completeBytes = protocol_BATTLE_START_GAME_ACK.GetCompleteBytes("Room.StartBattle");
					foreach (Account account in this.GetAllPlayers(SlotState.READY, 0))
					{
						SlotModel slot = this.GetSlot(account.SlotId);
						if (slot != null)
						{
							slot.WithHost = true;
							slot.State = SlotState.LOAD;
							slot.SetMissionsClone(account.Mission);
							account.SendCompletePacket(completeBytes, protocol_BATTLE_START_GAME_ACK.GetType().Name);
						}
					}
				}
				if (UpdateInfo)
				{
					this.UpdateSlotsInfo();
				}
				this.UpdateRoomInfo();
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00032FB8 File Offset: 0x000311B8
		public void StartCountDown()
		{
			using (PROTOCOL_BATTLE_START_COUNTDOWN_ACK protocol_BATTLE_START_COUNTDOWN_ACK = new PROTOCOL_BATTLE_START_COUNTDOWN_ACK(CountDownEnum.Start))
			{
				this.SendPacketToPlayers(protocol_BATTLE_START_COUNTDOWN_ACK);
			}
			this.CountdownTime.StartJob(5250, new TimerCallback(this.method_23));
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0003300C File Offset: 0x0003120C
		private void method_6()
		{
			if (this.Slots[this.LeaderSlot].State == SlotState.READY && this.State == RoomState.COUNTDOWN)
			{
				this.StartBattle(true);
				return;
			}
			using (PROTOCOL_BATTLE_READYBATTLE_ACK protocol_BATTLE_READYBATTLE_ACK = new PROTOCOL_BATTLE_READYBATTLE_ACK(2147487752U))
			{
				byte[] completeBytes = protocol_BATTLE_READYBATTLE_ACK.GetCompleteBytes("Room.ReadyBattle");
				foreach (Account account in this.GetAllPlayers(SlotState.READY, 0))
				{
					SlotModel slot = this.GetSlot(account.SlotId);
					if (slot != null && slot.State == SlotState.READY)
					{
						account.SendCompletePacket(completeBytes, protocol_BATTLE_READYBATTLE_ACK.GetType().Name);
					}
				}
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x000330E0 File Offset: 0x000312E0
		public void StopCountDown(CountDownEnum Type, bool RefreshRoom = true)
		{
			this.State = RoomState.READY;
			if (RefreshRoom)
			{
				this.UpdateRoomInfo();
			}
			this.CountdownTime.StopJob();
			using (PROTOCOL_BATTLE_START_COUNTDOWN_ACK protocol_BATTLE_START_COUNTDOWN_ACK = new PROTOCOL_BATTLE_START_COUNTDOWN_ACK(Type))
			{
				this.SendPacketToPlayers(protocol_BATTLE_START_COUNTDOWN_ACK);
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00033134 File Offset: 0x00031334
		public void StopCountDown(int SlotId)
		{
			if (this.State != RoomState.COUNTDOWN)
			{
				return;
			}
			if (SlotId == this.LeaderSlot)
			{
				this.StopCountDown(CountDownEnum.StopByHost, true);
				return;
			}
			if (this.GetPlayingPlayers((this.LeaderSlot % 2 == 0) ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM, SlotState.READY, 0) == 0)
			{
				this.ChangeSlotState(this.LeaderSlot, SlotState.NORMAL, false);
				this.StopCountDown(CountDownEnum.StopByPlayer, true);
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00033194 File Offset: 0x00031394
		public void CalculateResult()
		{
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				this.method_7(AllUtils.GetWinnerTeam(this), this.IsBotMode());
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000331E0 File Offset: 0x000313E0
		public void CalculateResult(TeamEnum resultType)
		{
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				this.method_7(resultType, this.IsBotMode());
			}
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00033228 File Offset: 0x00031428
		public void CalculateResult(TeamEnum resultType, bool isBotMode)
		{
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				this.method_7(resultType, isBotMode);
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0003326C File Offset: 0x0003146C
		public void CalculateResultFreeForAll(int SlotWin)
		{
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				this.method_7((TeamEnum)SlotWin, false);
			}
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000332B0 File Offset: 0x000314B0
		private void method_7(TeamEnum teamEnum_0, bool bool_0)
		{
			ServerConfig config = GameXender.Client.Config;
			EventRankUpModel runningEvent = EventRankUpXML.GetRunningEvent();
			EventBoostModel runningEvent2 = EventBoostXML.GetRunningEvent();
			EventPlaytimeModel runningEvent3 = EventPlaytimeXML.GetRunningEvent();
			BattlePassModel activeSeasonPass = SeasonChallengeXML.GetActiveSeasonPass();
			DateTime dateTime = DateTimeUtil.Now();
			int[] array = new int[18];
			int num = 0;
			if (config == null)
			{
				CLogger.Print("Server Config Null. RoomResult canceled.", LoggerType.Warning, null);
				return;
			}
			List<SlotModel> list = new List<SlotModel>();
			for (int i = 0; i < 18; i++)
			{
				SlotModel slotModel = this.Slots[i];
				if (slotModel.PlayerId != 0L)
				{
					array[i] = slotModel.AllKills;
				}
				else
				{
					array[i] = 0;
				}
				if (array[i] > array[num])
				{
					num = i;
				}
				Account account;
				if (!slotModel.Check && slotModel.State == SlotState.BATTLE && this.GetPlayerBySlot(slotModel, out account))
				{
					StatisticTotal basic = account.Statistic.Basic;
					StatisticSeason season = account.Statistic.Season;
					StatisticDaily daily = account.Statistic.Daily;
					StatisticWeapon weapon = account.Statistic.Weapon;
					DBQuery dbquery = new DBQuery();
					DBQuery dbquery2 = new DBQuery();
					DBQuery dbquery3 = new DBQuery();
					DBQuery dbquery4 = new DBQuery();
					DBQuery dbquery5 = new DBQuery();
					slotModel.Check = true;
					double num2 = slotModel.InBattleTime(dateTime);
					int gold = account.Gold;
					int exp = account.Exp;
					int cash = account.Cash;
					if (!bool_0)
					{
						if (config.Missions)
						{
							AllUtils.EndMatchMission(this, account, slotModel, teamEnum_0);
							if (slotModel.MissionsCompleted)
							{
								account.Mission = slotModel.Missions;
								DaoManagerSQL.UpdateCurrentPlayerMissionList(account.PlayerId, account.Mission);
							}
							AllUtils.GenerateMissionAwards(account, dbquery);
						}
						int num3 = ((slotModel.AllKills != 0 || slotModel.AllDeaths != 0) ? ((int)num2) : ((int)(num2 / 3.0)));
						if (this.RoomType == RoomCondition.Bomb || this.RoomType == RoomCondition.Annihilation)
						{
							goto IL_2A0;
						}
						if (this.RoomType == RoomCondition.Ace)
						{
							goto IL_2A0;
						}
						slotModel.Exp = (int)((double)slotModel.Score + (double)num3 / 2.5 + (double)slotModel.AllDeaths * 1.8 + (double)(slotModel.Objects * 20));
						slotModel.Gold = (int)((double)slotModel.Score + (double)num3 / 3.0 + (double)slotModel.AllDeaths * 1.8 + (double)(slotModel.Objects * 20));
						slotModel.Cash = (int)((double)slotModel.Score / 1.5 + (double)num3 / 4.5 + (double)slotModel.AllDeaths * 1.1 + (double)(slotModel.Objects * 20));
						IL_359:
						bool flag = slotModel.Team == teamEnum_0;
						if (this.Rule != MapRules.Chaos && this.Rule != MapRules.HeadHunter)
						{
							if (basic != null && season != null)
							{
								this.method_9(account, basic, season, slotModel, dbquery2, dbquery3, num, flag, (int)teamEnum_0);
							}
							if (daily != null)
							{
								this.method_10(account, daily, slotModel, dbquery4, num, flag, (int)teamEnum_0);
							}
							if (weapon != null)
							{
								this.method_11(account, weapon, slotModel, dbquery5);
							}
						}
						if (flag || (this.RoomType == RoomCondition.FreeForAll && teamEnum_0 == (TeamEnum)num))
						{
							slotModel.Gold += ComDiv.Percentage(slotModel.Gold, 15);
							slotModel.Exp += ComDiv.Percentage(slotModel.Exp, 20);
						}
						if (slotModel.EarnedEXP > 0)
						{
							slotModel.Exp += slotModel.EarnedEXP * 5;
							goto IL_47B;
						}
						goto IL_47B;
						IL_2A0:
						slotModel.Exp = (int)((double)slotModel.Score + (double)num3 / 2.5 + (double)slotModel.AllDeaths * 2.2 + (double)(slotModel.Objects * 20));
						slotModel.Gold = (int)((double)slotModel.Score + (double)num3 / 3.0 + (double)slotModel.AllDeaths * 2.2 + (double)(slotModel.Objects * 20));
						slotModel.Cash = (int)((double)(slotModel.Score / 2) + (double)num3 / 6.5 + (double)slotModel.AllDeaths * 1.5 + (double)(slotModel.Objects * 10));
						goto IL_359;
					}
					int num4 = (int)this.IngameAiLevel * (150 + slotModel.AllDeaths);
					if (num4 == 0)
					{
						num4++;
					}
					int num5 = slotModel.Score / num4;
					slotModel.Gold += num5;
					slotModel.Exp += num5;
					IL_47B:
					slotModel.Exp = ((slotModel.Exp > ConfigLoader.MaxExpReward) ? ConfigLoader.MaxExpReward : slotModel.Exp);
					slotModel.Gold = ((slotModel.Gold > ConfigLoader.MaxGoldReward) ? ConfigLoader.MaxGoldReward : slotModel.Gold);
					slotModel.Cash = ((slotModel.Cash > ConfigLoader.MaxCashReward) ? ConfigLoader.MaxCashReward : slotModel.Cash);
					if (this.RoomType == RoomCondition.Ace)
					{
						if (account.SlotId < 0 || account.SlotId > 1)
						{
							slotModel.Exp = 0;
							slotModel.Gold = 0;
							slotModel.Cash = 0;
						}
					}
					else if (slotModel.Exp < 0 || slotModel.Gold < 0 || slotModel.Cash < 0)
					{
						slotModel.Exp = 2;
						slotModel.Gold = 2;
						slotModel.Cash = 2;
					}
					int num6 = 0;
					int num7 = 0;
					int num8 = 0;
					int num9 = 0;
					int num10 = 0;
					int num11 = 0;
					int num12 = 0;
					if (runningEvent != null || runningEvent2 != null)
					{
						int[] bonuses = runningEvent.GetBonuses(account.Rank);
						if (runningEvent != null && bonuses != null)
						{
							num10 += ComDiv.Percentage(bonuses[0], bonuses[2]);
							num11 += ComDiv.Percentage(bonuses[1], bonuses[2]);
						}
						if (runningEvent2 != null && runningEvent2.BoostType == PortalBoostEvent.Mode)
						{
							num10 += runningEvent2.BonusExp;
							num11 += runningEvent2.BonusGold;
						}
						if (!slotModel.BonusFlags.HasFlag(ResultIcon.Event))
						{
							slotModel.BonusFlags |= ResultIcon.Event;
						}
						slotModel.BonusEventExp += num10;
						slotModel.BonusEventPoint += num11;
					}
					PlayerBonus bonus = account.Bonus;
					if (bonus != null && bonus.Bonuses > 0)
					{
						if ((bonus.Bonuses & 8) == 8)
						{
							num6 += 100;
						}
						if ((bonus.Bonuses & 128) == 128)
						{
							num7 += 100;
						}
						if ((bonus.Bonuses & 4) == 4)
						{
							num6 += 50;
						}
						if ((bonus.Bonuses & 64) == 64)
						{
							num7 += 50;
						}
						if ((bonus.Bonuses & 2) == 2)
						{
							num6 += 30;
						}
						if ((bonus.Bonuses & 32) == 32)
						{
							num7 += 30;
						}
						if ((bonus.Bonuses & 1) == 1)
						{
							num6 += 10;
						}
						if ((bonus.Bonuses & 16) == 16)
						{
							num7 += 10;
						}
						if ((bonus.Bonuses & 512) == 512)
						{
							num12 += 20;
						}
						if ((bonus.Bonuses & 1024) == 1024)
						{
							num12 += 30;
						}
						if ((bonus.Bonuses & 2048) == 2048)
						{
							num12 += 50;
						}
						if ((bonus.Bonuses & 4096) == 4096)
						{
							num12 += 100;
						}
						if (!slotModel.BonusFlags.HasFlag(ResultIcon.Item))
						{
							slotModel.BonusFlags |= ResultIcon.Item;
						}
						slotModel.BonusItemExp += num6;
						slotModel.BonusItemPoint += num7;
						slotModel.BonusBattlePass += num12;
					}
					PCCafeModel pccafe = TemplatePackXML.GetPCCafe(account.CafePC);
					if (pccafe != null)
					{
						PlayerVip playerVIP = DaoManagerSQL.GetPlayerVIP(account.PlayerId);
						if (playerVIP != null && InternetCafeXML.IsValidAddress(DaoManagerSQL.GetPlayerIP4Address(account.PlayerId), playerVIP.Address))
						{
							InternetCafe icafe = InternetCafeXML.GetICafe(ConfigLoader.InternetCafeId);
							if (icafe != null && (account.CafePC == CafeEnum.Gold || account.CafePC == CafeEnum.Silver))
							{
								num8 += ((account.CafePC == CafeEnum.Gold) ? icafe.PremiumExp : ((account.CafePC == CafeEnum.Silver) ? icafe.BasicExp : 0));
								num9 += ((account.CafePC == CafeEnum.Gold) ? icafe.PremiumGold : ((account.CafePC == CafeEnum.Silver) ? icafe.BasicGold : 0));
							}
						}
						num8 += pccafe.ExpUp;
						num9 += pccafe.PointUp;
						if (account.CafePC == CafeEnum.Silver && !slotModel.BonusFlags.HasFlag(ResultIcon.Pc))
						{
							slotModel.BonusFlags |= ResultIcon.Pc;
						}
						else if (account.CafePC == CafeEnum.Gold && !slotModel.BonusFlags.HasFlag(ResultIcon.PcPlus))
						{
							slotModel.BonusFlags |= ResultIcon.PcPlus;
						}
						slotModel.BonusCafeExp += num8;
						slotModel.BonusCafePoint += num9;
					}
					if (bool_0)
					{
						if (slotModel.BonusItemExp > 300)
						{
							slotModel.BonusItemExp = 300;
						}
						if (slotModel.BonusItemPoint > 300)
						{
							slotModel.BonusItemPoint = 300;
						}
						if (slotModel.BonusCafeExp > 300)
						{
							slotModel.BonusCafeExp = 300;
						}
						if (slotModel.BonusCafePoint > 300)
						{
							slotModel.BonusCafePoint = 300;
						}
						if (slotModel.BonusEventExp > 300)
						{
							slotModel.BonusEventExp = 300;
						}
						if (slotModel.BonusEventPoint > 300)
						{
							slotModel.BonusEventPoint = 300;
						}
					}
					int num13 = slotModel.BonusEventExp + slotModel.BonusCafeExp + slotModel.BonusItemExp;
					int num14 = slotModel.BonusEventPoint + slotModel.BonusCafePoint + slotModel.BonusItemPoint;
					account.Exp += slotModel.Exp + ComDiv.Percentage(slotModel.Exp, num13);
					account.Gold += slotModel.Gold + ComDiv.Percentage(slotModel.Gold, num14);
					if (daily != null)
					{
						daily.ExpGained += slotModel.Exp + ComDiv.Percentage(slotModel.Exp, num13);
						daily.PointGained += slotModel.Gold + ComDiv.Percentage(slotModel.Gold, num14);
						daily.Playtime += (uint)num2;
						dbquery4.AddQuery("exp_gained", daily.ExpGained);
						dbquery4.AddQuery("point_gained", daily.PointGained);
						dbquery4.AddQuery("playtime", (long)((ulong)daily.Playtime));
					}
					if (ConfigLoader.WinCashPerBattle)
					{
						account.Cash += slotModel.Cash;
					}
					RankModel rank = PlayerRankXML.GetRank(account.Rank);
					if (rank != null && account.Exp >= rank.OnNextLevel + rank.OnAllExp && account.Rank <= 50)
					{
						List<int> rewards = PlayerRankXML.GetRewards(account.Rank);
						if (rewards.Count > 0)
						{
							foreach (int num15 in rewards)
							{
								GoodsItem good = ShopManager.GetGood(num15);
								if (good != null)
								{
									if (ComDiv.GetIdStatics(good.Item.Id, 1) == 6 && account.Character.GetCharacter(good.Item.Id) == null)
									{
										AllUtils.CreateCharacter(account, good.Item);
									}
									else
									{
										account.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, account, good.Item));
									}
								}
							}
						}
						account.Gold += rank.OnGoldUp;
						account.LastRankUpDate = uint.Parse(dateTime.ToString("yyMMddHHmm"));
						Account account2 = account;
						Account account3 = account;
						int num16 = account3.Rank + 1;
						account3.Rank = num16;
						account2.SendPacket(new PROTOCOL_BASE_RANK_UP_ACK(num16, rank.OnNextLevel));
						dbquery.AddQuery("last_rank_update", (long)((ulong)account.LastRankUpDate));
						dbquery.AddQuery("rank", account.Rank);
					}
					if (runningEvent3 != null)
					{
						AllUtils.PlayTimeEvent(account, runningEvent3, bool_0, slotModel, (long)num2);
					}
					if (activeSeasonPass != null)
					{
						account.UpdateSeasonpass = true;
						AllUtils.CalculateBattlePass(account, slotModel, activeSeasonPass);
					}
					if (this.Competitive)
					{
						AllUtils.CalculateCompetitive(this, account, slotModel, teamEnum_0 == slotModel.Team);
					}
					AllUtils.DiscountPlayerItems(slotModel, account);
					if (exp != account.Exp)
					{
						dbquery.AddQuery("experience", account.Exp);
					}
					if (gold != account.Gold)
					{
						dbquery.AddQuery("gold", account.Gold);
					}
					if (cash != account.Cash)
					{
						dbquery.AddQuery("cash", account.Cash);
					}
					ComDiv.UpdateDB("accounts", "player_id", account.PlayerId, dbquery.GetTables(), dbquery.GetValues());
					ComDiv.UpdateDB("player_stat_basics", "owner_id", account.PlayerId, dbquery2.GetTables(), dbquery2.GetValues());
					ComDiv.UpdateDB("player_stat_seasons", "owner_id", account.PlayerId, dbquery3.GetTables(), dbquery3.GetValues());
					ComDiv.UpdateDB("player_stat_dailies", "owner_id", account.PlayerId, dbquery4.GetTables(), dbquery4.GetValues());
					ComDiv.UpdateDB("player_stat_weapons", "owner_id", account.PlayerId, dbquery5.GetTables(), dbquery5.GetValues());
					if (ConfigLoader.WinCashPerBattle && ConfigLoader.ShowCashReceiveWarn)
					{
						account.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Translation.GetLabel("CashReceived", new object[] { slotModel.Cash })));
					}
					list.Add(slotModel);
				}
			}
			if (list.Count > 0)
			{
				this.SlotRewards = AllUtils.GetRewardData(this, list);
				this.method_8(list, bool_0);
			}
			this.UpdateSlotsInfo();
			if (this.RoomType != RoomCondition.FreeForAll)
			{
				this.method_14(teamEnum_0);
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00034124 File Offset: 0x00032324
		private void method_8(List<SlotModel> list_0, bool bool_0)
		{
			SlotModel slotModel = list_0.OrderByDescending(new Func<SlotModel, int>(RoomModel.Class11.<>9.method_0)).FirstOrDefault<SlotModel>();
			Account account;
			if (slotModel != null && slotModel.Check && slotModel.State == SlotState.BATTLE && this.GetPlayerBySlot(slotModel, out account))
			{
				StatisticTotal basic = account.Statistic.Basic;
				StatisticSeason season = account.Statistic.Season;
				if (!bool_0 && basic != null && season != null)
				{
					basic.MvpCount++;
					season.MvpCount++;
					ComDiv.UpdateDB("player_stat_basics", "mvp_count", basic.MvpCount, "owner_id", account.PlayerId);
					ComDiv.UpdateDB("player_stat_seasons", "mvp_count", season.MvpCount, "owner_id", account.PlayerId);
				}
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0003421C File Offset: 0x0003241C
		private void method_9(Account account_0, StatisticTotal statisticTotal_0, StatisticSeason statisticSeason_0, SlotModel slotModel_0, DBQuery dbquery_0, DBQuery dbquery_1, int int_0, bool bool_0, int int_1)
		{
			statisticTotal_0.HeadshotsCount += slotModel_0.AllHeadshots;
			statisticTotal_0.KillsCount += slotModel_0.AllKills;
			statisticTotal_0.TotalKillsCount += slotModel_0.AllKills;
			statisticTotal_0.DeathsCount += slotModel_0.AllDeaths;
			statisticTotal_0.AssistsCount += slotModel_0.AllAssists;
			statisticSeason_0.HeadshotsCount += slotModel_0.AllHeadshots;
			statisticSeason_0.KillsCount += slotModel_0.AllKills;
			statisticSeason_0.TotalKillsCount += slotModel_0.AllKills;
			statisticSeason_0.DeathsCount += slotModel_0.AllDeaths;
			statisticSeason_0.AssistsCount += slotModel_0.AllAssists;
			this.method_12(slotModel_0, account_0.Statistic, dbquery_0, dbquery_1);
			if (this.RoomType == RoomCondition.FreeForAll)
			{
				AllUtils.UpdateMatchCountFFA(this, account_0, int_0, dbquery_0, dbquery_1);
				return;
			}
			AllUtils.UpdateMatchCount(bool_0, account_0, int_1, dbquery_0, dbquery_1);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0003432C File Offset: 0x0003252C
		private void method_10(Account account_0, StatisticDaily statisticDaily_0, SlotModel slotModel_0, DBQuery dbquery_0, int int_0, bool bool_0, int int_1)
		{
			statisticDaily_0.KillsCount += slotModel_0.AllKills;
			statisticDaily_0.DeathsCount += slotModel_0.AllDeaths;
			statisticDaily_0.HeadshotsCount += slotModel_0.AllHeadshots;
			this.method_13(slotModel_0, account_0.Statistic, dbquery_0);
			if (this.RoomType == RoomCondition.FreeForAll)
			{
				AllUtils.UpdateMatchDailyRecordFFA(this, account_0, int_0, dbquery_0);
				return;
			}
			AllUtils.UpdateDailyRecord(bool_0, account_0, int_1, dbquery_0);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x000343A4 File Offset: 0x000325A4
		private void method_11(Account account_0, StatisticWeapon statisticWeapon_0, SlotModel slotModel_0, DBQuery dbquery_0)
		{
			statisticWeapon_0.AssaultKills += slotModel_0.AR[0];
			statisticWeapon_0.AssaultDeaths += slotModel_0.AR[1];
			statisticWeapon_0.SmgKills += slotModel_0.SMG[0];
			statisticWeapon_0.SmgDeaths += slotModel_0.SMG[1];
			statisticWeapon_0.SniperKills += slotModel_0.SR[0];
			statisticWeapon_0.SniperDeaths += slotModel_0.SR[1];
			statisticWeapon_0.ShotgunKills += slotModel_0.SG[0];
			statisticWeapon_0.ShotgunDeaths += slotModel_0.SG[1];
			statisticWeapon_0.MachinegunKills += slotModel_0.MG[0];
			statisticWeapon_0.MachinegunDeaths += slotModel_0.MG[1];
			statisticWeapon_0.ShieldKills += slotModel_0.SHD[0];
			statisticWeapon_0.ShieldDeaths += slotModel_0.SHD[1];
			AllUtils.UpdateWeaponRecord(account_0, slotModel_0, dbquery_0);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000344B8 File Offset: 0x000326B8
		private void method_12(SlotModel slotModel_0, PlayerStatistic playerStatistic_0, DBQuery dbquery_0, DBQuery dbquery_1)
		{
			if (playerStatistic_0 != null)
			{
				if (slotModel_0.AllKills > 0)
				{
					dbquery_0.AddQuery("kills_count", playerStatistic_0.Basic.KillsCount);
					dbquery_0.AddQuery("total_kills", playerStatistic_0.Basic.TotalKillsCount);
					dbquery_1.AddQuery("kills_count", playerStatistic_0.Season.KillsCount);
					dbquery_1.AddQuery("total_kills", playerStatistic_0.Season.TotalKillsCount);
				}
				if (slotModel_0.AllAssists > 0)
				{
					dbquery_0.AddQuery("assists_count", playerStatistic_0.Basic.AssistsCount);
					dbquery_1.AddQuery("assists_count", playerStatistic_0.Season.AssistsCount);
				}
				if (slotModel_0.AllDeaths > 0)
				{
					dbquery_0.AddQuery("deaths_count", playerStatistic_0.Basic.DeathsCount);
					dbquery_1.AddQuery("deaths_count", playerStatistic_0.Season.DeathsCount);
				}
				if (slotModel_0.AllHeadshots > 0)
				{
					dbquery_0.AddQuery("headshots_count", playerStatistic_0.Basic.HeadshotsCount);
					dbquery_1.AddQuery("headshots_count", playerStatistic_0.Season.HeadshotsCount);
				}
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00034604 File Offset: 0x00032804
		private void method_13(SlotModel slotModel_0, PlayerStatistic playerStatistic_0, DBQuery dbquery_0)
		{
			if (playerStatistic_0 != null)
			{
				if (slotModel_0.AllKills > 0)
				{
					dbquery_0.AddQuery("kills_count", playerStatistic_0.Daily.KillsCount);
				}
				if (slotModel_0.AllDeaths > 0)
				{
					dbquery_0.AddQuery("deaths_count", playerStatistic_0.Daily.DeathsCount);
				}
				if (slotModel_0.AllHeadshots > 0)
				{
					dbquery_0.AddQuery("headshots_count", playerStatistic_0.Daily.HeadshotsCount);
				}
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00034680 File Offset: 0x00032880
		private void method_14(TeamEnum teamEnum_0)
		{
			if (this.ChannelType == ChannelType.Clan && !this.BlockedClan)
			{
				SortedList<int, ClanModel> sortedList = new SortedList<int, ClanModel>();
				foreach (SlotModel slotModel in this.Slots)
				{
					Account account;
					if (slotModel.State == SlotState.BATTLE && this.GetPlayerBySlot(slotModel, out account))
					{
						ClanModel clan = ClanManager.GetClan(account.ClanId);
						if (clan.Id != 0)
						{
							bool flag = slotModel.Team == teamEnum_0;
							clan.Exp += slotModel.Exp;
							clan.BestPlayers.SetBestExp(slotModel);
							clan.BestPlayers.SetBestKills(slotModel);
							clan.BestPlayers.SetBestHeadshot(slotModel);
							clan.BestPlayers.SetBestWins(account.Statistic, slotModel, flag);
							clan.BestPlayers.SetBestParticipation(account.Statistic, slotModel);
							if (!sortedList.ContainsKey(account.ClanId))
							{
								sortedList.Add(account.ClanId, clan);
								if (teamEnum_0 != TeamEnum.TEAM_DRAW)
								{
									this.method_15(clan, teamEnum_0, slotModel.Team);
									if (flag)
									{
										clan.MatchWins++;
									}
									else
									{
										clan.MatchLoses++;
									}
								}
								clan.Matches++;
								DaoManagerSQL.UpdateClanBattles(clan.Id, clan.Matches, clan.MatchWins, clan.MatchLoses);
							}
						}
					}
				}
				foreach (ClanModel clanModel in sortedList.Values)
				{
					DaoManagerSQL.UpdateClanExp(clanModel.Id, clanModel.Exp);
					DaoManagerSQL.UpdateClanPoints(clanModel.Id, clanModel.Points);
					DaoManagerSQL.UpdateClanBestPlayers(clanModel);
					RankModel rank = ClanRankXML.GetRank(clanModel.Rank);
					if (rank != null && clanModel.Exp >= rank.OnNextLevel + rank.OnAllExp)
					{
						int id = clanModel.Id;
						ClanModel clanModel2 = clanModel;
						int i = clanModel2.Rank + 1;
						clanModel2.Rank = i;
						DaoManagerSQL.UpdateClanRank(id, i);
					}
				}
				return;
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x000348B4 File Offset: 0x00032AB4
		private void method_15(ClanModel clanModel_0, TeamEnum teamEnum_0, TeamEnum teamEnum_1)
		{
			if (teamEnum_0 == TeamEnum.TEAM_DRAW)
			{
				return;
			}
			if (teamEnum_0 == teamEnum_1)
			{
				float num;
				if (this.RoomType == RoomCondition.DeathMatch)
				{
					num = (float)(((teamEnum_1 == TeamEnum.FR_TEAM) ? this.FRKills : this.CTKills) / 20);
				}
				else
				{
					num = (float)((teamEnum_1 == TeamEnum.FR_TEAM) ? this.FRRounds : this.CTRounds);
				}
				float num2 = 25f + num;
				clanModel_0.Points += num2;
				return;
			}
			if (clanModel_0.Points == 0f)
			{
				return;
			}
			float num3;
			if (this.RoomType == RoomCondition.DeathMatch)
			{
				num3 = (float)(((teamEnum_1 == TeamEnum.FR_TEAM) ? this.FRKills : this.CTKills) / 20);
			}
			else
			{
				num3 = (float)((teamEnum_1 == TeamEnum.FR_TEAM) ? this.FRRounds : this.CTRounds);
			}
			float num4 = 40f - num3;
			clanModel_0.Points -= num4;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0000605C File Offset: 0x0000425C
		public bool IsStartingMatch()
		{
			return this.State > RoomState.READY;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00006067 File Offset: 0x00004267
		public bool IsPreparing()
		{
			return this.State >= RoomState.LOADING;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00034970 File Offset: 0x00032B70
		public void UpdateRoomInfo()
		{
			this.GenerateSeed();
			using (PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK protocol_ROOM_CHANGE_ROOMINFO_ACK = new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(this))
			{
				this.SendPacketToPlayers(protocol_ROOM_CHANGE_ROOMINFO_ACK);
			}
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x000349B0 File Offset: 0x00032BB0
		public void SetSlotCount(int Count, bool IsCreateRoom, bool IsUpdateRoom)
		{
			MapMatch mapLimit = SystemMapXML.GetMapLimit((int)this.MapId, (int)this.Rule);
			if (mapLimit != null)
			{
				if (Count > mapLimit.Limit)
				{
					Count = mapLimit.Limit;
				}
				if (this.RoomType == RoomCondition.Tutorial)
				{
					Count = 1;
				}
				if (this.IsBotMode())
				{
					Count = 8;
				}
				if (Count <= 0)
				{
					Count = 1;
				}
				if (IsCreateRoom)
				{
					SlotModel[] slots = this.Slots;
					lock (slots)
					{
						foreach (SlotModel slotModel in this.Slots.Where(new Func<SlotModel, bool>(RoomModel.Class11.<>9.method_1)))
						{
							if (slotModel.Id >= Count)
							{
								slotModel.State = SlotState.CLOSE;
							}
						}
					}
				}
				if (IsUpdateRoom)
				{
					this.UpdateSlotsInfo();
				}
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00034AAC File Offset: 0x00032CAC
		public int GetSlotCount()
		{
			SlotModel[] slots = this.Slots;
			int num2;
			lock (slots)
			{
				int num = 0;
				using (IEnumerator<SlotModel> enumerator = this.Slots.Where(new Func<SlotModel, bool>(RoomModel.Class11.<>9.method_2)).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.State != SlotState.CLOSE)
						{
							num++;
						}
					}
				}
				num2 = num;
			}
			return num2;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00034B54 File Offset: 0x00032D54
		public void SwitchSlots(List<SlotChange> SlotChanges, Account Player, SlotModel OldSlot, SlotModel NewSlot, SlotState State = SlotState.NORMAL)
		{
			if (Player != null && OldSlot != null && NewSlot != null)
			{
				NewSlot.ResetSlot();
				NewSlot.State = SlotState.NORMAL;
				NewSlot.PlayerId = Player.PlayerId;
				NewSlot.Equipment = Player.Equipment;
				if (NewSlot.Id == 16 || NewSlot.Id == 17)
				{
					NewSlot.SpecGM = true;
					NewSlot.ViewType = ViewerType.SpecGM;
				}
				OldSlot.ResetSlot();
				OldSlot.State = SlotState.EMPTY;
				OldSlot.PlayerId = 0L;
				OldSlot.Equipment = null;
				OldSlot.SpecGM = false;
				OldSlot.ViewType = ViewerType.Normal;
				if (Player.SlotId == this.LeaderSlot)
				{
					this.LeaderName = Player.Nickname;
					this.LeaderSlot = NewSlot.Id;
				}
				Player.SlotId = NewSlot.Id;
				SlotChanges.Add(new SlotChange(OldSlot, NewSlot));
				return;
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00034C38 File Offset: 0x00032E38
		public void SwitchSlots(List<SlotChange> SlotChanges, int NewSlotId, int OldSlotId, bool ChangeReady)
		{
			SlotModel slotModel = this.Slots[NewSlotId];
			SlotModel slotModel2 = this.Slots[OldSlotId];
			if (ChangeReady)
			{
				if (slotModel.State == SlotState.READY)
				{
					slotModel.State = SlotState.NORMAL;
				}
				if (slotModel2.State == SlotState.READY)
				{
					slotModel2.State = SlotState.NORMAL;
				}
			}
			slotModel.SetSlotId(OldSlotId);
			slotModel2.SetSlotId(NewSlotId);
			this.Slots[NewSlotId] = slotModel2;
			this.Slots[OldSlotId] = slotModel;
			SlotChanges.Add(new SlotChange(slotModel, slotModel2));
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00006075 File Offset: 0x00004275
		public void ChangeSlotState(int SlotId, SlotState State, bool SendInfo)
		{
			this.ChangeSlotState(this.GetSlot(SlotId), State, SendInfo);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00006086 File Offset: 0x00004286
		public void ChangeSlotState(SlotModel Slot, SlotState State, bool SendInfo)
		{
			if (Slot != null)
			{
				if (Slot.State != State)
				{
					Slot.State = State;
					if (State == SlotState.EMPTY || State == SlotState.CLOSE)
					{
						AllUtils.ResetSlotInfo(this, Slot, false);
						Slot.PlayerId = 0L;
					}
					if (SendInfo)
					{
						this.UpdateSlotsInfo();
					}
					return;
				}
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00034CAC File Offset: 0x00032EAC
		public Account GetPlayerBySlot(SlotModel Slot)
		{
			Account account;
			try
			{
				long playerId = Slot.PlayerId;
				account = ((playerId > 0L) ? AccountManager.GetAccount(playerId, true) : null);
			}
			catch
			{
				account = null;
			}
			return account;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00034CF0 File Offset: 0x00032EF0
		public Account GetPlayerBySlot(int SlotId)
		{
			Account account;
			try
			{
				long playerId = this.Slots[SlotId].PlayerId;
				account = ((playerId > 0L) ? AccountManager.GetAccount(playerId, true) : null);
			}
			catch
			{
				account = null;
			}
			return account;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00034D3C File Offset: 0x00032F3C
		public bool GetPlayerBySlot(int SlotId, out Account Player)
		{
			bool flag;
			try
			{
				long playerId = this.Slots[SlotId].PlayerId;
				Player = ((playerId > 0L) ? AccountManager.GetAccount(playerId, true) : null);
				flag = Player != null;
			}
			catch
			{
				Player = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00034D90 File Offset: 0x00032F90
		public bool GetPlayerBySlot(SlotModel Slot, out Account Player)
		{
			bool flag;
			try
			{
				long playerId = Slot.PlayerId;
				Player = ((playerId > 0L) ? AccountManager.GetAccount(playerId, true) : null);
				flag = Player != null;
			}
			catch
			{
				Player = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000060C5 File Offset: 0x000042C5
		public int GetTimeByMask()
		{
			return this.TIMES[this.KillTime >> 4];
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000060D6 File Offset: 0x000042D6
		public int GetRoundsByMask()
		{
			return this.ROUNDS[this.KillTime & 15];
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x000060E8 File Offset: 0x000042E8
		public int GetKillsByMask()
		{
			return this.KILLS[this.KillTime & 15];
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00034DE0 File Offset: 0x00032FE0
		public void UpdateSlotsInfo()
		{
			using (PROTOCOL_ROOM_GET_SLOTINFO_ACK protocol_ROOM_GET_SLOTINFO_ACK = new PROTOCOL_ROOM_GET_SLOTINFO_ACK(this))
			{
				this.SendPacketToPlayers(protocol_ROOM_GET_SLOTINFO_ACK);
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00034E18 File Offset: 0x00033018
		public bool GetLeader(out Account Player)
		{
			Player = null;
			if (this.GetCountPlayers() <= 0)
			{
				return false;
			}
			if (this.LeaderSlot == -1)
			{
				this.SetNewLeader(-1, SlotState.EMPTY, -1, false);
			}
			if (this.LeaderSlot >= 0)
			{
				Player = AccountManager.GetAccount(this.Slots[this.LeaderSlot].PlayerId, true);
			}
			return Player != null;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00034E70 File Offset: 0x00033070
		public Account GetLeader()
		{
			if (this.GetCountPlayers() <= 0)
			{
				return null;
			}
			if (this.LeaderSlot == -1)
			{
				this.SetNewLeader(-1, SlotState.EMPTY, -1, false);
			}
			if (this.LeaderSlot != -1)
			{
				return AccountManager.GetAccount(this.Slots[this.LeaderSlot].PlayerId, true);
			}
			return null;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00034EC0 File Offset: 0x000330C0
		public void SetNewLeader(int LeaderSlot, SlotState State, int OldLeader, bool UpdateInfo)
		{
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				if (LeaderSlot == -1)
				{
					foreach (SlotModel slotModel in this.Slots)
					{
						if (slotModel.Id != OldLeader && slotModel.PlayerId > 0L && slotModel.State > State)
						{
							this.LeaderSlot = slotModel.Id;
							break;
						}
					}
				}
				else
				{
					this.LeaderSlot = LeaderSlot;
				}
				if (this.LeaderSlot != -1)
				{
					SlotModel slotModel2 = this.Slots[this.LeaderSlot];
					if (slotModel2.State == SlotState.READY)
					{
						slotModel2.State = SlotState.NORMAL;
					}
					if (UpdateInfo)
					{
						this.UpdateSlotsInfo();
					}
				}
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00034F8C File Offset: 0x0003318C
		public void SendPacketToPlayers(GameServerPacket Packet)
		{
			List<Account> allPlayers = this.GetAllPlayers();
			if (allPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket)");
			foreach (Account account in allPlayers)
			{
				account.SendCompletePacket(completeBytes, Packet.GetType().Name);
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00035000 File Offset: 0x00033200
		public void SendPacketToPlayers(GameServerPacket Packet, long PlayerId)
		{
			List<Account> allPlayers = this.GetAllPlayers(PlayerId);
			if (allPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,long)");
			foreach (Account account in allPlayers)
			{
				account.SendCompletePacket(completeBytes, Packet.GetType().Name);
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00035078 File Offset: 0x00033278
		public void SendPacketToPlayers(GameServerPacket Packet, SlotState State, int Type)
		{
			List<Account> allPlayers = this.GetAllPlayers(State, Type);
			if (allPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SLOT_STATE,int)");
			foreach (Account account in allPlayers)
			{
				account.SendCompletePacket(completeBytes, Packet.GetType().Name);
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000350F0 File Offset: 0x000332F0
		public void SendPacketToPlayers(GameServerPacket Packet, GameServerPacket Packet2, SlotState State, int Type)
		{
			List<Account> allPlayers = this.GetAllPlayers(State, Type);
			if (allPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SendPacket,SLOT_STATE,int)-1");
			byte[] completeBytes2 = Packet2.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SendPacket,SLOT_STATE,int)-2");
			foreach (Account account in allPlayers)
			{
				account.SendCompletePacket(completeBytes, Packet.GetType().Name);
				account.SendCompletePacket(completeBytes2, Packet2.GetType().Name);
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00035188 File Offset: 0x00033388
		public void SendPacketToPlayers(GameServerPacket Packet, SlotState State, int Type, int Exception)
		{
			List<Account> allPlayers = this.GetAllPlayers(State, Type, Exception);
			if (allPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SLOT_STATE,int,int)");
			foreach (Account account in allPlayers)
			{
				account.SendCompletePacket(completeBytes, Packet.GetType().Name);
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00035200 File Offset: 0x00033400
		public void SendPacketToPlayers(GameServerPacket Packet, SlotState State, int Type, int Exception, int Exception2)
		{
			List<Account> allPlayers = this.GetAllPlayers(State, Type, Exception, Exception2);
			if (allPlayers.Count == 0)
			{
				return;
			}
			byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SLOT_STATE,int,int,int)");
			foreach (Account account in allPlayers)
			{
				account.SendCompletePacket(completeBytes, Packet.GetType().Name);
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0003527C File Offset: 0x0003347C
		public void RemovePlayer(Account Player, bool WarnAllPlayers, int QuitMotive = 0)
		{
			SlotModel slotModel;
			if (Player != null && this.GetSlot(Player.SlotId, out slotModel))
			{
				this.method_16(Player, slotModel, WarnAllPlayers, QuitMotive);
				return;
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000060FA File Offset: 0x000042FA
		public void RemovePlayer(Account Player, SlotModel Slot, bool WarnAllPlayers, int QuitMotive = 0)
		{
			if (Player != null && Slot != null)
			{
				this.method_16(Player, Slot, WarnAllPlayers, QuitMotive);
				return;
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x000352A8 File Offset: 0x000334A8
		private void method_16(Account account_0, SlotModel slotModel_0, bool bool_0, int int_0)
		{
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				bool flag2 = false;
				bool flag3 = false;
				if (account_0 != null && slotModel_0 != null)
				{
					if (slotModel_0.State >= SlotState.LOAD)
					{
						if (this.LeaderSlot == slotModel_0.Id)
						{
							int leaderSlot = this.LeaderSlot;
							SlotState slotState = SlotState.CLOSE;
							if (this.State == RoomState.BATTLE)
							{
								slotState = SlotState.BATTLE_READY;
							}
							else if (this.State >= RoomState.LOADING)
							{
								slotState = SlotState.READY;
							}
							if (this.GetAllPlayers(slotModel_0.Id).Count >= 1)
							{
								this.SetNewLeader(-1, slotState, leaderSlot, false);
							}
							if (this.GetPlayingPlayers(TeamEnum.TEAM_DRAW, SlotState.READY, 1) >= 2)
							{
								using (PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK protocol_BATTLE_LEAVEP2PSERVER_ACK = new PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK(this))
								{
									this.SendPacketToPlayers(protocol_BATTLE_LEAVEP2PSERVER_ACK, SlotState.RENDEZVOUS, 1, slotModel_0.Id);
								}
							}
							flag3 = true;
						}
						using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK protocol_BATTLE_GIVEUPBATTLE_ACK = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(account_0, int_0))
						{
							this.SendPacketToPlayers(protocol_BATTLE_GIVEUPBATTLE_ACK, SlotState.READY, 1, (!bool_0) ? slotModel_0.Id : (-1));
						}
						BattleLeaveSync.SendUDPPlayerLeave(this, slotModel_0.Id);
						slotModel_0.ResetSlot();
						if (this.VoteKick != null)
						{
							this.VoteKick.TotalArray[slotModel_0.Id] = false;
						}
					}
					slotModel_0.PlayerId = 0L;
					slotModel_0.Equipment = null;
					slotModel_0.State = SlotState.EMPTY;
					if (this.State == RoomState.COUNTDOWN)
					{
						if (slotModel_0.Id == this.LeaderSlot)
						{
							this.State = RoomState.READY;
							flag2 = true;
							this.CountdownTime.StopJob();
							using (PROTOCOL_BATTLE_START_COUNTDOWN_ACK protocol_BATTLE_START_COUNTDOWN_ACK = new PROTOCOL_BATTLE_START_COUNTDOWN_ACK(CountDownEnum.StopByHost))
							{
								this.SendPacketToPlayers(protocol_BATTLE_START_COUNTDOWN_ACK);
								goto IL_1E3;
							}
						}
						if (this.GetPlayingPlayers(slotModel_0.Team, SlotState.READY, 0) == 0)
						{
							if (slotModel_0.Id != this.LeaderSlot)
							{
								this.ChangeSlotState(this.LeaderSlot, SlotState.NORMAL, false);
							}
							this.StopCountDown(CountDownEnum.StopByPlayer, false);
							flag2 = true;
						}
					}
					else if (this.IsPreparing())
					{
						AllUtils.BattleEndPlayersCount(this, this.IsBotMode());
						if (this.State == RoomState.BATTLE)
						{
							AllUtils.BattleEndRoundPlayersCount(this);
						}
					}
					IL_1E3:
					this.CheckToEndWaitingBattle(flag3);
					this.RequestRoomMaster.Remove(account_0.PlayerId);
					if (this.VoteTime.IsTimer() && this.VoteKick != null && this.VoteKick.VictimIdx == account_0.SlotId && int_0 != 2)
					{
						this.VoteTime.StopJob();
						this.VoteKick = null;
						using (PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK protocol_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK = new PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK())
						{
							this.SendPacketToPlayers(protocol_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK, SlotState.BATTLE, 0);
						}
					}
					MatchModel match = account_0.Match;
					if (match != null && account_0.MatchSlot >= 0)
					{
						match.Slots[account_0.MatchSlot].State = SlotMatchState.Normal;
						using (PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK protocol_CLAN_WAR_REGIST_MERCENARY_ACK = new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(match))
						{
							match.SendPacketToPlayers(protocol_CLAN_WAR_REGIST_MERCENARY_ACK);
						}
					}
					account_0.Room = null;
					account_0.SlotId = -1;
					account_0.Status.UpdateRoom(byte.MaxValue);
					AllUtils.SyncPlayerToClanMembers(account_0);
					AllUtils.SyncPlayerToFriends(account_0, false);
					account_0.UpdateCacheInfo();
				}
				this.UpdateSlotsInfo();
				if (flag2)
				{
					this.UpdateRoomInfo();
				}
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0003563C File Offset: 0x0003383C
		public int AddPlayer(Account Player)
		{
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				SlotModel[] slots2 = this.Slots;
				int i = 0;
				while (i < slots2.Length)
				{
					SlotModel slotModel = slots2[i];
					if (slotModel.PlayerId != 0L || slotModel.State != SlotState.EMPTY)
					{
						i++;
					}
					else
					{
						if ((slotModel.Id == 16 || slotModel.Id == 17) && !Player.IsGM())
						{
							return -1;
						}
						slotModel.PlayerId = Player.PlayerId;
						slotModel.State = SlotState.NORMAL;
						Player.Room = this;
						Player.SlotId = slotModel.Id;
						slotModel.Equipment = Player.Equipment;
						Player.Status.UpdateRoom((byte)this.RoomId);
						AllUtils.SyncPlayerToClanMembers(Player);
						AllUtils.SyncPlayerToFriends(Player, false);
						Player.UpdateCacheInfo();
						return slotModel.Id;
					}
				}
			}
			return -1;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00035734 File Offset: 0x00033934
		public int AddPlayer(Account Player, TeamEnum TeamIdx)
		{
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				int[] teamArray = this.GetTeamArray(TeamIdx);
				int i = 0;
				while (i < teamArray.Length)
				{
					int num = teamArray[i];
					SlotModel slotModel = this.Slots[num];
					if (slotModel.PlayerId != 0L || slotModel.State != SlotState.EMPTY)
					{
						i++;
					}
					else
					{
						if ((slotModel.Id == 16 || slotModel.Id == 17) && !Player.IsGM())
						{
							return -1;
						}
						slotModel.PlayerId = Player.PlayerId;
						slotModel.State = SlotState.NORMAL;
						Player.Room = this;
						Player.SlotId = slotModel.Id;
						slotModel.Equipment = Player.Equipment;
						Player.Status.UpdateRoom((byte)this.RoomId);
						AllUtils.SyncPlayerToClanMembers(Player);
						AllUtils.SyncPlayerToFriends(Player, false);
						Player.UpdateCacheInfo();
						return slotModel.Id;
					}
				}
			}
			return -1;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000610E File Offset: 0x0000430E
		public int[] GetTeamArray(TeamEnum Team)
		{
			if (Team == TeamEnum.FR_TEAM)
			{
				return this.FR_TEAM;
			}
			if (Team != TeamEnum.CT_TEAM)
			{
				return this.ALL_TEAM;
			}
			return this.CT_TEAM;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00035838 File Offset: 0x00033A38
		public List<Account> GetAllPlayers(SlotState State, int Type)
		{
			List<Account> list = new List<Account>();
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				foreach (SlotModel slotModel in this.Slots)
				{
					if (slotModel.PlayerId > 0L && ((Type == 0 && slotModel.State == State) || (Type == 1 && slotModel.State > State)))
					{
						Account account = AccountManager.GetAccount(slotModel.PlayerId, true);
						if (account != null && account.SlotId != -1)
						{
							list.Add(account);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x000358EC File Offset: 0x00033AEC
		public List<Account> GetAllPlayers(SlotState State, int Type, int Exception)
		{
			List<Account> list = new List<Account>();
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				foreach (SlotModel slotModel in this.Slots)
				{
					if (slotModel.PlayerId > 0L && slotModel.Id != Exception && ((Type == 0 && slotModel.State == State) || (Type == 1 && slotModel.State > State)))
					{
						Account account = AccountManager.GetAccount(slotModel.PlayerId, true);
						if (account != null && account.SlotId != -1)
						{
							list.Add(account);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x000359A8 File Offset: 0x00033BA8
		public List<Account> GetAllPlayers(SlotState State, int Type, int Exception, int Exception2)
		{
			List<Account> list = new List<Account>();
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				foreach (SlotModel slotModel in this.Slots)
				{
					if (slotModel.PlayerId > 0L && slotModel.Id != Exception && slotModel.Id != Exception2 && ((Type == 0 && slotModel.State == State) || (Type == 1 && slotModel.State > State)))
					{
						Account account = AccountManager.GetAccount(slotModel.PlayerId, true);
						if (account != null && account.SlotId != -1)
						{
							list.Add(account);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00035A70 File Offset: 0x00033C70
		public List<Account> GetAllPlayers(int Exception)
		{
			List<Account> list = new List<Account>();
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				foreach (SlotModel slotModel in this.Slots)
				{
					long playerId = slotModel.PlayerId;
					if (playerId > 0L && slotModel.Id != Exception)
					{
						Account account = AccountManager.GetAccount(playerId, true);
						if (account != null && account.SlotId != -1)
						{
							list.Add(account);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00035B10 File Offset: 0x00033D10
		public List<Account> GetAllPlayers(long Exception)
		{
			List<Account> list = new List<Account>();
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				foreach (SlotModel slotModel in this.Slots)
				{
					if (slotModel.PlayerId > 0L && slotModel.PlayerId != Exception)
					{
						Account account = AccountManager.GetAccount(slotModel.PlayerId, true);
						if (account != null && account.SlotId != -1)
						{
							list.Add(account);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00035BB4 File Offset: 0x00033DB4
		public List<Account> GetAllPlayers()
		{
			List<Account> list = new List<Account>();
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				foreach (SlotModel slotModel in this.Slots)
				{
					if (slotModel.PlayerId > 0L)
					{
						Account account = AccountManager.GetAccount(slotModel.PlayerId, true);
						if (account != null && account.SlotId != -1)
						{
							list.Add(account);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00035C4C File Offset: 0x00033E4C
		public int GetCountPlayers()
		{
			int num = 0;
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				foreach (SlotModel slotModel in this.Slots)
				{
					if (slotModel.PlayerId > 0L)
					{
						Account account = AccountManager.GetAccount(slotModel.PlayerId, true);
						if (account != null && account.SlotId != -1)
						{
							num++;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00035CDC File Offset: 0x00033EDC
		public int GetPlayingPlayers(TeamEnum Team, bool InBattle)
		{
			int num = 0;
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				foreach (SlotModel slotModel in this.Slots)
				{
					if (slotModel.PlayerId > 0L && (slotModel.Team == Team || Team == TeamEnum.TEAM_DRAW) && ((InBattle && slotModel.State == SlotState.BATTLE_LOAD && !slotModel.Spectator) || (!InBattle && slotModel.State >= SlotState.LOAD)))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00035D84 File Offset: 0x00033F84
		public int GetPlayingPlayers(TeamEnum Team, SlotState State, int Type)
		{
			int num = 0;
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				foreach (SlotModel slotModel in this.Slots)
				{
					if (slotModel.PlayerId > 0L && ((Type == 0 && slotModel.State == State) || (Type == 1 && slotModel.State > State)) && (Team == TeamEnum.TEAM_DRAW || slotModel.Team == Team))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00035E20 File Offset: 0x00034020
		public int GetPlayingPlayers(TeamEnum Team, SlotState State, int Type, int Exception)
		{
			int num = 0;
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				foreach (SlotModel slotModel in this.Slots)
				{
					if (slotModel.Id != Exception && slotModel.PlayerId > 0L && ((Type == 0 && slotModel.State == State) || (Type == 1 && slotModel.State > State)) && (Team == TeamEnum.TEAM_DRAW || slotModel.Team == Team))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00035EC8 File Offset: 0x000340C8
		public void GetPlayingPlayers(bool InBattle, out int PlayerFR, out int PlayerCT)
		{
			PlayerFR = 0;
			PlayerCT = 0;
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				foreach (SlotModel slotModel in this.Slots)
				{
					if (slotModel.PlayerId > 0L && ((InBattle && slotModel.State == SlotState.BATTLE && !slotModel.Spectator) || (!InBattle && slotModel.State >= SlotState.RENDEZVOUS)))
					{
						if (slotModel.Team == TeamEnum.FR_TEAM)
						{
							PlayerFR++;
						}
						else
						{
							PlayerCT++;
						}
					}
				}
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00035F70 File Offset: 0x00034170
		public void GetPlayingPlayers(bool InBattle, out int PlayerFR, out int PlayerCT, out int DeathFR, out int DeathCT)
		{
			PlayerFR = 0;
			PlayerCT = 0;
			DeathFR = 0;
			DeathCT = 0;
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				foreach (SlotModel slotModel in this.Slots)
				{
					if (slotModel.DeathState.HasFlag(DeadEnum.Dead))
					{
						if (slotModel.Team == TeamEnum.FR_TEAM)
						{
							DeathFR++;
						}
						else
						{
							DeathCT++;
						}
					}
					if (slotModel.PlayerId > 0L && ((InBattle && slotModel.State == SlotState.BATTLE && !slotModel.Spectator) || (!InBattle && slotModel.State >= SlotState.RENDEZVOUS)))
					{
						if (slotModel.Team == TeamEnum.FR_TEAM)
						{
							PlayerFR++;
						}
						else
						{
							PlayerCT++;
						}
					}
				}
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0000612B File Offset: 0x0000432B
		public void CheckToEndWaitingBattle(bool host)
		{
			if ((this.State == RoomState.COUNTDOWN || this.State == RoomState.LOADING || this.State == RoomState.RENDEZVOUS) && (host || this.Slots[this.LeaderSlot].State == SlotState.BATTLE_READY))
			{
				AllUtils.EndBattleNoPoints(this);
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0003605C File Offset: 0x0003425C
		public void SpawnReadyPlayers()
		{
			SlotModel[] slots = this.Slots;
			lock (slots)
			{
				bool flag2 = this.ThisModeHaveRounds() && (this.CountdownIG == 3 || this.CountdownIG == 5 || this.CountdownIG == 7 || this.CountdownIG == 9);
				if (this.State == RoomState.PRE_BATTLE && !this.PreMatchCD && flag2 && !this.IsBotMode())
				{
					this.PreMatchCD = true;
					using (PROTOCOL_BATTLE_COUNT_DOWN_ACK protocol_BATTLE_COUNT_DOWN_ACK = new PROTOCOL_BATTLE_COUNT_DOWN_ACK((int)this.CountdownIG))
					{
						this.SendPacketToPlayers(protocol_BATTLE_COUNT_DOWN_ACK);
					}
				}
				int num = ((this.CountdownIG == 0) ? 0 : ((int)this.CountdownIG * 1000 + 250));
				this.PreMatchTime.StartJob(num, new TimerCallback(this.method_24));
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00036158 File Offset: 0x00034358
		private void method_17()
		{
			DateTime dateTime = DateTimeUtil.Now();
			foreach (SlotModel slotModel in this.Slots)
			{
				if (slotModel.State == SlotState.BATTLE_READY && slotModel.IsPlaying == 0 && slotModel.PlayerId > 0L)
				{
					slotModel.IsPlaying = 1;
					slotModel.StartTime = dateTime;
					slotModel.State = SlotState.BATTLE;
					if (this.State == RoomState.BATTLE && (this.RoomType == RoomCondition.Bomb || this.RoomType == RoomCondition.Annihilation || this.RoomType == RoomCondition.Destroy || this.RoomType == RoomCondition.Ace || this.RoomType == RoomCondition.Glass))
					{
						slotModel.Spectator = true;
					}
				}
			}
			this.UpdateSlotsInfo();
			List<int> dinossaurs = AllUtils.GetDinossaurs(this, false, -1);
			if (this.State == RoomState.PRE_BATTLE)
			{
				this.BattleStart = (this.IsDinoMode("") ? dateTime.AddMinutes(5.0) : dateTime);
				this.method_1();
			}
			bool flag = false;
			using (PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK protocol_BATTLE_MISSION_ROUND_PRE_START_ACK = new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(this, dinossaurs))
			{
				using (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK protocol_BATTLE_MISSION_ROUND_START_ACK = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(this))
				{
					using (PROTOCOL_BATTLE_RECORD_ACK protocol_BATTLE_RECORD_ACK = new PROTOCOL_BATTLE_RECORD_ACK(this))
					{
						byte[] completeBytes = protocol_BATTLE_MISSION_ROUND_PRE_START_ACK.GetCompleteBytes("Room.BaseSpawnReadyPlayers-1");
						byte[] completeBytes2 = protocol_BATTLE_MISSION_ROUND_START_ACK.GetCompleteBytes("Room.BaseSpawnReadyPlayers-2");
						byte[] completeBytes3 = protocol_BATTLE_RECORD_ACK.GetCompleteBytes("Room.BaseSpawnReadyPlayers-3");
						foreach (SlotModel slotModel2 in this.Slots)
						{
							Account account;
							if (slotModel2.State == SlotState.BATTLE && slotModel2.IsPlaying == 1 && this.GetPlayerBySlot(slotModel2, out account))
							{
								slotModel2.FirstInactivityOff = true;
								slotModel2.IsPlaying = 2;
								if (this.State == RoomState.PRE_BATTLE)
								{
									using (PROTOCOL_BATTLE_STARTBATTLE_ACK protocol_BATTLE_STARTBATTLE_ACK = new PROTOCOL_BATTLE_STARTBATTLE_ACK(slotModel2, account, dinossaurs, true))
									{
										this.SendPacketToPlayers(protocol_BATTLE_STARTBATTLE_ACK, SlotState.READY, 1);
									}
									account.SendCompletePacket(completeBytes, protocol_BATTLE_MISSION_ROUND_PRE_START_ACK.GetType().Name);
									if (this.IsDinoMode(""))
									{
										flag = true;
									}
									else
									{
										account.SendCompletePacket(completeBytes2, protocol_BATTLE_MISSION_ROUND_START_ACK.GetType().Name);
									}
								}
								else if (this.State == RoomState.BATTLE)
								{
									using (PROTOCOL_BATTLE_STARTBATTLE_ACK protocol_BATTLE_STARTBATTLE_ACK2 = new PROTOCOL_BATTLE_STARTBATTLE_ACK(slotModel2, account, dinossaurs, false))
									{
										this.SendPacketToPlayers(protocol_BATTLE_STARTBATTLE_ACK2, SlotState.READY, 1);
									}
									if (this.RoomType == RoomCondition.Bomb || this.RoomType == RoomCondition.Annihilation || this.RoomType == RoomCondition.Destroy || this.RoomType == RoomCondition.Ace)
									{
										goto IL_272;
									}
									if (this.RoomType == RoomCondition.Glass)
									{
										goto IL_272;
									}
									account.SendCompletePacket(completeBytes, protocol_BATTLE_MISSION_ROUND_PRE_START_ACK.GetType().Name);
									IL_284:
									account.SendCompletePacket(completeBytes2, protocol_BATTLE_MISSION_ROUND_START_ACK.GetType().Name);
									account.SendCompletePacket(completeBytes3, protocol_BATTLE_RECORD_ACK.GetType().Name);
									goto IL_2AE;
									IL_272:
									EquipmentSync.SendUDPPlayerSync(this, slotModel2, (CouponEffects)0L, 1);
									goto IL_284;
								}
							}
							IL_2AE:;
						}
					}
				}
			}
			if (this.State == RoomState.PRE_BATTLE)
			{
				this.State = RoomState.BATTLE;
				this.UpdateRoomInfo();
			}
			if (flag)
			{
				this.method_18();
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00006167 File Offset: 0x00004367
		private void method_18()
		{
			this.RoundTime.StartJob(5250, new TimerCallback(this.method_25));
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x000364E4 File Offset: 0x000346E4
		public bool IsDinoMode(string Dino = "")
		{
			if (Dino.Equals("DE"))
			{
				return this.RoomType == RoomCondition.Boss;
			}
			if (Dino.Equals("CC"))
			{
				return this.RoomType == RoomCondition.CrossCounter;
			}
			return this.RoomType == RoomCondition.Boss || this.RoomType == RoomCondition.CrossCounter;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00036538 File Offset: 0x00034738
		public int GetReadyPlayers()
		{
			int num = 0;
			foreach (SlotModel slotModel in this.Slots)
			{
				if (slotModel != null && slotModel.State >= SlotState.READY && slotModel.Equipment != null)
				{
					Account playerBySlot = this.GetPlayerBySlot(slotModel);
					if (playerBySlot != null && playerBySlot.SlotId == slotModel.Id)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00036598 File Offset: 0x00034798
		public int ResetReadyPlayers()
		{
			int num = 0;
			foreach (SlotModel slotModel in this.Slots)
			{
				if (slotModel.State == SlotState.READY)
				{
					slotModel.State = SlotState.NORMAL;
					num++;
				}
			}
			return num;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x000365D8 File Offset: 0x000347D8
		public TeamEnum CheckTeam(int SlotIdx)
		{
			RoomModel.Class13 @class = new RoomModel.Class13();
			@class.int_0 = SlotIdx;
			if (Array.Exists<int>(this.FR_TEAM, new Predicate<int>(@class.method_0)))
			{
				return TeamEnum.FR_TEAM;
			}
			if (Array.Exists<int>(this.CT_TEAM, new Predicate<int>(@class.method_1)))
			{
				return TeamEnum.CT_TEAM;
			}
			return TeamEnum.ALL_TEAM;
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00006185 File Offset: 0x00004385
		public TeamEnum ValidateTeam(TeamEnum Team, TeamEnum Costume)
		{
			if (this.RoomType == RoomCondition.FreeForAll)
			{
				return Costume;
			}
			if (!this.SwapRound)
			{
				return Team;
			}
			if (Team != TeamEnum.FR_TEAM)
			{
				return TeamEnum.FR_TEAM;
			}
			return TeamEnum.CT_TEAM;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x000061A3 File Offset: 0x000043A3
		public string RandomName(int Random)
		{
			switch (Random)
			{
			case 1:
				return "Feel the Headshots!!";
			case 2:
				return "Land of Dead!";
			case 3:
				return "Kill! or be Killed!!";
			case 4:
				return "Show Me Your Skills!!";
			default:
				return "Point Blank!!";
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x000061DA File Offset: 0x000043DA
		public void CheckGhostSlot(SlotModel Slot)
		{
			if (Slot.State != SlotState.EMPTY && Slot.State != SlotState.CLOSE && Slot.PlayerId == 0L && !this.IsBotMode())
			{
				Slot.ResetSlot();
				Slot.State = SlotState.EMPTY;
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0003662C File Offset: 0x0003482C
		[CompilerGenerated]
		private void method_19(object object_0)
		{
			if (this != null && this.ActiveC4)
			{
				if (this.SwapRound)
				{
					this.CTRounds++;
				}
				else
				{
					this.FRRounds++;
				}
				this.ActiveC4 = false;
				AllUtils.BattleEndRound(this, TeamEnum.FR_TEAM, RoundEndType.BombFire);
			}
			lock (object_0)
			{
				this.BombTime.StopJob();
			}
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000366AC File Offset: 0x000348AC
		[CompilerGenerated]
		private void method_20(object object_0)
		{
			AllUtils.VotekickResult(this);
			lock (object_0)
			{
				this.VoteTime.StopJob();
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000366F4 File Offset: 0x000348F4
		[CompilerGenerated]
		private void method_21(object object_0)
		{
			this.method_4();
			lock (object_0)
			{
				this.RoundTime.StopJob();
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0003673C File Offset: 0x0003493C
		[CompilerGenerated]
		private void method_22(object object_0)
		{
			this.method_5();
			lock (object_0)
			{
				this.RoundTeamSwap.StopJob();
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00036784 File Offset: 0x00034984
		[CompilerGenerated]
		private void method_23(object object_0)
		{
			this.method_6();
			lock (object_0)
			{
				this.CountdownTime.StopJob();
			}
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000367CC File Offset: 0x000349CC
		[CompilerGenerated]
		private void method_24(object object_0)
		{
			this.method_17();
			lock (object_0)
			{
				this.PreMatchTime.StopJob();
			}
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00036814 File Offset: 0x00034A14
		[CompilerGenerated]
		private void method_25(object object_0)
		{
			if (this.State == RoomState.BATTLE)
			{
				using (PROTOCOL_BATTLE_MISSION_ROUND_START_ACK protocol_BATTLE_MISSION_ROUND_START_ACK = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(this))
				{
					this.SendPacketToPlayers(protocol_BATTLE_MISSION_ROUND_START_ACK, SlotState.BATTLE, 0);
				}
			}
			lock (object_0)
			{
				this.RoundTime.StopJob();
			}
		}

		// Token: 0x040003F1 RID: 1009
		public SlotModel[] Slots = new SlotModel[18];

		// Token: 0x040003F2 RID: 1010
		public int Rounds;

		// Token: 0x040003F3 RID: 1011
		public int TRex;

		// Token: 0x040003F4 RID: 1012
		public int CTRounds;

		// Token: 0x040003F5 RID: 1013
		public int CTDino;

		// Token: 0x040003F6 RID: 1014
		public int FRRounds;

		// Token: 0x040003F7 RID: 1015
		public int FRDino;

		// Token: 0x040003F8 RID: 1016
		public int Bar1;

		// Token: 0x040003F9 RID: 1017
		public int Bar2;

		// Token: 0x040003FA RID: 1018
		public int Ping;

		// Token: 0x040003FB RID: 1019
		public int FRKills;

		// Token: 0x040003FC RID: 1020
		public int FRDeaths;

		// Token: 0x040003FD RID: 1021
		public int FRAssists;

		// Token: 0x040003FE RID: 1022
		public int CTKills;

		// Token: 0x040003FF RID: 1023
		public int CTDeaths;

		// Token: 0x04000400 RID: 1024
		public int CTAssists;

		// Token: 0x04000401 RID: 1025
		public int SpawnsCount;

		// Token: 0x04000402 RID: 1026
		public int KillTime;

		// Token: 0x04000403 RID: 1027
		public int RoomId;

		// Token: 0x04000404 RID: 1028
		public int ChannelId;

		// Token: 0x04000405 RID: 1029
		public int ServerId;

		// Token: 0x04000406 RID: 1030
		public int LeaderSlot;

		// Token: 0x04000407 RID: 1031
		public int CountPlayers;

		// Token: 0x04000408 RID: 1032
		public int CountMaxSlots;

		// Token: 0x04000409 RID: 1033
		public int NewInt;

		// Token: 0x0400040A RID: 1034
		public byte Limit;

		// Token: 0x0400040B RID: 1035
		public byte WatchRuleFlag;

		// Token: 0x0400040C RID: 1036
		public byte AiCount;

		// Token: 0x0400040D RID: 1037
		public byte IngameAiLevel;

		// Token: 0x0400040E RID: 1038
		public byte AiLevel;

		// Token: 0x0400040F RID: 1039
		public byte AiType;

		// Token: 0x04000410 RID: 1040
		public byte CountdownIG;

		// Token: 0x04000411 RID: 1041
		public byte KillCam;

		// Token: 0x04000412 RID: 1042
		public uint TimeRoom;

		// Token: 0x04000413 RID: 1043
		public uint StartDate;

		// Token: 0x04000414 RID: 1044
		public uint UniqueRoomId;

		// Token: 0x04000415 RID: 1045
		public uint Seed;

		// Token: 0x04000416 RID: 1046
		public long StartTick;

		// Token: 0x04000417 RID: 1047
		public string Name;

		// Token: 0x04000418 RID: 1048
		public string Password;

		// Token: 0x04000419 RID: 1049
		public string MapName;

		// Token: 0x0400041A RID: 1050
		public string LeaderName;

		// Token: 0x0400041B RID: 1051
		public bool ActiveC4;

		// Token: 0x0400041C RID: 1052
		public bool ChangingSlots;

		// Token: 0x0400041D RID: 1053
		public bool BlockedClan;

		// Token: 0x0400041E RID: 1054
		public bool PreMatchCD;

		// Token: 0x0400041F RID: 1055
		public bool SwapRound;

		// Token: 0x04000420 RID: 1056
		public bool Competitive;

		// Token: 0x04000421 RID: 1057
		public readonly int[] TIMES = new int[]
		{
			3, 3, 3, 5, 7, 5, 10, 15, 20, 25,
			30
		};

		// Token: 0x04000422 RID: 1058
		public readonly int[] KILLS = new int[] { 15, 30, 50, 60, 80, 100, 120, 140, 160 };

		// Token: 0x04000423 RID: 1059
		public readonly int[] ROUNDS = new int[] { 1, 2, 3, 5, 7, 9 };

		// Token: 0x04000424 RID: 1060
		public readonly int[] FR_TEAM = new int[] { 0, 2, 4, 6, 8, 10, 12, 14, 16 };

		// Token: 0x04000425 RID: 1061
		public readonly int[] CT_TEAM = new int[] { 1, 3, 5, 7, 9, 11, 13, 15, 17 };

		// Token: 0x04000426 RID: 1062
		public readonly int[] ALL_TEAM = new int[]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
			10, 11, 12, 13, 14, 15, 16, 17
		};

		// Token: 0x04000427 RID: 1063
		public readonly int[] INVERT_FR_TEAM = new int[] { 16, 14, 12, 10, 8, 6, 4, 2, 0 };

		// Token: 0x04000428 RID: 1064
		public readonly int[] INVERT_CT_TEAM = new int[] { 17, 15, 13, 11, 9, 7, 5, 3, 1 };

		// Token: 0x04000429 RID: 1065
		public byte[] RandomMaps;

		// Token: 0x0400042A RID: 1066
		public byte[] LeaderAddr = new byte[4];

		// Token: 0x0400042B RID: 1067
		public byte[] HitParts = new byte[]
		{
			0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
			10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
			20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
			30, 31, 32, 33, 34
		};

		// Token: 0x0400042C RID: 1068
		public ValueTuple<byte[], int[]> SlotRewards;

		// Token: 0x0400042D RID: 1069
		public ChannelType ChannelType;

		// Token: 0x0400042E RID: 1070
		public MapIdEnum MapId;

		// Token: 0x0400042F RID: 1071
		public RoomCondition RoomType;

		// Token: 0x04000430 RID: 1072
		public RoomState State;

		// Token: 0x04000431 RID: 1073
		public MapRules Rule;

		// Token: 0x04000432 RID: 1074
		public StageOptions Stage;

		// Token: 0x04000433 RID: 1075
		public TeamBalance BalanceType;

		// Token: 0x04000434 RID: 1076
		public RoomStageFlag Flag;

		// Token: 0x04000435 RID: 1077
		public RoomWeaponsFlag WeaponsFlag;

		// Token: 0x04000436 RID: 1078
		public VoteKickModel VoteKick;

		// Token: 0x04000437 RID: 1079
		public Synchronize UdpServer;

		// Token: 0x04000438 RID: 1080
		public DateTime BattleStart;

		// Token: 0x04000439 RID: 1081
		public DateTime LastPingSync;

		// Token: 0x0400043A RID: 1082
		public DateTime LastChangeTeam;

		// Token: 0x0400043B RID: 1083
		public TimerState BombTime = new TimerState();

		// Token: 0x0400043C RID: 1084
		public TimerState CountdownTime = new TimerState();

		// Token: 0x0400043D RID: 1085
		public TimerState RoundTime = new TimerState();

		// Token: 0x0400043E RID: 1086
		public TimerState RoundTeamSwap = new TimerState();

		// Token: 0x0400043F RID: 1087
		public TimerState VoteTime = new TimerState();

		// Token: 0x04000440 RID: 1088
		public TimerState PreMatchTime = new TimerState();

		// Token: 0x04000441 RID: 1089
		public TimerState MatchEndTime = new TimerState();

		// Token: 0x04000442 RID: 1090
		public SafeList<long> KickedPlayersVote = new SafeList<long>();

		// Token: 0x04000443 RID: 1091
		public SafeList<long> RequestRoomMaster = new SafeList<long>();

		// Token: 0x04000444 RID: 1092
		public SortedList<long, DateTime> KickedPlayersHost = new SortedList<long, DateTime>();

		// Token: 0x02000205 RID: 517
		[CompilerGenerated]
		[Serializable]
		private sealed class Class11
		{
			// Token: 0x060006C7 RID: 1735 RVA: 0x0000620A File Offset: 0x0000440A
			// Note: this type is marked as 'beforefieldinit'.
			static Class11()
			{
			}

			// Token: 0x060006C8 RID: 1736 RVA: 0x000025DF File Offset: 0x000007DF
			public Class11()
			{
			}

			// Token: 0x060006C9 RID: 1737 RVA: 0x00005AC9 File Offset: 0x00003CC9
			internal int method_0(SlotModel slotModel_0)
			{
				return slotModel_0.Score;
			}

			// Token: 0x060006CA RID: 1738 RVA: 0x00006216 File Offset: 0x00004416
			internal bool method_1(SlotModel slotModel_0)
			{
				return slotModel_0.Id != 16 && slotModel_0.Id != 17;
			}

			// Token: 0x060006CB RID: 1739 RVA: 0x00006216 File Offset: 0x00004416
			internal bool method_2(SlotModel slotModel_0)
			{
				return slotModel_0.Id != 16 && slotModel_0.Id != 17;
			}

			// Token: 0x04000445 RID: 1093
			public static readonly RoomModel.Class11 <>9 = new RoomModel.Class11();

			// Token: 0x04000446 RID: 1094
			public static Func<SlotModel, int> <>9__119_0;

			// Token: 0x04000447 RID: 1095
			public static Func<SlotModel, bool> <>9__130_0;

			// Token: 0x04000448 RID: 1096
			public static Func<SlotModel, bool> <>9__131_0;
		}

		// Token: 0x02000206 RID: 518
		[CompilerGenerated]
		private sealed class Class12
		{
			// Token: 0x060006CC RID: 1740 RVA: 0x000025DF File Offset: 0x000007DF
			public Class12()
			{
			}

			// Token: 0x060006CD RID: 1741 RVA: 0x00036888 File Offset: 0x00034A88
			internal void method_0(object object_0)
			{
				if (!this.slotModel_0.FirstInactivityOff && this.slotModel_0.State < SlotState.BATTLE && this.slotModel_0.IsPlaying == 0)
				{
					this.roomModel_0.method_3(this.eventErrorEnum_0, this.account_0, this.slotModel_0);
				}
				lock (object_0)
				{
					if (this.slotModel_0 != null)
					{
						this.slotModel_0.StopTiming();
					}
				}
			}

			// Token: 0x04000449 RID: 1097
			public SlotModel slotModel_0;

			// Token: 0x0400044A RID: 1098
			public RoomModel roomModel_0;

			// Token: 0x0400044B RID: 1099
			public EventErrorEnum eventErrorEnum_0;

			// Token: 0x0400044C RID: 1100
			public Account account_0;
		}

		// Token: 0x02000207 RID: 519
		[CompilerGenerated]
		private sealed class Class13
		{
			// Token: 0x060006CE RID: 1742 RVA: 0x000025DF File Offset: 0x000007DF
			public Class13()
			{
			}

			// Token: 0x060006CF RID: 1743 RVA: 0x00006231 File Offset: 0x00004431
			internal bool method_0(int int_1)
			{
				return int_1 == this.int_0;
			}

			// Token: 0x060006D0 RID: 1744 RVA: 0x00006231 File Offset: 0x00004431
			internal bool method_1(int int_1)
			{
				return int_1 == this.int_0;
			}

			// Token: 0x0400044D RID: 1101
			public int int_0;
		}
	}
}
