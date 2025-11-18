using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x0200004F RID: 79
	public class CompetitiveRank
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00003A56 File Offset: 0x00001C56
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00003A5E File Offset: 0x00001C5E
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

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00003A67 File Offset: 0x00001C67
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00003A6F File Offset: 0x00001C6F
		public int TourneyLevel
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00003A78 File Offset: 0x00001C78
		// (set) Token: 0x060002FA RID: 762 RVA: 0x00003A80 File Offset: 0x00001C80
		public int Points
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00003A89 File Offset: 0x00001C89
		// (set) Token: 0x060002FC RID: 764 RVA: 0x00003A91 File Offset: 0x00001C91
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

		// Token: 0x060002FD RID: 765 RVA: 0x00002116 File Offset: 0x00000316
		public CompetitiveRank()
		{
		}

		// Token: 0x04000114 RID: 276
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000115 RID: 277
		[CompilerGenerated]
		private int int_1;

		// Token: 0x04000116 RID: 278
		[CompilerGenerated]
		private int int_2;

		// Token: 0x04000117 RID: 279
		[CompilerGenerated]
		private string string_0;
	}
}
