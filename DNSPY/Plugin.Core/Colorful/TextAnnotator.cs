using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	// Token: 0x02000117 RID: 279
	public sealed class TextAnnotator
	{
		// Token: 0x06000A00 RID: 2560 RVA: 0x00022524 File Offset: 0x00020724
		public TextAnnotator(StyleSheet styleSheet_1)
		{
			this.styleSheet_0 = styleSheet_1;
			foreach (StyleClass<TextPattern> styleClass in styleSheet_1.Styles)
			{
				this.dictionary_0.Add(styleClass, (styleClass as Styler).MatchFoundHandler);
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x000225A0 File Offset: 0x000207A0
		public List<KeyValuePair<string, Color>> GetAnnotationMap(string input)
		{
			IEnumerable<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> enumerable = this.method_0(input);
			return this.method_1(enumerable, input);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000225C0 File Offset: 0x000207C0
		private List<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> method_0(string string_0)
		{
			List<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> list = new List<KeyValuePair<StyleClass<TextPattern>, MatchLocation>>();
			List<MatchLocation> list2 = new List<MatchLocation>();
			foreach (StyleClass<TextPattern> styleClass in this.styleSheet_0.Styles)
			{
				foreach (MatchLocation matchLocation in styleClass.Target.GetMatchLocations(string_0))
				{
					if (list2.Contains(matchLocation))
					{
						int num = list2.IndexOf(matchLocation);
						list.RemoveAt(num);
						list2.RemoveAt(num);
					}
					list.Add(new KeyValuePair<StyleClass<TextPattern>, MatchLocation>(styleClass, matchLocation));
					list2.Add(matchLocation);
				}
			}
			list = list.OrderBy(new Func<KeyValuePair<StyleClass<TextPattern>, MatchLocation>, MatchLocation>(TextAnnotator.Class32.<>9.method_0)).ToList<KeyValuePair<StyleClass<TextPattern>, MatchLocation>>();
			return list;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x000226CC File Offset: 0x000208CC
		private List<KeyValuePair<string, Color>> method_1(IEnumerable<KeyValuePair<StyleClass<TextPattern>, MatchLocation>> ienumerable_0, string string_0)
		{
			List<KeyValuePair<string, Color>> list = new List<KeyValuePair<string, Color>>();
			MatchLocation matchLocation = new MatchLocation(0, 0);
			int num = 0;
			foreach (KeyValuePair<StyleClass<TextPattern>, MatchLocation> keyValuePair in ienumerable_0)
			{
				MatchLocation value = keyValuePair.Value;
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
				text2 = this.dictionary_0[keyValuePair.Key](string_0, keyValuePair.Value, string_0.Substring(num2, num - num2));
				if (text != "")
				{
					list.Add(new KeyValuePair<string, Color>(text, this.styleSheet_0.UnstyledColor));
				}
				if (text2 != "")
				{
					list.Add(new KeyValuePair<string, Color>(text2, keyValuePair.Key.Color));
				}
				matchLocation = value.Prototype();
			}
			if (num < string_0.Length)
			{
				string text3 = string_0.Substring(num, string_0.Length - num);
				list.Add(new KeyValuePair<string, Color>(text3, this.styleSheet_0.UnstyledColor));
			}
			return list;
		}

		// Token: 0x04000735 RID: 1845
		private StyleSheet styleSheet_0;

		// Token: 0x04000736 RID: 1846
		private Dictionary<StyleClass<TextPattern>, Styler.MatchFound> dictionary_0 = new Dictionary<StyleClass<TextPattern>, Styler.MatchFound>();

		// Token: 0x02000118 RID: 280
		[CompilerGenerated]
		[Serializable]
		private sealed class Class32
		{
			// Token: 0x06000A04 RID: 2564 RVA: 0x00007F10 File Offset: 0x00006110
			// Note: this type is marked as 'beforefieldinit'.
			static Class32()
			{
			}

			// Token: 0x06000A05 RID: 2565 RVA: 0x00002116 File Offset: 0x00000316
			public Class32()
			{
			}

			// Token: 0x06000A06 RID: 2566 RVA: 0x00007F1C File Offset: 0x0000611C
			internal MatchLocation method_0(KeyValuePair<StyleClass<TextPattern>, MatchLocation> keyValuePair_0)
			{
				return keyValuePair_0.Value;
			}

			// Token: 0x04000737 RID: 1847
			public static readonly TextAnnotator.Class32 <>9 = new TextAnnotator.Class32();

			// Token: 0x04000738 RID: 1848
			public static Func<KeyValuePair<StyleClass<TextPattern>, MatchLocation>, MatchLocation> <>9__4_0;
		}
	}
}
