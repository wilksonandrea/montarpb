using System;
using System.Runtime.CompilerServices;

namespace Server.Match.Data.Models
{
	// Token: 0x02000038 RID: 56
	public class DeffectModel
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00002612 File Offset: 0x00000812
		// (set) Token: 0x06000119 RID: 281 RVA: 0x0000261A File Offset: 0x0000081A
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00002623 File Offset: 0x00000823
		// (set) Token: 0x0600011B RID: 283 RVA: 0x0000262B File Offset: 0x0000082B
		public int Life
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

		// Token: 0x0600011C RID: 284 RVA: 0x000020A2 File Offset: 0x000002A2
		public DeffectModel()
		{
		}

		// Token: 0x04000038 RID: 56
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000039 RID: 57
		[CompilerGenerated]
		private int int_1;
	}
}
