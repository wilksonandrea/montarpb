using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Plugin.Core.Colorful
{
	// Token: 0x02000100 RID: 256
	public class Figlet
	{
		// Token: 0x06000971 RID: 2417 RVA: 0x00007976 File Offset: 0x00005B76
		public Figlet()
		{
			this.figletFont_0 = FigletFont.Default;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00007989 File Offset: 0x00005B89
		public Figlet(FigletFont figletFont_1)
		{
			if (figletFont_1 == null)
			{
				throw new ArgumentNullException("font");
			}
			this.figletFont_0 = figletFont_1;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0002190C File Offset: 0x0001FB0C
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
			int num = Figlet.smethod_1(this.figletFont_0, value);
			char[,] array = new char[this.figletFont_0.Height + 1, num];
			int[,] array2 = new int[this.figletFont_0.Height + 1, num];
			Color[,] array3 = new Color[this.figletFont_0.Height + 1, num];
			for (int i = 1; i <= this.figletFont_0.Height; i++)
			{
				int num2 = 0;
				for (int j = 0; j < value.Length; j++)
				{
					char c = value[j];
					string text = Figlet.smethod_2(this.figletFont_0, c, i);
					stringBuilder.Append(text);
					Figlet.smethod_0(text, j, num2, i, array, array2);
					num2 += text.Length;
				}
				stringBuilder.AppendLine();
			}
			return new StyledString(value, stringBuilder.ToString())
			{
				CharacterGeometry = array,
				CharacterIndexGeometry = array2,
				ColorGeometry = array3
			};
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00021A30 File Offset: 0x0001FC30
		private static void smethod_0(string string_0, int int_0, int int_1, int int_2, char[,] char_0, int[,] int_3)
		{
			for (int i = int_1; i < int_1 + string_0.Length; i++)
			{
				char_0[int_2, i] = string_0[i - int_1];
				int_3[int_2, i] = int_0;
			}
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00021A6C File Offset: 0x0001FC6C
		private static int smethod_1(FigletFont figletFont_1, string string_0)
		{
			List<int> list = new List<int>();
			foreach (char c in string_0)
			{
				int num = 0;
				for (int j = 1; j <= figletFont_1.Height; j++)
				{
					string text = Figlet.smethod_2(figletFont_1, c, j);
					num = ((text.Length > num) ? text.Length : num);
				}
				list.Add(num);
			}
			return list.Sum();
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00021AE4 File Offset: 0x0001FCE4
		private static string smethod_2(FigletFont figletFont_1, char char_0, int int_0)
		{
			int num = figletFont_1.CommentLines + (Convert.ToInt32(char_0) - 32) * figletFont_1.Height;
			string text = figletFont_1.Lines[num + int_0];
			text = Regex.Replace(text, "\\" + text[text.Length - 1].ToString() + "{1,2}$", string.Empty);
			if (figletFont_1.Kerning > 0)
			{
				text += new string(' ', figletFont_1.Kerning);
			}
			return text.Replace(figletFont_1.HardBlank, " ");
		}

		// Token: 0x040006FF RID: 1791
		private readonly FigletFont figletFont_0;
	}
}
