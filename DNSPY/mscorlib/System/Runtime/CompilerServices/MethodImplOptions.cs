using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008BF RID: 2239
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum MethodImplOptions
	{
		// Token: 0x04002A23 RID: 10787
		Unmanaged = 4,
		// Token: 0x04002A24 RID: 10788
		ForwardRef = 16,
		// Token: 0x04002A25 RID: 10789
		[__DynamicallyInvokable]
		PreserveSig = 128,
		// Token: 0x04002A26 RID: 10790
		InternalCall = 4096,
		// Token: 0x04002A27 RID: 10791
		Synchronized = 32,
		// Token: 0x04002A28 RID: 10792
		[__DynamicallyInvokable]
		NoInlining = 8,
		// Token: 0x04002A29 RID: 10793
		[ComVisible(false)]
		[__DynamicallyInvokable]
		AggressiveInlining = 256,
		// Token: 0x04002A2A RID: 10794
		[__DynamicallyInvokable]
		NoOptimization = 64,
		// Token: 0x04002A2B RID: 10795
		SecurityMitigations = 1024
	}
}
