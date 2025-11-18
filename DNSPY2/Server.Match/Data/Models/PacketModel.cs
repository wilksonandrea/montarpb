using System;
using System.Runtime.CompilerServices;

namespace Server.Match.Data.Models
{
	// Token: 0x0200003D RID: 61
	public class PacketModel
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000028E3 File Offset: 0x00000AE3
		// (set) Token: 0x0600016C RID: 364 RVA: 0x000028EB File Offset: 0x00000AEB
		public int Opcode
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600016D RID: 365 RVA: 0x000028F4 File Offset: 0x00000AF4
		// (set) Token: 0x0600016E RID: 366 RVA: 0x000028FC File Offset: 0x00000AFC
		public int Slot
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00002905 File Offset: 0x00000B05
		// (set) Token: 0x06000170 RID: 368 RVA: 0x0000290D File Offset: 0x00000B0D
		public int Round
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00002916 File Offset: 0x00000B16
		// (set) Token: 0x06000172 RID: 370 RVA: 0x0000291E File Offset: 0x00000B1E
		public int Length
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

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00002927 File Offset: 0x00000B27
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000292F File Offset: 0x00000B2F
		public int Respawn
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00002938 File Offset: 0x00000B38
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00002940 File Offset: 0x00000B40
		public int RoundNumber
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00002949 File Offset: 0x00000B49
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00002951 File Offset: 0x00000B51
		public int AccountId
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000295A File Offset: 0x00000B5A
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00002962 File Offset: 0x00000B62
		public int Unk1
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000296B File Offset: 0x00000B6B
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00002973 File Offset: 0x00000B73
		public int Unk2
		{
			[CompilerGenerated]
			get
			{
				return this.int_8;
			}
			[CompilerGenerated]
			set
			{
				this.int_8 = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000297C File Offset: 0x00000B7C
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00002984 File Offset: 0x00000B84
		public float Time
		{
			[CompilerGenerated]
			get
			{
				return this.float_0;
			}
			[CompilerGenerated]
			set
			{
				this.float_0 = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000298D File Offset: 0x00000B8D
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00002995 File Offset: 0x00000B95
		public byte[] Data
		{
			[CompilerGenerated]
			get
			{
				return this.byte_0;
			}
			[CompilerGenerated]
			set
			{
				this.byte_0 = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000299E File Offset: 0x00000B9E
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000029A6 File Offset: 0x00000BA6
		public byte[] WithEndData
		{
			[CompilerGenerated]
			get
			{
				return this.byte_1;
			}
			[CompilerGenerated]
			set
			{
				this.byte_1 = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000029AF File Offset: 0x00000BAF
		// (set) Token: 0x06000184 RID: 388 RVA: 0x000029B7 File Offset: 0x00000BB7
		public byte[] WithoutEndData
		{
			[CompilerGenerated]
			get
			{
				return this.byte_2;
			}
			[CompilerGenerated]
			set
			{
				this.byte_2 = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000029C0 File Offset: 0x00000BC0
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000029C8 File Offset: 0x00000BC8
		public DateTime ReceiveDate
		{
			[CompilerGenerated]
			get
			{
				return this.dateTime_0;
			}
			[CompilerGenerated]
			set
			{
				this.dateTime_0 = value;
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000020A2 File Offset: 0x000002A2
		public PacketModel()
		{
		}

		// Token: 0x0400005D RID: 93
		[CompilerGenerated]
		private int int_0;

		// Token: 0x0400005E RID: 94
		[CompilerGenerated]
		private int int_1;

		// Token: 0x0400005F RID: 95
		[CompilerGenerated]
		private int int_2;

		// Token: 0x04000060 RID: 96
		[CompilerGenerated]
		private int int_3;

		// Token: 0x04000061 RID: 97
		[CompilerGenerated]
		private int int_4;

		// Token: 0x04000062 RID: 98
		[CompilerGenerated]
		private int int_5;

		// Token: 0x04000063 RID: 99
		[CompilerGenerated]
		private int int_6;

		// Token: 0x04000064 RID: 100
		[CompilerGenerated]
		private int int_7;

		// Token: 0x04000065 RID: 101
		[CompilerGenerated]
		private int int_8;

		// Token: 0x04000066 RID: 102
		[CompilerGenerated]
		private float float_0;

		// Token: 0x04000067 RID: 103
		[CompilerGenerated]
		private byte[] byte_0;

		// Token: 0x04000068 RID: 104
		[CompilerGenerated]
		private byte[] byte_1;

		// Token: 0x04000069 RID: 105
		[CompilerGenerated]
		private byte[] byte_2;

		// Token: 0x0400006A RID: 106
		[CompilerGenerated]
		private DateTime dateTime_0;
	}
}
