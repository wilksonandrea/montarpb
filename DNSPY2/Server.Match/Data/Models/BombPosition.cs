using System;
using System.Runtime.CompilerServices;
using Plugin.Core.SharpDX;

namespace Server.Match.Data.Models
{
	// Token: 0x02000035 RID: 53
	public class BombPosition
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00002559 File Offset: 0x00000759
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00002561 File Offset: 0x00000761
		public float X
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

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000256A File Offset: 0x0000076A
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00002572 File Offset: 0x00000772
		public float Y
		{
			[CompilerGenerated]
			get
			{
				return this.float_1;
			}
			[CompilerGenerated]
			set
			{
				this.float_1 = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000257B File Offset: 0x0000077B
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00002583 File Offset: 0x00000783
		public float Z
		{
			[CompilerGenerated]
			get
			{
				return this.float_2;
			}
			[CompilerGenerated]
			set
			{
				this.float_2 = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000258C File Offset: 0x0000078C
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00002594 File Offset: 0x00000794
		public Half3 Position
		{
			[CompilerGenerated]
			get
			{
				return this.half3_0;
			}
			[CompilerGenerated]
			set
			{
				this.half3_0 = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000259D File Offset: 0x0000079D
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000025A5 File Offset: 0x000007A5
		public bool EveryWhere
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

		// Token: 0x0600010B RID: 267 RVA: 0x000020A2 File Offset: 0x000002A2
		public BombPosition()
		{
		}

		// Token: 0x0400002E RID: 46
		[CompilerGenerated]
		private float float_0;

		// Token: 0x0400002F RID: 47
		[CompilerGenerated]
		private float float_1;

		// Token: 0x04000030 RID: 48
		[CompilerGenerated]
		private float float_2;

		// Token: 0x04000031 RID: 49
		[CompilerGenerated]
		private Half3 half3_0;

		// Token: 0x04000032 RID: 50
		[CompilerGenerated]
		private bool bool_0;
	}
}
