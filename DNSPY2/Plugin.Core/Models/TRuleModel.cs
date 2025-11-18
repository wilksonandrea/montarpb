using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x0200007F RID: 127
	public class TRuleModel
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0000529B File Offset: 0x0000349B
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x000052A3 File Offset: 0x000034A3
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.string_0;
			}
			[CompilerGenerated]
			set
			{
				this.string_0 = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x000052AC File Offset: 0x000034AC
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x000052B4 File Offset: 0x000034B4
		public List<int> BanIndexes
		{
			[CompilerGenerated]
			get
			{
				return this.list_0;
			}
			[CompilerGenerated]
			set
			{
				this.list_0 = value;
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00002116 File Offset: 0x00000316
		public TRuleModel()
		{
		}

		// Token: 0x0400024D RID: 589
		[CompilerGenerated]
		private string string_0;

		// Token: 0x0400024E RID: 590
		[CompilerGenerated]
		private List<int> list_0;
	}
}
