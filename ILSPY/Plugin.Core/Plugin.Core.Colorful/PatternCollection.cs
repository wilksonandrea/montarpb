using System.Collections.Generic;

namespace Plugin.Core.Colorful;

public abstract class PatternCollection<T> : IPrototypable<PatternCollection<T>>
{
	protected List<Pattern<T>> patterns = new List<Pattern<T>>();

	public PatternCollection()
	{
	}

	public PatternCollection<T> Prototype()
	{
		return PrototypeCore();
	}

	protected abstract PatternCollection<T> PrototypeCore();

	public abstract bool MatchFound(string input);
}
