using Plugin.Core.Enums;
using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class BattleRewardModel
	{
		public bool Enable
		{
			get;
			set;
		}

		public int Percentage
		{
			get;
			set;
		}

		public int[] Rewards
		{
			get;
			set;
		}

		public BattleRewardType Type
		{
			get;
			set;
		}

		public BattleRewardModel()
		{
		}
	}
}