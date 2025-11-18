using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000844 RID: 2116
	[ComVisible(true)]
	public interface IServerChannelSinkProvider
	{
		// Token: 0x06005A1C RID: 23068
		[SecurityCritical]
		void GetChannelData(IChannelDataStore channelData);

		// Token: 0x06005A1D RID: 23069
		[SecurityCritical]
		IServerChannelSink CreateSink(IChannelReceiver channel);

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06005A1E RID: 23070
		// (set) Token: 0x06005A1F RID: 23071
		IServerChannelSinkProvider Next
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}
	}
}
