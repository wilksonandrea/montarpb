using System;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;
using Plugin.Core.Utility;

namespace Plugin.Core.Models
{
	// Token: 0x02000051 RID: 81
	public class EventBoostModel
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00003B40 File Offset: 0x00001D40
		// (set) Token: 0x06000311 RID: 785 RVA: 0x00003B48 File Offset: 0x00001D48
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

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00003B51 File Offset: 0x00001D51
		// (set) Token: 0x06000313 RID: 787 RVA: 0x00003B59 File Offset: 0x00001D59
		public int BonusExp
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

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00003B62 File Offset: 0x00001D62
		// (set) Token: 0x06000315 RID: 789 RVA: 0x00003B6A File Offset: 0x00001D6A
		public int BonusGold
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

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00003B73 File Offset: 0x00001D73
		// (set) Token: 0x06000317 RID: 791 RVA: 0x00003B7B File Offset: 0x00001D7B
		public int Percent
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00003B84 File Offset: 0x00001D84
		// (set) Token: 0x06000319 RID: 793 RVA: 0x00003B8C File Offset: 0x00001D8C
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

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00003B95 File Offset: 0x00001D95
		// (set) Token: 0x0600031B RID: 795 RVA: 0x00003B9D File Offset: 0x00001D9D
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00003BA6 File Offset: 0x00001DA6
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00003BAE File Offset: 0x00001DAE
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

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00003BB7 File Offset: 0x00001DB7
		// (set) Token: 0x0600031F RID: 799 RVA: 0x00003BBF File Offset: 0x00001DBF
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

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00003BC8 File Offset: 0x00001DC8
		// (set) Token: 0x06000321 RID: 801 RVA: 0x00003BD0 File Offset: 0x00001DD0
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

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00003BD9 File Offset: 0x00001DD9
		// (set) Token: 0x06000323 RID: 803 RVA: 0x00003BE1 File Offset: 0x00001DE1
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

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00003BEA File Offset: 0x00001DEA
		// (set) Token: 0x06000325 RID: 805 RVA: 0x00003BF2 File Offset: 0x00001DF2
		public PortalBoostEvent BoostType
		{
			[CompilerGenerated]
			get
			{
				return this.portalBoostEvent_0;
			}
			[CompilerGenerated]
			set
			{
				this.portalBoostEvent_0 = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00003BFB File Offset: 0x00001DFB
		// (set) Token: 0x06000327 RID: 807 RVA: 0x00003C03 File Offset: 0x00001E03
		public int BoostValue
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

		// Token: 0x06000328 RID: 808 RVA: 0x00003C0C File Offset: 0x00001E0C
		public EventBoostModel()
		{
			this.Name = "";
			this.Description = "";
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0001AB30 File Offset: 0x00018D30
		public bool EventIsEnabled()
		{
			uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			return this.BeginDate <= num && num < this.EndedDate;
		}

		// Token: 0x04000120 RID: 288
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000121 RID: 289
		[CompilerGenerated]
		private int int_1;

		// Token: 0x04000122 RID: 290
		[CompilerGenerated]
		private int int_2;

		// Token: 0x04000123 RID: 291
		[CompilerGenerated]
		private int int_3;

		// Token: 0x04000124 RID: 292
		[CompilerGenerated]
		private uint uint_0;

		// Token: 0x04000125 RID: 293
		[CompilerGenerated]
		private uint uint_1;

		// Token: 0x04000126 RID: 294
		[CompilerGenerated]
		private string string_0;

		// Token: 0x04000127 RID: 295
		[CompilerGenerated]
		private string string_1;

		// Token: 0x04000128 RID: 296
		[CompilerGenerated]
		private bool bool_0;

		// Token: 0x04000129 RID: 297
		[CompilerGenerated]
		private bool bool_1;

		// Token: 0x0400012A RID: 298
		[CompilerGenerated]
		private PortalBoostEvent portalBoostEvent_0;

		// Token: 0x0400012B RID: 299
		[CompilerGenerated]
		private int int_4;
	}
}
