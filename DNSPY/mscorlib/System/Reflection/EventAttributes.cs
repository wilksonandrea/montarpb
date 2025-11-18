using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005E3 RID: 1507
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum EventAttributes
	{
		// Token: 0x04001C9D RID: 7325
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x04001C9E RID: 7326
		[__DynamicallyInvokable]
		SpecialName = 512,
		// Token: 0x04001C9F RID: 7327
		ReservedMask = 1024,
		// Token: 0x04001CA0 RID: 7328
		[__DynamicallyInvokable]
		RTSpecialName = 1024
	}
}
