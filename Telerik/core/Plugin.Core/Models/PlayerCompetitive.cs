using Plugin.Core.XML;
using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class PlayerCompetitive
	{
		public int Level
		{
			get;
			set;
		}

		public long OwnerId
		{
			get;
			set;
		}

		public int Points
		{
			get;
			set;
		}

		public PlayerCompetitive()
		{
		}

		public CompetitiveRank Rank()
		{
			return CompetitiveXML.GetRank(this.Level) ?? new CompetitiveRank();
		}
	}
}