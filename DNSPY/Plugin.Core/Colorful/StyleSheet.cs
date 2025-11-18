using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	// Token: 0x02000111 RID: 273
	public sealed class StyleSheet
	{
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x00007E98 File Offset: 0x00006098
		// (set) Token: 0x060009EF RID: 2543 RVA: 0x00007EA0 File Offset: 0x000060A0
		public List<StyleClass<TextPattern>> Styles
		{
			[CompilerGenerated]
			get
			{
				return this.list_0;
			}
			[CompilerGenerated]
			private set
			{
				this.list_0 = value;
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00007EA9 File Offset: 0x000060A9
		public StyleSheet(Color color_0)
		{
			this.Styles = new List<StyleClass<TextPattern>>();
			this.UnstyledColor = color_0;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0002215C File Offset: 0x0002035C
		public void AddStyle(string target, Color color, Styler.MatchFound matchHandler)
		{
			Styler styler = new Styler(target, color, matchHandler);
			this.Styles.Add(styler);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00022180 File Offset: 0x00020380
		public void AddStyle(string target, Color color, Styler.MatchFoundLite matchHandler)
		{
			StyleSheet.Class31 @class = new StyleSheet.Class31();
			@class.matchFoundLite_0 = matchHandler;
			Styler.MatchFound matchFound = new Styler.MatchFound(@class.method_0);
			Styler styler = new Styler(target, color, matchFound);
			this.Styles.Add(styler);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x000221BC File Offset: 0x000203BC
		public void AddStyle(string target, Color color)
		{
			Styler.MatchFound matchFound = new Styler.MatchFound(StyleSheet.Class30.<>9.method_0);
			Styler styler = new Styler(target, color, matchFound);
			this.Styles.Add(styler);
		}

		// Token: 0x04000724 RID: 1828
		[CompilerGenerated]
		private List<StyleClass<TextPattern>> list_0;

		// Token: 0x04000725 RID: 1829
		public Color UnstyledColor;

		// Token: 0x02000112 RID: 274
		[CompilerGenerated]
		[Serializable]
		private sealed class Class30
		{
			// Token: 0x060009F4 RID: 2548 RVA: 0x00007EC3 File Offset: 0x000060C3
			// Note: this type is marked as 'beforefieldinit'.
			static Class30()
			{
			}

			// Token: 0x060009F5 RID: 2549 RVA: 0x00002116 File Offset: 0x00000316
			public Class30()
			{
			}

			// Token: 0x060009F6 RID: 2550 RVA: 0x00007ECF File Offset: 0x000060CF
			internal string method_0(string string_0, MatchLocation matchLocation_0, string string_1)
			{
				return string_1;
			}

			// Token: 0x04000726 RID: 1830
			public static readonly StyleSheet.Class30 <>9 = new StyleSheet.Class30();

			// Token: 0x04000727 RID: 1831
			public static Styler.MatchFound <>9__8_0;
		}

		// Token: 0x02000113 RID: 275
		[CompilerGenerated]
		private sealed class Class31
		{
			// Token: 0x060009F7 RID: 2551 RVA: 0x00002116 File Offset: 0x00000316
			public Class31()
			{
			}

			// Token: 0x060009F8 RID: 2552 RVA: 0x00007ED2 File Offset: 0x000060D2
			internal string method_0(string string_0, MatchLocation matchLocation_0, string string_1)
			{
				return this.matchFoundLite_0(string_1);
			}

			// Token: 0x04000728 RID: 1832
			public Styler.MatchFoundLite matchFoundLite_0;
		}
	}
}
