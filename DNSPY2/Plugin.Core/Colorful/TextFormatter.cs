using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Plugin.Core.Colorful
{
	// Token: 0x02000119 RID: 281
	public sealed class TextFormatter
	{
		// Token: 0x06000A07 RID: 2567 RVA: 0x00007F25 File Offset: 0x00006125
		public TextFormatter(Color color_1)
		{
			this.color_0 = color_1;
			this.textPattern_0 = new TextPattern(this.string_0);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00007F25 File Offset: 0x00006125
		public TextFormatter(Color color_1, string string_1)
		{
			this.color_0 = color_1;
			this.textPattern_0 = new TextPattern(this.string_0);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00022834 File Offset: 0x00020A34
		public List<KeyValuePair<string, Color>> GetFormatMap(string input, object[] args, Color[] colors)
		{
			List<KeyValuePair<string, Color>> list = new List<KeyValuePair<string, Color>>();
			List<MatchLocation> list2 = this.textPattern_0.GetMatchLocations(input).ToList<MatchLocation>();
			List<string> list3 = this.textPattern_0.GetMatches(input).ToList<string>();
			this.method_0(ref args, ref colors);
			int num = 0;
			for (int i = 0; i < list2.Count<MatchLocation>(); i++)
			{
				int num2 = int.Parse(list3[i].TrimStart(new char[] { '{' }).TrimEnd(new char[] { '}' }));
				int num3 = 0;
				if (i > 0)
				{
					num3 = list2[i - 1].End;
				}
				int beginning = list2[i].Beginning;
				num = list2[i].End;
				string text = input.Substring(num3, beginning - num3);
				string text2 = args[num2].ToString();
				list.Add(new KeyValuePair<string, Color>(text, this.color_0));
				list.Add(new KeyValuePair<string, Color>(text2, colors[num2]));
			}
			if (num < input.Length)
			{
				string text3 = input.Substring(num, input.Length - num);
				list.Add(new KeyValuePair<string, Color>(text3, this.color_0));
			}
			return list;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00022968 File Offset: 0x00020B68
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

		// Token: 0x04000739 RID: 1849
		private Color color_0;

		// Token: 0x0400073A RID: 1850
		private TextPattern textPattern_0;

		// Token: 0x0400073B RID: 1851
		private readonly string string_0 = "{[0-9][^}]*}";
	}
}
