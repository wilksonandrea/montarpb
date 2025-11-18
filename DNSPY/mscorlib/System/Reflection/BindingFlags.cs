using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005D2 RID: 1490
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum BindingFlags
	{
		// Token: 0x04001C38 RID: 7224
		Default = 0,
		// Token: 0x04001C39 RID: 7225
		[__DynamicallyInvokable]
		IgnoreCase = 1,
		// Token: 0x04001C3A RID: 7226
		[__DynamicallyInvokable]
		DeclaredOnly = 2,
		// Token: 0x04001C3B RID: 7227
		[__DynamicallyInvokable]
		Instance = 4,
		// Token: 0x04001C3C RID: 7228
		[__DynamicallyInvokable]
		Static = 8,
		// Token: 0x04001C3D RID: 7229
		[__DynamicallyInvokable]
		Public = 16,
		// Token: 0x04001C3E RID: 7230
		[__DynamicallyInvokable]
		NonPublic = 32,
		// Token: 0x04001C3F RID: 7231
		[__DynamicallyInvokable]
		FlattenHierarchy = 64,
		// Token: 0x04001C40 RID: 7232
		InvokeMethod = 256,
		// Token: 0x04001C41 RID: 7233
		CreateInstance = 512,
		// Token: 0x04001C42 RID: 7234
		GetField = 1024,
		// Token: 0x04001C43 RID: 7235
		SetField = 2048,
		// Token: 0x04001C44 RID: 7236
		GetProperty = 4096,
		// Token: 0x04001C45 RID: 7237
		SetProperty = 8192,
		// Token: 0x04001C46 RID: 7238
		PutDispProperty = 16384,
		// Token: 0x04001C47 RID: 7239
		PutRefDispProperty = 32768,
		// Token: 0x04001C48 RID: 7240
		[__DynamicallyInvokable]
		ExactBinding = 65536,
		// Token: 0x04001C49 RID: 7241
		SuppressChangeType = 131072,
		// Token: 0x04001C4A RID: 7242
		[__DynamicallyInvokable]
		OptionalParamBinding = 262144,
		// Token: 0x04001C4B RID: 7243
		IgnoreReturn = 16777216
	}
}
