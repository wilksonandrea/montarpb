using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models;

public class MissionCardModel
{
	[CompilerGenerated]
	private ClassType classType_0;

	[CompilerGenerated]
	private MissionType missionType_0;

	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	[CompilerGenerated]
	private int int_2;

	[CompilerGenerated]
	private int int_3;

	[CompilerGenerated]
	private int int_4;

	[CompilerGenerated]
	private int int_5;

	[CompilerGenerated]
	private int int_6;

	[CompilerGenerated]
	private int int_7;

	public ClassType WeaponReq
	{
		[CompilerGenerated]
		get
		{
			return classType_0;
		}
		[CompilerGenerated]
		set
		{
			classType_0 = value;
		}
	}

	public MissionType MissionType
	{
		[CompilerGenerated]
		get
		{
			return missionType_0;
		}
		[CompilerGenerated]
		set
		{
			missionType_0 = value;
		}
	}

	public int MissionId
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

	public int MapId
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

	public int WeaponReqId
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

	public int MissionLimit
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

	public int MissionBasicId
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

	public int CardBasicId
	{
		[CompilerGenerated]
		get
		{
			return int_5;
		}
		[CompilerGenerated]
		set
		{
			int_5 = value;
		}
	}

	public int ArrayIdx
	{
		[CompilerGenerated]
		get
		{
			return int_6;
		}
		[CompilerGenerated]
		set
		{
			int_6 = value;
		}
	}

	public int Flag
	{
		[CompilerGenerated]
		get
		{
			return int_7;
		}
		[CompilerGenerated]
		set
		{
			int_7 = value;
		}
	}

	public MissionCardModel(int int_8, int int_9)
	{
		CardBasicId = int_8;
		MissionBasicId = int_9;
		ArrayIdx = int_8 * 4 + int_9;
		Flag = 15 << 4 * int_9;
	}
}
