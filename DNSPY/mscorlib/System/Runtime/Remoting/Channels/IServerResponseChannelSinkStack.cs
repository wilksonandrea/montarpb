using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000833 RID: 2099
	[ComVisible(true)]
	public interface IServerResponseChannelSinkStack
	{
		// Token: 0x060059B7 RID: 22967
		[SecurityCritical]
		void AsyncProcessResponse(IMessage msg, ITransportHeaders headers, Stream stream);

		// Token: 0x060059B8 RID: 22968
		[SecurityCritical]
		Stream GetResponseStream(IMessage msg, ITransportHeaders headers);
	}
}
