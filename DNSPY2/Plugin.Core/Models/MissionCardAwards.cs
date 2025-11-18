using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000058 RID: 88
	public class MissionCardAwards
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00003F0A File Offset: 0x0000210A
		// (set) Token: 0x0600037F RID: 895 RVA: 0x00003F12 File Offset: 0x00002112
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

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00003F1B File Offset: 0x0000211B
		// (set) Token: 0x06000381 RID: 897 RVA: 0x00003F23 File Offset: 0x00002123
		public int Card
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

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00003F2C File Offset: 0x0000212C
		// (set) Token: 0x06000383 RID: 899 RVA: 0x00003F34 File Offset: 0x00002134
		public int Ensign
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

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00003F3D File Offset: 0x0000213D
		// (set) Token: 0x06000385 RID: 901 RVA: 0x00003F45 File Offset: 0x00002145
		public int Medal
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

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00003F4E File Offset: 0x0000214E
		// (set) Token: 0x06000387 RID: 903 RVA: 0x00003F56 File Offset: 0x00002156
		public int Ribbon
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

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00003F5F File Offset: 0x0000215F
		// (set) Token: 0x06000389 RID: 905 RVA: 0x00003F67 File Offset: 0x00002167
		public int Exp
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

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00003F70 File Offset: 0x00002170
		// (set) Token: 0x0600038B RID: 907 RVA: 0x00003F78 File Offset: 0x00002178
		public int Gold
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

		// Token: 0x0600038C RID: 908 RVA: 0x00003F81 File Offset: 0x00002181
		public bool Unusable()
		{
			return this.Ensign == 0 && this.Medal == 0 && this.Ribbon == 0 && this.Exp == 0 && this.Gold == 0;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00002116 File Offset: 0x00000316
		public MissionCardAwards()
		{
		}

		// Token: 0x04000150 RID: 336
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000151 RID: 337
		[CompilerGenerated]
		private int int_1;

		// Token: 0x04000152 RID: 338
		[CompilerGenerated]
		private int int_2;

		// Token: 0x04000153 RID: 339
		[CompilerGenerated]
		private int int_3;

		// Token: 0x04000154 RID: 340
		[CompilerGenerated]
		private int int_4;

		// Token: 0x04000155 RID: 341
		[CompilerGenerated]
		private int int_5;

		// Token: 0x04000156 RID: 342
		[CompilerGenerated]
		private int int_6;
	}
}
