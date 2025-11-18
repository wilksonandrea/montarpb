using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000841 RID: 2113
	[ComVisible(true)]
	public interface IChannelReceiver : IChannel
	{
		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06005A11 RID: 23057
		object ChannelData
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x06005A12 RID: 23058
		[SecurityCritical]
		string[] GetUrlsForUri(string objectURI);

		// Token: 0x06005A13 RID: 23059
		[SecurityCritical]
		void StartListening(object data);

		// Token: 0x06005A14 RID: 23060
		[SecurityCritical]
		void StopListening(object data);
	}
}
