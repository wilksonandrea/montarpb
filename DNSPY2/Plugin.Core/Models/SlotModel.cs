using System;
using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Utility;

namespace Plugin.Core.Models
{
	// Token: 0x0200009C RID: 156
	public class SlotModel
	{
		// Token: 0x06000764 RID: 1892 RVA: 0x0001CA84 File Offset: 0x0001AC84
		public SlotModel(int int_0)
		{
			this.SetSlotId(int_0);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00006279 File Offset: 0x00004479
		public void SetSlotId(int SlotIdx)
		{
			this.Id = SlotIdx;
			this.Team = (TeamEnum)(SlotIdx % 2);
			this.Flag = 1 << SlotIdx;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00006297 File Offset: 0x00004497
		public void StopTiming()
		{
			if (this.Timing != null)
			{
				this.Timing.StopJob();
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001CB0C File Offset: 0x0001AD0C
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
			this.NextVoteDate = default(DateTime);
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

		// Token: 0x06000768 RID: 1896 RVA: 0x000062AC File Offset: 0x000044AC
		public void SetMissionsClone(PlayerMissions Missions)
		{
			this.MissionsCompleted = false;
			this.Missions = null;
			this.Missions = Missions.DeepCopy();
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001CCC8 File Offset: 0x0001AEC8
		public double InBattleTime(DateTime Date)
		{
			if (this.StartTime == default(DateTime))
			{
				return 0.0;
			}
			return (Date - this.StartTime).TotalSeconds;
		}

		// Token: 0x04000314 RID: 788
		public int Id;

		// Token: 0x04000315 RID: 789
		public int Flag;

		// Token: 0x04000316 RID: 790
		public int AiLevel;

		// Token: 0x04000317 RID: 791
		public int Latency;

		// Token: 0x04000318 RID: 792
		public int FailLatencyTimes;

		// Token: 0x04000319 RID: 793
		public int Ping;

		// Token: 0x0400031A RID: 794
		public int PassSequence;

		// Token: 0x0400031B RID: 795
		public int IsPlaying;

		// Token: 0x0400031C RID: 796
		public int EarnedEXP;

		// Token: 0x0400031D RID: 797
		public int SpawnsCount;

		// Token: 0x0400031E RID: 798
		public int LastKillState;

		// Token: 0x0400031F RID: 799
		public int KillsOnLife;

		// Token: 0x04000320 RID: 800
		public int Exp;

		// Token: 0x04000321 RID: 801
		public int Cash;

		// Token: 0x04000322 RID: 802
		public int Gold;

		// Token: 0x04000323 RID: 803
		public int Score;

		// Token: 0x04000324 RID: 804
		public int AllKills;

		// Token: 0x04000325 RID: 805
		public int AllHeadshots;

		// Token: 0x04000326 RID: 806
		public int AllDeaths;

		// Token: 0x04000327 RID: 807
		public int AllAssists;

		// Token: 0x04000328 RID: 808
		public int Objects;

		// Token: 0x04000329 RID: 809
		public int BonusItemExp;

		// Token: 0x0400032A RID: 810
		public int BonusItemPoint;

		// Token: 0x0400032B RID: 811
		public int BonusEventExp;

		// Token: 0x0400032C RID: 812
		public int BonusEventPoint;

		// Token: 0x0400032D RID: 813
		public int BonusCafePoint;

		// Token: 0x0400032E RID: 814
		public int BonusCafeExp;

		// Token: 0x0400032F RID: 815
		public int UnkItem;

		// Token: 0x04000330 RID: 816
		public int SeasonPoint;

		// Token: 0x04000331 RID: 817
		public int BonusBattlePass;

		// Token: 0x04000332 RID: 818
		public ushort DamageBar1;

		// Token: 0x04000333 RID: 819
		public ushort DamageBar2;

		// Token: 0x04000334 RID: 820
		public long PlayerId;

		// Token: 0x04000335 RID: 821
		public bool Check;

		// Token: 0x04000336 RID: 822
		public bool SpecGM;

		// Token: 0x04000337 RID: 823
		public bool WithHost;

		// Token: 0x04000338 RID: 824
		public bool Spectator;

		// Token: 0x04000339 RID: 825
		public bool RepeatLastState;

		// Token: 0x0400033A RID: 826
		public bool MissionsCompleted;

		// Token: 0x0400033B RID: 827
		public bool FirstInactivityOff;

		// Token: 0x0400033C RID: 828
		public bool FirstRespawn = true;

		// Token: 0x0400033D RID: 829
		public TeamEnum Team;

		// Token: 0x0400033E RID: 830
		public TeamEnum CostumeTeam;

		// Token: 0x0400033F RID: 831
		public SlotState State;

		// Token: 0x04000340 RID: 832
		public ResultIcon BonusFlags;

		// Token: 0x04000341 RID: 833
		public ViewerType ViewType;

		// Token: 0x04000342 RID: 834
		public DeadEnum DeathState = DeadEnum.Alive;

		// Token: 0x04000343 RID: 835
		public DateTime NextVoteDate;

		// Token: 0x04000344 RID: 836
		public DateTime StartTime;

		// Token: 0x04000345 RID: 837
		public DateTime PreStartDate;

		// Token: 0x04000346 RID: 838
		public DateTime PreLoadDate;

		// Token: 0x04000347 RID: 839
		public PlayerEquipment Equipment;

		// Token: 0x04000348 RID: 840
		public PlayerMissions Missions;

		// Token: 0x04000349 RID: 841
		public int[] AR = new int[2];

		// Token: 0x0400034A RID: 842
		public int[] SMG = new int[2];

		// Token: 0x0400034B RID: 843
		public int[] SR = new int[2];

		// Token: 0x0400034C RID: 844
		public int[] MG = new int[2];

		// Token: 0x0400034D RID: 845
		public int[] SG = new int[2];

		// Token: 0x0400034E RID: 846
		public int[] SHD = new int[2];

		// Token: 0x0400034F RID: 847
		public List<int> ItemUsages = new List<int>();

		// Token: 0x04000350 RID: 848
		public TimerState Timing = new TimerState();
	}
}
