namespace Plugin.Core.Colorful
{
    using System;
    using System.Collections.Generic;

    public abstract class PatternCollection<T> : IPrototypable<PatternCollection<T>>
    {
        protected List<Pattern<T>> patterns;

        public PatternCollection()
        {
            this.patterns = new List<Pattern<T>>();
        }

        public abstract bool MatchFound(string input);
        public PatternCollection<T> Prototype() => 
            this.PrototypeCore();

        protected abstract PatternCollection<T> PrototypeCore();
    }
}

