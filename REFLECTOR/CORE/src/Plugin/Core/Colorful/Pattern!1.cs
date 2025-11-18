namespace Plugin.Core.Colorful
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class Pattern<T> : IEquatable<Pattern<T>>
    {
        [CompilerGenerated]
        private T gparam_0;

        public Pattern(T gparam_1)
        {
            this.Value = gparam_1;
        }

        public bool Equals(Pattern<T> other) => 
            (other != null) ? this.Value.Equals(other.Value) : false;

        public override bool Equals(object obj) => 
            this.Equals(obj as Pattern<T>);

        public override int GetHashCode() => 
            0xa3 * (0x4f + this.Value.GetHashCode());

        public abstract IEnumerable<T> GetMatches(T input);
        public abstract IEnumerable<MatchLocation> GetMatchLocations(T input);

        public T Value
        {
            get => 
                this.gparam_0;
            private set => 
                this.gparam_0 = value;
        }
    }
}

