using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x0200005E RID: 94
	public class PassItemModel
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060003CA RID: 970 RVA: 0x000041D2 File Offset: 0x000023D2
		// (set) Token: 0x060003CB RID: 971 RVA: 0x000041DA File Offset: 0x000023DA
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

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060003CC RID: 972 RVA: 0x000041E3 File Offset: 0x000023E3
		// (set) Token: 0x060003CD RID: 973 RVA: 0x000041EB File Offset: 0x000023EB
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

		// Token: 0x060003CE RID: 974 RVA: 0x000041F4 File Offset: 0x000023F4
		public void SetGoodId(int GoodId)
		{
			this.GoodId = GoodId;
			if (GoodId > 0)
			{
				this.IsReward = true;
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00002116 File Offset: 0x00000316
		public PassItemModel()
		{
		}

		// Token: 0x04000172 RID: 370
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000173 RID: 371
		[CompilerGenerated]
		private bool bool_0;
	}
}
