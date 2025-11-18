using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200085C RID: 2140
	[ComVisible(true)]
	public interface IMethodMessage : IMessage
	{
		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06005A88 RID: 23176
		string Uri
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06005A89 RID: 23177
		string MethodName
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06005A8A RID: 23178
		string TypeName
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06005A8B RID: 23179
		object MethodSignature
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06005A8C RID: 23180
		int ArgCount
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x06005A8D RID: 23181
		[SecurityCritical]
		string GetArgName(int index);

		// Token: 0x06005A8E RID: 23182
		[SecurityCritical]
		object GetArg(int argNum);

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06005A8F RID: 23183
		object[] Args
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06005A90 RID: 23184
		bool HasVarArgs
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06005A91 RID: 23185
		LogicalCallContext LogicalCallContext
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06005A92 RID: 23186
		MethodBase MethodBase
		{
			[SecurityCritical]
			get;
		}
	}
}
