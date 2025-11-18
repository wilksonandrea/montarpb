using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models;

public class PCCafeModel
{
	[CompilerGenerated]
	private CafeEnum cafeEnum_0;

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private SortedList<CafeEnum, List<ItemsModel>> sortedList_0;

	public CafeEnum Type
	{
		[CompilerGenerated]
		get
		{
			return cafeEnum_0;
		}
		[CompilerGenerated]
		set
		{
			cafeEnum_0 = value;
		}
	}

	public int PointUp
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

	public int ExpUp
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

	public SortedList<CafeEnum, List<ItemsModel>> Rewards
	{
		[CompilerGenerated]
		get
		{
			return sortedList_0;
		}
		[CompilerGenerated]
		set
		{
			sortedList_0 = value;
		}
	}

	public PCCafeModel(CafeEnum cafeEnum_1)
	{
		Type = cafeEnum_1;
		PointUp = 0;
		ExpUp = 0;
	}
}
