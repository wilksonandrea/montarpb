using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000060 RID: 96
	public class PlayerBattlepass
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00004269 File Offset: 0x00002469
		// (set) Token: 0x060003DA RID: 986 RVA: 0x00004271 File Offset: 0x00002471
		public int Id
		{
			[CompilerGenerated]
			get
			{
				return this.int_0;
			}
			[CompilerGenerated]
			set
			{
				this.int_0 = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000427A File Offset: 0x0000247A
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00004282 File Offset: 0x00002482
		public int Level
		{
			[CompilerGenerated]
			get
			{
				return this.int_1;
			}
			[CompilerGenerated]
			set
			{
				this.int_1 = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000428B File Offset: 0x0000248B
		// (set) Token: 0x060003DE RID: 990 RVA: 0x00004293 File Offset: 0x00002493
		public bool IsPremium
		{
			[CompilerGenerated]
			get
			{
				return this.bool_0;
			}
			[CompilerGenerated]
			set
			{
				this.bool_0 = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000429C File Offset: 0x0000249C
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x000042A4 File Offset: 0x000024A4
		public int TotalPoints
		{
			[CompilerGenerated]
			get
			{
				return this.int_2;
			}
			[CompilerGenerated]
			set
			{
				this.int_2 = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x000042AD File Offset: 0x000024AD
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x000042B5 File Offset: 0x000024B5
		public int DailyPoints
		{
			[CompilerGenerated]
			get
			{
				return this.int_3;
			}
			[CompilerGenerated]
			set
			{
				this.int_3 = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x000042BE File Offset: 0x000024BE
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x000042C6 File Offset: 0x000024C6
		public uint LastRecord
		{
			[CompilerGenerated]
			get
			{
				return this.uint_0;
			}
			[CompilerGenerated]
			set
			{
				this.uint_0 = value;
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00002116 File Offset: 0x00000316
		public PlayerBattlepass()
		{
		}

		// Token: 0x04000178 RID: 376
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000179 RID: 377
		[CompilerGenerated]
		private int int_1;

		// Token: 0x0400017A RID: 378
		[CompilerGenerated]
		private bool bool_0;

		// Token: 0x0400017B RID: 379
		[CompilerGenerated]
		private int int_2;

		// Token: 0x0400017C RID: 380
		[CompilerGenerated]
		private int int_3;

		// Token: 0x0400017D RID: 381
		[CompilerGenerated]
		private uint uint_0;
	}
}
