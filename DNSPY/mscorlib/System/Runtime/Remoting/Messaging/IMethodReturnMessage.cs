using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200085E RID: 2142
	[ComVisible(true)]
	public interface IMethodReturnMessage : IMethodMessage, IMessage
	{
		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06005A97 RID: 23191
		int OutArgCount
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x06005A98 RID: 23192
		[SecurityCritical]
		string GetOutArgName(int index);

		// Token: 0x06005A99 RID: 23193
		[SecurityCritical]
		object GetOutArg(int argNum);

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06005A9A RID: 23194
		object[] OutArgs
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06005A9B RID: 23195
		Exception Exception
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06005A9C RID: 23196
		object ReturnValue
		{
			[SecurityCritical]
			get;
		}
	}
}
