using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Colorful
{
	// Token: 0x020000F6 RID: 246
	public sealed class ColorMappingException : Exception
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x000077AD File Offset: 0x000059AD
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x000077B5 File Offset: 0x000059B5
		public int ErrorCode
		{
			[CompilerGenerated]
			get
			{
				return this.int_0;
			}
			[CompilerGenerated]
			private set
			{
				this.int_0 = value;
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x000077BE File Offset: 0x000059BE
		public ColorMappingException(int int_1)
			: base(string.Format("Color conversion failed with system error code {0}!", int_1))
		{
			this.ErrorCode = int_1;
		}

		// Token: 0x040006EA RID: 1770
		[CompilerGenerated]
		private int int_0;
	}
}
