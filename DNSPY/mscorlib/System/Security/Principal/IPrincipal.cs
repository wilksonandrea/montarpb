using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x02000325 RID: 805
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IPrincipal
	{
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060028A3 RID: 10403
		[__DynamicallyInvokable]
		IIdentity Identity
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x060028A4 RID: 10404
		[__DynamicallyInvokable]
		bool IsInRole(string role);
	}
}
