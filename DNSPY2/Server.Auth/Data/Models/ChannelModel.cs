using System;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Server.Auth.Data.Models
{
	// Token: 0x0200005F RID: 95
	public class ChannelModel
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00002BF0 File Offset: 0x00000DF0
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00002BF8 File Offset: 0x00000DF8
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

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00002C01 File Offset: 0x00000E01
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00002C09 File Offset: 0x00000E09
		public ChannelType Type
		{
			[CompilerGenerated]
			get
			{
				return this.channelType_0;
			}
			[CompilerGenerated]
			set
			{
				this.channelType_0 = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00002C12 File Offset: 0x00000E12
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00002C1A File Offset: 0x00000E1A
		public int ServerId
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

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00002C23 File Offset: 0x00000E23
		// (set) Token: 0x06000164 RID: 356 RVA: 0x00002C2B File Offset: 0x00000E2B
		public int TotalPlayers
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00002C34 File Offset: 0x00000E34
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00002C3C File Offset: 0x00000E3C
		public int MaxRooms
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

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00002C45 File Offset: 0x00000E45
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00002C4D File Offset: 0x00000E4D
		public int ExpBonus
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00002C56 File Offset: 0x00000E56
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00002C5E File Offset: 0x00000E5E
		public int GoldBonus
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00002C67 File Offset: 0x00000E67
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00002C6F File Offset: 0x00000E6F
		public int CashBonus
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00002C78 File Offset: 0x00000E78
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00002C80 File Offset: 0x00000E80
		public string Password
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

		// Token: 0x0600016F RID: 367 RVA: 0x00002C89 File Offset: 0x00000E89
		public ChannelModel(int int_7)
		{
			this.ServerId = int_7;
		}

		// Token: 0x040000CC RID: 204
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040000CD RID: 205
		[CompilerGenerated]
		private ChannelType channelType_0;

		// Token: 0x040000CE RID: 206
		[CompilerGenerated]
		private int int_1;

		// Token: 0x040000CF RID: 207
		[CompilerGenerated]
		private int int_2;

		// Token: 0x040000D0 RID: 208
		[CompilerGenerated]
		private int int_3;

		// Token: 0x040000D1 RID: 209
		[CompilerGenerated]
		private int int_4;

		// Token: 0x040000D2 RID: 210
		[CompilerGenerated]
		private int int_5;

		// Token: 0x040000D3 RID: 211
		[CompilerGenerated]
		private int int_6;

		// Token: 0x040000D4 RID: 212
		[CompilerGenerated]
		private string string_0;
	}
}
