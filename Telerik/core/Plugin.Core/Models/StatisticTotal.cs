using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class StatisticTotal
	{
		public int AssistsCount
		{
			get;
			set;
		}

		public int DeathsCount
		{
			get;
			set;
		}

		public int EscapesCount
		{
			get;
			set;
		}

		public int HeadshotsCount
		{
			get;
			set;
		}

		public int KillsCount
		{
			get;
			set;
		}

		public int MatchDraws
		{
			get;
			set;
		}

		public int Matches
		{
			get;
			set;
		}

		public int MatchLoses
		{
			get;
			set;
		}

		public int MatchWins
		{
			get;
			set;
		}

		public int MvpCount
		{
			get;
			set;
		}

		public long OwnerId
		{
			get;
			set;
		}

		public int TotalKillsCount
		{
			get;
			set;
		}

		public int TotalMatchesCount
		{
			get;
			set;
		}

		public StatisticTotal()
		{
		}
	}
}