using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class PassBoxModel
{
	[CompilerGenerated]
	private PassItemModel passItemModel_0;

	[CompilerGenerated]
	private PassItemModel passItemModel_1;

	[CompilerGenerated]
	private PassItemModel passItemModel_2;

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private int int_2;

	public PassItemModel Normal
	{
		[CompilerGenerated]
		get
		{
			return passItemModel_0;
		}
		[CompilerGenerated]
		set
		{
			passItemModel_0 = value;
		}
	}

	public PassItemModel PremiumA
	{
		[CompilerGenerated]
		get
		{
			return passItemModel_1;
		}
		[CompilerGenerated]
		set
		{
			passItemModel_1 = value;
		}
	}

	public PassItemModel PremiumB
	{
		[CompilerGenerated]
		get
		{
			return passItemModel_2;
		}
		[CompilerGenerated]
		set
		{
			passItemModel_2 = value;
		}
	}

	public int RequiredPoints
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

	public int RewardCount
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

	public int Card
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

	public PassBoxModel()
	{
		Normal = new PassItemModel();
		PremiumA = new PassItemModel();
		PremiumB = new PassItemModel();
	}

	public void SetCount()
	{
		if (Normal != null && Normal.GoodId > 0)
		{
			RewardCount++;
		}
		if (PremiumA != null && PremiumA.GoodId > 0)
		{
			RewardCount++;
		}
		if (PremiumB != null && PremiumB.GoodId > 0)
		{
			RewardCount++;
		}
	}
}
