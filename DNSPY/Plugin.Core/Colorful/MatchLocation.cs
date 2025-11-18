using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	// Token: 0x02000108 RID: 264
	public class MatchLocation : IEquatable<MatchLocation>, IComparable<MatchLocation>, IPrototypable<MatchLocation>
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00007BC3 File Offset: 0x00005DC3
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x00007BCB File Offset: 0x00005DCB
		public int Beginning
		{
			[CompilerGenerated]
			get
			{
				return this.int_0;
			}
			[CompilerGenerated]
			private set
			{
				this.int_0 = value;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00007BD4 File Offset: 0x00005DD4
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x00007BDC File Offset: 0x00005DDC
		public int End
		{
			[CompilerGenerated]
			get
			{
				return this.int_1;
			}
			[CompilerGenerated]
			private set
			{
				this.int_1 = value;
			}
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00007BE5 File Offset: 0x00005DE5
		public MatchLocation(int int_2, int int_3)
		{
			this.Beginning = int_2;
			this.End = int_3;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00007BFB File Offset: 0x00005DFB
		public MatchLocation Prototype()
		{
			return new MatchLocation(this.Beginning, this.End);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00007C0E File Offset: 0x00005E0E
		public bool Equals(MatchLocation other)
		{
			return other != null && this.Beginning == other.Beginning && this.End == other.End;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00007C33 File Offset: 0x00005E33
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MatchLocation);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00021F78 File Offset: 0x00020178
		public override int GetHashCode()
		{
			return 163 * (79 + this.Beginning.GetHashCode()) * (79 + this.End.GetHashCode());
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00021FB0 File Offset: 0x000201B0
		public int CompareTo(MatchLocation other)
		{
			return this.Beginning.CompareTo(other.Beginning);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00021FD4 File Offset: 0x000201D4
		public override string ToString()
		{
			return this.Beginning.ToString() + ", " + this.End.ToString();
		}

		// Token: 0x04000716 RID: 1814
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000717 RID: 1815
		[CompilerGenerated]
		private int int_1;
	}
}
