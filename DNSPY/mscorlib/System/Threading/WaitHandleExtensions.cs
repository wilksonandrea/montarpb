using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x02000534 RID: 1332
	[__DynamicallyInvokable]
	public static class WaitHandleExtensions
	{
		// Token: 0x06003EA9 RID: 16041 RVA: 0x000E9040 File Offset: 0x000E7240
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static SafeWaitHandle GetSafeWaitHandle(this WaitHandle waitHandle)
		{
			if (waitHandle == null)
			{
				throw new ArgumentNullException("waitHandle");
			}
			return waitHandle.SafeWaitHandle;
		}

		// Token: 0x06003EAA RID: 16042 RVA: 0x000E9056 File Offset: 0x000E7256
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static void SetSafeWaitHandle(this WaitHandle waitHandle, SafeWaitHandle value)
		{
			if (waitHandle == null)
			{
				throw new ArgumentNullException("waitHandle");
			}
			waitHandle.SafeWaitHandle = value;
		}
	}
}
