using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000080 RID: 128
	public class VisitItemModel
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x000052BD File Offset: 0x000034BD
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x000052C5 File Offset: 0x000034C5
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

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x000052CE File Offset: 0x000034CE
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x000052D6 File Offset: 0x000034D6
		public bool IsReward
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

		// Token: 0x060005C2 RID: 1474 RVA: 0x000052DF File Offset: 0x000034DF
		public void SetGoodId(int GoodId)
		{
			this.GoodId = GoodId;
			if (GoodId > 0)
			{
				this.IsReward = true;
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00002116 File Offset: 0x00000316
		public VisitItemModel()
		{
		}

		// Token: 0x0400024F RID: 591
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000250 RID: 592
		[CompilerGenerated]
		private bool bool_0;
	}
}
