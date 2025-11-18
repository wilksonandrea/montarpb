using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Server.Game.Data.Models;

public class RoomModel
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class11
	{
		public static readonly Class11 _003C_003E9 = new Class11();

		public static Func<SlotModel, int> _003C_003E9__119_0;

		public static Func<SlotModel, bool> _003C_003E9__130_0;

		public static Func<SlotModel, bool> _003C_003E9__131_0;

		internal int method_0(SlotModel slotModel_0)
		{
			return slotModel_0.Score;
		}

		internal bool method_1(SlotModel slotModel_0)
		{
			if (slotModel_0.Id != 16)
			{
				return slotModel_0.Id != 17;
			}
			return false;
		}

		internal bool method_2(SlotModel slotModel_0)
		{
			if (slotModel_0.Id != 16)
			{
				return slotModel_0.Id != 17;
			}
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class Class12
	{
		public SlotModel slotModel_0;

		public RoomModel roomModel_0;

		public EventErrorEnum eventErrorEnum_0;

		public Account account_0;

		internal void method_0(object object_0)
		{
			if (!slotModel_0.FirstInactivityOff && slotModel_0.State < SlotState.BATTLE && slotModel_0.IsPlaying == 0)
			{
				roomModel_0.method_3(eventErrorEnum_0, account_0, slotModel_0);
			}
			lock (object_0)
			{
				if (slotModel_0 != null)
				{
					slotModel_0.StopTiming();
				}
			}
		}
	}

	[CompilerGenerated]
	private sealed class Class13
	{
		public int int_0;

		internal bool method_0(int int_1)
		{
			return int_1 == int_0;
		}

		internal bool method_1(int int_1)
		{
			return int_1 == int_0;
		}
	}

	public SlotModel[] Slots = new SlotModel[18];

	public int Rounds;

	public int TRex;

	public int CTRounds;

	public int CTDino;

	public int FRRounds;

	public int FRDino;

	public int Bar1;

	public int Bar2;

	public int Ping;

	public int FRKills;

	public int FRDeaths;

	public int FRAssists;

	public int CTKills;

	public int CTDeaths;

	public int CTAssists;

	public int SpawnsCount;

	public int KillTime;

	public int RoomId;

	public int ChannelId;

	public int ServerId;

	public int LeaderSlot;

	public int CountPlayers;

	public int CountMaxSlots;

	public int NewInt;

	public byte Limit;

	public byte WatchRuleFlag;

	public byte AiCount;

	public byte IngameAiLevel;

	public byte AiLevel;

	public byte AiType;

	public byte CountdownIG;

	public byte KillCam;

	public uint TimeRoom;

	public uint StartDate;

	public uint UniqueRoomId;

	public uint Seed;

	public long StartTick;

	public string Name;

	public string Password;

	public string MapName;

	public string LeaderName;

	public bool ActiveC4;

	public bool ChangingSlots;

	public bool BlockedClan;

	public bool PreMatchCD;

	public bool SwapRound;

	public bool Competitive;

	public readonly int[] TIMES = new int[11]
	{
		3, 3, 3, 5, 7, 5, 10, 15, 20, 25,
		30
	};

	public readonly int[] KILLS = new int[9] { 15, 30, 50, 60, 80, 100, 120, 140, 160 };

	public readonly int[] ROUNDS = new int[6] { 1, 2, 3, 5, 7, 9 };

	public readonly int[] FR_TEAM = new int[9] { 0, 2, 4, 6, 8, 10, 12, 14, 16 };

	public readonly int[] CT_TEAM = new int[9] { 1, 3, 5, 7, 9, 11, 13, 15, 17 };

	public readonly int[] ALL_TEAM = new int[18]
	{
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
		10, 11, 12, 13, 14, 15, 16, 17
	};

	public readonly int[] INVERT_FR_TEAM = new int[9] { 16, 14, 12, 10, 8, 6, 4, 2, 0 };

	public readonly int[] INVERT_CT_TEAM = new int[9] { 17, 15, 13, 11, 9, 7, 5, 3, 1 };

	public byte[] RandomMaps;

	public byte[] LeaderAddr = new byte[4];

	public byte[] HitParts = new byte[35]
	{
		0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
		10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
		20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
		30, 31, 32, 33, 34
	};

	public (byte[], int[]) SlotRewards;

	public ChannelType ChannelType;

	public MapIdEnum MapId;

	public RoomCondition RoomType;

	public RoomState State;

	public MapRules Rule;

	public StageOptions Stage;

	public TeamBalance BalanceType;

	public RoomStageFlag Flag;

	public RoomWeaponsFlag WeaponsFlag;

	public VoteKickModel VoteKick;

	public Synchronize UdpServer;

	public DateTime BattleStart;

	public DateTime LastPingSync;

	public DateTime LastChangeTeam;

	public TimerState BombTime = new TimerState();

	public TimerState CountdownTime = new TimerState();

	public TimerState RoundTime = new TimerState();

	public TimerState RoundTeamSwap = new TimerState();

	public TimerState VoteTime = new TimerState();

	public TimerState PreMatchTime = new TimerState();

	public TimerState MatchEndTime = new TimerState();

	public SafeList<long> KickedPlayersVote = new SafeList<long>();

	public SafeList<long> RequestRoomMaster = new SafeList<long>();

	public SortedList<long, DateTime> KickedPlayersHost = new SortedList<long, DateTime>();

	public RoomModel(int int_0, ChannelModel channelModel_0)
	{
		RoomId = int_0;
		for (int i = 0; i < Slots.Length; i++)
		{
			Slots[i] = new SlotModel(i);
		}
		ChannelId = channelModel_0.Id;
		ChannelType = channelModel_0.Type;
		ServerId = channelModel_0.ServerId;
		Rounds = 1;
		AiCount = 1;
		AiLevel = 1;
		TRex = -1;
		Ping = 5;
		Name = "";
		Password = "";
		MapName = "";
		LeaderName = "";
		method_0();
		LastPingSync = (LastChangeTeam = DateTimeUtil.Now());
	}

	private void method_0()
	{
		UniqueRoomId = (uint)(((ServerId & 0xFF) << 20) | ((ChannelId & 0xFF) << 12)) | ((uint)RoomId & 0xFFFu);
	}

	public void GenerateSeed()
	{
		Seed = (uint)((int)(MapId & (MapIdEnum)255) << 20) | ((uint)(Rule & (MapRules)255) << 12) | (uint)(RoomType & (RoomCondition)4095);
	}

	public bool ThisModeHaveCD()
	{
		if (RoomType != RoomCondition.Bomb && RoomType != RoomCondition.Annihilation && RoomType != RoomCondition.Boss && RoomType != RoomCondition.CrossCounter && RoomType != RoomCondition.Destroy && RoomType != RoomCondition.Ace && RoomType != RoomCondition.Escape)
		{
			return RoomType == RoomCondition.Glass;
		}
		return true;
	}

	public bool ThisModeHaveRounds()
	{
		if (RoomType != RoomCondition.Bomb && RoomType != RoomCondition.Destroy && RoomType != RoomCondition.Annihilation && RoomType != RoomCondition.Defense && RoomType != RoomCondition.Destroy && RoomType != RoomCondition.Ace && RoomType != RoomCondition.Escape)
		{
			return RoomType == RoomCondition.Glass;
		}
		return true;
	}

	public int GetFlag()
	{
		int num = 0;
		if (Flag.HasFlag(RoomStageFlag.TEAM_SWAP))
		{
			num++;
		}
		if (Flag.HasFlag(RoomStageFlag.RANDOM_MAP))
		{
			num += 2;
		}
		if (Flag.HasFlag(RoomStageFlag.PASSWORD) || Password.Length > 0)
		{
			num += 4;
		}
		if (Flag.HasFlag(RoomStageFlag.OBSERVER_MODE))
		{
			num += 8;
		}
		if (Flag.HasFlag(RoomStageFlag.REAL_IP))
		{
			num += 16;
		}
		if (Flag.HasFlag(RoomStageFlag.TEAM_BALANCE) || BalanceType == TeamBalance.Count)
		{
			num += 32;
		}
		if (Flag.HasFlag(RoomStageFlag.OBSERVER))
		{
			num += 64;
		}
		if (Flag.HasFlag(RoomStageFlag.INTER_ENTER) || (Limit > 0 && IsStartingMatch()))
		{
			num += 128;
		}
		Flag = (RoomStageFlag)num;
		return num;
	}

	public bool IsBotMode()
	{
		if (Stage != StageOptions.AI && Stage != StageOptions.DieHard)
		{
			return Stage == StageOptions.Infection;
		}
		return true;
	}

	public void SetBotLevel()
	{
		if (IsBotMode())
		{
			IngameAiLevel = AiLevel;
			for (int i = 0; i < 18; i++)
			{
				Slots[i].AiLevel = IngameAiLevel;
			}
		}
	}

	private void method_1()
	{
		if (RoomType == RoomCondition.Defense)
		{
			if (MapId == MapIdEnum.BlackPanther)
			{
				Bar1 = 6000;
				Bar2 = 9000;
			}
		}
		else if (RoomType == RoomCondition.Destroy)
		{
			if (MapId == MapIdEnum.Helispot)
			{
				Bar1 = 12000;
				Bar2 = 12000;
			}
			else if (MapId == MapIdEnum.BreakDown)
			{
				Bar1 = 6000;
				Bar2 = 6000;
			}
		}
	}

	public void ChangeRounds()
	{
		Rounds++;
		if (method_2() && !SwapRound)
		{
			SwapRound = true;
		}
	}

	private bool method_2()
	{
		if (!Flag.HasFlag(RoomStageFlag.TEAM_SWAP))
		{
			return false;
		}
		if (IsDinoMode())
		{
			return Rounds == 2;
		}
		int num = GetRoundsByMask() - 1;
		if ((FRRounds == num && CTRounds == 0) || (CTRounds == num && FRRounds == 0))
		{
			return true;
		}
		return false;
	}

	public int GetInBattleTime()
	{
		int num = 0;
		if (BattleStart != default(DateTime) && (State == RoomState.BATTLE || State == RoomState.PRE_BATTLE))
		{
			num = (int)ComDiv.GetDuration(BattleStart);
			if (num < 0)
			{
				num = 0;
			}
		}
		return num;
	}

	public int GetInBattleTimeLeft()
	{
		return GetTimeByMask() * 60 - GetInBattleTime();
	}

	public ChannelModel GetChannel()
	{
		return ChannelsXML.GetChannel(ServerId, ChannelId);
	}

	public bool GetChannel(out ChannelModel Channel)
	{
		Channel = ChannelsXML.GetChannel(ServerId, ChannelId);
		return Channel != null;
	}

	public bool GetSlot(int SlotId, out SlotModel Slot)
	{
		Slot = null;
		lock (Slots)
		{
			if (SlotId >= 0 && SlotId <= 17)
			{
				Slot = Slots[SlotId];
			}
			return Slot != null;
		}
	}

	public SlotModel GetSlot(int SlotIdx)
	{
		lock (Slots)
		{
			if (SlotIdx >= 0 && SlotIdx <= 17)
			{
				return Slots[SlotIdx];
			}
			return null;
		}
	}

	public void StartCounter(int Type, Account Player, SlotModel Slot)
	{
		int num = 0;
		EventErrorEnum eventErrorEnum_0 = EventErrorEnum.SUCCESS;
		switch (Type)
		{
		case 0:
			eventErrorEnum_0 = EventErrorEnum.BATTLE_FIRST_MAINLOAD;
			num = 90000;
			break;
		case 1:
			eventErrorEnum_0 = EventErrorEnum.BATTLE_FIRST_HOLE;
			num = 30000;
			break;
		}
		if (num > 0)
		{
			Slot.FirstInactivityOff = true;
		}
		Slot.Timing.StartJob(num, delegate(object object_0)
		{
			if (!Slot.FirstInactivityOff && Slot.State < SlotState.BATTLE && Slot.IsPlaying == 0)
			{
				method_3(eventErrorEnum_0, Player, Slot);
			}
			lock (object_0)
			{
				if (Slot != null)
				{
					Slot.StopTiming();
				}
			}
		});
	}

	private void method_3(EventErrorEnum eventErrorEnum_0, Account account_0, SlotModel slotModel_0)
	{
		account_0.SendPacket(new PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK(eventErrorEnum_0));
		account_0.SendPacket(new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(account_0, 0));
		ChangeSlotState(slotModel_0.Id, SlotState.NORMAL, SendInfo: true);
		AllUtils.BattleEndPlayersCount(this, IsBotMode());
	}

	public void StartBomb()
	{
		BombTime.StartJob(42000, delegate(object object_0)
		{
			if (this != null && ActiveC4)
			{
				if (SwapRound)
				{
					CTRounds++;
				}
				else
				{
					FRRounds++;
				}
				ActiveC4 = false;
				AllUtils.BattleEndRound(this, TeamEnum.FR_TEAM, RoundEndType.BombFire);
			}
			lock (object_0)
			{
				BombTime.StopJob();
			}
		});
	}

	public void StartVote()
	{
		if (VoteKick == null)
		{
			return;
		}
		VoteTime.StartJob(20000, delegate(object object_0)
		{
			AllUtils.VotekickResult(this);
			lock (object_0)
			{
				VoteTime.StopJob();
			}
		});
	}

	public void RoundRestart()
	{
		StopBomb();
		SlotModel[] slots = Slots;
		foreach (SlotModel slotModel in slots)
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
				if (slotModel.KillsOnLife >= 3 && (RoomType == RoomCondition.Annihilation || RoomType == RoomCondition.Ace))
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
		RoundTime.StartJob(8250, delegate(object object_0)
		{
			method_4();
			lock (object_0)
			{
				RoundTime.StopJob();
			}
		});
	}

	private void method_4()
	{
		SlotModel[] slots = Slots;
		foreach (SlotModel slotModel in slots)
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
		StopBomb();
		DateTime dateTime = DateTimeUtil.Now();
		if (State == RoomState.BATTLE)
		{
			BattleStart = (IsDinoMode() ? dateTime.AddSeconds(5.0) : dateTime);
		}
		List<int> dinossaurs = AllUtils.GetDinossaurs(this, ForceNewTRex: false, -1);
		using (PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK packet = new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(this, dinossaurs))
		{
			SendPacketToPlayers(packet, SlotState.BATTLE, 0);
		}
		if (method_2())
		{
			RoundTeamSwap.StartJob(5250, delegate(object object_0)
			{
				method_5();
				lock (object_0)
				{
					RoundTeamSwap.StopJob();
				}
			});
		}
		else
		{
			method_5();
		}
		StopBomb();
	}

	private void method_5()
	{
		using PROTOCOL_BATTLE_MISSION_ROUND_START_ACK packet = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(this);
		SendPacketToPlayers(packet, SlotState.BATTLE, 0);
	}

	public void StopBomb()
	{
		if (ActiveC4)
		{
			ActiveC4 = false;
			if (BombTime != null)
			{
				BombTime.StopJob();
			}
		}
	}

	public void StartBattle(bool UpdateInfo)
	{
		lock (Slots)
		{
			State = RoomState.LOADING;
			RequestRoomMaster.Clear();
			SetBotLevel();
			AllUtils.CheckClanMatchRestrict(this);
			StartTick = DateTimeUtil.Now().Ticks;
			StartDate = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			using (PROTOCOL_BATTLE_START_GAME_ACK pROTOCOL_BATTLE_START_GAME_ACK = new PROTOCOL_BATTLE_START_GAME_ACK(this))
			{
				byte[] completeBytes = pROTOCOL_BATTLE_START_GAME_ACK.GetCompleteBytes("Room.StartBattle");
				foreach (Account allPlayer in GetAllPlayers(SlotState.READY, 0))
				{
					SlotModel slot = GetSlot(allPlayer.SlotId);
					if (slot != null)
					{
						slot.WithHost = true;
						slot.State = SlotState.LOAD;
						slot.SetMissionsClone(allPlayer.Mission);
						allPlayer.SendCompletePacket(completeBytes, pROTOCOL_BATTLE_START_GAME_ACK.GetType().Name);
					}
				}
			}
			if (UpdateInfo)
			{
				UpdateSlotsInfo();
			}
			UpdateRoomInfo();
		}
	}

	public void StartCountDown()
	{
		using (PROTOCOL_BATTLE_START_COUNTDOWN_ACK packet = new PROTOCOL_BATTLE_START_COUNTDOWN_ACK(CountDownEnum.Start))
		{
			SendPacketToPlayers(packet);
		}
		CountdownTime.StartJob(5250, delegate(object object_0)
		{
			method_6();
			lock (object_0)
			{
				CountdownTime.StopJob();
			}
		});
	}

	private void method_6()
	{
		if (Slots[LeaderSlot].State == SlotState.READY && State == RoomState.COUNTDOWN)
		{
			StartBattle(UpdateInfo: true);
			return;
		}
		using PROTOCOL_BATTLE_READYBATTLE_ACK pROTOCOL_BATTLE_READYBATTLE_ACK = new PROTOCOL_BATTLE_READYBATTLE_ACK(2147487752u);
		byte[] completeBytes = pROTOCOL_BATTLE_READYBATTLE_ACK.GetCompleteBytes("Room.ReadyBattle");
		foreach (Account allPlayer in GetAllPlayers(SlotState.READY, 0))
		{
			SlotModel slot = GetSlot(allPlayer.SlotId);
			if (slot != null && slot.State == SlotState.READY)
			{
				allPlayer.SendCompletePacket(completeBytes, pROTOCOL_BATTLE_READYBATTLE_ACK.GetType().Name);
			}
		}
	}

	public void StopCountDown(CountDownEnum Type, bool RefreshRoom = true)
	{
		State = RoomState.READY;
		if (RefreshRoom)
		{
			UpdateRoomInfo();
		}
		CountdownTime.StopJob();
		using PROTOCOL_BATTLE_START_COUNTDOWN_ACK packet = new PROTOCOL_BATTLE_START_COUNTDOWN_ACK(Type);
		SendPacketToPlayers(packet);
	}

	public void StopCountDown(int SlotId)
	{
		if (State == RoomState.COUNTDOWN)
		{
			if (SlotId == LeaderSlot)
			{
				StopCountDown(CountDownEnum.StopByHost);
			}
			else if (GetPlayingPlayers((LeaderSlot % 2 == 0) ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM, SlotState.READY, 0) == 0)
			{
				ChangeSlotState(LeaderSlot, SlotState.NORMAL, SendInfo: false);
				StopCountDown(CountDownEnum.StopByPlayer);
			}
		}
	}

	public void CalculateResult()
	{
		lock (Slots)
		{
			method_7(AllUtils.GetWinnerTeam(this), IsBotMode());
		}
	}

	public void CalculateResult(TeamEnum resultType)
	{
		lock (Slots)
		{
			method_7(resultType, IsBotMode());
		}
	}

	public void CalculateResult(TeamEnum resultType, bool isBotMode)
	{
		lock (Slots)
		{
			method_7(resultType, isBotMode);
		}
	}

	public void CalculateResultFreeForAll(int SlotWin)
	{
		lock (Slots)
		{
			method_7((TeamEnum)SlotWin, bool_0: false);
		}
	}

	private void method_7(TeamEnum teamEnum_0, bool bool_0)
	{
		ServerConfig config = GameXender.Client.Config;
		EventRankUpModel runningEvent = EventRankUpXML.GetRunningEvent();
		EventBoostModel runningEvent2 = EventBoostXML.GetRunningEvent();
		EventPlaytimeModel runningEvent3 = EventPlaytimeXML.GetRunningEvent();
		BattlePassModel activeSeasonPass = SeasonChallengeXML.GetActiveSeasonPass();
		DateTime date = DateTimeUtil.Now();
		int[] array = new int[18];
		int num = 0;
		if (config == null)
		{
			CLogger.Print("Server Config Null. RoomResult canceled.", LoggerType.Warning);
			return;
		}
		List<SlotModel> list = new List<SlotModel>();
		for (int i = 0; i < 18; i++)
		{
			SlotModel slotModel = Slots[i];
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
			if (slotModel.Check || slotModel.State != SlotState.BATTLE || !GetPlayerBySlot(slotModel, out var Player))
			{
				continue;
			}
			StatisticTotal basic = Player.Statistic.Basic;
			StatisticSeason season = Player.Statistic.Season;
			StatisticDaily daily = Player.Statistic.Daily;
			StatisticWeapon weapon = Player.Statistic.Weapon;
			DBQuery dBQuery = new DBQuery();
			DBQuery dBQuery2 = new DBQuery();
			DBQuery dBQuery3 = new DBQuery();
			DBQuery dBQuery4 = new DBQuery();
			DBQuery dBQuery5 = new DBQuery();
			slotModel.Check = true;
			double num2 = slotModel.InBattleTime(date);
			int gold = Player.Gold;
			int exp = Player.Exp;
			int cash = Player.Cash;
			if (!bool_0)
			{
				if (config.Missions)
				{
					AllUtils.EndMatchMission(this, Player, slotModel, teamEnum_0);
					if (slotModel.MissionsCompleted)
					{
						Player.Mission = slotModel.Missions;
						DaoManagerSQL.UpdateCurrentPlayerMissionList(Player.PlayerId, Player.Mission);
					}
					AllUtils.GenerateMissionAwards(Player, dBQuery);
				}
				int num3 = ((slotModel.AllKills != 0 || slotModel.AllDeaths != 0) ? ((int)num2) : ((int)(num2 / 3.0)));
				if (RoomType != RoomCondition.Bomb && RoomType != RoomCondition.Annihilation && RoomType != RoomCondition.Ace)
				{
					slotModel.Exp = (int)((double)slotModel.Score + (double)num3 / 2.5 + (double)slotModel.AllDeaths * 1.8 + (double)(slotModel.Objects * 20));
					slotModel.Gold = (int)((double)slotModel.Score + (double)num3 / 3.0 + (double)slotModel.AllDeaths * 1.8 + (double)(slotModel.Objects * 20));
					slotModel.Cash = (int)((double)slotModel.Score / 1.5 + (double)num3 / 4.5 + (double)slotModel.AllDeaths * 1.1 + (double)(slotModel.Objects * 20));
				}
				else
				{
					slotModel.Exp = (int)((double)slotModel.Score + (double)num3 / 2.5 + (double)slotModel.AllDeaths * 2.2 + (double)(slotModel.Objects * 20));
					slotModel.Gold = (int)((double)slotModel.Score + (double)num3 / 3.0 + (double)slotModel.AllDeaths * 2.2 + (double)(slotModel.Objects * 20));
					slotModel.Cash = (int)((double)(slotModel.Score / 2) + (double)num3 / 6.5 + (double)slotModel.AllDeaths * 1.5 + (double)(slotModel.Objects * 10));
				}
				bool flag = slotModel.Team == teamEnum_0;
				if (Rule != MapRules.Chaos && Rule != MapRules.HeadHunter)
				{
					if (basic != null && season != null)
					{
						method_9(Player, basic, season, slotModel, dBQuery2, dBQuery3, num, flag, (int)teamEnum_0);
					}
					if (daily != null)
					{
						method_10(Player, daily, slotModel, dBQuery4, num, flag, (int)teamEnum_0);
					}
					if (weapon != null)
					{
						method_11(Player, weapon, slotModel, dBQuery5);
					}
				}
				if (flag || (RoomType == RoomCondition.FreeForAll && teamEnum_0 == (TeamEnum)num))
				{
					slotModel.Gold += ComDiv.Percentage(slotModel.Gold, 15);
					slotModel.Exp += ComDiv.Percentage(slotModel.Exp, 20);
				}
				if (slotModel.EarnedEXP > 0)
				{
					slotModel.Exp += slotModel.EarnedEXP * 5;
				}
			}
			else
			{
				int num4 = IngameAiLevel * (150 + slotModel.AllDeaths);
				if (num4 == 0)
				{
					num4++;
				}
				int num5 = slotModel.Score / num4;
				slotModel.Gold += num5;
				slotModel.Exp += num5;
			}
			slotModel.Exp = ((slotModel.Exp > ConfigLoader.MaxExpReward) ? ConfigLoader.MaxExpReward : slotModel.Exp);
			slotModel.Gold = ((slotModel.Gold > ConfigLoader.MaxGoldReward) ? ConfigLoader.MaxGoldReward : slotModel.Gold);
			slotModel.Cash = ((slotModel.Cash > ConfigLoader.MaxCashReward) ? ConfigLoader.MaxCashReward : slotModel.Cash);
			if (RoomType == RoomCondition.Ace)
			{
				if (Player.SlotId < 0 || Player.SlotId > 1)
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
				int[] bonuses = runningEvent.GetBonuses(Player.Rank);
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
			PlayerBonus bonus = Player.Bonus;
			if (bonus != null && bonus.Bonuses > 0)
			{
				if ((bonus.Bonuses & 8) == 8)
				{
					num6 += 100;
				}
				if ((bonus.Bonuses & 0x80) == 128)
				{
					num7 += 100;
				}
				if ((bonus.Bonuses & 4) == 4)
				{
					num6 += 50;
				}
				if ((bonus.Bonuses & 0x40) == 64)
				{
					num7 += 50;
				}
				if ((bonus.Bonuses & 2) == 2)
				{
					num6 += 30;
				}
				if ((bonus.Bonuses & 0x20) == 32)
				{
					num7 += 30;
				}
				if ((bonus.Bonuses & 1) == 1)
				{
					num6 += 10;
				}
				if ((bonus.Bonuses & 0x10) == 16)
				{
					num7 += 10;
				}
				if ((bonus.Bonuses & 0x200) == 512)
				{
					num12 += 20;
				}
				if ((bonus.Bonuses & 0x400) == 1024)
				{
					num12 += 30;
				}
				if ((bonus.Bonuses & 0x800) == 2048)
				{
					num12 += 50;
				}
				if ((bonus.Bonuses & 0x1000) == 4096)
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
			PCCafeModel pCCafe = TemplatePackXML.GetPCCafe(Player.CafePC);
			if (pCCafe != null)
			{
				PlayerVip playerVIP = DaoManagerSQL.GetPlayerVIP(Player.PlayerId);
				if (playerVIP != null && InternetCafeXML.IsValidAddress(DaoManagerSQL.GetPlayerIP4Address(Player.PlayerId), playerVIP.Address))
				{
					InternetCafe ıCafe = InternetCafeXML.GetICafe(ConfigLoader.InternetCafeId);
					if (ıCafe != null && (Player.CafePC == CafeEnum.Gold || Player.CafePC == CafeEnum.Silver))
					{
						num8 += ((Player.CafePC == CafeEnum.Gold) ? ıCafe.PremiumExp : ((Player.CafePC == CafeEnum.Silver) ? ıCafe.BasicExp : 0));
						num9 += ((Player.CafePC == CafeEnum.Gold) ? ıCafe.PremiumGold : ((Player.CafePC == CafeEnum.Silver) ? ıCafe.BasicGold : 0));
					}
				}
				num8 += pCCafe.ExpUp;
				num9 += pCCafe.PointUp;
				if (Player.CafePC == CafeEnum.Silver && !slotModel.BonusFlags.HasFlag(ResultIcon.Pc))
				{
					slotModel.BonusFlags |= ResultIcon.Pc;
				}
				else if (Player.CafePC == CafeEnum.Gold && !slotModel.BonusFlags.HasFlag(ResultIcon.PcPlus))
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
			int percent = slotModel.BonusEventExp + slotModel.BonusCafeExp + slotModel.BonusItemExp;
			int percent2 = slotModel.BonusEventPoint + slotModel.BonusCafePoint + slotModel.BonusItemPoint;
			Player.Exp += slotModel.Exp + ComDiv.Percentage(slotModel.Exp, percent);
			Player.Gold += slotModel.Gold + ComDiv.Percentage(slotModel.Gold, percent2);
			if (daily != null)
			{
				daily.ExpGained += slotModel.Exp + ComDiv.Percentage(slotModel.Exp, percent);
				daily.PointGained += slotModel.Gold + ComDiv.Percentage(slotModel.Gold, percent2);
				daily.Playtime += (uint)num2;
				dBQuery4.AddQuery("exp_gained", daily.ExpGained);
				dBQuery4.AddQuery("point_gained", daily.PointGained);
				dBQuery4.AddQuery("playtime", (long)daily.Playtime);
			}
			if (ConfigLoader.WinCashPerBattle)
			{
				Player.Cash += slotModel.Cash;
			}
			RankModel rank = PlayerRankXML.GetRank(Player.Rank);
			if (rank != null && Player.Exp >= rank.OnNextLevel + rank.OnAllExp && Player.Rank <= 50)
			{
				List<int> rewards = PlayerRankXML.GetRewards(Player.Rank);
				if (rewards.Count > 0)
				{
					foreach (int item in rewards)
					{
						GoodsItem good = ShopManager.GetGood(item);
						if (good != null)
						{
							if (ComDiv.GetIdStatics(good.Item.Id, 1) == 6 && Player.Character.GetCharacter(good.Item.Id) == null)
							{
								AllUtils.CreateCharacter(Player, good.Item);
							}
							else
							{
								Player.SendPacket(new PROTOCOL_INVENTORY_GET_INFO_ACK(0, Player, good.Item));
							}
						}
					}
				}
				Player.Gold += rank.OnGoldUp;
				Player.LastRankUpDate = uint.Parse(date.ToString("yyMMddHHmm"));
				Player.SendPacket(new PROTOCOL_BASE_RANK_UP_ACK(++Player.Rank, rank.OnNextLevel));
				dBQuery.AddQuery("last_rank_update", (long)Player.LastRankUpDate);
				dBQuery.AddQuery("rank", Player.Rank);
			}
			if (runningEvent3 != null)
			{
				AllUtils.PlayTimeEvent(Player, runningEvent3, bool_0, slotModel, (long)num2);
			}
			if (activeSeasonPass != null)
			{
				Player.UpdateSeasonpass = true;
				AllUtils.CalculateBattlePass(Player, slotModel, activeSeasonPass);
			}
			if (Competitive)
			{
				AllUtils.CalculateCompetitive(this, Player, slotModel, teamEnum_0 == slotModel.Team);
			}
			AllUtils.DiscountPlayerItems(slotModel, Player);
			if (exp != Player.Exp)
			{
				dBQuery.AddQuery("experience", Player.Exp);
			}
			if (gold != Player.Gold)
			{
				dBQuery.AddQuery("gold", Player.Gold);
			}
			if (cash != Player.Cash)
			{
				dBQuery.AddQuery("cash", Player.Cash);
			}
			ComDiv.UpdateDB("accounts", "player_id", Player.PlayerId, dBQuery.GetTables(), dBQuery.GetValues());
			ComDiv.UpdateDB("player_stat_basics", "owner_id", Player.PlayerId, dBQuery2.GetTables(), dBQuery2.GetValues());
			ComDiv.UpdateDB("player_stat_seasons", "owner_id", Player.PlayerId, dBQuery3.GetTables(), dBQuery3.GetValues());
			ComDiv.UpdateDB("player_stat_dailies", "owner_id", Player.PlayerId, dBQuery4.GetTables(), dBQuery4.GetValues());
			ComDiv.UpdateDB("player_stat_weapons", "owner_id", Player.PlayerId, dBQuery5.GetTables(), dBQuery5.GetValues());
			if (ConfigLoader.WinCashPerBattle && ConfigLoader.ShowCashReceiveWarn)
			{
				Player.SendPacket(new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(Translation.GetLabel("CashReceived", slotModel.Cash)));
			}
			list.Add(slotModel);
		}
		if (list.Count > 0)
		{
			SlotRewards = AllUtils.GetRewardData(this, list);
			method_8(list, bool_0);
		}
		UpdateSlotsInfo();
		if (RoomType != RoomCondition.FreeForAll)
		{
			method_14(teamEnum_0);
		}
	}

	private void method_8(List<SlotModel> list_0, bool bool_0)
	{
		SlotModel slotModel = list_0.OrderByDescending((SlotModel slotModel_0) => slotModel_0.Score).FirstOrDefault();
		if (slotModel != null && slotModel.Check && slotModel.State == SlotState.BATTLE && GetPlayerBySlot(slotModel, out var Player))
		{
			StatisticTotal basic = Player.Statistic.Basic;
			StatisticSeason season = Player.Statistic.Season;
			if (!bool_0 && basic != null && season != null)
			{
				basic.MvpCount++;
				season.MvpCount++;
				ComDiv.UpdateDB("player_stat_basics", "mvp_count", basic.MvpCount, "owner_id", Player.PlayerId);
				ComDiv.UpdateDB("player_stat_seasons", "mvp_count", season.MvpCount, "owner_id", Player.PlayerId);
			}
		}
	}

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
		method_12(slotModel_0, account_0.Statistic, dbquery_0, dbquery_1);
		if (RoomType == RoomCondition.FreeForAll)
		{
			AllUtils.UpdateMatchCountFFA(this, account_0, int_0, dbquery_0, dbquery_1);
		}
		else
		{
			AllUtils.UpdateMatchCount(bool_0, account_0, int_1, dbquery_0, dbquery_1);
		}
	}

	private void method_10(Account account_0, StatisticDaily statisticDaily_0, SlotModel slotModel_0, DBQuery dbquery_0, int int_0, bool bool_0, int int_1)
	{
		statisticDaily_0.KillsCount += slotModel_0.AllKills;
		statisticDaily_0.DeathsCount += slotModel_0.AllDeaths;
		statisticDaily_0.HeadshotsCount += slotModel_0.AllHeadshots;
		method_13(slotModel_0, account_0.Statistic, dbquery_0);
		if (RoomType == RoomCondition.FreeForAll)
		{
			AllUtils.UpdateMatchDailyRecordFFA(this, account_0, int_0, dbquery_0);
		}
		else
		{
			AllUtils.UpdateDailyRecord(bool_0, account_0, int_1, dbquery_0);
		}
	}

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

	private void method_14(TeamEnum teamEnum_0)
	{
		if (ChannelType != ChannelType.Clan || BlockedClan)
		{
			return;
		}
		SortedList<int, ClanModel> sortedList = new SortedList<int, ClanModel>();
		SlotModel[] slots = Slots;
		foreach (SlotModel slotModel in slots)
		{
			if (slotModel.State != SlotState.BATTLE || !GetPlayerBySlot(slotModel, out var Player))
			{
				continue;
			}
			ClanModel clan = ClanManager.GetClan(Player.ClanId);
			if (clan.Id == 0)
			{
				continue;
			}
			bool flag = slotModel.Team == teamEnum_0;
			clan.Exp += slotModel.Exp;
			clan.BestPlayers.SetBestExp(slotModel);
			clan.BestPlayers.SetBestKills(slotModel);
			clan.BestPlayers.SetBestHeadshot(slotModel);
			clan.BestPlayers.SetBestWins(Player.Statistic, slotModel, flag);
			clan.BestPlayers.SetBestParticipation(Player.Statistic, slotModel);
			if (sortedList.ContainsKey(Player.ClanId))
			{
				continue;
			}
			sortedList.Add(Player.ClanId, clan);
			if (teamEnum_0 != TeamEnum.TEAM_DRAW)
			{
				method_15(clan, teamEnum_0, slotModel.Team);
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
		foreach (ClanModel value in sortedList.Values)
		{
			DaoManagerSQL.UpdateClanExp(value.Id, value.Exp);
			DaoManagerSQL.UpdateClanPoints(value.Id, value.Points);
			DaoManagerSQL.UpdateClanBestPlayers(value);
			RankModel rank = ClanRankXML.GetRank(value.Rank);
			if (rank != null && value.Exp >= rank.OnNextLevel + rank.OnAllExp)
			{
				DaoManagerSQL.UpdateClanRank(value.Id, ++value.Rank);
			}
		}
	}

	private void method_15(ClanModel clanModel_0, TeamEnum teamEnum_0, TeamEnum teamEnum_1)
	{
		if (teamEnum_0 != TeamEnum.TEAM_DRAW)
		{
			if (teamEnum_0 == teamEnum_1)
			{
				float num = ((RoomType != RoomCondition.DeathMatch) ? ((float)((teamEnum_1 == TeamEnum.FR_TEAM) ? FRRounds : CTRounds)) : ((float)(((teamEnum_1 == TeamEnum.FR_TEAM) ? FRKills : CTKills) / 20)));
				float num2 = 25f + num;
				clanModel_0.Points += num2;
			}
			else if (clanModel_0.Points != 0f)
			{
				float num3 = ((RoomType != RoomCondition.DeathMatch) ? ((float)((teamEnum_1 == TeamEnum.FR_TEAM) ? FRRounds : CTRounds)) : ((float)(((teamEnum_1 == TeamEnum.FR_TEAM) ? FRKills : CTKills) / 20)));
				float num4 = 40f - num3;
				clanModel_0.Points -= num4;
			}
		}
	}

	public bool IsStartingMatch()
	{
		return State > RoomState.READY;
	}

	public bool IsPreparing()
	{
		return State >= RoomState.LOADING;
	}

	public void UpdateRoomInfo()
	{
		GenerateSeed();
		using PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK packet = new PROTOCOL_ROOM_CHANGE_ROOMINFO_ACK(this);
		SendPacketToPlayers(packet);
	}

	public void SetSlotCount(int Count, bool IsCreateRoom, bool IsUpdateRoom)
	{
		MapMatch mapLimit = SystemMapXML.GetMapLimit((int)MapId, (int)Rule);
		if (mapLimit == null)
		{
			return;
		}
		if (Count > mapLimit.Limit)
		{
			Count = mapLimit.Limit;
		}
		if (RoomType == RoomCondition.Tutorial)
		{
			Count = 1;
		}
		if (IsBotMode())
		{
			Count = 8;
		}
		if (Count <= 0)
		{
			Count = 1;
		}
		if (IsCreateRoom)
		{
			lock (Slots)
			{
				foreach (SlotModel item in Slots.Where((SlotModel slotModel_0) => slotModel_0.Id != 16 && slotModel_0.Id != 17))
				{
					if (item.Id >= Count)
					{
						item.State = SlotState.CLOSE;
					}
				}
			}
		}
		if (IsUpdateRoom)
		{
			UpdateSlotsInfo();
		}
	}

	public int GetSlotCount()
	{
		lock (Slots)
		{
			int num = 0;
			foreach (SlotModel item in Slots.Where((SlotModel slotModel_0) => slotModel_0.Id != 16 && slotModel_0.Id != 17))
			{
				if (item.State != SlotState.CLOSE)
				{
					num++;
				}
			}
			return num;
		}
	}

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
			if (Player.SlotId == LeaderSlot)
			{
				LeaderName = Player.Nickname;
				LeaderSlot = NewSlot.Id;
			}
			Player.SlotId = NewSlot.Id;
			SlotChanges.Add(new SlotChange(OldSlot, NewSlot));
		}
	}

	public void SwitchSlots(List<SlotChange> SlotChanges, int NewSlotId, int OldSlotId, bool ChangeReady)
	{
		SlotModel slotModel = Slots[NewSlotId];
		SlotModel slotModel2 = Slots[OldSlotId];
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
		Slots[NewSlotId] = slotModel2;
		Slots[OldSlotId] = slotModel;
		SlotChanges.Add(new SlotChange(slotModel, slotModel2));
	}

	public void ChangeSlotState(int SlotId, SlotState State, bool SendInfo)
	{
		ChangeSlotState(GetSlot(SlotId), State, SendInfo);
	}

	public void ChangeSlotState(SlotModel Slot, SlotState State, bool SendInfo)
	{
		if (Slot != null && Slot.State != State)
		{
			Slot.State = State;
			if (State == SlotState.EMPTY || State == SlotState.CLOSE)
			{
				AllUtils.ResetSlotInfo(this, Slot, UpdateInfo: false);
				Slot.PlayerId = 0L;
			}
			if (SendInfo)
			{
				UpdateSlotsInfo();
			}
		}
	}

	public Account GetPlayerBySlot(SlotModel Slot)
	{
		try
		{
			long playerId = Slot.PlayerId;
			return (playerId > 0L) ? AccountManager.GetAccount(playerId, noUseDB: true) : null;
		}
		catch
		{
			return null;
		}
	}

	public Account GetPlayerBySlot(int SlotId)
	{
		try
		{
			long playerId = Slots[SlotId].PlayerId;
			return (playerId > 0L) ? AccountManager.GetAccount(playerId, noUseDB: true) : null;
		}
		catch
		{
			return null;
		}
	}

	public bool GetPlayerBySlot(int SlotId, out Account Player)
	{
		try
		{
			long playerId = Slots[SlotId].PlayerId;
			Player = ((playerId > 0L) ? AccountManager.GetAccount(playerId, noUseDB: true) : null);
			return Player != null;
		}
		catch
		{
			Player = null;
			return false;
		}
	}

	public bool GetPlayerBySlot(SlotModel Slot, out Account Player)
	{
		try
		{
			long playerId = Slot.PlayerId;
			Player = ((playerId > 0L) ? AccountManager.GetAccount(playerId, noUseDB: true) : null);
			return Player != null;
		}
		catch
		{
			Player = null;
			return false;
		}
	}

	public int GetTimeByMask()
	{
		return TIMES[KillTime >> 4];
	}

	public int GetRoundsByMask()
	{
		return ROUNDS[KillTime & 0xF];
	}

	public int GetKillsByMask()
	{
		return KILLS[KillTime & 0xF];
	}

	public void UpdateSlotsInfo()
	{
		using PROTOCOL_ROOM_GET_SLOTINFO_ACK packet = new PROTOCOL_ROOM_GET_SLOTINFO_ACK(this);
		SendPacketToPlayers(packet);
	}

	public bool GetLeader(out Account Player)
	{
		Player = null;
		if (GetCountPlayers() <= 0)
		{
			return false;
		}
		if (LeaderSlot == -1)
		{
			SetNewLeader(-1, SlotState.EMPTY, -1, UpdateInfo: false);
		}
		if (LeaderSlot >= 0)
		{
			Player = AccountManager.GetAccount(Slots[LeaderSlot].PlayerId, noUseDB: true);
		}
		return Player != null;
	}

	public Account GetLeader()
	{
		if (GetCountPlayers() <= 0)
		{
			return null;
		}
		if (LeaderSlot == -1)
		{
			SetNewLeader(-1, SlotState.EMPTY, -1, UpdateInfo: false);
		}
		if (LeaderSlot != -1)
		{
			return AccountManager.GetAccount(Slots[LeaderSlot].PlayerId, noUseDB: true);
		}
		return null;
	}

	public void SetNewLeader(int LeaderSlot, SlotState State, int OldLeader, bool UpdateInfo)
	{
		lock (Slots)
		{
			if (LeaderSlot == -1)
			{
				SlotModel[] slots = Slots;
				foreach (SlotModel slotModel in slots)
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
				SlotModel slotModel2 = Slots[this.LeaderSlot];
				if (slotModel2.State == SlotState.READY)
				{
					slotModel2.State = SlotState.NORMAL;
				}
				if (UpdateInfo)
				{
					UpdateSlotsInfo();
				}
			}
		}
	}

	public void SendPacketToPlayers(GameServerPacket Packet)
	{
		List<Account> allPlayers = GetAllPlayers();
		if (allPlayers.Count == 0)
		{
			return;
		}
		byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket)");
		foreach (Account item in allPlayers)
		{
			item.SendCompletePacket(completeBytes, Packet.GetType().Name);
		}
	}

	public void SendPacketToPlayers(GameServerPacket Packet, long PlayerId)
	{
		List<Account> allPlayers = GetAllPlayers(PlayerId);
		if (allPlayers.Count == 0)
		{
			return;
		}
		byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,long)");
		foreach (Account item in allPlayers)
		{
			item.SendCompletePacket(completeBytes, Packet.GetType().Name);
		}
	}

	public void SendPacketToPlayers(GameServerPacket Packet, SlotState State, int Type)
	{
		List<Account> allPlayers = GetAllPlayers(State, Type);
		if (allPlayers.Count == 0)
		{
			return;
		}
		byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SLOT_STATE,int)");
		foreach (Account item in allPlayers)
		{
			item.SendCompletePacket(completeBytes, Packet.GetType().Name);
		}
	}

	public void SendPacketToPlayers(GameServerPacket Packet, GameServerPacket Packet2, SlotState State, int Type)
	{
		List<Account> allPlayers = GetAllPlayers(State, Type);
		if (allPlayers.Count == 0)
		{
			return;
		}
		byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SendPacket,SLOT_STATE,int)-1");
		byte[] completeBytes2 = Packet2.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SendPacket,SLOT_STATE,int)-2");
		foreach (Account item in allPlayers)
		{
			item.SendCompletePacket(completeBytes, Packet.GetType().Name);
			item.SendCompletePacket(completeBytes2, Packet2.GetType().Name);
		}
	}

	public void SendPacketToPlayers(GameServerPacket Packet, SlotState State, int Type, int Exception)
	{
		List<Account> allPlayers = GetAllPlayers(State, Type, Exception);
		if (allPlayers.Count == 0)
		{
			return;
		}
		byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SLOT_STATE,int,int)");
		foreach (Account item in allPlayers)
		{
			item.SendCompletePacket(completeBytes, Packet.GetType().Name);
		}
	}

	public void SendPacketToPlayers(GameServerPacket Packet, SlotState State, int Type, int Exception, int Exception2)
	{
		List<Account> allPlayers = GetAllPlayers(State, Type, Exception, Exception2);
		if (allPlayers.Count == 0)
		{
			return;
		}
		byte[] completeBytes = Packet.GetCompleteBytes("Room.SendPacketToPlayers(SendPacket,SLOT_STATE,int,int,int)");
		foreach (Account item in allPlayers)
		{
			item.SendCompletePacket(completeBytes, Packet.GetType().Name);
		}
	}

	public void RemovePlayer(Account Player, bool WarnAllPlayers, int QuitMotive = 0)
	{
		if (Player != null && GetSlot(Player.SlotId, out var Slot))
		{
			method_16(Player, Slot, WarnAllPlayers, QuitMotive);
		}
	}

	public void RemovePlayer(Account Player, SlotModel Slot, bool WarnAllPlayers, int QuitMotive = 0)
	{
		if (Player != null && Slot != null)
		{
			method_16(Player, Slot, WarnAllPlayers, QuitMotive);
		}
	}

	private void method_16(Account account_0, SlotModel slotModel_0, bool bool_0, int int_0)
	{
		lock (Slots)
		{
			bool flag = false;
			bool host = false;
			if (account_0 != null && slotModel_0 != null)
			{
				if (slotModel_0.State >= SlotState.LOAD)
				{
					if (LeaderSlot == slotModel_0.Id)
					{
						int leaderSlot = LeaderSlot;
						SlotState state = SlotState.CLOSE;
						if (State == RoomState.BATTLE)
						{
							state = SlotState.BATTLE_READY;
						}
						else if (State >= RoomState.LOADING)
						{
							state = SlotState.READY;
						}
						if (GetAllPlayers(slotModel_0.Id).Count >= 1)
						{
							SetNewLeader(-1, state, leaderSlot, UpdateInfo: false);
						}
						if (GetPlayingPlayers(TeamEnum.TEAM_DRAW, SlotState.READY, 1) >= 2)
						{
							using PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK packet = new PROTOCOL_BATTLE_LEAVEP2PSERVER_ACK(this);
							SendPacketToPlayers(packet, SlotState.RENDEZVOUS, 1, slotModel_0.Id);
						}
						host = true;
					}
					using (PROTOCOL_BATTLE_GIVEUPBATTLE_ACK packet2 = new PROTOCOL_BATTLE_GIVEUPBATTLE_ACK(account_0, int_0))
					{
						SendPacketToPlayers(packet2, SlotState.READY, 1, (!bool_0) ? slotModel_0.Id : (-1));
					}
					BattleLeaveSync.SendUDPPlayerLeave(this, slotModel_0.Id);
					slotModel_0.ResetSlot();
					if (VoteKick != null)
					{
						VoteKick.TotalArray[slotModel_0.Id] = false;
					}
				}
				slotModel_0.PlayerId = 0L;
				slotModel_0.Equipment = null;
				slotModel_0.State = SlotState.EMPTY;
				if (State == RoomState.COUNTDOWN)
				{
					if (slotModel_0.Id == LeaderSlot)
					{
						State = RoomState.READY;
						flag = true;
						CountdownTime.StopJob();
						using PROTOCOL_BATTLE_START_COUNTDOWN_ACK packet3 = new PROTOCOL_BATTLE_START_COUNTDOWN_ACK(CountDownEnum.StopByHost);
						SendPacketToPlayers(packet3);
					}
					else if (GetPlayingPlayers(slotModel_0.Team, SlotState.READY, 0) == 0)
					{
						if (slotModel_0.Id != LeaderSlot)
						{
							ChangeSlotState(LeaderSlot, SlotState.NORMAL, SendInfo: false);
						}
						StopCountDown(CountDownEnum.StopByPlayer, RefreshRoom: false);
						flag = true;
					}
				}
				else if (IsPreparing())
				{
					AllUtils.BattleEndPlayersCount(this, IsBotMode());
					if (State == RoomState.BATTLE)
					{
						AllUtils.BattleEndRoundPlayersCount(this);
					}
				}
				CheckToEndWaitingBattle(host);
				RequestRoomMaster.Remove(account_0.PlayerId);
				if (VoteTime.IsTimer() && VoteKick != null && VoteKick.VictimIdx == account_0.SlotId && int_0 != 2)
				{
					VoteTime.StopJob();
					VoteKick = null;
					using PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK packet4 = new PROTOCOL_BATTLE_NOTIFY_KICKVOTE_CANCEL_ACK();
					SendPacketToPlayers(packet4, SlotState.BATTLE, 0);
				}
				MatchModel match = account_0.Match;
				if (match != null && account_0.MatchSlot >= 0)
				{
					match.Slots[account_0.MatchSlot].State = SlotMatchState.Normal;
					using PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK packet5 = new PROTOCOL_CLAN_WAR_REGIST_MERCENARY_ACK(match);
					match.SendPacketToPlayers(packet5);
				}
				account_0.Room = null;
				account_0.SlotId = -1;
				account_0.Status.UpdateRoom(byte.MaxValue);
				AllUtils.SyncPlayerToClanMembers(account_0);
				AllUtils.SyncPlayerToFriends(account_0, all: false);
				account_0.UpdateCacheInfo();
			}
			UpdateSlotsInfo();
			if (flag)
			{
				UpdateRoomInfo();
			}
		}
	}

	public int AddPlayer(Account Player)
	{
		lock (Slots)
		{
			SlotModel[] slots = Slots;
			foreach (SlotModel slotModel in slots)
			{
				if (slotModel.PlayerId == 0L && slotModel.State == SlotState.EMPTY)
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
					Player.Status.UpdateRoom((byte)RoomId);
					AllUtils.SyncPlayerToClanMembers(Player);
					AllUtils.SyncPlayerToFriends(Player, all: false);
					Player.UpdateCacheInfo();
					return slotModel.Id;
				}
			}
		}
		return -1;
	}

	public int AddPlayer(Account Player, TeamEnum TeamIdx)
	{
		lock (Slots)
		{
			int[] teamArray = GetTeamArray(TeamIdx);
			foreach (int num in teamArray)
			{
				SlotModel slotModel = Slots[num];
				if (slotModel.PlayerId == 0L && slotModel.State == SlotState.EMPTY)
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
					Player.Status.UpdateRoom((byte)RoomId);
					AllUtils.SyncPlayerToClanMembers(Player);
					AllUtils.SyncPlayerToFriends(Player, all: false);
					Player.UpdateCacheInfo();
					return slotModel.Id;
				}
			}
		}
		return -1;
	}

	public int[] GetTeamArray(TeamEnum Team)
	{
		return Team switch
		{
			TeamEnum.CT_TEAM => CT_TEAM, 
			TeamEnum.FR_TEAM => FR_TEAM, 
			_ => ALL_TEAM, 
		};
	}

	public List<Account> GetAllPlayers(SlotState State, int Type)
	{
		List<Account> list = new List<Account>();
		lock (Slots)
		{
			SlotModel[] slots = Slots;
			foreach (SlotModel slotModel in slots)
			{
				if (slotModel.PlayerId > 0L && ((Type == 0 && slotModel.State == State) || (Type == 1 && slotModel.State > State)))
				{
					Account account = AccountManager.GetAccount(slotModel.PlayerId, noUseDB: true);
					if (account != null && account.SlotId != -1)
					{
						list.Add(account);
					}
				}
			}
			return list;
		}
	}

	public List<Account> GetAllPlayers(SlotState State, int Type, int Exception)
	{
		List<Account> list = new List<Account>();
		lock (Slots)
		{
			SlotModel[] slots = Slots;
			foreach (SlotModel slotModel in slots)
			{
				if (slotModel.PlayerId > 0L && slotModel.Id != Exception && ((Type == 0 && slotModel.State == State) || (Type == 1 && slotModel.State > State)))
				{
					Account account = AccountManager.GetAccount(slotModel.PlayerId, noUseDB: true);
					if (account != null && account.SlotId != -1)
					{
						list.Add(account);
					}
				}
			}
			return list;
		}
	}

	public List<Account> GetAllPlayers(SlotState State, int Type, int Exception, int Exception2)
	{
		List<Account> list = new List<Account>();
		lock (Slots)
		{
			SlotModel[] slots = Slots;
			foreach (SlotModel slotModel in slots)
			{
				if (slotModel.PlayerId > 0L && slotModel.Id != Exception && slotModel.Id != Exception2 && ((Type == 0 && slotModel.State == State) || (Type == 1 && slotModel.State > State)))
				{
					Account account = AccountManager.GetAccount(slotModel.PlayerId, noUseDB: true);
					if (account != null && account.SlotId != -1)
					{
						list.Add(account);
					}
				}
			}
			return list;
		}
	}

	public List<Account> GetAllPlayers(int Exception)
	{
		List<Account> list = new List<Account>();
		lock (Slots)
		{
			SlotModel[] slots = Slots;
			foreach (SlotModel slotModel in slots)
			{
				long playerId = slotModel.PlayerId;
				if (playerId > 0L && slotModel.Id != Exception)
				{
					Account account = AccountManager.GetAccount(playerId, noUseDB: true);
					if (account != null && account.SlotId != -1)
					{
						list.Add(account);
					}
				}
			}
			return list;
		}
	}

	public List<Account> GetAllPlayers(long Exception)
	{
		List<Account> list = new List<Account>();
		lock (Slots)
		{
			SlotModel[] slots = Slots;
			foreach (SlotModel slotModel in slots)
			{
				if (slotModel.PlayerId > 0L && slotModel.PlayerId != Exception)
				{
					Account account = AccountManager.GetAccount(slotModel.PlayerId, noUseDB: true);
					if (account != null && account.SlotId != -1)
					{
						list.Add(account);
					}
				}
			}
			return list;
		}
	}

	public List<Account> GetAllPlayers()
	{
		List<Account> list = new List<Account>();
		lock (Slots)
		{
			SlotModel[] slots = Slots;
			foreach (SlotModel slotModel in slots)
			{
				if (slotModel.PlayerId > 0L)
				{
					Account account = AccountManager.GetAccount(slotModel.PlayerId, noUseDB: true);
					if (account != null && account.SlotId != -1)
					{
						list.Add(account);
					}
				}
			}
			return list;
		}
	}

	public int GetCountPlayers()
	{
		int num = 0;
		lock (Slots)
		{
			SlotModel[] slots = Slots;
			foreach (SlotModel slotModel in slots)
			{
				if (slotModel.PlayerId > 0L)
				{
					Account account = AccountManager.GetAccount(slotModel.PlayerId, noUseDB: true);
					if (account != null && account.SlotId != -1)
					{
						num++;
					}
				}
			}
			return num;
		}
	}

	public int GetPlayingPlayers(TeamEnum Team, bool InBattle)
	{
		int num = 0;
		lock (Slots)
		{
			SlotModel[] slots = Slots;
			foreach (SlotModel slotModel in slots)
			{
				if (slotModel.PlayerId > 0L && (slotModel.Team == Team || Team == TeamEnum.TEAM_DRAW) && ((InBattle && slotModel.State == SlotState.BATTLE_LOAD && !slotModel.Spectator) || (!InBattle && slotModel.State >= SlotState.LOAD)))
				{
					num++;
				}
			}
			return num;
		}
	}

	public int GetPlayingPlayers(TeamEnum Team, SlotState State, int Type)
	{
		int num = 0;
		lock (Slots)
		{
			SlotModel[] slots = Slots;
			foreach (SlotModel slotModel in slots)
			{
				if (slotModel.PlayerId > 0L && ((Type == 0 && slotModel.State == State) || (Type == 1 && slotModel.State > State)) && (Team == TeamEnum.TEAM_DRAW || slotModel.Team == Team))
				{
					num++;
				}
			}
			return num;
		}
	}

	public int GetPlayingPlayers(TeamEnum Team, SlotState State, int Type, int Exception)
	{
		int num = 0;
		lock (Slots)
		{
			SlotModel[] slots = Slots;
			foreach (SlotModel slotModel in slots)
			{
				if (slotModel.Id != Exception && slotModel.PlayerId > 0L && ((Type == 0 && slotModel.State == State) || (Type == 1 && slotModel.State > State)) && (Team == TeamEnum.TEAM_DRAW || slotModel.Team == Team))
				{
					num++;
				}
			}
			return num;
		}
	}

	public void GetPlayingPlayers(bool InBattle, out int PlayerFR, out int PlayerCT)
	{
		PlayerFR = 0;
		PlayerCT = 0;
		lock (Slots)
		{
			SlotModel[] slots = Slots;
			foreach (SlotModel slotModel in slots)
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

	public void GetPlayingPlayers(bool InBattle, out int PlayerFR, out int PlayerCT, out int DeathFR, out int DeathCT)
	{
		PlayerFR = 0;
		PlayerCT = 0;
		DeathFR = 0;
		DeathCT = 0;
		lock (Slots)
		{
			SlotModel[] slots = Slots;
			foreach (SlotModel slotModel in slots)
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

	public void CheckToEndWaitingBattle(bool host)
	{
		if ((State == RoomState.COUNTDOWN || State == RoomState.LOADING || State == RoomState.RENDEZVOUS) && (host || Slots[LeaderSlot].State == SlotState.BATTLE_READY))
		{
			AllUtils.EndBattleNoPoints(this);
		}
	}

	public void SpawnReadyPlayers()
	{
		lock (Slots)
		{
			bool flag = ThisModeHaveRounds() && (CountdownIG == 3 || CountdownIG == 5 || CountdownIG == 7 || CountdownIG == 9);
			if (State == RoomState.PRE_BATTLE && !PreMatchCD && flag && !IsBotMode())
			{
				PreMatchCD = true;
				using PROTOCOL_BATTLE_COUNT_DOWN_ACK packet = new PROTOCOL_BATTLE_COUNT_DOWN_ACK(CountdownIG);
				SendPacketToPlayers(packet);
			}
			int period = ((CountdownIG != 0) ? (CountdownIG * 1000 + 250) : 0);
			PreMatchTime.StartJob(period, delegate(object object_0)
			{
				method_17();
				lock (object_0)
				{
					PreMatchTime.StopJob();
				}
			});
		}
	}

	private void method_17()
	{
		DateTime dateTime = DateTimeUtil.Now();
		SlotModel[] slots = Slots;
		foreach (SlotModel slotModel in slots)
		{
			if (slotModel.State == SlotState.BATTLE_READY && slotModel.IsPlaying == 0 && slotModel.PlayerId > 0L)
			{
				slotModel.IsPlaying = 1;
				slotModel.StartTime = dateTime;
				slotModel.State = SlotState.BATTLE;
				if (State == RoomState.BATTLE && (RoomType == RoomCondition.Bomb || RoomType == RoomCondition.Annihilation || RoomType == RoomCondition.Destroy || RoomType == RoomCondition.Ace || RoomType == RoomCondition.Glass))
				{
					slotModel.Spectator = true;
				}
			}
		}
		UpdateSlotsInfo();
		List<int> dinossaurs = AllUtils.GetDinossaurs(this, ForceNewTRex: false, -1);
		if (State == RoomState.PRE_BATTLE)
		{
			BattleStart = (IsDinoMode() ? dateTime.AddMinutes(5.0) : dateTime);
			method_1();
		}
		bool flag = false;
		using (PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK pROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK = new PROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK(this, dinossaurs))
		{
			using PROTOCOL_BATTLE_MISSION_ROUND_START_ACK pROTOCOL_BATTLE_MISSION_ROUND_START_ACK = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(this);
			using PROTOCOL_BATTLE_RECORD_ACK pROTOCOL_BATTLE_RECORD_ACK = new PROTOCOL_BATTLE_RECORD_ACK(this);
			byte[] completeBytes = pROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK.GetCompleteBytes("Room.BaseSpawnReadyPlayers-1");
			byte[] completeBytes2 = pROTOCOL_BATTLE_MISSION_ROUND_START_ACK.GetCompleteBytes("Room.BaseSpawnReadyPlayers-2");
			byte[] completeBytes3 = pROTOCOL_BATTLE_RECORD_ACK.GetCompleteBytes("Room.BaseSpawnReadyPlayers-3");
			slots = Slots;
			foreach (SlotModel slotModel2 in slots)
			{
				if (slotModel2.State != SlotState.BATTLE || slotModel2.IsPlaying != 1 || !GetPlayerBySlot(slotModel2, out var Player))
				{
					continue;
				}
				slotModel2.FirstInactivityOff = true;
				slotModel2.IsPlaying = 2;
				if (State == RoomState.PRE_BATTLE)
				{
					using (PROTOCOL_BATTLE_STARTBATTLE_ACK packet = new PROTOCOL_BATTLE_STARTBATTLE_ACK(slotModel2, Player, dinossaurs, bool_1: true))
					{
						SendPacketToPlayers(packet, SlotState.READY, 1);
					}
					Player.SendCompletePacket(completeBytes, pROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK.GetType().Name);
					if (IsDinoMode())
					{
						flag = true;
					}
					else
					{
						Player.SendCompletePacket(completeBytes2, pROTOCOL_BATTLE_MISSION_ROUND_START_ACK.GetType().Name);
					}
				}
				else if (State == RoomState.BATTLE)
				{
					using (PROTOCOL_BATTLE_STARTBATTLE_ACK packet2 = new PROTOCOL_BATTLE_STARTBATTLE_ACK(slotModel2, Player, dinossaurs, bool_1: false))
					{
						SendPacketToPlayers(packet2, SlotState.READY, 1);
					}
					if (RoomType != RoomCondition.Bomb && RoomType != RoomCondition.Annihilation && RoomType != RoomCondition.Destroy && RoomType != RoomCondition.Ace && RoomType != RoomCondition.Glass)
					{
						Player.SendCompletePacket(completeBytes, pROTOCOL_BATTLE_MISSION_ROUND_PRE_START_ACK.GetType().Name);
					}
					else
					{
						EquipmentSync.SendUDPPlayerSync(this, slotModel2, (CouponEffects)0L, 1);
					}
					Player.SendCompletePacket(completeBytes2, pROTOCOL_BATTLE_MISSION_ROUND_START_ACK.GetType().Name);
					Player.SendCompletePacket(completeBytes3, pROTOCOL_BATTLE_RECORD_ACK.GetType().Name);
				}
			}
		}
		if (State == RoomState.PRE_BATTLE)
		{
			State = RoomState.BATTLE;
			UpdateRoomInfo();
		}
		if (flag)
		{
			method_18();
		}
	}

	private void method_18()
	{
		RoundTime.StartJob(5250, delegate(object object_0)
		{
			if (State == RoomState.BATTLE)
			{
				using PROTOCOL_BATTLE_MISSION_ROUND_START_ACK packet = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(this);
				SendPacketToPlayers(packet, SlotState.BATTLE, 0);
			}
			lock (object_0)
			{
				RoundTime.StopJob();
			}
		});
	}

	public bool IsDinoMode(string Dino = "")
	{
		if (Dino.Equals("DE"))
		{
			return RoomType == RoomCondition.Boss;
		}
		if (Dino.Equals("CC"))
		{
			return RoomType == RoomCondition.CrossCounter;
		}
		if (RoomType != RoomCondition.Boss)
		{
			return RoomType == RoomCondition.CrossCounter;
		}
		return true;
	}

	public int GetReadyPlayers()
	{
		int num = 0;
		SlotModel[] slots = Slots;
		foreach (SlotModel slotModel in slots)
		{
			if (slotModel != null && slotModel.State >= SlotState.READY && slotModel.Equipment != null)
			{
				Account playerBySlot = GetPlayerBySlot(slotModel);
				if (playerBySlot != null && playerBySlot.SlotId == slotModel.Id)
				{
					num++;
				}
			}
		}
		return num;
	}

	public int ResetReadyPlayers()
	{
		int num = 0;
		SlotModel[] slots = Slots;
		foreach (SlotModel slotModel in slots)
		{
			if (slotModel.State == SlotState.READY)
			{
				slotModel.State = SlotState.NORMAL;
				num++;
			}
		}
		return num;
	}

	public TeamEnum CheckTeam(int SlotIdx)
	{
		if (Array.Exists(FR_TEAM, (int int_1) => int_1 == SlotIdx))
		{
			return TeamEnum.FR_TEAM;
		}
		if (Array.Exists(CT_TEAM, (int int_1) => int_1 == SlotIdx))
		{
			return TeamEnum.CT_TEAM;
		}
		return TeamEnum.ALL_TEAM;
	}

	public TeamEnum ValidateTeam(TeamEnum Team, TeamEnum Costume)
	{
		if (RoomType == RoomCondition.FreeForAll)
		{
			return Costume;
		}
		if (SwapRound)
		{
			if (Team != 0)
			{
				return TeamEnum.FR_TEAM;
			}
			return TeamEnum.CT_TEAM;
		}
		return Team;
	}

	public string RandomName(int Random)
	{
		return Random switch
		{
			1 => "Feel the Headshots!!", 
			2 => "Land of Dead!", 
			3 => "Kill! or be Killed!!", 
			4 => "Show Me Your Skills!!", 
			_ => "Point Blank!!", 
		};
	}

	public void CheckGhostSlot(SlotModel Slot)
	{
		if (Slot.State != 0 && Slot.State != SlotState.CLOSE && Slot.PlayerId == 0L && !IsBotMode())
		{
			Slot.ResetSlot();
			Slot.State = SlotState.EMPTY;
		}
	}

	[CompilerGenerated]
	private void method_19(object object_0)
	{
		if (this != null && ActiveC4)
		{
			if (SwapRound)
			{
				CTRounds++;
			}
			else
			{
				FRRounds++;
			}
			ActiveC4 = false;
			AllUtils.BattleEndRound(this, TeamEnum.FR_TEAM, RoundEndType.BombFire);
		}
		lock (object_0)
		{
			BombTime.StopJob();
		}
	}

	[CompilerGenerated]
	private void method_20(object object_0)
	{
		AllUtils.VotekickResult(this);
		lock (object_0)
		{
			VoteTime.StopJob();
		}
	}

	[CompilerGenerated]
	private void method_21(object object_0)
	{
		method_4();
		lock (object_0)
		{
			RoundTime.StopJob();
		}
	}

	[CompilerGenerated]
	private void method_22(object object_0)
	{
		method_5();
		lock (object_0)
		{
			RoundTeamSwap.StopJob();
		}
	}

	[CompilerGenerated]
	private void method_23(object object_0)
	{
		method_6();
		lock (object_0)
		{
			CountdownTime.StopJob();
		}
	}

	[CompilerGenerated]
	private void method_24(object object_0)
	{
		method_17();
		lock (object_0)
		{
			PreMatchTime.StopJob();
		}
	}

	[CompilerGenerated]
	private void method_25(object object_0)
	{
		if (State == RoomState.BATTLE)
		{
			using PROTOCOL_BATTLE_MISSION_ROUND_START_ACK packet = new PROTOCOL_BATTLE_MISSION_ROUND_START_ACK(this);
			SendPacketToPlayers(packet, SlotState.BATTLE, 0);
		}
		lock (object_0)
		{
			RoundTime.StopJob();
		}
	}
}
