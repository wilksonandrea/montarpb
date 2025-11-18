using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000832 RID: 2098
	[ComVisible(true)]
	public interface IServerChannelSinkStack : IServerResponseChannelSinkStack
	{
		// Token: 0x060059B2 RID: 22962
		[SecurityCritical]
		void Push(IServerChannelSink sink, object state);

		// Token: 0x060059B3 RID: 22963
		[SecurityCritical]
		object Pop(IServerChannelSink sink);

		// Token: 0x060059B4 RID: 22964
		[SecurityCritical]
		void Store(IServerChannelSink sink, object state);

		// Token: 0x060059B5 RID: 22965
		[SecurityCritical]
		void StoreAndDispatch(IServerChannelSink sink, object state);

		// Token: 0x060059B6 RID: 22966
		[SecurityCritical]
		void ServerCallback(IAsyncResult ar);
	}
}
