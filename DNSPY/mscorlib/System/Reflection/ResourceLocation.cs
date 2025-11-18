using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005F6 RID: 1526
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum ResourceLocation
	{
		// Token: 0x04001CE4 RID: 7396
		[__DynamicallyInvokable]
		Embedded = 1,
		// Token: 0x04001CE5 RID: 7397
		[__DynamicallyInvokable]
		ContainedInAnotherAssembly = 2,
		// Token: 0x04001CE6 RID: 7398
		[__DynamicallyInvokable]
		ContainedInManifestFile = 4
	}
}
