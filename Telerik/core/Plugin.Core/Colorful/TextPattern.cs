using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Plugin.Core.Colorful
{
	public sealed class TextPattern : Pattern<string>
	{
		private Regex regex_0;

		public TextPattern(string string_0) : base(string_0)
		{
			this.regex_0 = new Regex(string_0);
		}

		public override IEnumerable<string> GetMatches(string input)
		{
			TextPattern textPattern = null;
			MatchCollection matchCollections = textPattern.regex_0.Matches(input);
			if (matchCollections.Count != 0)
			{
				foreach (Match match in matchCollections)
				{
					yield return match.Value;
				}
			}
			else
			{
			}
		}

		public override IEnumerable<MatchLocation> GetMatchLocations(string input)
		{
			TextPattern textPattern = null;
			MatchCollection matchCollections = textPattern.regex_0.Matches(input);
			if (matchCollections.Count != 0)
			{
				foreach (Match match in matchCollections)
				{
					int ındex = match.Index;
					yield return new MatchLocation(ındex, ındex + match.Length);
				}
			}
			else
			{
			}
		}
	}
}