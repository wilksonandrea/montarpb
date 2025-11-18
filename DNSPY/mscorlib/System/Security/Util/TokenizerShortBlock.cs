using System;

namespace System.Security.Util
{
	// Token: 0x02000386 RID: 902
	internal sealed class TokenizerShortBlock
	{
		// Token: 0x06002CD1 RID: 11473 RVA: 0x000A8CCF File Offset: 0x000A6ECF
		public TokenizerShortBlock()
		{
		}

		// Token: 0x0400121E RID: 4638
		internal short[] m_block = new short[16];

		// Token: 0x0400121F RID: 4639
		internal TokenizerShortBlock m_next;
	}
}
