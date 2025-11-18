using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005CB RID: 1483
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum AssemblyNameFlags
	{
		// Token: 0x04001C28 RID: 7208
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x04001C29 RID: 7209
		[__DynamicallyInvokable]
		PublicKey = 1,
		// Token: 0x04001C2A RID: 7210
		EnableJITcompileOptimizer = 16384,
		// Token: 0x04001C2B RID: 7211
		EnableJITcompileTracking = 32768,
		// Token: 0x04001C2C RID: 7212
		[__DynamicallyInvokable]
		Retargetable = 256
	}
}
