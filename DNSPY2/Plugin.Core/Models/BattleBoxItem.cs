using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000049 RID: 73
	public class BattleBoxItem
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060002AD RID: 685 RVA: 0x00003827 File Offset: 0x00001A27
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000382F File Offset: 0x00001A2F
		public int GoodsId
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00003838 File Offset: 0x00001A38
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00003840 File Offset: 0x00001A40
		public int Percent
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

		// Token: 0x060002B1 RID: 689 RVA: 0x00002116 File Offset: 0x00000316
		public BattleBoxItem()
		{
		}

		// Token: 0x040000F4 RID: 244
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040000F5 RID: 245
		[CompilerGenerated]
		private int int_1;
	}
}
