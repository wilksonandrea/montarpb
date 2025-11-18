using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful;

public class MatchLocation : IEquatable<MatchLocation>, IComparable<MatchLocation>, IPrototypable<MatchLocation>
{
	[CompilerGenerated]
	private int int_0;

	[CompilerGenerated]
	private int int_1;

	public int Beginning
	{
		[CompilerGenerated]
		get
		{
			return int_0;
		}
		[CompilerGenerated]
		private set
		{
			int_0 = value;
		}
	}

	public int End
	{
		[CompilerGenerated]
		get
		{
			return int_1;
		}
		[CompilerGenerated]
		private set
		{
			int_1 = value;
		}
	}

	public MatchLocation(int int_2, int int_3)
	{
		Beginning = int_2;
		End = int_3;
	}

	public MatchLocation Prototype()
	{
		return new MatchLocation(Beginning, End);
	}

	public bool Equals(MatchLocation other)
	{
		if (other == null)
		{
			return false;
		}
		if (Beginning == other.Beginning)
		{
			return End == other.End;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as MatchLocation);
	}

	public override int GetHashCode()
	{
		return 163 * (79 + Beginning.GetHashCode()) * (79 + End.GetHashCode());
	}

	public int CompareTo(MatchLocation other)
	{
		return Beginning.CompareTo(other.Beginning);
	}

	public override string ToString()
	{
		return Beginning + ", " + End;
	}
}
