using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models;

public class SChannelModel
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private int int_3;

	[CompilerGenerated]
	private bool bool_0;

	[CompilerGenerated]
	private SChannelType schannelType_0;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private ushort ushort_0;

	[CompilerGenerated]
	private bool bool_1;

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

	public int LastPlayers
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

	public int MaxPlayers
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

	public int ChannelPlayers
	{
		[CompilerGenerated]
		get
		{
			return int_3;
		}
		[CompilerGenerated]
		set
		{
			int_3 = value;
		}
	}

	public bool State
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

	public SChannelType Type
	{
		[CompilerGenerated]
		get
		{
			return schannelType_0;
		}
		[CompilerGenerated]
		set
		{
			schannelType_0 = value;
		}
	}

	public string Host
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

	public ushort Port
	{
		[CompilerGenerated]
		get
		{
			return ushort_0;
		}
		[CompilerGenerated]
		set
		{
			ushort_0 = value;
		}
	}

	public bool IsMobile
	{
		[CompilerGenerated]
		get
		{
			return bool_1;
		}
		[CompilerGenerated]
		set
		{
			bool_1 = value;
		}
	}

	public SChannelModel(string string_1, ushort ushort_1)
	{
		Host = string_1;
		Port = ushort_1;
	}
}
