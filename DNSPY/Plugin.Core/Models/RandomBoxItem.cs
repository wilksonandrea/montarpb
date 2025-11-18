using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000098 RID: 152
	public class RandomBoxItem
	{
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x00006079 File Offset: 0x00004279
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x00006081 File Offset: 0x00004281
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

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0000608A File Offset: 0x0000428A
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x00006092 File Offset: 0x00004292
		public int GoodsId
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

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0000609B File Offset: 0x0000429B
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x000060A3 File Offset: 0x000042A3
		public int Percent
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

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x000060AC File Offset: 0x000042AC
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x000060B4 File Offset: 0x000042B4
		public bool Special
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

		// Token: 0x0600072A RID: 1834 RVA: 0x00002116 File Offset: 0x00000316
		public RandomBoxItem()
		{
		}

		// Token: 0x040002F7 RID: 759
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040002F8 RID: 760
		[CompilerGenerated]
		private int int_1;

		// Token: 0x040002F9 RID: 761
		[CompilerGenerated]
		private int int_2;

		// Token: 0x040002FA RID: 762
		[CompilerGenerated]
		private bool bool_0;
	}
}
