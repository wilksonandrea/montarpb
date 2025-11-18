using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200083E RID: 2110
	internal class DispatchChannelSink : IServerChannelSink, IChannelSinkBase
	{
		// Token: 0x06005A07 RID: 23047 RVA: 0x0013D329 File Offset: 0x0013B529
		internal DispatchChannelSink()
		{
		}

		// Token: 0x06005A08 RID: 23048 RVA: 0x0013D331 File Offset: 0x0013B531
		[SecurityCritical]
		public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			if (requestMsg == null)
			{
				throw new ArgumentNullException("requestMsg", Environment.GetResourceString("Remoting_Channel_DispatchSinkMessageMissing"));
			}
			if (requestStream != null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_DispatchSinkWantsNullRequestStream"));
			}
			responseHeaders = null;
			responseStream = null;
			return ChannelServices.DispatchMessage(sinkStack, requestMsg, out responseMsg);
		}

		// Token: 0x06005A09 RID: 23049 RVA: 0x0013D370 File Offset: 0x0013B570
		[SecurityCritical]
		public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers, Stream stream)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005A0A RID: 23050 RVA: 0x0013D377 File Offset: 0x0013B577
		[SecurityCritical]
		public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06005A0B RID: 23051 RVA: 0x0013D37E File Offset: 0x0013B57E
		public IServerChannelSink NextChannelSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06005A0C RID: 23052 RVA: 0x0013D381 File Offset: 0x0013B581
		public IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}
	}
}
