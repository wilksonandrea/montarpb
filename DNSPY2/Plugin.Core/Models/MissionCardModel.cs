using System;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models
{
	// Token: 0x02000059 RID: 89
	public class MissionCardModel
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00003FAE File Offset: 0x000021AE
		// (set) Token: 0x0600038F RID: 911 RVA: 0x00003FB6 File Offset: 0x000021B6
		public ClassType WeaponReq
		{
			[CompilerGenerated]
			get
			{
				return this.classType_0;
			}
			[CompilerGenerated]
			set
			{
				this.classType_0 = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00003FBF File Offset: 0x000021BF
		// (set) Token: 0x06000391 RID: 913 RVA: 0x00003FC7 File Offset: 0x000021C7
		public MissionType MissionType
		{
			[CompilerGenerated]
			get
			{
				return this.missionType_0;
			}
			[CompilerGenerated]
			set
			{
				this.missionType_0 = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00003FD0 File Offset: 0x000021D0
		// (set) Token: 0x06000393 RID: 915 RVA: 0x00003FD8 File Offset: 0x000021D8
		public int MissionId
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

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00003FE1 File Offset: 0x000021E1
		// (set) Token: 0x06000395 RID: 917 RVA: 0x00003FE9 File Offset: 0x000021E9
		public int MapId
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

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00003FF2 File Offset: 0x000021F2
		// (set) Token: 0x06000397 RID: 919 RVA: 0x00003FFA File Offset: 0x000021FA
		public int WeaponReqId
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

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00004003 File Offset: 0x00002203
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000400B File Offset: 0x0000220B
		public int MissionLimit
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

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00004014 File Offset: 0x00002214
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0000401C File Offset: 0x0000221C
		public int MissionBasicId
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

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00004025 File Offset: 0x00002225
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0000402D File Offset: 0x0000222D
		public int CardBasicId
		{
			[CompilerGenerated]
			get
			{
				return this.int_5;
			}
			[CompilerGenerated]
			set
			{
				this.int_5 = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00004036 File Offset: 0x00002236
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0000403E File Offset: 0x0000223E
		public int ArrayIdx
		{
			[CompilerGenerated]
			get
			{
				return this.int_6;
			}
			[CompilerGenerated]
			set
			{
				this.int_6 = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00004047 File Offset: 0x00002247
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000404F File Offset: 0x0000224F
		public int Flag
		{
			[CompilerGenerated]
			get
			{
				return this.int_7;
			}
			[CompilerGenerated]
			set
			{
				this.int_7 = value;
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00004058 File Offset: 0x00002258
		public MissionCardModel(int int_8, int int_9)
		{
			this.CardBasicId = int_8;
			this.MissionBasicId = int_9;
			this.ArrayIdx = int_8 * 4 + int_9;
			this.Flag = 15 << 4 * int_9;
		}

		// Token: 0x04000157 RID: 343
		[CompilerGenerated]
		private ClassType classType_0;

		// Token: 0x04000158 RID: 344
		[CompilerGenerated]
		private MissionType missionType_0;

		// Token: 0x04000159 RID: 345
		[CompilerGenerated]
		private int int_0;

		// Token: 0x0400015A RID: 346
		[CompilerGenerated]
		private int int_1;

		// Token: 0x0400015B RID: 347
		[CompilerGenerated]
		private int int_2;

		// Token: 0x0400015C RID: 348
		[CompilerGenerated]
		private int int_3;

		// Token: 0x0400015D RID: 349
		[CompilerGenerated]
		private int int_4;

		// Token: 0x0400015E RID: 350
		[CompilerGenerated]
		private int int_5;

		// Token: 0x0400015F RID: 351
		[CompilerGenerated]
		private int int_6;

		// Token: 0x04000160 RID: 352
		[CompilerGenerated]
		private int int_7;
	}
}
