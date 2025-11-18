using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000843 RID: 2115
	[ComVisible(true)]
	public interface IClientChannelSinkProvider
	{
		// Token: 0x06005A19 RID: 23065
		[SecurityCritical]
		IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData);

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06005A1A RID: 23066
		// (set) Token: 0x06005A1B RID: 23067
		IClientChannelSinkProvider Next
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}
	}
}
