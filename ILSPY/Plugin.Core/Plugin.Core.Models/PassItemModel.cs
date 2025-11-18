using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class PassItemModel
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private bool bool_0;

	public int GoodId
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

	public bool IsReward
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

	public void SetGoodId(int GoodId)
	{
		this.GoodId = GoodId;
		if (GoodId > 0)
		{
			IsReward = true;
		}
	}
}
