using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x0200004D RID: 77
	public class CartGoods
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00003904 File Offset: 0x00001B04
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000390C File Offset: 0x00001B0C
		public int GoodId
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00003915 File Offset: 0x00001B15
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000391D File Offset: 0x00001B1D
		public int BuyType
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

		// Token: 0x060002D1 RID: 721 RVA: 0x00002116 File Offset: 0x00000316
		public CartGoods()
		{
		}

		// Token: 0x04000101 RID: 257
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000102 RID: 258
		[CompilerGenerated]
		private int int_1;
	}
}
