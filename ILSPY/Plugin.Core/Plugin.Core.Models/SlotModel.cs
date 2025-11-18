using System;
using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Utility;

namespace Plugin.Core.Models;

public class SlotModel
{
	public int Id;

	public int Flag;

	public int AiLevel;

	public int Latency;

	public int FailLatencyTimes;

	public int Ping;

	public int PassSequence;

	public int IsPlaying;

	public int EarnedEXP;

	public int SpawnsCount;

	public int LastKillState;

	public int KillsOnLife;

	public int Exp;

	public int Cash;

	public int Gold;

	public int Score;

	public int AllKills;

	public int AllHeadshots;

	public int AllDeaths;

	public int AllAssists;

	public int Objects;

	public int BonusItemExp;

	public int BonusItemPoint;

	public int BonusEventExp;

	public int BonusEventPoint;

	public int BonusCafePoint;

	public int BonusCafeExp;

	public int UnkItem;

	public int SeasonPoint;

	public int BonusBattlePass;

	public ushort DamageBar1;

	public ushort DamageBar2;

	public long PlayerId;

	public bool Check;

	public bool SpecGM;

	public bool WithHost;

	public bool Spectator;

	public bool RepeatLastState;

	public bool MissionsCompleted;

	public bool FirstInactivityOff;

	public bool FirstRespawn = true;

	public TeamEnum Team;

	public TeamEnum CostumeTeam;

	public SlotState State;

	public ResultIcon BonusFlags;

	public ViewerType ViewType;

	public DeadEnum DeathState = DeadEnum.Alive;

	public DateTime NextVoteDate;

	public DateTime StartTime;

	public DateTime PreStartDate;

	public DateTime PreLoadDate;

	public PlayerEquipment Equipment;

	public PlayerMissions Missions;

	public int[] AR = new int[2];

	public int[] SMG = new int[2];

	public int[] SR = new int[2];

	public int[] MG = new int[2];

	public int[] SG = new int[2];

	public int[] SHD = new int[2];

	public List<int> ItemUsages = new List<int>();

	public TimerState Timing = new TimerState();

	public SlotModel(int int_0)
	{
		SetSlotId(int_0);
	}

	public void SetSlotId(int SlotIdx)
	{
		Id = SlotIdx;
		Team = (TeamEnum)(SlotIdx % 2);
		Flag = 1 << SlotIdx;
	}

	public void StopTiming()
	{
		if (Timing != null)
		{
			Timing.StopJob();
		}
	}

	public void ResetSlot()
	{
		StopTiming();
		DeathState = DeadEnum.Alive;
		FailLatencyTimes = 0;
		Latency = 0;
		Ping = 5;
		PassSequence = 0;
		AllKills = 0;
		AllHeadshots = 0;
		AllDeaths = 0;
		AllAssists = 0;
		BonusFlags = ResultIcon.None;
		KillsOnLife = 0;
		LastKillState = 0;
		Score = 0;
		Gold = 0;
		Exp = 0;
		Cash = 0;
		Objects = 0;
		SeasonPoint = 0;
		BonusItemExp = 0;
		BonusItemPoint = 0;
		BonusCafeExp = 0;
		BonusCafePoint = 0;
		BonusEventExp = 0;
		BonusEventPoint = 0;
		BonusBattlePass = 0;
		SpawnsCount = 0;
		DamageBar1 = 0;
		DamageBar2 = 0;
		EarnedEXP = 0;
		IsPlaying = 0;
		AiLevel = 0;
		NextVoteDate = default(DateTime);
		Check = false;
		SpecGM = false;
		WithHost = false;
		Spectator = false;
		FirstRespawn = true;
		RepeatLastState = false;
		MissionsCompleted = false;
		FirstInactivityOff = false;
		Missions = null;
		ItemUsages.Clear();
		Array.Clear(AR, 0, AR.Length);
		Array.Clear(SMG, 0, SMG.Length);
		Array.Clear(SR, 0, SR.Length);
		Array.Clear(SG, 0, SG.Length);
		Array.Clear(MG, 0, MG.Length);
		Array.Clear(SHD, 0, MG.Length);
	}

	public void SetMissionsClone(PlayerMissions Missions)
	{
		MissionsCompleted = false;
		this.Missions = null;
		this.Missions = Missions.DeepCopy();
	}

	public double InBattleTime(DateTime Date)
	{
		if (StartTime == default(DateTime))
		{
			return 0.0;
		}
		return (Date - StartTime).TotalSeconds;
	}
}
