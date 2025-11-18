using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000273 RID: 627
	[ComVisible(true)]
	public abstract class MaskGenerationMethod
	{
		// Token: 0x06002232 RID: 8754
		[ComVisible(true)]
		public abstract byte[] GenerateMask(byte[] rgbSeed, int cbReturn);

		// Token: 0x06002233 RID: 8755 RVA: 0x00078B2D File Offset: 0x00076D2D
		protected MaskGenerationMethod()
		{
		}
	}
}
