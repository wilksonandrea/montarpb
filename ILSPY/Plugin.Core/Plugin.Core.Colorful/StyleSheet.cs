using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful;

public sealed class StyleSheet
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class30
	{
		public static readonly Class30 _003C_003E9 = new Class30();

		public static Styler.MatchFound _003C_003E9__8_0;

		internal string method_0(string string_0, MatchLocation matchLocation_0, string string_1)
		{
			return string_1;
		}
	}

	[CompilerGenerated]
	private sealed class Class31
	{
		public Styler.MatchFoundLite matchFoundLite_0;

		internal string method_0(string string_0, MatchLocation matchLocation_0, string string_1)
		{
			return matchFoundLite_0(string_1);
		}
	}

	[CompilerGenerated]
	private List<StyleClass<TextPattern>> list_0;

	public Color UnstyledColor;

	public List<StyleClass<TextPattern>> Styles
	{
		[CompilerGenerated]
		get
		{
			return list_0;
		}
		[CompilerGenerated]
		private set
		{
			list_0 = value;
		}
	}

	public StyleSheet(Color color_0)
	{
		Styles = new List<StyleClass<TextPattern>>();
		UnstyledColor = color_0;
	}

	public void AddStyle(string target, Color color, Styler.MatchFound matchHandler)
	{
		Styler item = new Styler(target, color, matchHandler);
		Styles.Add(item);
	}

	public void AddStyle(string target, Color color, Styler.MatchFoundLite matchHandler)
	{
		Styler.MatchFound matchFound_ = (string string_0, MatchLocation matchLocation_0, string string_1) => matchHandler(string_1);
		Styler item = new Styler(target, color, matchFound_);
		Styles.Add(item);
	}

	public void AddStyle(string target, Color color)
	{
		Styler.MatchFound matchFound_ = (string string_0, MatchLocation matchLocation_0, string string_1) => string_1;
		Styler item = new Styler(target, color, matchFound_);
		Styles.Add(item);
	}
}
