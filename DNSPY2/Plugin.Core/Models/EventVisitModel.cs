using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Utility;

namespace Plugin.Core.Models
{
	// Token: 0x02000055 RID: 85
	public class EventVisitModel
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00003DED File Offset: 0x00001FED
		// (set) Token: 0x0600035F RID: 863 RVA: 0x00003DF5 File Offset: 0x00001FF5
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

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000360 RID: 864 RVA: 0x00003DFE File Offset: 0x00001FFE
		// (set) Token: 0x06000361 RID: 865 RVA: 0x00003E06 File Offset: 0x00002006
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

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00003E0F File Offset: 0x0000200F
		// (set) Token: 0x06000363 RID: 867 RVA: 0x00003E17 File Offset: 0x00002017
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00003E20 File Offset: 0x00002020
		// (set) Token: 0x06000365 RID: 869 RVA: 0x00003E28 File Offset: 0x00002028
		public int Checks
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

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00003E31 File Offset: 0x00002031
		// (set) Token: 0x06000367 RID: 871 RVA: 0x00003E39 File Offset: 0x00002039
		public string Title
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

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000368 RID: 872 RVA: 0x00003E42 File Offset: 0x00002042
		// (set) Token: 0x06000369 RID: 873 RVA: 0x00003E4A File Offset: 0x0000204A
		public List<VisitBoxModel> Boxes
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

		// Token: 0x0600036A RID: 874 RVA: 0x00003E53 File Offset: 0x00002053
		public EventVisitModel()
		{
			this.Checks = 31;
			this.Title = "";
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001AC70 File Offset: 0x00018E70
		public bool EventIsEnabled()
		{
			uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			return this.BeginDate <= num && num < this.EndedDate;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001ACA4 File Offset: 0x00018EA4
		public List<VisitItemModel> GetReward(int Day, int Type)
		{
			List<VisitItemModel> list = new List<VisitItemModel>();
			if (Type == 0)
			{
				list.Add(this.Boxes[Day].Reward1);
			}
			else if (Type == 1)
			{
				list.Add(this.Boxes[Day].Reward2);
			}
			else
			{
				list.Add(this.Boxes[Day].Reward1);
				list.Add(this.Boxes[Day].Reward2);
			}
			return list;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001AD20 File Offset: 0x00018F20
		public void SetBoxCounts()
		{
			for (int i = 0; i < 31; i++)
			{
				this.Boxes[i].SetCount();
			}
		}

		// Token: 0x04000143 RID: 323
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000144 RID: 324
		[CompilerGenerated]
		private uint uint_0;

		// Token: 0x04000145 RID: 325
		[CompilerGenerated]
		private uint uint_1;

		// Token: 0x04000146 RID: 326
		[CompilerGenerated]
		private int int_1;

		// Token: 0x04000147 RID: 327
		[CompilerGenerated]
		private string string_0;

		// Token: 0x04000148 RID: 328
		[CompilerGenerated]
		private List<VisitBoxModel> list_0;
	}
}
