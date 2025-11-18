using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200061C RID: 1564
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum PropertyAttributes
	{
		// Token: 0x04001E0B RID: 7691
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x04001E0C RID: 7692
		[__DynamicallyInvokable]
		SpecialName = 512,
		// Token: 0x04001E0D RID: 7693
		ReservedMask = 62464,
		// Token: 0x04001E0E RID: 7694
		[__DynamicallyInvokable]
		RTSpecialName = 1024,
		// Token: 0x04001E0F RID: 7695
		[__DynamicallyInvokable]
		HasDefault = 4096,
		// Token: 0x04001E10 RID: 7696
		Reserved2 = 8192,
		// Token: 0x04001E11 RID: 7697
		Reserved3 = 16384,
		// Token: 0x04001E12 RID: 7698
		Reserved4 = 32768
	}
}
