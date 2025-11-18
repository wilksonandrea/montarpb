using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x02000324 RID: 804
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public interface IIdentity
	{
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060028A0 RID: 10400
		[__DynamicallyInvokable]
		string Name
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060028A1 RID: 10401
		[__DynamicallyInvokable]
		string AuthenticationType
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060028A2 RID: 10402
		[__DynamicallyInvokable]
		bool IsAuthenticated
		{
			[__DynamicallyInvokable]
			get;
		}
	}
}
