using System;
using System.Runtime.CompilerServices;

namespace Server.Match.Data.Models
{
	// Token: 0x02000034 RID: 52
	public class AssistServerData
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000024CC File Offset: 0x000006CC
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x000024D4 File Offset: 0x000006D4
		public int RoomId
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

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000024DD File Offset: 0x000006DD
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x000024E5 File Offset: 0x000006E5
		public int Killer
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000024EE File Offset: 0x000006EE
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x000024F6 File Offset: 0x000006F6
		public int Victim
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000024FF File Offset: 0x000006FF
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00002507 File Offset: 0x00000707
		public int Damage
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00002510 File Offset: 0x00000710
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00002518 File Offset: 0x00000718
		public bool IsAssist
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00002521 File Offset: 0x00000721
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00002529 File Offset: 0x00000729
		public bool IsKiller
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00002532 File Offset: 0x00000732
		// (set) Token: 0x060000FF RID: 255 RVA: 0x0000253A File Offset: 0x0000073A
		public bool VictimDead
		{
			[CompilerGenerated]
			get
			{
				return this.bool_2;
			}
			[CompilerGenerated]
			set
			{
				this.bool_2 = value;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00002543 File Offset: 0x00000743
		public AssistServerData()
		{
			this.Killer = -1;
			this.Victim = -1;
		}

		// Token: 0x04000027 RID: 39
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000028 RID: 40
		[CompilerGenerated]
		private int int_1;

		// Token: 0x04000029 RID: 41
		[CompilerGenerated]
		private int int_2;

		// Token: 0x0400002A RID: 42
		[CompilerGenerated]
		private int int_3;

		// Token: 0x0400002B RID: 43
		[CompilerGenerated]
		private bool bool_0;

		// Token: 0x0400002C RID: 44
		[CompilerGenerated]
		private bool bool_1;

		// Token: 0x0400002D RID: 45
		[CompilerGenerated]
		private bool bool_2;
	}
}
