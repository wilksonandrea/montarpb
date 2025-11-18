using System;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models
{
	// Token: 0x0200006B RID: 107
	public class RHistoryModel
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000488C File Offset: 0x00002A8C
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x00004894 File Offset: 0x00002A94
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

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000489D File Offset: 0x00002A9D
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x000048A5 File Offset: 0x00002AA5
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

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x000048AE File Offset: 0x00002AAE
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x000048B6 File Offset: 0x00002AB6
		public long SenderId
		{
			[CompilerGenerated]
			get
			{
				return this.long_2;
			}
			[CompilerGenerated]
			set
			{
				this.long_2 = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x000048BF File Offset: 0x00002ABF
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x000048C7 File Offset: 0x00002AC7
		public uint Date
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

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x000048D0 File Offset: 0x00002AD0
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x000048D8 File Offset: 0x00002AD8
		public string OwnerNick
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

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x000048E1 File Offset: 0x00002AE1
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x000048E9 File Offset: 0x00002AE9
		public string SenderNick
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

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x000048F2 File Offset: 0x00002AF2
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x000048FA File Offset: 0x00002AFA
		public string Message
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

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00004903 File Offset: 0x00002B03
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x0000490B File Offset: 0x00002B0B
		public ReportType Type
		{
			[CompilerGenerated]
			get
			{
				return this.reportType_0;
			}
			[CompilerGenerated]
			set
			{
				this.reportType_0 = value;
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00002116 File Offset: 0x00000316
		public RHistoryModel()
		{
		}

		// Token: 0x040001BA RID: 442
		[CompilerGenerated]
		private long long_0;

		// Token: 0x040001BB RID: 443
		[CompilerGenerated]
		private long long_1;

		// Token: 0x040001BC RID: 444
		[CompilerGenerated]
		private long long_2;

		// Token: 0x040001BD RID: 445
		[CompilerGenerated]
		private uint uint_0;

		// Token: 0x040001BE RID: 446
		[CompilerGenerated]
		private string string_0;

		// Token: 0x040001BF RID: 447
		[CompilerGenerated]
		private string string_1;

		// Token: 0x040001C0 RID: 448
		[CompilerGenerated]
		private string string_2;

		// Token: 0x040001C1 RID: 449
		[CompilerGenerated]
		private ReportType reportType_0;
	}
}
