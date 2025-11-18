using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000818 RID: 2072
	[ComVisible(true)]
	public interface IContributeServerContextSink
	{
		// Token: 0x060058E5 RID: 22757
		[SecurityCritical]
		IMessageSink GetServerContextSink(IMessageSink nextSink);
	}
}
