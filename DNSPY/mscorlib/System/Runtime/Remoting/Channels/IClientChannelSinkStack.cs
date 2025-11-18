using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200082F RID: 2095
	[ComVisible(true)]
	public interface IClientChannelSinkStack : IClientResponseChannelSinkStack
	{
		// Token: 0x060059A6 RID: 22950
		[SecurityCritical]
		void Push(IClientChannelSink sink, object state);

		// Token: 0x060059A7 RID: 22951
		[SecurityCritical]
		object Pop(IClientChannelSink sink);
	}
}
