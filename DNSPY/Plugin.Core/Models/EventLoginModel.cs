using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Utility;

namespace Plugin.Core.Models
{
	// Token: 0x02000050 RID: 80
	public class EventLoginModel
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00003A9A File Offset: 0x00001C9A
		// (set) Token: 0x060002FF RID: 767 RVA: 0x00003AA2 File Offset: 0x00001CA2
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

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00003AAB File Offset: 0x00001CAB
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00003AB3 File Offset: 0x00001CB3
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

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00003ABC File Offset: 0x00001CBC
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00003AC4 File Offset: 0x00001CC4
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

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00003ACD File Offset: 0x00001CCD
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00003AD5 File Offset: 0x00001CD5
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

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00003ADE File Offset: 0x00001CDE
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00003AE6 File Offset: 0x00001CE6
		public string Description
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

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00003AEF File Offset: 0x00001CEF
		// (set) Token: 0x06000309 RID: 777 RVA: 0x00003AF7 File Offset: 0x00001CF7
		public bool Period
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

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00003B00 File Offset: 0x00001D00
		// (set) Token: 0x0600030B RID: 779 RVA: 0x00003B08 File Offset: 0x00001D08
		public bool Priority
		{
			[CompilerGenerated]
			get
			{
				return this.bool_1;
			}
			[CompilerGenerated]
			set
			{
				this.bool_1 = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00003B11 File Offset: 0x00001D11
		// (set) Token: 0x0600030D RID: 781 RVA: 0x00003B19 File Offset: 0x00001D19
		public List<int> Goods
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

		// Token: 0x0600030E RID: 782 RVA: 0x00003B22 File Offset: 0x00001D22
		public EventLoginModel()
		{
			this.Name = "";
			this.Description = "";
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0001AAFC File Offset: 0x00018CFC
		public bool EventIsEnabled()
		{
			uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			return this.BeginDate <= num && num < this.EndedDate;
		}

		// Token: 0x04000118 RID: 280
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000119 RID: 281
		[CompilerGenerated]
		private uint uint_0;

		// Token: 0x0400011A RID: 282
		[CompilerGenerated]
		private uint uint_1;

		// Token: 0x0400011B RID: 283
		[CompilerGenerated]
		private string string_0;

		// Token: 0x0400011C RID: 284
		[CompilerGenerated]
		private string string_1;

		// Token: 0x0400011D RID: 285
		[CompilerGenerated]
		private bool bool_0;

		// Token: 0x0400011E RID: 286
		[CompilerGenerated]
		private bool bool_1;

		// Token: 0x0400011F RID: 287
		[CompilerGenerated]
		private List<int> list_0;
	}
}
