using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000934 RID: 2356
	[Flags]
	[__DynamicallyInvokable]
	public enum DllImportSearchPath
	{
		// Token: 0x04002B12 RID: 11026
		[__DynamicallyInvokable]
		UseDllDirectoryForDependencies = 256,
		// Token: 0x04002B13 RID: 11027
		[__DynamicallyInvokable]
		ApplicationDirectory = 512,
		// Token: 0x04002B14 RID: 11028
		[__DynamicallyInvokable]
		UserDirectories = 1024,
		// Token: 0x04002B15 RID: 11029
		[__DynamicallyInvokable]
		System32 = 2048,
		// Token: 0x04002B16 RID: 11030
		[__DynamicallyInvokable]
		SafeDirectories = 4096,
		// Token: 0x04002B17 RID: 11031
		[__DynamicallyInvokable]
		AssemblyDirectory = 2,
		// Token: 0x04002B18 RID: 11032
		[__DynamicallyInvokable]
		LegacyBehavior = 0
	}
}
