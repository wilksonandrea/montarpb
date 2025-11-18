namespace Plugin.Core.Colorful
{
    using System;
    using System.Runtime.CompilerServices;

    public class MatchLocation : IEquatable<MatchLocation>, IComparable<MatchLocation>, IPrototypable<MatchLocation>
    {
        public MatchLocation(int int_2, int int_3)
        {
            this.Beginning = int_2;
            this.End = int_3;
        }

        public int CompareTo(MatchLocation other) => 
            this.Beginning.CompareTo(other.Beginning);

        public bool Equals(MatchLocation other) => 
            (other != null) ? ((this.Beginning == other.Beginning) && (this.End == other.End)) : false;

        public override bool Equals(object obj) => 
            this.Equals(obj as MatchLocation);

        public override int GetHashCode() => 
            (0xa3 * (0x4f + this.Beginning.GetHashCode())) * (0x4f + this.End.GetHashCode());

        public MatchLocation Prototype() => 
            new MatchLocation(this.Beginning, this.End);

        public override string ToString() => 
            this.Beginning.ToString() + ", " + this.End.ToString();

        public int Beginning { get; private set; }

        public int End { get; private set; }
    }
}

