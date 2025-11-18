using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	public class MatchLocation : IEquatable<MatchLocation>, IComparable<MatchLocation>, IPrototypable<MatchLocation>
	{
		public int Beginning
		{
			get;
			private set;
		}

		public int End
		{
			get;
			private set;
		}

		public MatchLocation(int int_2, int int_3)
		{
			this.Beginning = int_2;
			this.End = int_3;
		}

		public int CompareTo(MatchLocation other)
		{
			return this.Beginning.CompareTo(other.Beginning);
		}

		public bool Equals(MatchLocation other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.Beginning != other.Beginning)
			{
				return false;
			}
			return this.End == other.End;
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as MatchLocation);
		}

		public override int GetHashCode()
		{
			int beginning = this.Beginning;
			int hashCode = 163 * (79 + beginning.GetHashCode());
			beginning = this.End;
			return hashCode * (79 + beginning.GetHashCode());
		}

		public MatchLocation Prototype()
		{
			return new MatchLocation(this.Beginning, this.End);
		}

		public override string ToString()
		{
			string str = this.Beginning.ToString();
			int end = this.End;
			return string.Concat(str, ", ", end.ToString());
		}
	}
}