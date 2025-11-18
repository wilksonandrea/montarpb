using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class CartGoods
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

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

	public int BuyType
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
