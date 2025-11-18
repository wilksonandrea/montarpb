using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class StatisticDaily
	{
		public int DeathsCount
		{
			get;
			set;
		}

		public int ExpGained
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

		public long OwnerId
		{
			get;
			set;
		}

		public uint Playtime
		{
			get;
			set;
		}

		public int PointGained
		{
			get;
			set;
		}

		public StatisticDaily()
		{
		}
	}
}