using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000949 RID: 2377
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum GCHandleType
	{
		// Token: 0x04002B49 RID: 11081
		[__DynamicallyInvokable]
		Weak,
		// Token: 0x04002B4A RID: 11082
		[__DynamicallyInvokable]
		WeakTrackResurrection,
		// Token: 0x04002B4B RID: 11083
		[__DynamicallyInvokable]
		Normal,
		// Token: 0x04002B4C RID: 11084
		[__DynamicallyInvokable]
		Pinned
	}
}
