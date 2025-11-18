using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000847 RID: 2119
	[ComVisible(true)]
	public interface IClientChannelSink : IChannelSinkBase
	{
		// Token: 0x06005A20 RID: 23072
		[SecurityCritical]
		void ProcessMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream, out ITransportHeaders responseHeaders, out Stream responseStream);

		// Token: 0x06005A21 RID: 23073
		[SecurityCritical]
		void AsyncProcessRequest(IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream stream);

		// Token: 0x06005A22 RID: 23074
		[SecurityCritical]
		void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream);

		// Token: 0x06005A23 RID: 23075
		[SecurityCritical]
		Stream GetRequestStream(IMessage msg, ITransportHeaders headers);

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06005A24 RID: 23076
		IClientChannelSink NextChannelSink
		{
			[SecurityCritical]
			get;
		}
	}
}
