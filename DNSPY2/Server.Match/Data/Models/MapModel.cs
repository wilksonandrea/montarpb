using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Server.Match.Data.Models
{
	// Token: 0x02000039 RID: 57
	public class MapModel
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00002634 File Offset: 0x00000834
		// (set) Token: 0x0600011E RID: 286 RVA: 0x0000263C File Offset: 0x0000083C
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00002645 File Offset: 0x00000845
		// (set) Token: 0x06000120 RID: 288 RVA: 0x0000264D File Offset: 0x0000084D
		public List<ObjectModel> Objects
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

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00002656 File Offset: 0x00000856
		// (set) Token: 0x06000122 RID: 290 RVA: 0x0000265E File Offset: 0x0000085E
		public List<BombPosition> Bombs
		{
			[CompilerGenerated]
			get
			{
				return this.list_1;
			}
			[CompilerGenerated]
			set
			{
				this.list_1 = value;
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		public BombPosition GetBomb(int BombId)
		{
			BombPosition bombPosition;
			try
			{
				bombPosition = this.Bombs[BombId];
			}
			catch
			{
				bombPosition = null;
			}
			return bombPosition;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000020A2 File Offset: 0x000002A2
		public MapModel()
		{
		}

		// Token: 0x0400003A RID: 58
		[CompilerGenerated]
		private int int_0;

		// Token: 0x0400003B RID: 59
		[CompilerGenerated]
		private List<ObjectModel> list_0;

		// Token: 0x0400003C RID: 60
		[CompilerGenerated]
		private List<BombPosition> list_1;
	}
}
