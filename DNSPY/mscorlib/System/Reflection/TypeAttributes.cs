using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000626 RID: 1574
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum TypeAttributes
	{
		// Token: 0x04001E29 RID: 7721
		[__DynamicallyInvokable]
		VisibilityMask = 7,
		// Token: 0x04001E2A RID: 7722
		[__DynamicallyInvokable]
		NotPublic = 0,
		// Token: 0x04001E2B RID: 7723
		[__DynamicallyInvokable]
		Public = 1,
		// Token: 0x04001E2C RID: 7724
		[__DynamicallyInvokable]
		NestedPublic = 2,
		// Token: 0x04001E2D RID: 7725
		[__DynamicallyInvokable]
		NestedPrivate = 3,
		// Token: 0x04001E2E RID: 7726
		[__DynamicallyInvokable]
		NestedFamily = 4,
		// Token: 0x04001E2F RID: 7727
		[__DynamicallyInvokable]
		NestedAssembly = 5,
		// Token: 0x04001E30 RID: 7728
		[__DynamicallyInvokable]
		NestedFamANDAssem = 6,
		// Token: 0x04001E31 RID: 7729
		[__DynamicallyInvokable]
		NestedFamORAssem = 7,
		// Token: 0x04001E32 RID: 7730
		[__DynamicallyInvokable]
		LayoutMask = 24,
		// Token: 0x04001E33 RID: 7731
		[__DynamicallyInvokable]
		AutoLayout = 0,
		// Token: 0x04001E34 RID: 7732
		[__DynamicallyInvokable]
		SequentialLayout = 8,
		// Token: 0x04001E35 RID: 7733
		[__DynamicallyInvokable]
		ExplicitLayout = 16,
		// Token: 0x04001E36 RID: 7734
		[__DynamicallyInvokable]
		ClassSemanticsMask = 32,
		// Token: 0x04001E37 RID: 7735
		[__DynamicallyInvokable]
		Class = 0,
		// Token: 0x04001E38 RID: 7736
		[__DynamicallyInvokable]
		Interface = 32,
		// Token: 0x04001E39 RID: 7737
		[__DynamicallyInvokable]
		Abstract = 128,
		// Token: 0x04001E3A RID: 7738
		[__DynamicallyInvokable]
		Sealed = 256,
		// Token: 0x04001E3B RID: 7739
		[__DynamicallyInvokable]
		SpecialName = 1024,
		// Token: 0x04001E3C RID: 7740
		[__DynamicallyInvokable]
		Import = 4096,
		// Token: 0x04001E3D RID: 7741
		[__DynamicallyInvokable]
		Serializable = 8192,
		// Token: 0x04001E3E RID: 7742
		[ComVisible(false)]
		[__DynamicallyInvokable]
		WindowsRuntime = 16384,
		// Token: 0x04001E3F RID: 7743
		[__DynamicallyInvokable]
		StringFormatMask = 196608,
		// Token: 0x04001E40 RID: 7744
		[__DynamicallyInvokable]
		AnsiClass = 0,
		// Token: 0x04001E41 RID: 7745
		[__DynamicallyInvokable]
		UnicodeClass = 65536,
		// Token: 0x04001E42 RID: 7746
		[__DynamicallyInvokable]
		AutoClass = 131072,
		// Token: 0x04001E43 RID: 7747
		[__DynamicallyInvokable]
		CustomFormatClass = 196608,
		// Token: 0x04001E44 RID: 7748
		[__DynamicallyInvokable]
		CustomFormatMask = 12582912,
		// Token: 0x04001E45 RID: 7749
		[__DynamicallyInvokable]
		BeforeFieldInit = 1048576,
		// Token: 0x04001E46 RID: 7750
		ReservedMask = 264192,
		// Token: 0x04001E47 RID: 7751
		[__DynamicallyInvokable]
		RTSpecialName = 2048,
		// Token: 0x04001E48 RID: 7752
		[__DynamicallyInvokable]
		HasSecurity = 262144
	}
}
