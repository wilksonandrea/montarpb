using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Plugin.Core.Colorful;

public sealed class TextFormatter
{
	private Color color_0;

	private TextPattern textPattern_0;

	private readonly string string_0 = "{[0-9][^}]*}";

	public TextFormatter(Color color_1)
	{
		color_0 = color_1;
		textPattern_0 = new TextPattern(string_0);
	}

	public TextFormatter(Color color_1, string string_1)
	{
		color_0 = color_1;
		textPattern_0 = new TextPattern(string_0);
	}

	public List<KeyValuePair<string, Color>> GetFormatMap(string input, object[] args, Color[] colors)
	{
		List<KeyValuePair<string, Color>> list = new List<KeyValuePair<string, Color>>();
		List<MatchLocation> list2 = textPattern_0.GetMatchLocations(input).ToList();
		List<string> list3 = textPattern_0.GetMatches(input).ToList();
		method_0(ref args, ref colors);
		int num = 0;
		for (int i = 0; i < list2.Count(); i++)
		{
			int num2 = int.Parse(list3[i].TrimStart('{').TrimEnd('}'));
			int num3 = 0;
			if (i > 0)
			{
				num3 = list2[i - 1].End;
			}
			int beginning = list2[i].Beginning;
			num = list2[i].End;
			string key = input.Substring(num3, beginning - num3);
			string key2 = args[num2].ToString();
			list.Add(new KeyValuePair<string, Color>(key, color_0));
			list.Add(new KeyValuePair<string, Color>(key2, colors[num2]));
		}
		if (num < input.Length)
		{
			string key3 = input.Substring(num, input.Length - num);
			list.Add(new KeyValuePair<string, Color>(key3, color_0));
		}
		return list;
	}

	private void method_0(ref object[] object_0, ref Color[] color_1)
	{
		if (color_1.Length < object_0.Length)
		{
			Color color = color_1[0];
			color_1 = new Color[object_0.Length];
			for (int i = 0; i < object_0.Length; i++)
			{
				color_1[i] = color;
			}
		}
	}
}
