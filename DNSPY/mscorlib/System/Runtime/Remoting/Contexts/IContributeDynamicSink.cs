using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000815 RID: 2069
	[ComVisible(true)]
	public interface IContributeDynamicSink
	{
		// Token: 0x060058E2 RID: 22754
		[SecurityCritical]
		IDynamicMessageSink GetDynamicSink();
	}
}
