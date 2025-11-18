using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful;

public abstract class Pattern<T> : IEquatable<Pattern<T>>
{
	[CompilerGenerated]
	private T gparam_0;

	public T Value
	{
		[CompilerGenerated]
		get
		{
			return gparam_0;
		}
		[CompilerGenerated]
		private set
		{
			gparam_0 = value;
		}
	}

	public Pattern(T gparam_1)
	{
		Value = gparam_1;
	}

	public abstract IEnumerable<MatchLocation> GetMatchLocations(T input);

	public abstract IEnumerable<T> GetMatches(T input);

	public bool Equals(Pattern<T> other)
	{
		if (other == null)
		{
			return false;
		}
		return Value.Equals(other.Value);
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as Pattern<T>);
	}

	public override int GetHashCode()
	{
		return 163 * (79 + Value.GetHashCode());
	}
}
