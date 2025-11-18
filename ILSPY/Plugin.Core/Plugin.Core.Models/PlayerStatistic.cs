using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class PlayerStatistic
{
	[CompilerGenerated]
	private StatisticTotal statisticTotal_0;

	[CompilerGenerated]
	private StatisticSeason statisticSeason_0;

	[CompilerGenerated]
	private StatisticDaily statisticDaily_0;

	[CompilerGenerated]
	private StatisticClan statisticClan_0;

	[CompilerGenerated]
	private StatisticWeapon statisticWeapon_0;

	[CompilerGenerated]
	private StatisticAcemode statisticAcemode_0;

	[CompilerGenerated]
	private StatisticBattlecup statisticBattlecup_0;

	public StatisticTotal Basic
	{
		[CompilerGenerated]
		get
		{
			return statisticTotal_0;
		}
		[CompilerGenerated]
		set
		{
			statisticTotal_0 = value;
		}
	}

	public StatisticSeason Season
	{
		[CompilerGenerated]
		get
		{
			return statisticSeason_0;
		}
		[CompilerGenerated]
		set
		{
			statisticSeason_0 = value;
		}
	}

	public StatisticDaily Daily
	{
		[CompilerGenerated]
		get
		{
			return statisticDaily_0;
		}
		[CompilerGenerated]
		set
		{
			statisticDaily_0 = value;
		}
	}

	public StatisticClan Clan
	{
		[CompilerGenerated]
		get
		{
			return statisticClan_0;
		}
		[CompilerGenerated]
		set
		{
			statisticClan_0 = value;
		}
	}

	public StatisticWeapon Weapon
	{
		[CompilerGenerated]
		get
		{
			return statisticWeapon_0;
		}
		[CompilerGenerated]
		set
		{
			statisticWeapon_0 = value;
		}
	}

	public StatisticAcemode Acemode
	{
		[CompilerGenerated]
		get
		{
			return statisticAcemode_0;
		}
		[CompilerGenerated]
		set
		{
			statisticAcemode_0 = value;
		}
	}

	public StatisticBattlecup Battlecup
	{
		[CompilerGenerated]
		get
		{
			return statisticBattlecup_0;
		}
		[CompilerGenerated]
		set
		{
			statisticBattlecup_0 = value;
		}
	}

	public int GetKDRatio()
	{
		if (Basic.HeadshotsCount <= 0 && Basic.KillsCount <= 0)
		{
			return 0;
		}
		return (int)Math.Floor(((double)(Basic.KillsCount * 100) + 0.5) / (double)(Basic.KillsCount + Basic.DeathsCount));
	}

	public int GetHSRatio()
	{
		if (Basic.KillsCount <= 0)
		{
			return 0;
		}
		return (int)Math.Floor((double)(Basic.HeadshotsCount * 100) / ((double)Basic.KillsCount + 0.5));
	}

	public int GetSeasonKDRatio()
	{
		if (Season.HeadshotsCount <= 0 && Season.KillsCount <= 0)
		{
			return 0;
		}
		return (int)Math.Floor(((double)(Season.KillsCount * 100) + 0.5) / (double)(Season.KillsCount + Season.DeathsCount));
	}

	public int GetSeasonHSRatio()
	{
		if (Season.KillsCount <= 0)
		{
			return 0;
		}
		return (int)Math.Floor((double)(Season.HeadshotsCount * 100) / ((double)Season.KillsCount + 0.5));
	}

	public int GetBCWinRatio()
	{
		if (Battlecup.MatchWins <= 0 && Battlecup.Matches <= 0)
		{
			return 0;
		}
		return (int)Math.Floor(((double)(Battlecup.MatchWins * 100) + 0.5) / (double)(Battlecup.MatchWins + Battlecup.MatchLoses));
	}

	public int GetBCKDRatio()
	{
		if (Battlecup.HeadshotsCount <= 0 && Battlecup.KillsCount <= 0)
		{
			return 0;
		}
		return (int)Math.Floor(((double)(Battlecup.KillsCount * 100) + 0.5) / (double)(Battlecup.KillsCount + Battlecup.DeathsCount));
	}
}
