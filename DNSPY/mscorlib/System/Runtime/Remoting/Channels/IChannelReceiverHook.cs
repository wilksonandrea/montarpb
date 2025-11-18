using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000842 RID: 2114
	[ComVisible(true)]
	public interface IChannelReceiverHook
	{
		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06005A15 RID: 23061
		string ChannelScheme
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06005A16 RID: 23062
		bool WantsToListen
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06005A17 RID: 23063
		IServerChannelSink ChannelSinkChain
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x06005A18 RID: 23064
		[SecurityCritical]
		void AddHookChannelUri(string channelUri);
	}
}
