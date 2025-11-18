using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models;

public class BattleRewardModel
{
	[CompilerGenerated]
	private BattleRewardType battleRewardType_0;

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private bool bool_0;

	[CompilerGenerated]
	private int[] int_1;

	public BattleRewardType Type
	{
		[CompilerGenerated]
		get
		{
			return battleRewardType_0;
		}
		[CompilerGenerated]
		set
		{
			battleRewardType_0 = value;
		}
	}

	public int Percentage
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

	public bool Enable
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

	public int[] Rewards
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
}
