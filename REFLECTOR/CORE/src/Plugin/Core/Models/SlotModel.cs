namespace Plugin.Core.Models
{
    using Plugin.Core.Enums;
    using Plugin.Core.Utility;
    using System;
    using System.Collections.Generic;

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
            this.SetSlotId(int_0);
        }

        public double InBattleTime(DateTime Date)
        {
            DateTime time = new DateTime();
            return (!(this.StartTime == time) ? (Date - this.StartTime).TotalSeconds : 0.0);
        }

        public void ResetSlot()
        {
            this.StopTiming();
            this.DeathState = DeadEnum.Alive;
            this.FailLatencyTimes = 0;
            this.Latency = 0;
            this.Ping = 5;
            this.PassSequence = 0;
            this.AllKills = 0;
            this.AllHeadshots = 0;
            this.AllDeaths = 0;
            this.AllAssists = 0;
            this.BonusFlags = ResultIcon.None;
            this.KillsOnLife = 0;
            this.LastKillState = 0;
            this.Score = 0;
            this.Gold = 0;
            this.Exp = 0;
            this.Cash = 0;
            this.Objects = 0;
            this.SeasonPoint = 0;
            this.BonusItemExp = 0;
            this.BonusItemPoint = 0;
            this.BonusCafeExp = 0;
            this.BonusCafePoint = 0;
            this.BonusEventExp = 0;
            this.BonusEventPoint = 0;
            this.BonusBattlePass = 0;
            this.SpawnsCount = 0;
            this.DamageBar1 = 0;
            this.DamageBar2 = 0;
            this.EarnedEXP = 0;
            this.IsPlaying = 0;
            this.AiLevel = 0;
            this.NextVoteDate = new DateTime();
            this.Check = false;
            this.SpecGM = false;
            this.WithHost = false;
            this.Spectator = false;
            this.FirstRespawn = true;
            this.RepeatLastState = false;
            this.MissionsCompleted = false;
            this.FirstInactivityOff = false;
            this.Missions = null;
            this.ItemUsages.Clear();
            Array.Clear(this.AR, 0, this.AR.Length);
            Array.Clear(this.SMG, 0, this.SMG.Length);
            Array.Clear(this.SR, 0, this.SR.Length);
            Array.Clear(this.SG, 0, this.SG.Length);
            Array.Clear(this.MG, 0, this.MG.Length);
            Array.Clear(this.SHD, 0, this.MG.Length);
        }

        public void SetMissionsClone(PlayerMissions Missions)
        {
            this.MissionsCompleted = false;
            this.Missions = null;
            this.Missions = Missions.DeepCopy();
        }

        public void SetSlotId(int SlotIdx)
        {
            this.Id = SlotIdx;
            this.Team = (TeamEnum) (SlotIdx % 2);
            this.Flag = 1 << (SlotIdx & 0x1f);
        }

        public void StopTiming()
        {
            if (this.Timing != null)
            {
                this.Timing.StopJob();
            }
        }
    }
}

