using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Plugin.Core.Colorful
{
	public class Figlet
	{
		private readonly FigletFont figletFont_0;

		public Figlet()
		{
			this.figletFont_0 = FigletFont.Default;
		}

		public Figlet(FigletFont figletFont_1)
		{
			if (figletFont_1 == null)
			{
				throw new ArgumentNullException("font");
			}
			this.figletFont_0 = figletFont_1;
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
			List<int> ınt32s = new List<int>();
			string string0 = string_0;
			for (int i = 0; i < string0.Length; i++)
			{
				char chr = string0[i];
				int ınt32 = 0;
				for (int j = 1; j <= figletFont_1.Height; j++)
				{
					string str = Figlet.smethod_2(figletFont_1, chr, j);
					ınt32 = (str.Length > ınt32 ? str.Length : ınt32);
				}
				ınt32s.Add(ınt32);
			}
			return ınt32s.Sum();
		}

		private static string smethod_2(FigletFont figletFont_1, char char_0, int int_0)
		{
			int commentLines = figletFont_1.CommentLines + (Convert.ToInt32(char_0) - 32) * figletFont_1.Height;
			string lines = figletFont_1.Lines[commentLines + int_0];
			char chr = lines[lines.Length - 1];
			lines = Regex.Replace(lines, string.Concat("\\", chr.ToString(), "{1,2}$"), string.Empty);
			if (figletFont_1.Kerning > 0)
			{
				lines = string.Concat(lines, new string(' ', figletFont_1.Kerning));
			}
			return lines.Replace(figletFont_1.HardBlank, " ");
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
			int ınt32 = Figlet.smethod_1(this.figletFont_0, value);
			char[,] chrArray = new char[this.figletFont_0.Height + 1, ınt32];
			int[,] ınt32Array = new int[this.figletFont_0.Height + 1, ınt32];
			Color[,] colorArray = new Color[this.figletFont_0.Height + 1, ınt32];
			for (int i = 1; i <= this.figletFont_0.Height; i++)
			{
				int length = 0;
				for (int j = 0; j < value.Length; j++)
				{
					char chr = value[j];
					string str = Figlet.smethod_2(this.figletFont_0, chr, i);
					stringBuilder.Append(str);
					Figlet.smethod_0(str, j, length, i, chrArray, ınt32Array);
					length += str.Length;
				}
				stringBuilder.AppendLine();
			}
			return new StyledString(value, stringBuilder.ToString())
			{
				CharacterGeometry = chrArray,
				CharacterIndexGeometry = ınt32Array,
				ColorGeometry = colorArray
			};
		}
	}
}