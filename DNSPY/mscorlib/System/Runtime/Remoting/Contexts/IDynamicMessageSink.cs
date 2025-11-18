using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200081A RID: 2074
	[ComVisible(true)]
	public interface IDynamicMessageSink
	{
		// Token: 0x060058E7 RID: 22759
		[SecurityCritical]
		void ProcessMessageStart(IMessage reqMsg, bool bCliSide, bool bAsync);

		// Token: 0x060058E8 RID: 22760
		[SecurityCritical]
		void ProcessMessageFinish(IMessage replyMsg, bool bCliSide, bool bAsync);
	}
}
