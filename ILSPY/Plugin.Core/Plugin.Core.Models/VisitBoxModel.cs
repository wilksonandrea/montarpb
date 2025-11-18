using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class VisitBoxModel
{
	[CompilerGenerated]
	private VisitItemModel visitItemModel_0;

	[CompilerGenerated]
	private VisitItemModel visitItemModel_1;

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private bool bool_0;

	public VisitItemModel Reward1
	{
		[CompilerGenerated]
		get
		{
			return visitItemModel_0;
		}
		[CompilerGenerated]
		set
		{
			visitItemModel_0 = value;
		}
	}

	public VisitItemModel Reward2
	{
		[CompilerGenerated]
		get
		{
			return visitItemModel_1;
		}
		[CompilerGenerated]
		set
		{
			visitItemModel_1 = value;
		}
	}

	public int RewardCount
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

	public bool IsBothReward
	{
		[CompilerGenerated]
		get
		{
			return bool_0;
		}
		[CompilerGenerated]
		set
		{
			bool_0 = value;
		}
	}

	public VisitBoxModel()
	{
		Reward1 = new VisitItemModel();
		Reward2 = new VisitItemModel();
	}

	public void SetCount()
	{
		if (Reward1 != null && Reward1.GoodId > 0)
		{
			RewardCount++;
		}
		if (Reward2 != null && Reward2.GoodId > 0)
		{
			RewardCount++;
		}
	}
}
