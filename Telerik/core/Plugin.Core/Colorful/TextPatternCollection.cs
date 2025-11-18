using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	public sealed class TextPatternCollection : PatternCollection<string>
	{
		public TextPatternCollection(string[] string_0)
		{
			string[] string0 = string_0;
			for (int i = 0; i < (int)string0.Length; i++)
			{
				string str = string0[i];
				this.patterns.Add(new TextPattern(str));
			}
		}

		public override bool MatchFound(string input)
		{
			return this.patterns.Any<Pattern<string>>((Pattern<string> pattern_0) => pattern_0.GetMatchLocations(input).Count<MatchLocation>() > 0);
		}

		public new TextPatternCollection Prototype()
		{
			return new TextPatternCollection((
				from pattern_0 in this.patterns
				select pattern_0.Value).ToArray<string>());
		}

		protected override PatternCollection<string> PrototypeCore()
		{
			return this.Prototype();
		}
	}
}