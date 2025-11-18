using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Plugin.Core.Colorful
{
	public sealed class TextFormatter
	{
		private Color color_0;

		private TextPattern textPattern_0;

		private readonly string string_0 = "{[0-9][^}]*}";

		public TextFormatter(Color color_1)
		{
			this.color_0 = color_1;
			this.textPattern_0 = new TextPattern(this.string_0);
		}

		public TextFormatter(Color color_1, string string_1)
		{
			this.color_0 = color_1;
			this.textPattern_0 = new TextPattern(this.string_0);
		}

		public List<KeyValuePair<string, Color>> GetFormatMap(string input, object[] args, Color[] colors)
		{
			List<KeyValuePair<string, Color>> keyValuePairs = new List<KeyValuePair<string, Color>>();
			List<MatchLocation> list = this.textPattern_0.GetMatchLocations(input).ToList<MatchLocation>();
			List<string> strs = this.textPattern_0.GetMatches(input).ToList<string>();
			this.method_0(ref args, ref colors);
			int end = 0;
			for (int i = 0; i < list.Count<MatchLocation>(); i++)
			{
				int ınt32 = int.Parse(strs[i].TrimStart(new char[] { '{' }).TrimEnd(new char[] { '}' }));
				int end1 = 0;
				if (i > 0)
				{
					end1 = list[i - 1].End;
				}
				int beginning = list[i].Beginning;
				end = list[i].End;
				string str = input.Substring(end1, beginning - end1);
				string str1 = args[ınt32].ToString();
				keyValuePairs.Add(new KeyValuePair<string, Color>(str, this.color_0));
				keyValuePairs.Add(new KeyValuePair<string, Color>(str1, colors[ınt32]));
			}
			if (end < input.Length)
			{
				string str2 = input.Substring(end, input.Length - end);
				keyValuePairs.Add(new KeyValuePair<string, Color>(str2, this.color_0));
			}
			return keyValuePairs;
		}

		private void method_0(ref object[] object_0, ref Color[] color_1)
		{
			if ((int)color_1.Length < (int)object_0.Length)
			{
				Color color1 = color_1[0];
				color_1 = new Color[(int)object_0.Length];
				for (int i = 0; i < (int)object_0.Length; i++)
				{
					color_1[i] = color1;
				}
			}
		}
	}
}