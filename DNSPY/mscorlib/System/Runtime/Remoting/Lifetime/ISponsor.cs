using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000821 RID: 2081
	[ComVisible(true)]
	public interface ISponsor
	{
		// Token: 0x0600592F RID: 22831
		[SecurityCritical]
		TimeSpan Renewal(ILease lease);
	}
}
