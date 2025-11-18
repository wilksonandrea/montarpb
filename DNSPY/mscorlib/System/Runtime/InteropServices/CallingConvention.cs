using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000944 RID: 2372
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum CallingConvention
	{
		// Token: 0x04002B3C RID: 11068
		[__DynamicallyInvokable]
		Winapi = 1,
		// Token: 0x04002B3D RID: 11069
		[__DynamicallyInvokable]
		Cdecl,
		// Token: 0x04002B3E RID: 11070
		[__DynamicallyInvokable]
		StdCall,
		// Token: 0x04002B3F RID: 11071
		[__DynamicallyInvokable]
		ThisCall,
		// Token: 0x04002B40 RID: 11072
		FastCall
	}
}
