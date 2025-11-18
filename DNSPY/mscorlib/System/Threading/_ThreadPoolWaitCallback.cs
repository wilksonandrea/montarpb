using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000521 RID: 1313
	internal static class _ThreadPoolWaitCallback
	{
		// Token: 0x06003DE6 RID: 15846 RVA: 0x000E72C9 File Offset: 0x000E54C9
		[SecurityCritical]
		internal static bool PerformWaitCallback()
		{
			return ThreadPoolWorkQueue.Dispatch();
		}
	}
}
