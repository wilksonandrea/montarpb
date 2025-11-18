using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200085D RID: 2141
	[ComVisible(true)]
	public interface IMethodCallMessage : IMethodMessage, IMessage
	{
		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06005A93 RID: 23187
		int InArgCount
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x06005A94 RID: 23188
		[SecurityCritical]
		string GetInArgName(int index);

		// Token: 0x06005A95 RID: 23189
		[SecurityCritical]
		object GetInArg(int argNum);

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06005A96 RID: 23190
		object[] InArgs
		{
			[SecurityCritical]
			get;
		}
	}
}
