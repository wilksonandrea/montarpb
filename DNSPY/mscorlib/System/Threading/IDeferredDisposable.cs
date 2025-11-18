using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x0200050A RID: 1290
	internal interface IDeferredDisposable
	{
		// Token: 0x06003CC6 RID: 15558
		[SecurityCritical]
		void OnFinalRelease(bool disposed);
	}
}
