using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models;

public class BattleBoxModel
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private List<BattleBoxItem> list_0;

	[CompilerGenerated]
	private List<int> list_1;

	[CompilerGenerated]
	private List<double> list_2;

	public int CouponId
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

	public int RequireTags
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

	public List<BattleBoxItem> Items
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

	public List<int> Goods
	{
		[CompilerGenerated]
		get
		{
			return list_1;
		}
		[CompilerGenerated]
		set
		{
			list_1 = value;
		}
	}

	public List<double> Probabilities
	{
		[CompilerGenerated]
		get
		{
			return list_2;
		}
		[CompilerGenerated]
		set
		{
			list_2 = value;
		}
	}

	public T GetItemWithProbabilities<T>(List<T> Items, List<double> Probabilities)
	{
		if (Items == null || Items.Count == 0 || Probabilities == null || Probabilities.Count == 0 || Items.Count != Probabilities.Count)
		{
			CLogger.Print("Battle Box Item List Is Not Valid!", LoggerType.Warning);
		}
		double num = new Random().NextDouble();
		double num2 = 0.0;
		int num3 = 0;
		while (true)
		{
			if (num3 < Items.Count)
			{
				num2 += Probabilities[num3] / 100.0;
				if (!(num >= num2))
				{
					break;
				}
				num3++;
				continue;
			}
			return Items[Items.Count - 1];
		}
		return Items[num3];
	}

	public void InitItemPercentages()
	{
		Goods = new List<int>();
		Probabilities = new List<double>();
		foreach (BattleBoxItem ıtem in Items)
		{
			Goods.Add(ıtem.GoodsId);
			Probabilities.Add(ıtem.Percent);
		}
	}
}
