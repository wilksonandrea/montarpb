using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x0200051E RID: 1310
	[ComVisible(true)]
	public sealed class RegisteredWaitHandle : MarshalByRefObject
	{
		// Token: 0x06003DDA RID: 15834 RVA: 0x000E728C File Offset: 0x000E548C
		internal RegisteredWaitHandle()
		{
			this.internalRegisteredWait = new RegisteredWaitHandleSafe();
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x000E729F File Offset: 0x000E549F
		internal void SetHandle(IntPtr handle)
		{
			this.internalRegisteredWait.SetHandle(handle);
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x000E72AD File Offset: 0x000E54AD
		[SecurityCritical]
		internal void SetWaitObject(WaitHandle waitObject)
		{
			this.internalRegisteredWait.SetWaitObject(waitObject);
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x000E72BB File Offset: 0x000E54BB
		[SecuritySafeCritical]
		[ComVisible(true)]
		public bool Unregister(WaitHandle waitObject)
		{
			return this.internalRegisteredWait.Unregister(waitObject);
		}

		// Token: 0x04001A14 RID: 6676
		private RegisteredWaitHandleSafe internalRegisteredWait;
	}
}
