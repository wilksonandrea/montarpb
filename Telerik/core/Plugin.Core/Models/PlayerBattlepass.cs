using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class PlayerBattlepass
	{
		public int DailyPoints
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public bool IsPremium
		{
			get;
			set;
		}

		public uint LastRecord
		{
			get;
			set;
		}

		public int Level
		{
			get;
			set;
		}

		public int TotalPoints
		{
			get;
			set;
		}

		public PlayerBattlepass()
		{
		}
	}
}