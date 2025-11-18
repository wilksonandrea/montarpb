using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000096 RID: 150
	public class InternetCafe
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00005FB1 File Offset: 0x000041B1
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x00005FB9 File Offset: 0x000041B9
		public int ConfigId
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

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x00005FC2 File Offset: 0x000041C2
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x00005FCA File Offset: 0x000041CA
		public int BasicExp
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

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00005FD3 File Offset: 0x000041D3
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x00005FDB File Offset: 0x000041DB
		public int BasicGold
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

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00005FE4 File Offset: 0x000041E4
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x00005FEC File Offset: 0x000041EC
		public int PremiumExp
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

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00005FF5 File Offset: 0x000041F5
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x00005FFD File Offset: 0x000041FD
		public int PremiumGold
		{
			[CompilerGenerated]
			get
			{
				return this.int_4;
			}
			[CompilerGenerated]
			set
			{
				this.int_4 = value;
			}
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00006006 File Offset: 0x00004206
		public InternetCafe(int int_5)
		{
			this.ConfigId = int_5;
		}

		// Token: 0x040002ED RID: 749
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040002EE RID: 750
		[CompilerGenerated]
		private int int_1;

		// Token: 0x040002EF RID: 751
		[CompilerGenerated]
		private int int_2;

		// Token: 0x040002F0 RID: 752
		[CompilerGenerated]
		private int int_3;

		// Token: 0x040002F1 RID: 753
		[CompilerGenerated]
		private int int_4;
	}
}
