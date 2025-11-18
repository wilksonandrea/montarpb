using System;
using System.Collections.Generic;

namespace Plugin.Core.Colorful
{
	public abstract class PatternCollection<T> : IPrototypable<PatternCollection<T>>
	{
		protected List<Pattern<T>> patterns;

		public PatternCollection()
		{
		}

		public abstract bool MatchFound(string input);

		public PatternCollection<T> Prototype()
		{
			return this.PrototypeCore();
		}

		protected abstract PatternCollection<T> PrototypeCore();
	}
}