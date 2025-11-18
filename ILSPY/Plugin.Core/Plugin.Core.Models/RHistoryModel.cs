using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models;

public class RHistoryModel
{
	[CompilerGenerated]
	private long long_0;

	[CompilerGenerated]
	private long long_1;

	[CompilerGenerated]
	private long long_2;

	[CompilerGenerated]
	private uint uint_0;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private string string_1;

	[CompilerGenerated]
	private string string_2;

	[CompilerGenerated]
	private ReportType reportType_0;

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

	public long OwnerId
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

	public long SenderId
	{
		[CompilerGenerated]
		get
		{
			return long_2;
		}
		[CompilerGenerated]
		set
		{
			long_2 = value;
		}
	}

	public uint Date
	{
		[CompilerGenerated]
		get
		{
			return uint_0;
		}
		[CompilerGenerated]
		set
		{
			uint_0 = value;
		}
	}

	public string OwnerNick
	{
		[CompilerGenerated]
		get
		{
			return string_0;
		}
		[CompilerGenerated]
		set
		{
			string_0 = value;
		}
	}

	public string SenderNick
	{
		[CompilerGenerated]
		get
		{
			return string_1;
		}
		[CompilerGenerated]
		set
		{
			string_1 = value;
		}
	}

	public string Message
	{
		[CompilerGenerated]
		get
		{
			return string_2;
		}
		[CompilerGenerated]
		set
		{
			string_2 = value;
		}
	}

	public ReportType Type
	{
		[CompilerGenerated]
		get
		{
			return reportType_0;
		}
		[CompilerGenerated]
		set
		{
			reportType_0 = value;
		}
	}
}
