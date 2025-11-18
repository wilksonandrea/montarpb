using Plugin.Core.Utility;
using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class ClanBestPlayers
	{
		public RecordInfo Exp
		{
			get;
			set;
		}

		public RecordInfo Headshots
		{
			get;
			set;
		}

		public RecordInfo Kills
		{
			get;
			set;
		}

		public RecordInfo Participation
		{
			get;
			set;
		}

		public RecordInfo Wins
		{
			get;
			set;
		}

		public ClanBestPlayers()
		{
		}

		public long GetPlayerId(string[] Split)
		{
			long ınt64;
			try
			{
				ınt64 = long.Parse(Split[0]);
			}
			catch
			{
				ınt64 = 0L;
			}
			return ınt64;
		}

		public int GetPlayerValue(string[] Split)
		{
			int ınt32;
			try
			{
				ınt32 = int.Parse(Split[1]);
			}
			catch
			{
				ınt32 = 0;
			}
			return ınt32;
		}

		public void SetBestExp(SlotModel Slot)
		{
			if (Slot.Exp <= this.Exp.RecordValue)
			{
				return;
			}
			this.Exp.PlayerId = Slot.PlayerId;
			this.Exp.RecordValue = Slot.Exp;
		}

		public void SetBestHeadshot(SlotModel Slot)
		{
			if (Slot.AllHeadshots <= this.Headshots.RecordValue)
			{
				return;
			}
			this.Headshots.PlayerId = Slot.PlayerId;
			this.Headshots.RecordValue = Slot.AllHeadshots;
		}

		public void SetBestKills(SlotModel Slot)
		{
			if (Slot.AllKills <= this.Kills.RecordValue)
			{
				return;
			}
			this.Kills.PlayerId = Slot.PlayerId;
			this.Kills.RecordValue = Slot.AllKills;
		}

		public void SetBestParticipation(PlayerStatistic Stat, SlotModel Slot)
		{
			StatisticClan clan = Stat.Clan;
			int matches = clan.Matches + 1;
			clan.Matches = matches;
			ComDiv.UpdateDB("player_stat_clans", "clan_matches", matches, "owner_id", Slot.PlayerId);
			if (Stat.Clan.Matches <= this.Participation.RecordValue)
			{
				return;
			}
			this.Participation.PlayerId = Slot.PlayerId;
			this.Participation.RecordValue = Stat.Clan.Matches;
		}

		public void SetBestWins(PlayerStatistic Stat, SlotModel Slot, bool WonTheMatch)
		{
			if (!WonTheMatch)
			{
				return;
			}
			StatisticClan clan = Stat.Clan;
			int matchWins = clan.MatchWins + 1;
			clan.MatchWins = matchWins;
			ComDiv.UpdateDB("player_stat_clans", "clan_match_wins", matchWins, "owner_id", Slot.PlayerId);
			if (Stat.Clan.MatchWins <= this.Wins.RecordValue)
			{
				return;
			}
			this.Wins.PlayerId = Slot.PlayerId;
			this.Wins.RecordValue = Stat.Clan.MatchWins;
		}

		public void SetDefault()
		{
			string[] strArrays = new string[] { "0", "0" };
			this.Exp = new RecordInfo(strArrays);
			this.Participation = new RecordInfo(strArrays);
			this.Wins = new RecordInfo(strArrays);
			this.Kills = new RecordInfo(strArrays);
			this.Headshots = new RecordInfo(strArrays);
		}

		public void SetPlayers(string Exp, string Participation, string Wins, string Kills, string Headshots)
		{
			this.Exp = new RecordInfo(Exp.Split(new char[] { '-' }));
			this.Participation = new RecordInfo(Participation.Split(new char[] { '-' }));
			this.Wins = new RecordInfo(Wins.Split(new char[] { '-' }));
			this.Kills = new RecordInfo(Kills.Split(new char[] { '-' }));
			this.Headshots = new RecordInfo(Headshots.Split(new char[] { '-' }));
		}
	}
}