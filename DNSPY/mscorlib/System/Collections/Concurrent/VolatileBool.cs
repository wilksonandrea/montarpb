using System;

namespace System.Collections.Concurrent
{
	// Token: 0x020004B0 RID: 1200
	internal struct VolatileBool
	{
		// Token: 0x06003991 RID: 14737 RVA: 0x000DC5DF File Offset: 0x000DA7DF
		public VolatileBool(bool value)
		{
			this.m_value = value;
		}

		// Token: 0x0400192C RID: 6444
		public volatile bool m_value;
	}
}
