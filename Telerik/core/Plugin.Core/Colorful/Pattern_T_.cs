using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	public abstract class Pattern<T> : IEquatable<Pattern<T>>
	{
		public T Value
		{
			get;
			private set;
		}

		public Pattern(T gparam_1)
		{
			this.Value = gparam_1;
		}

		public bool Equals(Pattern<T> other)
		{
			if (other == null)
			{
				return false;
			}
			return this.Value.Equals(other.Value);
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as Pattern<T>);
		}

		public override int GetHashCode()
		{
			return 163 * (79 + this.Value.GetHashCode());
		}

		public abstract IEnumerable<T> GetMatches(T input);

		public abstract IEnumerable<MatchLocation> GetMatchLocations(T input);
	}
}