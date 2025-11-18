using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x0200005A RID: 90
	public class MissionItemAward
	{
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00004088 File Offset: 0x00002288
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00004090 File Offset: 0x00002290
		public int MissionId
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

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00004099 File Offset: 0x00002299
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x000040A1 File Offset: 0x000022A1
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

		// Token: 0x060003A7 RID: 935 RVA: 0x00002116 File Offset: 0x00000316
		public MissionItemAward()
		{
		}

		// Token: 0x04000161 RID: 353
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000162 RID: 354
		[CompilerGenerated]
		private ItemsModel itemsModel_0;
	}
}
