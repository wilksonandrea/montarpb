using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200085B RID: 2139
	[ComVisible(true)]
	public interface IMessageSink
	{
		// Token: 0x06005A85 RID: 23173
		[SecurityCritical]
		IMessage SyncProcessMessage(IMessage msg);

		// Token: 0x06005A86 RID: 23174
		[SecurityCritical]
		IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink);

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06005A87 RID: 23175
		IMessageSink NextSink
		{
			[SecurityCritical]
			get;
		}
	}
}
