using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x020004EA RID: 1258
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public sealed class AutoResetEvent : EventWaitHandle
	{
		// Token: 0x06003B8E RID: 15246 RVA: 0x000E2336 File Offset: 0x000E0536
		[__DynamicallyInvokable]
		public AutoResetEvent(bool initialState)
			: base(initialState, EventResetMode.AutoReset)
		{
		}
	}
}
