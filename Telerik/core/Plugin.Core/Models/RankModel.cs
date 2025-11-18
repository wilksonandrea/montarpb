using System;
using System.Collections.Generic;

namespace Plugin.Core.Models
{
	public class RankModel
	{
		public int Id;

		public string Title;

		public int OnNextLevel;

		public int OnGoldUp;

		public int OnAllExp;

		public SortedList<int, List<int>> Rewards;

		public RankModel(int int_0)
		{
			this.Id = int_0;
		}
	}
}