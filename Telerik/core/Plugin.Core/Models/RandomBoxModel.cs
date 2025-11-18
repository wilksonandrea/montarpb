using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class RandomBoxModel
	{
		public List<RandomBoxItem> Items
		{
			get;
			set;
		}

		public int ItemsCount
		{
			get;
			set;
		}

		public int MaxPercent
		{
			get;
			set;
		}

		public int MinPercent
		{
			get;
			set;
		}

		public RandomBoxModel()
		{
		}

		public List<RandomBoxItem> GetRewardList(List<RandomBoxItem> SortedLists, int RandomId)
		{
			List<RandomBoxItem> randomBoxItems = new List<RandomBoxItem>();
			if (SortedLists.Count > 0)
			{
				int ındex = SortedLists[RandomId].Index;
				foreach (RandomBoxItem sortedList in SortedLists)
				{
					if (sortedList.Index != ındex)
					{
						continue;
					}
					randomBoxItems.Add(sortedList);
				}
			}
			return randomBoxItems;
		}

		public List<RandomBoxItem> GetSortedList(int Percent)
		{
			if (Percent < this.MinPercent)
			{
				Percent = this.MinPercent;
			}
			List<RandomBoxItem> randomBoxItems = new List<RandomBoxItem>();
			foreach (RandomBoxItem ıtem in this.Items)
			{
				if (Percent > ıtem.Percent)
				{
					continue;
				}
				randomBoxItems.Add(ıtem);
			}
			return randomBoxItems;
		}

		public void SetTopPercent()
		{
			int percent = 100;
			int ınt32 = 0;
			foreach (RandomBoxItem ıtem in this.Items)
			{
				if (ıtem.Percent < percent)
				{
					percent = ıtem.Percent;
				}
				if (ıtem.Percent <= ınt32)
				{
					continue;
				}
				ınt32 = ıtem.Percent;
			}
			this.MinPercent = percent;
			this.MaxPercent = ınt32;
		}
	}
}