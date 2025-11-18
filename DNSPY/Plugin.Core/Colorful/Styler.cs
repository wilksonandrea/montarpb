using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	// Token: 0x0200010E RID: 270
	public sealed class Styler : StyleClass<TextPattern>, IEquatable<Styler>
	{
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00007E1D File Offset: 0x0000601D
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x00007E25 File Offset: 0x00006025
		public Styler.MatchFound MatchFoundHandler
		{
			[CompilerGenerated]
			get
			{
				return this.matchFound_0;
			}
			[CompilerGenerated]
			private set
			{
				this.matchFound_0 = value;
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00007E2E File Offset: 0x0000602E
		public Styler(string string_0, Color color_1, Styler.MatchFound matchFound_1)
		{
			base.Target = new TextPattern(string_0);
			base.Color = color_1;
			this.MatchFoundHandler = matchFound_1;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00007E50 File Offset: 0x00006050
		public bool Equals(Styler other)
		{
			return other != null && base.Equals(other) && this.MatchFoundHandler == other.MatchFoundHandler;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00007E73 File Offset: 0x00006073
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Styler);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00007E81 File Offset: 0x00006081
		public override int GetHashCode()
		{
			return base.GetHashCode() * (79 + this.MatchFoundHandler.GetHashCode());
		}

		// Token: 0x04000723 RID: 1827
		[CompilerGenerated]
		private Styler.MatchFound matchFound_0;

		// Token: 0x0200010F RID: 271
		// (Invoke) Token: 0x060009E7 RID: 2535
		public delegate string MatchFound(string unstyledInput, MatchLocation matchLocation, string match);

		// Token: 0x02000110 RID: 272
		// (Invoke) Token: 0x060009EB RID: 2539
		public delegate string MatchFoundLite(string match);
	}
}
