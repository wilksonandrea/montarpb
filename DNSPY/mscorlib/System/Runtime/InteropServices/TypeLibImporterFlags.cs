using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200096D RID: 2413
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibImporterFlags
	{
		// Token: 0x04002BA5 RID: 11173
		None = 0,
		// Token: 0x04002BA6 RID: 11174
		PrimaryInteropAssembly = 1,
		// Token: 0x04002BA7 RID: 11175
		UnsafeInterfaces = 2,
		// Token: 0x04002BA8 RID: 11176
		SafeArrayAsSystemArray = 4,
		// Token: 0x04002BA9 RID: 11177
		TransformDispRetVals = 8,
		// Token: 0x04002BAA RID: 11178
		PreventClassMembers = 16,
		// Token: 0x04002BAB RID: 11179
		SerializableValueClasses = 32,
		// Token: 0x04002BAC RID: 11180
		ImportAsX86 = 256,
		// Token: 0x04002BAD RID: 11181
		ImportAsX64 = 512,
		// Token: 0x04002BAE RID: 11182
		ImportAsItanium = 1024,
		// Token: 0x04002BAF RID: 11183
		ImportAsAgnostic = 2048,
		// Token: 0x04002BB0 RID: 11184
		ReflectionOnlyLoading = 4096,
		// Token: 0x04002BB1 RID: 11185
		NoDefineVersionResource = 8192,
		// Token: 0x04002BB2 RID: 11186
		ImportAsArm = 16384
	}
}
