using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000814 RID: 2068
	[ComVisible(true)]
	public interface IContributeClientContextSink
	{
		// Token: 0x060058E1 RID: 22753
		[SecurityCritical]
		IMessageSink GetClientContextSink(IMessageSink nextSink);
	}
}
