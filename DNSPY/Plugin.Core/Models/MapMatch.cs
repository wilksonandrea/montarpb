using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000097 RID: 151
	public class MapMatch
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00006015 File Offset: 0x00004215
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0000601D File Offset: 0x0000421D
		public int Mode
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

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00006026 File Offset: 0x00004226
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0000602E File Offset: 0x0000422E
		public int Id
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

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00006037 File Offset: 0x00004237
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0000603F File Offset: 0x0000423F
		public int Limit
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

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00006048 File Offset: 0x00004248
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x00006050 File Offset: 0x00004250
		public int Tag
		{
			[CompilerGenerated]
			get
			{
				return this.int_3;
			}
			[CompilerGenerated]
			set
			{
				this.int_3 = value;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00006059 File Offset: 0x00004259
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x00006061 File Offset: 0x00004261
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

		// Token: 0x06000721 RID: 1825 RVA: 0x0000606A File Offset: 0x0000426A
		public MapMatch(int int_4)
		{
			this.Mode = int_4;
		}

		// Token: 0x040002F2 RID: 754
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040002F3 RID: 755
		[CompilerGenerated]
		private int int_1;

		// Token: 0x040002F4 RID: 756
		[CompilerGenerated]
		private int int_2;

		// Token: 0x040002F5 RID: 757
		[CompilerGenerated]
		private int int_3;

		// Token: 0x040002F6 RID: 758
		[CompilerGenerated]
		private string string_0;
	}
}
