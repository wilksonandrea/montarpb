using System;
using System.Runtime.CompilerServices;

namespace Server.Match.Data.Models
{
	// Token: 0x02000036 RID: 54
	public class CharaModel
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000025AE File Offset: 0x000007AE
		// (set) Token: 0x0600010D RID: 269 RVA: 0x000025B6 File Offset: 0x000007B6
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000025BF File Offset: 0x000007BF
		// (set) Token: 0x0600010F RID: 271 RVA: 0x000025C7 File Offset: 0x000007C7
		public int HP
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

		// Token: 0x06000110 RID: 272 RVA: 0x000020A2 File Offset: 0x000002A2
		public CharaModel()
		{
		}

		// Token: 0x04000033 RID: 51
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000034 RID: 52
		[CompilerGenerated]
		private int int_1;
	}
}
