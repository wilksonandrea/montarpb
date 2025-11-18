using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000522 RID: 1314
	internal interface IThreadPoolWorkItem
	{
		// Token: 0x06003DE7 RID: 15847
		[SecurityCritical]
		void ExecuteWorkItem();

		// Token: 0x06003DE8 RID: 15848
		[SecurityCritical]
		void MarkAborted(ThreadAbortException tae);
	}
}
