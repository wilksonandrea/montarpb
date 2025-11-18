using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x0200004B RID: 75
	public class BattleRewardItem
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000389E File Offset: 0x00001A9E
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x000038A6 File Offset: 0x00001AA6
		public int Index
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x000038AF File Offset: 0x00001AAF
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x000038B7 File Offset: 0x00001AB7
		public int GoodId
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

		// Token: 0x060002C3 RID: 707 RVA: 0x00002116 File Offset: 0x00000316
		public BattleRewardItem()
		{
		}

		// Token: 0x040000FB RID: 251
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040000FC RID: 252
		[CompilerGenerated]
		private int int_1;
	}
}
