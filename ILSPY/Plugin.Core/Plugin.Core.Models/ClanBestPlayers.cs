using System.Runtime.CompilerServices;
using Plugin.Core.Utility;

namespace Plugin.Core.Models;

public class ClanBestPlayers
{
	[CompilerGenerated]
	private RecordInfo recordInfo_0;

	[CompilerGenerated]
	private RecordInfo recordInfo_1;

	[CompilerGenerated]
	private RecordInfo recordInfo_2;

	[CompilerGenerated]
	private RecordInfo recordInfo_3;

	[CompilerGenerated]
	private RecordInfo recordInfo_4;

	public RecordInfo Exp
	{
		[CompilerGenerated]
		get
		{
			return recordInfo_0;
		}
		[CompilerGenerated]
		set
		{
			recordInfo_0 = value;
		}
	}

	public RecordInfo Participation
	{
		[CompilerGenerated]
		get
		{
			return recordInfo_1;
		}
		[CompilerGenerated]
		set
		{
			recordInfo_1 = value;
		}
	}

	public RecordInfo Wins
	{
		[CompilerGenerated]
		get
		{
			return recordInfo_2;
		}
		[CompilerGenerated]
		set
		{
			recordInfo_2 = value;
		}
	}

	public RecordInfo Kills
	{
		[CompilerGenerated]
		get
		{
			return recordInfo_3;
		}
		[CompilerGenerated]
		set
		{
			recordInfo_3 = value;
		}
	}

	public RecordInfo Headshots
	{
		[CompilerGenerated]
		get
		{
			return recordInfo_4;
		}
		[CompilerGenerated]
		set
		{
			recordInfo_4 = value;
		}
	}

	public void SetPlayers(string Exp, string Participation, string Wins, string Kills, string Headshots)
	{
		this.Exp = new RecordInfo(Exp.Split('-'));
		this.Participation = new RecordInfo(Participation.Split('-'));
		this.Wins = new RecordInfo(Wins.Split('-'));
		this.Kills = new RecordInfo(Kills.Split('-'));
		this.Headshots = new RecordInfo(Headshots.Split('-'));
	}

	public void SetDefault()
	{
		string[] string_ = new string[2] { "0", "0" };
		Exp = new RecordInfo(string_);
		Participation = new RecordInfo(string_);
		Wins = new RecordInfo(string_);
		Kills = new RecordInfo(string_);
		Headshots = new RecordInfo(string_);
	}

	public long GetPlayerId(string[] Split)
	{
		try
		{
			return long.Parse(Split[0]);
		}
		catch
		{
			return 0L;
		}
	}

	public int GetPlayerValue(string[] Split)
	{
		try
		{
			return int.Parse(Split[1]);
		}
		catch
		{
			return 0;
		}
	}

	public void SetBestExp(SlotModel Slot)
	{
		if (Slot.Exp > Exp.RecordValue)
		{
			Exp.PlayerId = Slot.PlayerId;
			Exp.RecordValue = Slot.Exp;
		}
	}

	public void SetBestHeadshot(SlotModel Slot)
	{
		if (Slot.AllHeadshots > Headshots.RecordValue)
		{
			Headshots.PlayerId = Slot.PlayerId;
			Headshots.RecordValue = Slot.AllHeadshots;
		}
	}

	public void SetBestKills(SlotModel Slot)
	{
		if (Slot.AllKills > Kills.RecordValue)
		{
			Kills.PlayerId = Slot.PlayerId;
			Kills.RecordValue = Slot.AllKills;
		}
	}

	public void SetBestWins(PlayerStatistic Stat, SlotModel Slot, bool WonTheMatch)
	{
		if (WonTheMatch)
		{
			ComDiv.UpdateDB("player_stat_clans", "clan_match_wins", ++Stat.Clan.MatchWins, "owner_id", Slot.PlayerId);
			if (Stat.Clan.MatchWins > Wins.RecordValue)
			{
				Wins.PlayerId = Slot.PlayerId;
				Wins.RecordValue = Stat.Clan.MatchWins;
			}
		}
	}

	public void SetBestParticipation(PlayerStatistic Stat, SlotModel Slot)
	{
		ComDiv.UpdateDB("player_stat_clans", "clan_matches", ++Stat.Clan.Matches, "owner_id", Slot.PlayerId);
		if (Stat.Clan.Matches > Participation.RecordValue)
		{
			Participation.PlayerId = Slot.PlayerId;
			Participation.RecordValue = Stat.Clan.Matches;
		}
	}
}
