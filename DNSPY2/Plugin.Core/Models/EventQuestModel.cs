using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000053 RID: 83
	public class EventQuestModel
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00003D25 File Offset: 0x00001F25
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00003D2D File Offset: 0x00001F2D
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00003D36 File Offset: 0x00001F36
		// (set) Token: 0x06000349 RID: 841 RVA: 0x00003D3E File Offset: 0x00001F3E
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

		// Token: 0x0600034A RID: 842 RVA: 0x00002116 File Offset: 0x00000316
		public EventQuestModel()
		{
		}

		// Token: 0x04000139 RID: 313
		[CompilerGenerated]
		private uint uint_0;

		// Token: 0x0400013A RID: 314
		[CompilerGenerated]
		private uint uint_1;
	}
}
