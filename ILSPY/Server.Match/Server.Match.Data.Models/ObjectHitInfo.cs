using System.Runtime.CompilerServices;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using Server.Match.Data.Enums;

namespace Server.Match.Data.Models;

public class ObjectHitInfo
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
	private int int_4;

	[CompilerGenerated]
	private int int_5;

	[CompilerGenerated]
	private int int_6;

	[CompilerGenerated]
	private int int_7;

	[CompilerGenerated]
	private int int_8;

	[CompilerGenerated]
	private byte byte_0;

	[CompilerGenerated]
	private byte byte_1;

	[CompilerGenerated]
	private float float_0;

	[CompilerGenerated]
	private Half3 half3_0;

	[CompilerGenerated]
	private ClassType classType_0;

	[CompilerGenerated]
	private CharaHitPart charaHitPart_0;

	[CompilerGenerated]
	private CharaDeath charaDeath_0;

	public int Type
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

	public int ObjSyncId
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

	public int ObjId
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

	public int ObjLife
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

	public int KillerSlot
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

	public int AnimId1
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

	public int AnimId2
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

	public int DestroyState
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

	public int WeaponId
	{
		[CompilerGenerated]
		get
		{
			return int_8;
		}
		[CompilerGenerated]
		set
		{
			int_8 = value;
		}
	}

	public byte Accessory
	{
		[CompilerGenerated]
		get
		{
			return byte_0;
		}
		[CompilerGenerated]
		set
		{
			byte_0 = value;
		}
	}

	public byte Extensions
	{
		[CompilerGenerated]
		get
		{
			return byte_1;
		}
		[CompilerGenerated]
		set
		{
			byte_1 = value;
		}
	}

	public float SpecialUse
	{
		[CompilerGenerated]
		get
		{
			return float_0;
		}
		[CompilerGenerated]
		set
		{
			float_0 = value;
		}
	}

	public Half3 Position
	{
		[CompilerGenerated]
		get
		{
			return half3_0;
		}
		[CompilerGenerated]
		set
		{
			half3_0 = value;
		}
	}

	public ClassType WeaponClass
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

	public CharaHitPart HitPart
	{
		[CompilerGenerated]
		get
		{
			return charaHitPart_0;
		}
		[CompilerGenerated]
		set
		{
			charaHitPart_0 = value;
		}
	}

	public CharaDeath DeathType
	{
		[CompilerGenerated]
		get
		{
			return charaDeath_0;
		}
		[CompilerGenerated]
		set
		{
			charaDeath_0 = value;
		}
	}

	public ObjectHitInfo(int int_9)
	{
		Type = int_9;
		DeathType = CharaDeath.DEFAULT;
	}
}
