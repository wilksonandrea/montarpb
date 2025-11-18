using System;
using System.Security;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000969 RID: 2409
	[ComVisible(false)]
	[__DynamicallyInvokable]
	public interface ICustomQueryInterface
	{
		// Token: 0x0600622E RID: 25134
		[SecurityCritical]
		CustomQueryInterfaceResult GetInterface([In] ref Guid iid, out IntPtr ppv);
	}
}
