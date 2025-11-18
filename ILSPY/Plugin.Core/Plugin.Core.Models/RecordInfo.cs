using System.Runtime.CompilerServices;

namespace Plugin.Core.Models;

public class RecordInfo
{
	[CompilerGenerated]
	private long long_0;

	[CompilerGenerated]
	private int int_0;

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

	public int RecordValue
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

	public RecordInfo(string[] string_0)
	{
		PlayerId = GetPlayerId(string_0);
		RecordValue = GetPlayerValue(string_0);
	}

	public long GetPlayerId(string[] Split)
	{
		try
		{
			return long.Parse(Split[0]);
		}
		catch
		{
			return 0L;
		}
	}

	public int GetPlayerValue(string[] Split)
	{
		try
		{
			return int.Parse(Split[1]);
		}
		catch
		{
			return 0;
		}
	}

	public string GetSplit()
	{
		return PlayerId + "-" + RecordValue;
	}
}
