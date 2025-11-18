using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Plugin.Core.Colorful;

public class Figlet
{
	private readonly FigletFont figletFont_0;

	public Figlet()
	{
		figletFont_0 = FigletFont.Default;
	}

	public Figlet(FigletFont figletFont_1)
	{
		if (figletFont_1 == null)
		{
			throw new ArgumentNullException("font");
		}
		figletFont_0 = figletFont_1;
	}

	public StyledString ToAscii(string value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (Encoding.UTF8.GetByteCount(value) != value.Length)
		{
			throw new ArgumentException("String contains non-ascii characters");
		}
		StringBuilder stringBuilder = new StringBuilder();
		int num = smethod_1(figletFont_0, value);
		char[,] array = new char[figletFont_0.Height + 1, num];
		int[,] array2 = new int[figletFont_0.Height + 1, num];
		Color[,] colorGeometry = new Color[figletFont_0.Height + 1, num];
		for (int i = 1; i <= figletFont_0.Height; i++)
		{
			int num2 = 0;
			for (int j = 0; j < value.Length; j++)
			{
				char char_ = value[j];
				string text = smethod_2(figletFont_0, char_, i);
				stringBuilder.Append(text);
				smethod_0(text, j, num2, i, array, array2);
				num2 += text.Length;
			}
			stringBuilder.AppendLine();
		}
		return new StyledString(value, stringBuilder.ToString())
		{
			CharacterGeometry = array,
			CharacterIndexGeometry = array2,
			ColorGeometry = colorGeometry
		};
	}

	private static void smethod_0(string string_0, int int_0, int int_1, int int_2, char[,] char_0, int[,] int_3)
	{
		for (int i = int_1; i < int_1 + string_0.Length; i++)
		{
			char_0[int_2, i] = string_0[i - int_1];
			int_3[int_2, i] = int_0;
		}
	}

	private static int smethod_1(FigletFont figletFont_1, string string_0)
	{
		List<int> list = new List<int>();
		foreach (char char_ in string_0)
		{
			int num = 0;
			for (int j = 1; j <= figletFont_1.Height; j++)
			{
				string text = smethod_2(figletFont_1, char_, j);
				num = ((text.Length > num) ? text.Length : num);
			}
			list.Add(num);
		}
		return list.Sum();
	}

	private static string smethod_2(FigletFont figletFont_1, char char_0, int int_0)
	{
		int num = figletFont_1.CommentLines + (Convert.ToInt32(char_0) - 32) * figletFont_1.Height;
		string text = figletFont_1.Lines[num + int_0];
		text = Regex.Replace(text, "\\" + text[text.Length - 1] + "{1,2}$", string.Empty);
		if (figletFont_1.Kerning > 0)
		{
			text += new string(' ', figletFont_1.Kerning);
		}
		return text.Replace(figletFont_1.HardBlank, " ");
	}
}
