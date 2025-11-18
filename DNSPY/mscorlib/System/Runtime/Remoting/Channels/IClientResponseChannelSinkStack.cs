using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000830 RID: 2096
	[ComVisible(true)]
	public interface IClientResponseChannelSinkStack
	{
		// Token: 0x060059A8 RID: 22952
		[SecurityCritical]
		void AsyncProcessResponse(ITransportHeaders headers, Stream stream);

		// Token: 0x060059A9 RID: 22953
		[SecurityCritical]
		void DispatchReplyMessage(IMessage msg);

		// Token: 0x060059AA RID: 22954
		[SecurityCritical]
		void DispatchException(Exception e);
	}
}
