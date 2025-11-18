using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class CompetitiveRank
	{
		public int Id
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public int Points
		{
			get;
			set;
		}

		public int TourneyLevel
		{
			get;
			set;
		}

		public CompetitiveRank()
		{
		}
	}
}