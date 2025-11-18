using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class PlayerStatistic
	{
		public StatisticAcemode Acemode
		{
			get;
			set;
		}

		public StatisticTotal Basic
		{
			get;
			set;
		}

		public StatisticBattlecup Battlecup
		{
			get;
			set;
		}

		public StatisticClan Clan
		{
			get;
			set;
		}

		public StatisticDaily Daily
		{
			get;
			set;
		}

		public StatisticSeason Season
		{
			get;
			set;
		}

		public StatisticWeapon Weapon
		{
			get;
			set;
		}

		public PlayerStatistic()
		{
		}

		public int GetBCKDRatio()
		{
			if (this.Battlecup.HeadshotsCount <= 0 && this.Battlecup.KillsCount <= 0)
			{
				return 0;
			}
			return (int)Math.Floor(((double)(this.Battlecup.KillsCount * 100) + 0.5) / (double)(this.Battlecup.KillsCount + this.Battlecup.DeathsCount));
		}

		public int GetBCWinRatio()
		{
			if (this.Battlecup.MatchWins <= 0 && this.Battlecup.Matches <= 0)
			{
				return 0;
			}
			return (int)Math.Floor(((double)(this.Battlecup.MatchWins * 100) + 0.5) / (double)(this.Battlecup.MatchWins + this.Battlecup.MatchLoses));
		}

		public int GetHSRatio()
		{
			if (this.Basic.KillsCount <= 0)
			{
				return 0;
			}
			return (int)Math.Floor((double)(this.Basic.HeadshotsCount * 100) / (double)((double)this.Basic.KillsCount + 0.5));
		}

		public int GetKDRatio()
		{
			if (this.Basic.HeadshotsCount <= 0 && this.Basic.KillsCount <= 0)
			{
				return 0;
			}
			return (int)Math.Floor(((double)(this.Basic.KillsCount * 100) + 0.5) / (double)(this.Basic.KillsCount + this.Basic.DeathsCount));
		}

		public int GetSeasonHSRatio()
		{
			if (this.Season.KillsCount <= 0)
			{
				return 0;
			}
			return (int)Math.Floor((double)(this.Season.HeadshotsCount * 100) / (double)((double)this.Season.KillsCount + 0.5));
		}

		public int GetSeasonKDRatio()
		{
			if (this.Season.HeadshotsCount <= 0 && this.Season.KillsCount <= 0)
			{
				return 0;
			}
			return (int)Math.Floor(((double)(this.Season.KillsCount * 100) + 0.5) / (double)(this.Season.KillsCount + this.Season.DeathsCount));
		}
	}
}