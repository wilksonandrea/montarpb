using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x0200005C RID: 92
	public class NHistoryModel
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000040DD File Offset: 0x000022DD
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x000040E5 File Offset: 0x000022E5
		public string OldNick
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

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x000040EE File Offset: 0x000022EE
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x000040F6 File Offset: 0x000022F6
		public string NewNick
		{
			[CompilerGenerated]
			get
			{
				return this.string_1;
			}
			[CompilerGenerated]
			set
			{
				this.string_1 = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x000040FF File Offset: 0x000022FF
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x00004107 File Offset: 0x00002307
		public string Motive
		{
			[CompilerGenerated]
			get
			{
				return this.string_2;
			}
			[CompilerGenerated]
			set
			{
				this.string_2 = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00004110 File Offset: 0x00002310
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x00004118 File Offset: 0x00002318
		public long ObjectId
		{
			[CompilerGenerated]
			get
			{
				return this.long_0;
			}
			[CompilerGenerated]
			set
			{
				this.long_0 = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00004121 File Offset: 0x00002321
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x00004129 File Offset: 0x00002329
		public long OwnerId
		{
			[CompilerGenerated]
			get
			{
				return this.long_1;
			}
			[CompilerGenerated]
			set
			{
				this.long_1 = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00004132 File Offset: 0x00002332
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0000413A File Offset: 0x0000233A
		public uint ChangeDate
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

		// Token: 0x060003BB RID: 955 RVA: 0x00002116 File Offset: 0x00000316
		public NHistoryModel()
		{
		}

		// Token: 0x04000166 RID: 358
		[CompilerGenerated]
		private string string_0;

		// Token: 0x04000167 RID: 359
		[CompilerGenerated]
		private string string_1;

		// Token: 0x04000168 RID: 360
		[CompilerGenerated]
		private string string_2;

		// Token: 0x04000169 RID: 361
		[CompilerGenerated]
		private long long_0;

		// Token: 0x0400016A RID: 362
		[CompilerGenerated]
		private long long_1;

		// Token: 0x0400016B RID: 363
		[CompilerGenerated]
		private uint uint_0;
	}
}
