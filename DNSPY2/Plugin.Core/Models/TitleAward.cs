using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x0200007D RID: 125
	public class TitleAward
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000519B File Offset: 0x0000339B
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x000051A3 File Offset: 0x000033A3
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

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x000051AC File Offset: 0x000033AC
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x000051B4 File Offset: 0x000033B4
		public ItemsModel Item
		{
			[CompilerGenerated]
			get
			{
				return this.itemsModel_0;
			}
			[CompilerGenerated]
			set
			{
				this.itemsModel_0 = value;
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00002116 File Offset: 0x00000316
		public TitleAward()
		{
		}

		// Token: 0x04000240 RID: 576
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000241 RID: 577
		[CompilerGenerated]
		private ItemsModel itemsModel_0;
	}
}
