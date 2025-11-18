using System;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000856 RID: 2134
	public interface ISecurableChannel
	{
		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06005A6C RID: 23148
		// (set) Token: 0x06005A6D RID: 23149
		bool IsSecured
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}
	}
}
