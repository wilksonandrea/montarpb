using System;
using System.Runtime.CompilerServices;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x02000034 RID: 52
	public class Mission
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002A25 File Offset: 0x00000C25
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002A2D File Offset: 0x00000C2D
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

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002A36 File Offset: 0x00000C36
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00002A3E File Offset: 0x00000C3E
		public int RewardId
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00002A47 File Offset: 0x00000C47
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00002A4F File Offset: 0x00000C4F
		public int RewardCount
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00002A58 File Offset: 0x00000C58
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00002A60 File Offset: 0x00000C60
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00002A69 File Offset: 0x00000C69
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00002A71 File Offset: 0x00000C71
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00002A7A File Offset: 0x00000C7A
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00002A82 File Offset: 0x00000C82
		public byte[] ObjectivesData
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

		// Token: 0x060000B3 RID: 179 RVA: 0x00002409 File Offset: 0x00000609
		public Mission()
		{
		}

		// Token: 0x04000068 RID: 104
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000069 RID: 105
		[CompilerGenerated]
		private int int_1;

		// Token: 0x0400006A RID: 106
		[CompilerGenerated]
		private int int_2;

		// Token: 0x0400006B RID: 107
		[CompilerGenerated]
		private string string_0;

		// Token: 0x0400006C RID: 108
		[CompilerGenerated]
		private string string_1;

		// Token: 0x0400006D RID: 109
		[CompilerGenerated]
		private byte[] byte_0;
	}
}
