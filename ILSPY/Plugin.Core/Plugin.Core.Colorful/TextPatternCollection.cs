using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful;

public sealed class TextPatternCollection : PatternCollection<string>
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class35
	{
		public static readonly Class35 _003C_003E9 = new Class35();

		public static Func<Pattern<string>, string> _003C_003E9__1_0;

		internal string method_0(Pattern<string> pattern_0)
		{
			return pattern_0.Value;
		}
	}

	[CompilerGenerated]
	private sealed class Class36
	{
		public string string_0;

		internal bool method_0(Pattern<string> pattern_0)
		{
			return pattern_0.GetMatchLocations(string_0).Count() > 0;
		}
	}

	public TextPatternCollection(string[] string_0)
	{
		foreach (string string_ in string_0)
		{
			patterns.Add(new TextPattern(string_));
		}
	}

	public new TextPatternCollection Prototype()
	{
		return new TextPatternCollection(patterns.Select((Pattern<string> pattern_0) => pattern_0.Value).ToArray());
	}

	protected override PatternCollection<string> PrototypeCore()
	{
		return Prototype();
	}

	public override bool MatchFound(string input)
	{
		return patterns.Any((Pattern<string> pattern_0) => pattern_0.GetMatchLocations(input).Count() > 0);
	}
}
