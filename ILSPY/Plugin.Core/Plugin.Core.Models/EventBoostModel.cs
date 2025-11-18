using System.Runtime.CompilerServices;
using Plugin.Core.Enums;
using Plugin.Core.Utility;

namespace Plugin.Core.Models;

public class EventBoostModel
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
	private uint uint_0;

	[CompilerGenerated]
	private uint uint_1;

	[CompilerGenerated]
	private string string_0;

	[CompilerGenerated]
	private string string_1;

	[CompilerGenerated]
	private bool bool_0;

	[CompilerGenerated]
	private bool bool_1;

	[CompilerGenerated]
	private PortalBoostEvent portalBoostEvent_0;

	[CompilerGenerated]
	private int int_4;

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

	public int BonusExp
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

	public int BonusGold
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

	public int Percent
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

	public uint BeginDate
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

	public uint EndedDate
	{
		[CompilerGenerated]
		get
		{
			return uint_1;
		}
		[CompilerGenerated]
		set
		{
			uint_1 = value;
		}
	}

	public string Name
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

	public string Description
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

	public bool Period
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

	public bool Priority
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

	public PortalBoostEvent BoostType
	{
		[CompilerGenerated]
		get
		{
			return portalBoostEvent_0;
		}
		[CompilerGenerated]
		set
		{
			portalBoostEvent_0 = value;
		}
	}

	public int BoostValue
	{
		[CompilerGenerated]
		get
		{
			return int_4;
		}
		[CompilerGenerated]
		set
		{
			int_4 = value;
		}
	}

	public EventBoostModel()
	{
		Name = "";
		Description = "";
	}

	public bool EventIsEnabled()
	{
		uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
		if (BeginDate <= num)
		{
			return num < EndedDate;
		}
		return false;
	}
}
