using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x02000500 RID: 1280
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public sealed class ManualResetEvent : EventWaitHandle
	{
		// Token: 0x06003C63 RID: 15459 RVA: 0x000E40CA File Offset: 0x000E22CA
		[__DynamicallyInvokable]
		public ManualResetEvent(bool initialState)
			: base(initialState, EventResetMode.ManualReset)
		{
		}
	}
}
