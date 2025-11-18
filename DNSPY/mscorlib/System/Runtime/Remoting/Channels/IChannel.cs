using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200083F RID: 2111
	[ComVisible(true)]
	public interface IChannel
	{
		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06005A0D RID: 23053
		int ChannelPriority
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06005A0E RID: 23054
		string ChannelName
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x06005A0F RID: 23055
		[SecurityCritical]
		string Parse(string url, out string objectURI);
	}
}
