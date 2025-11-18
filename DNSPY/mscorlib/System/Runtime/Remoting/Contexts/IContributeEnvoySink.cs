using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000816 RID: 2070
	[ComVisible(true)]
	public interface IContributeEnvoySink
	{
		// Token: 0x060058E3 RID: 22755
		[SecurityCritical]
		IMessageSink GetEnvoySink(MarshalByRefObject obj, IMessageSink nextSink);
	}
}
