using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000825 RID: 2085
	[ComVisible(true)]
	[Serializable]
	public enum LeaseState
	{
		// Token: 0x040028B9 RID: 10425
		Null,
		// Token: 0x040028BA RID: 10426
		Initial,
		// Token: 0x040028BB RID: 10427
		Active,
		// Token: 0x040028BC RID: 10428
		Renewing,
		// Token: 0x040028BD RID: 10429
		Expired
	}
}
