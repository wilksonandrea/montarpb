using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000092 RID: 146
	public class PlayerStatistic
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x00005DC3 File Offset: 0x00003FC3
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x00005DCB File Offset: 0x00003FCB
		public StatisticTotal Basic
		{
			[CompilerGenerated]
			get
			{
				return this.statisticTotal_0;
			}
			[CompilerGenerated]
			set
			{
				this.statisticTotal_0 = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00005DD4 File Offset: 0x00003FD4
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x00005DDC File Offset: 0x00003FDC
		public StatisticSeason Season
		{
			[CompilerGenerated]
			get
			{
				return this.statisticSeason_0;
			}
			[CompilerGenerated]
			set
			{
				this.statisticSeason_0 = value;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x00005DE5 File Offset: 0x00003FE5
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x00005DED File Offset: 0x00003FED
		public StatisticDaily Daily
		{
			[CompilerGenerated]
			get
			{
				return this.statisticDaily_0;
			}
			[CompilerGenerated]
			set
			{
				this.statisticDaily_0 = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x00005DF6 File Offset: 0x00003FF6
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x00005DFE File Offset: 0x00003FFE
		public StatisticClan Clan
		{
			[CompilerGenerated]
			get
			{
				return this.statisticClan_0;
			}
			[CompilerGenerated]
			set
			{
				this.statisticClan_0 = value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x00005E07 File Offset: 0x00004007
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x00005E0F File Offset: 0x0000400F
		public StatisticWeapon Weapon
		{
			[CompilerGenerated]
			get
			{
				return this.statisticWeapon_0;
			}
			[CompilerGenerated]
			set
			{
				this.statisticWeapon_0 = value;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00005E18 File Offset: 0x00004018
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x00005E20 File Offset: 0x00004020
		public StatisticAcemode Acemode
		{
			[CompilerGenerated]
			get
			{
				return this.statisticAcemode_0;
			}
			[CompilerGenerated]
			set
			{
				this.statisticAcemode_0 = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00005E29 File Offset: 0x00004029
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x00005E31 File Offset: 0x00004031
		public StatisticBattlecup Battlecup
		{
			[CompilerGenerated]
			get
			{
				return this.statisticBattlecup_0;
			}
			[CompilerGenerated]
			set
			{
				this.statisticBattlecup_0 = value;
			}
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001C6B4 File Offset: 0x0001A8B4
		public int GetKDRatio()
		{
			if (this.Basic.HeadshotsCount <= 0 && this.Basic.KillsCount <= 0)
			{
				return 0;
			}
			return (int)Math.Floor(((double)(this.Basic.KillsCount * 100) + 0.5) / (double)(this.Basic.KillsCount + this.Basic.DeathsCount));
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00005E3A File Offset: 0x0000403A
		public int GetHSRatio()
		{
			if (this.Basic.KillsCount <= 0)
			{
				return 0;
			}
			return (int)Math.Floor((double)(this.Basic.HeadshotsCount * 100) / ((double)this.Basic.KillsCount + 0.5));
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001C718 File Offset: 0x0001A918
		public int GetSeasonKDRatio()
		{
			if (this.Season.HeadshotsCount <= 0 && this.Season.KillsCount <= 0)
			{
				return 0;
			}
			return (int)Math.Floor(((double)(this.Season.KillsCount * 100) + 0.5) / (double)(this.Season.KillsCount + this.Season.DeathsCount));
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00005E79 File Offset: 0x00004079
		public int GetSeasonHSRatio()
		{
			if (this.Season.KillsCount <= 0)
			{
				return 0;
			}
			return (int)Math.Floor((double)(this.Season.HeadshotsCount * 100) / ((double)this.Season.KillsCount + 0.5));
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001C77C File Offset: 0x0001A97C
		public int GetBCWinRatio()
		{
			if (this.Battlecup.MatchWins <= 0 && this.Battlecup.Matches <= 0)
			{
				return 0;
			}
			return (int)Math.Floor(((double)(this.Battlecup.MatchWins * 100) + 0.5) / (double)(this.Battlecup.MatchWins + this.Battlecup.MatchLoses));
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001C7E0 File Offset: 0x0001A9E0
		public int GetBCKDRatio()
		{
			if (this.Battlecup.HeadshotsCount <= 0 && this.Battlecup.KillsCount <= 0)
			{
				return 0;
			}
			return (int)Math.Floor(((double)(this.Battlecup.KillsCount * 100) + 0.5) / (double)(this.Battlecup.KillsCount + this.Battlecup.DeathsCount));
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00002116 File Offset: 0x00000316
		public PlayerStatistic()
		{
		}

		// Token: 0x040002D4 RID: 724
		[CompilerGenerated]
		private StatisticTotal statisticTotal_0;

		// Token: 0x040002D5 RID: 725
		[CompilerGenerated]
		private StatisticSeason statisticSeason_0;

		// Token: 0x040002D6 RID: 726
		[CompilerGenerated]
		private StatisticDaily statisticDaily_0;

		// Token: 0x040002D7 RID: 727
		[CompilerGenerated]
		private StatisticClan statisticClan_0;

		// Token: 0x040002D8 RID: 728
		[CompilerGenerated]
		private StatisticWeapon statisticWeapon_0;

		// Token: 0x040002D9 RID: 729
		[CompilerGenerated]
		private StatisticAcemode statisticAcemode_0;

		// Token: 0x040002DA RID: 730
		[CompilerGenerated]
		private StatisticBattlecup statisticBattlecup_0;
	}
}
