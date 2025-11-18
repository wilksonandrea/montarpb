using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A4C RID: 2636
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum INVOKEKIND
	{
		// Token: 0x04002DDE RID: 11742
		[__DynamicallyInvokable]
		INVOKE_FUNC = 1,
		// Token: 0x04002DDF RID: 11743
		[__DynamicallyInvokable]
		INVOKE_PROPERTYGET = 2,
		// Token: 0x04002DE0 RID: 11744
		[__DynamicallyInvokable]
		INVOKE_PROPERTYPUT = 4,
		// Token: 0x04002DE1 RID: 11745
		[__DynamicallyInvokable]
		INVOKE_PROPERTYPUTREF = 8
	}
}
