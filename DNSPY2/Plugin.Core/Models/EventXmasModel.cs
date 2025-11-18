using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000056 RID: 86
	public class EventXmasModel
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00003E6E File Offset: 0x0000206E
		// (set) Token: 0x0600036F RID: 879 RVA: 0x00003E76 File Offset: 0x00002076
		public uint BeginDate
		{
			[CompilerGenerated]
			get
			{
				return this.uint_0;
			}
			[CompilerGenerated]
			set
			{
				this.uint_0 = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00003E7F File Offset: 0x0000207F
		// (set) Token: 0x06000371 RID: 881 RVA: 0x00003E87 File Offset: 0x00002087
		public uint EndedDate
		{
			[CompilerGenerated]
			get
			{
				return this.uint_1;
			}
			[CompilerGenerated]
			set
			{
				this.uint_1 = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00003E90 File Offset: 0x00002090
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00003E98 File Offset: 0x00002098
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

		// Token: 0x06000374 RID: 884 RVA: 0x00002116 File Offset: 0x00000316
		public EventXmasModel()
		{
		}

		// Token: 0x04000149 RID: 329
		[CompilerGenerated]
		private uint uint_0;

		// Token: 0x0400014A RID: 330
		[CompilerGenerated]
		private uint uint_1;

		// Token: 0x0400014B RID: 331
		[CompilerGenerated]
		private int int_0;
	}
}
