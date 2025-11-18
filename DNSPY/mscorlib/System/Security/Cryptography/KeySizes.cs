using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000242 RID: 578
	[ComVisible(true)]
	public sealed class KeySizes
	{
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x0007260F File Offset: 0x0007080F
		public int MinSize
		{
			get
			{
				return this.m_minSize;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x00072617 File Offset: 0x00070817
		public int MaxSize
		{
			get
			{
				return this.m_maxSize;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x0007261F File Offset: 0x0007081F
		public int SkipSize
		{
			get
			{
				return this.m_skipSize;
			}
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x00072627 File Offset: 0x00070827
		public KeySizes(int minSize, int maxSize, int skipSize)
		{
			this.m_minSize = minSize;
			this.m_maxSize = maxSize;
			this.m_skipSize = skipSize;
		}

		// Token: 0x04000BDF RID: 3039
		private int m_minSize;

		// Token: 0x04000BE0 RID: 3040
		private int m_maxSize;

		// Token: 0x04000BE1 RID: 3041
		private int m_skipSize;
	}
}
