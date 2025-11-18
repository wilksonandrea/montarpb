using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Utility;

namespace Plugin.Core.Models;

public class EventRankUpModel
{
	[CompilerGenerated]
	private int int_0;

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
	private List<int[]> list_0;

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

	public List<int[]> Ranks
	{
		[CompilerGenerated]
		get
		{
			return list_0;
		}
		[CompilerGenerated]
		set
		{
			list_0 = value;
		}
	}

	public EventRankUpModel()
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

	public int[] GetBonuses(int RankId)
	{
		lock (Ranks)
		{
			foreach (int[] rank in Ranks)
			{
				if (rank[0] == RankId)
				{
					return new int[3]
					{
						rank[1],
						rank[2],
						rank[3]
					};
				}
			}
			return new int[3];
		}
	}
}
