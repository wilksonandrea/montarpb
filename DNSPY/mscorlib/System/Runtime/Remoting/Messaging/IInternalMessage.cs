using System;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000858 RID: 2136
	internal interface IInternalMessage
	{
		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06005A7C RID: 23164
		// (set) Token: 0x06005A7D RID: 23165
		ServerIdentity ServerIdentityObject
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06005A7E RID: 23166
		// (set) Token: 0x06005A7F RID: 23167
		Identity IdentityObject
		{
			[SecurityCritical]
			get;
			[SecurityCritical]
			set;
		}

		// Token: 0x06005A80 RID: 23168
		[SecurityCritical]
		void SetURI(string uri);

		// Token: 0x06005A81 RID: 23169
		[SecurityCritical]
		void SetCallContext(LogicalCallContext callContext);

		// Token: 0x06005A82 RID: 23170
		[SecurityCritical]
		bool HasProperties();
	}
}
