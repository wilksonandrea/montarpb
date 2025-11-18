using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005D3 RID: 1491
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum CallingConventions
	{
		// Token: 0x04001C4D RID: 7245
		[__DynamicallyInvokable]
		Standard = 1,
		// Token: 0x04001C4E RID: 7246
		[__DynamicallyInvokable]
		VarArgs = 2,
		// Token: 0x04001C4F RID: 7247
		[__DynamicallyInvokable]
		Any = 3,
		// Token: 0x04001C50 RID: 7248
		[__DynamicallyInvokable]
		HasThis = 32,
		// Token: 0x04001C51 RID: 7249
		[__DynamicallyInvokable]
		ExplicitThis = 64
	}
}
