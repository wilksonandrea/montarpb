using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	// Token: 0x0200011D RID: 285
	public sealed class TextPatternCollection : PatternCollection<string>
	{
		// Token: 0x06000A20 RID: 2592 RVA: 0x00022CE4 File Offset: 0x00020EE4
		public TextPatternCollection(string[] string_0)
		{
			foreach (string text in string_0)
			{
				this.patterns.Add(new TextPattern(text));
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00007FE7 File Offset: 0x000061E7
		public new TextPatternCollection Prototype()
		{
			return new TextPatternCollection(this.patterns.Select(new Func<Pattern<string>, string>(TextPatternCollection.Class35.<>9.method_0)).ToArray<string>());
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0000801D File Offset: 0x0000621D
		protected override PatternCollection<string> PrototypeCore()
		{
			return this.Prototype();
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00022D1C File Offset: 0x00020F1C
		public override bool MatchFound(string input)
		{
			TextPatternCollection.Class36 @class = new TextPatternCollection.Class36();
			@class.string_0 = input;
			return this.patterns.Any(new Func<Pattern<string>, bool>(@class.method_0));
		}

		// Token: 0x0200011E RID: 286
		[CompilerGenerated]
		[Serializable]
		private sealed class Class35
		{
			// Token: 0x06000A24 RID: 2596 RVA: 0x00008025 File Offset: 0x00006225
			// Note: this type is marked as 'beforefieldinit'.
			static Class35()
			{
			}

			// Token: 0x06000A25 RID: 2597 RVA: 0x00002116 File Offset: 0x00000316
			public Class35()
			{
			}

			// Token: 0x06000A26 RID: 2598 RVA: 0x00008031 File Offset: 0x00006231
			internal string method_0(Pattern<string> pattern_0)
			{
				return pattern_0.Value;
			}

			// Token: 0x0400074B RID: 1867
			public static readonly TextPatternCollection.Class35 <>9 = new TextPatternCollection.Class35();

			// Token: 0x0400074C RID: 1868
			public static Func<Pattern<string>, string> <>9__1_0;
		}

		// Token: 0x0200011F RID: 287
		[CompilerGenerated]
		private sealed class Class36
		{
			// Token: 0x06000A27 RID: 2599 RVA: 0x00002116 File Offset: 0x00000316
			public Class36()
			{
			}

			// Token: 0x06000A28 RID: 2600 RVA: 0x00008039 File Offset: 0x00006239
			internal bool method_0(Pattern<string> pattern_0)
			{
				return pattern_0.GetMatchLocations(this.string_0).Count<MatchLocation>() > 0;
			}

			// Token: 0x0400074D RID: 1869
			public string string_0;
		}
	}
}
