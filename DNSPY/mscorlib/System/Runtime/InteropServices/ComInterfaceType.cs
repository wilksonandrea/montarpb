using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000915 RID: 2325
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum ComInterfaceType
	{
		// Token: 0x04002A6E RID: 10862
		[__DynamicallyInvokable]
		InterfaceIsDual,
		// Token: 0x04002A6F RID: 10863
		[__DynamicallyInvokable]
		InterfaceIsIUnknown,
		// Token: 0x04002A70 RID: 10864
		[__DynamicallyInvokable]
		InterfaceIsIDispatch,
		// Token: 0x04002A71 RID: 10865
		[ComVisible(false)]
		[__DynamicallyInvokable]
		InterfaceIsIInspectable
	}
}
