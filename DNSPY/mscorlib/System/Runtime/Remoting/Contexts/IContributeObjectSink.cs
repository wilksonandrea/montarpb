using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000817 RID: 2071
	[ComVisible(true)]
	public interface IContributeObjectSink
	{
		// Token: 0x060058E4 RID: 22756
		[SecurityCritical]
		IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink);
	}
}
