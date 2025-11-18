using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200084A RID: 2122
	[ComVisible(true)]
	public interface IChannelSinkBase
	{
		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06005A29 RID: 23081
		IDictionary Properties
		{
			[SecurityCritical]
			get;
		}
	}
}
