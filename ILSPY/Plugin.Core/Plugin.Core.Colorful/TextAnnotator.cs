using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful;

public sealed class TextAnnotator
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class32
	{
		public static readonly Class32 _003C_003E9 = new Class32();

		public static Func<KeyValuePair<StyleClass<TextPattern>, MatchLocation>, MatchLocation> _003C_003E9__4_0;

		internal MatchLocation method_0(KeyValuePair<StyleClass<TextPattern>, MatchLocation> keyValuePair_0)
		{
			return keyValuePair_0.Value;
		}
	}

	private StyleSheet styleSheet_0;

	private Dictionary<StyleClass<TextPattern>, Styler.MatchFound> dictionary_0 = new Dictionary<StyleClass<TextPattern>, Styler.MatchFound>();

	public TextAnnotator(StyleSheet styleSheet_1)
	{
		styleSheet_0 = styleSheet_1;
		foreach (StyleClass<TextPattern> style in styleSheet_1.Styles)
		{
			dictionary_0.Add(style, (style as Styler).MatchFoundHandler);
		}
	}

	public List<KeyValuePair<string, Color>> GetAnnotationMap(string input)
	{
		IEnumerable<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> ienumerable_ = method_0(input);
		return method_1(ienumerable_, input);
	}

	private List<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> method_0(string string_0)
	{
		List<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> list = new List<KeyValuePair<StyleClass<TextPattern>, MatchLocation>>();
		List<MatchLocation> list2 = new List<MatchLocation>();
		foreach (StyleClass<TextPattern> style in styleSheet_0.Styles)
		{
			foreach (MatchLocation matchLocation in style.Target.GetMatchLocations(string_0))
			{
				if (list2.Contains(matchLocation))
				{
					int index = list2.IndexOf(matchLocation);
					list.RemoveAt(index);
					list2.RemoveAt(index);
				}
				list.Add(new KeyValuePair<StyleClass<TextPattern>, MatchLocation>(style, matchLocation));
				list2.Add(matchLocation);
			}
		}
		return list.OrderBy((KeyValuePair<StyleClass<TextPattern>, MatchLocation> keyValuePair_0) => keyValuePair_0.Value).ToList();
	}

	private List<KeyValuePair<string, Color>> method_1(IEnumerable<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> ienumerable_0, string string_0)
	{
		List<KeyValuePair<string, Color>> list = new List<KeyValuePair<string, Color>>();
		MatchLocation matchLocation = new MatchLocation(0, 0);
		int num = 0;
		foreach (KeyValuePair<StyleClass<TextPattern>, MatchLocation> item in ienumerable_0)
		{
			MatchLocation value = item.Value;
			if (matchLocation.End > value.Beginning)
			{
				matchLocation = new MatchLocation(0, 0);
			}
			int end = matchLocation.End;
			int beginning = value.Beginning;
			int num2 = beginning;
			num = value.End;
			string text = string_0.Substring(end, beginning - end);
			string text2 = string_0.Substring(num2, num - num2);
			text2 = dictionary_0[item.Key](string_0, item.Value, string_0.Substring(num2, num - num2));
			if (text != "")
			{
				list.Add(new KeyValuePair<string, Color>(text, styleSheet_0.UnstyledColor));
			}
			if (text2 != "")
			{
				list.Add(new KeyValuePair<string, Color>(text2, item.Key.Color));
			}
			matchLocation = value.Prototype();
		}
		if (num < string_0.Length)
		{
			string key = string_0.Substring(num, string_0.Length - num);
			list.Add(new KeyValuePair<string, Color>(key, styleSheet_0.UnstyledColor));
		}
		return list;
	}
}
