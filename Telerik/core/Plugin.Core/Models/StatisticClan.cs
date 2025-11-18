using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class StatisticClan
	{
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

		public long OwnerId
		{
			get;
			set;
		}

		public StatisticClan()
		{
		}
	}
}