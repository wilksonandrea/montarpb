using System;
using System.Runtime.CompilerServices;

namespace Server.Match.Data.Models
{
	// Token: 0x0200003B RID: 59
	public class ObjectInfo
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000278D File Offset: 0x0000098D
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00002795 File Offset: 0x00000995
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000279E File Offset: 0x0000099E
		// (set) Token: 0x06000149 RID: 329 RVA: 0x000027A6 File Offset: 0x000009A6
		public int Life
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

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000027AF File Offset: 0x000009AF
		// (set) Token: 0x0600014B RID: 331 RVA: 0x000027B7 File Offset: 0x000009B7
		public int DestroyState
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

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000027C0 File Offset: 0x000009C0
		// (set) Token: 0x0600014D RID: 333 RVA: 0x000027C8 File Offset: 0x000009C8
		public AnimModel Animation
		{
			[CompilerGenerated]
			get
			{
				return this.animModel_0;
			}
			[CompilerGenerated]
			set
			{
				this.animModel_0 = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000027D1 File Offset: 0x000009D1
		// (set) Token: 0x0600014F RID: 335 RVA: 0x000027D9 File Offset: 0x000009D9
		public DateTime UseDate
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000027E2 File Offset: 0x000009E2
		// (set) Token: 0x06000151 RID: 337 RVA: 0x000027EA File Offset: 0x000009EA
		public ObjectModel Model
		{
			[CompilerGenerated]
			get
			{
				return this.objectModel_0;
			}
			[CompilerGenerated]
			set
			{
				this.objectModel_0 = value;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000027F3 File Offset: 0x000009F3
		public ObjectInfo(int int_3)
		{
			this.Id = int_3;
			this.Life = 100;
		}

		// Token: 0x0400004D RID: 77
		[CompilerGenerated]
		private int int_0;

		// Token: 0x0400004E RID: 78
		[CompilerGenerated]
		private int int_1;

		// Token: 0x0400004F RID: 79
		[CompilerGenerated]
		private int int_2;

		// Token: 0x04000050 RID: 80
		[CompilerGenerated]
		private AnimModel animModel_0;

		// Token: 0x04000051 RID: 81
		[CompilerGenerated]
		private DateTime dateTime_0;

		// Token: 0x04000052 RID: 82
		[CompilerGenerated]
		private ObjectModel objectModel_0;
	}
}
