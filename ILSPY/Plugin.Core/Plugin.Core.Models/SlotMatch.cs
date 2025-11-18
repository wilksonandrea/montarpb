using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models;

public class SlotMatch
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private long long_0;

	[CompilerGenerated]
	private SlotMatchState slotMatchState_0;

	public int Id
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

	public SlotMatchState State
	{
		[CompilerGenerated]
		get
		{
			return slotMatchState_0;
		}
		[CompilerGenerated]
		set
		{
			slotMatchState_0 = value;
		}
	}

	public SlotMatch(int int_1)
	{
		Id = int_1;
	}
}
