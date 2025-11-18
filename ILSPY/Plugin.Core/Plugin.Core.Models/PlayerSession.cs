using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class PlayerSession
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private long long_0;

	public int SessionId
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

	public long PlayerId
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
}
