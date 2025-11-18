using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	public sealed class StyleSheet
	{
		public Color UnstyledColor;

		public List<StyleClass<TextPattern>> Styles
		{
			get;
			private set;
		}

		public StyleSheet(Color color_0)
		{
			this.Styles = new List<StyleClass<TextPattern>>();
			this.UnstyledColor = color_0;
		}

		public void AddStyle(string target, Color color, Styler.MatchFound matchHandler)
		{
			Styler styler = new Styler(target, color, matchHandler);
			this.Styles.Add(styler);
		}

		public void AddStyle(string target, Color color, Styler.MatchFoundLite matchHandler)
		{
			Styler.MatchFound matchFound = (string string_0, MatchLocation matchLocation_0, string string_1) => matchHandler(string_1);
			Styler styler = new Styler(target, color, matchFound);
			this.Styles.Add(styler);
		}

		public void AddStyle(string target, Color color)
		{
			Styler styler = new Styler(target, color, (string string_0, MatchLocation matchLocation_0, string string_1) => string_1);
			this.Styles.Add(styler);
		}
	}
}