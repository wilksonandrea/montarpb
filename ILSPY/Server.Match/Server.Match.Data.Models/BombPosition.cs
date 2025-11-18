using System.Runtime.CompilerServices;
using Plugin.Core.SharpDX;

namespace Server.Match.Data.Models;

public class BombPosition
{
	[CompilerGenerated]
	private float float_0;

	[CompilerGenerated]
	private float float_1;

	[CompilerGenerated]
	private float float_2;

	[CompilerGenerated]
	private Half3 half3_0;

	[CompilerGenerated]
	private bool bool_0;

	public float X
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

	public float Y
	{
		[CompilerGenerated]
		get
		{
			return float_1;
		}
		[CompilerGenerated]
		set
		{
			float_1 = value;
		}
	}

	public float Z
	{
		[CompilerGenerated]
		get
		{
			return float_2;
		}
		[CompilerGenerated]
		set
		{
			float_2 = value;
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

	public bool EveryWhere
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
}
