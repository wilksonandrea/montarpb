using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000849 RID: 2121
	[ComVisible(true)]
	public interface IServerChannelSink : IChannelSinkBase
	{
		// Token: 0x06005A25 RID: 23077
		[SecurityCritical]
		ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream);

		// Token: 0x06005A26 RID: 23078
		[SecurityCritical]
		void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream stream);

		// Token: 0x06005A27 RID: 23079
		[SecurityCritical]
		Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers);

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06005A28 RID: 23080
		IServerChannelSink NextChannelSink
		{
			[SecurityCritical]
			get;
		}
	}
}
