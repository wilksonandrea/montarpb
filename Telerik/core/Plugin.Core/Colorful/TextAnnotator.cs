using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	public sealed class TextAnnotator
	{
		private StyleSheet styleSheet_0;

		private Dictionary<StyleClass<TextPattern>, Styler.MatchFound> dictionary_0 = new Dictionary<StyleClass<TextPattern>, Styler.MatchFound>();

		public TextAnnotator(StyleSheet styleSheet_1)
		{
			this.styleSheet_0 = styleSheet_1;
			foreach (StyleClass<TextPattern> style in styleSheet_1.Styles)
			{
				this.dictionary_0.Add(style, (style as Styler).MatchFoundHandler);
			}
		}

		public List<KeyValuePair<string, Color>> GetAnnotationMap(string input)
		{
			return this.method_1(this.method_0(input), input);
		}

		private List<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> method_0(string string_0)
		{
			List<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> keyValuePairs = new List<KeyValuePair<StyleClass<TextPattern>, MatchLocation>>();
			List<MatchLocation> matchLocations = new List<MatchLocation>();
			foreach (StyleClass<TextPattern> style in this.styleSheet_0.Styles)
			{
				foreach (MatchLocation matchLocation in style.Target.GetMatchLocations(string_0))
				{
					if (matchLocations.Contains(matchLocation))
					{
						int ınt32 = matchLocations.IndexOf(matchLocation);
						keyValuePairs.RemoveAt(ınt32);
						matchLocations.RemoveAt(ınt32);
					}
					keyValuePairs.Add(new KeyValuePair<StyleClass<TextPattern>, MatchLocation>(style, matchLocation));
					matchLocations.Add(matchLocation);
				}
			}
			keyValuePairs = (
				from keyValuePair_0 in keyValuePairs
				orderby keyValuePair_0.Value
				select keyValuePair_0).ToList<KeyValuePair<StyleClass<TextPattern>, MatchLocation>>();
			return keyValuePairs;
		}

		private List<KeyValuePair<string, Color>> method_1(IEnumerable<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> ienumerable_0, string string_0)
		{
			List<KeyValuePair<string, Color>> keyValuePairs = new List<KeyValuePair<string, Color>>();
			MatchLocation matchLocation = new MatchLocation(0, 0);
			int end = 0;
			foreach (KeyValuePair<StyleClass<TextPattern>, MatchLocation> ienumerable0 in ienumerable_0)
			{
				MatchLocation value = ienumerable0.Value;
				if (matchLocation.End > value.Beginning)
				{
					matchLocation = new MatchLocation(0, 0);
				}
				int ınt32 = matchLocation.End;
				int beginning = value.Beginning;
				int ınt321 = beginning;
				end = value.End;
				string str = string_0.Substring(ınt32, beginning - ınt32);
				string ıtem = string_0.Substring(ınt321, end - ınt321);
				ıtem = this.dictionary_0[ienumerable0.Key](string_0, ienumerable0.Value, string_0.Substring(ınt321, end - ınt321));
				if (str != "")
				{
					keyValuePairs.Add(new KeyValuePair<string, Color>(str, this.styleSheet_0.UnstyledColor));
				}
				if (ıtem != "")
				{
					keyValuePairs.Add(new KeyValuePair<string, Color>(ıtem, ienumerable0.Key.Color));
				}
				matchLocation = value.Prototype();
			}
			if (end < string_0.Length)
			{
				string str1 = string_0.Substring(end, string_0.Length - end);
				keyValuePairs.Add(new KeyValuePair<string, Color>(str1, this.styleSheet_0.UnstyledColor));
			}
			return keyValuePairs;
		}
	}
}