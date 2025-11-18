using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class PlayerTopup
{
	[CompilerGenerated]
	private long long_0;

	[CompilerGenerated]
	private long long_1;

	[CompilerGenerated]
	private int int_0;

	public long ObjectId
	{
		[CompilerGenerated]
		get
		{
			return long_0;
		}
		[CompilerGenerated]
		set
		{
			long_0 = value;
		}
	}

	public long PlayerId
	{
		[CompilerGenerated]
		get
		{
			return long_1;
		}
		[CompilerGenerated]
		set
		{
			long_1 = value;
		}
	}

	public int GoodsId
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
}
