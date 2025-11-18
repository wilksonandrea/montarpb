using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000609 RID: 1545
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum MethodImplAttributes
	{
		// Token: 0x04001D97 RID: 7575
		[__DynamicallyInvokable]
		CodeTypeMask = 3,
		// Token: 0x04001D98 RID: 7576
		[__DynamicallyInvokable]
		IL = 0,
		// Token: 0x04001D99 RID: 7577
		[__DynamicallyInvokable]
		Native,
		// Token: 0x04001D9A RID: 7578
		[__DynamicallyInvokable]
		OPTIL,
		// Token: 0x04001D9B RID: 7579
		[__DynamicallyInvokable]
		Runtime,
		// Token: 0x04001D9C RID: 7580
		[__DynamicallyInvokable]
		ManagedMask,
		// Token: 0x04001D9D RID: 7581
		[__DynamicallyInvokable]
		Unmanaged = 4,
		// Token: 0x04001D9E RID: 7582
		[__DynamicallyInvokable]
		Managed = 0,
		// Token: 0x04001D9F RID: 7583
		[__DynamicallyInvokable]
		ForwardRef = 16,
		// Token: 0x04001DA0 RID: 7584
		[__DynamicallyInvokable]
		PreserveSig = 128,
		// Token: 0x04001DA1 RID: 7585
		[__DynamicallyInvokable]
		InternalCall = 4096,
		// Token: 0x04001DA2 RID: 7586
		[__DynamicallyInvokable]
		Synchronized = 32,
		// Token: 0x04001DA3 RID: 7587
		[__DynamicallyInvokable]
		NoInlining = 8,
		// Token: 0x04001DA4 RID: 7588
		[ComVisible(false)]
		[__DynamicallyInvokable]
		AggressiveInlining = 256,
		// Token: 0x04001DA5 RID: 7589
		[__DynamicallyInvokable]
		NoOptimization = 64,
		// Token: 0x04001DA6 RID: 7590
		SecurityMitigations = 1024,
		// Token: 0x04001DA7 RID: 7591
		MaxMethodImplVal = 65535
	}
}
