using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000840 RID: 2112
	[ComVisible(true)]
	public interface IChannelSender : IChannel
	{
		// Token: 0x06005A10 RID: 23056
		[SecurityCritical]
		IMessageSink CreateMessageSink(string url, object remoteChannelData, out string objectURI);
	}
}
