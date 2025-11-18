using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class RandomBoxModel
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private List<RandomBoxItem> list_0;

	public int ItemsCount
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		set
		{
			int_0 = value;
		}
	}

	public int MinPercent
	{
		[CompilerGenerated]
		get
		{
			return int_1;
		}
		[CompilerGenerated]
		set
		{
			int_1 = value;
		}
	}

	public int MaxPercent
	{
		[CompilerGenerated]
		get
		{
			return int_2;
		}
		[CompilerGenerated]
		set
		{
			int_2 = value;
		}
	}

	public List<RandomBoxItem> Items
	{
		[CompilerGenerated]
		get
		{
			return list_0;
		}
		[CompilerGenerated]
		set
		{
			list_0 = value;
		}
	}

	public List<RandomBoxItem> GetRewardList(List<RandomBoxItem> SortedLists, int RandomId)
	{
		List<RandomBoxItem> list = new List<RandomBoxItem>();
		if (SortedLists.Count > 0)
		{
			int ındex = SortedLists[RandomId].Index;
			{
				foreach (RandomBoxItem SortedList in SortedLists)
				{
					if (SortedList.Index == ındex)
					{
						list.Add(SortedList);
					}
				}
				return list;
			}
		}
		return list;
	}

	public List<RandomBoxItem> GetSortedList(int Percent)
	{
		if (Percent < MinPercent)
		{
			Percent = MinPercent;
		}
		List<RandomBoxItem> list = new List<RandomBoxItem>();
		foreach (RandomBoxItem ıtem in Items)
		{
			if (Percent <= ıtem.Percent)
			{
				list.Add(ıtem);
			}
		}
		return list;
	}

	public void SetTopPercent()
	{
		int num = 100;
		int num2 = 0;
		foreach (RandomBoxItem ıtem in Items)
		{
			if (ıtem.Percent < num)
			{
				num = ıtem.Percent;
			}
			if (ıtem.Percent > num2)
			{
				num2 = ıtem.Percent;
			}
		}
		MinPercent = num;
		MaxPercent = num2;
	}
}
