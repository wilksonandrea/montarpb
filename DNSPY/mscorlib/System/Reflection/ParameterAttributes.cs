using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000617 RID: 1559
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum ParameterAttributes
	{
		// Token: 0x04001DE9 RID: 7657
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x04001DEA RID: 7658
		[__DynamicallyInvokable]
		In = 1,
		// Token: 0x04001DEB RID: 7659
		[__DynamicallyInvokable]
		Out = 2,
		// Token: 0x04001DEC RID: 7660
		[__DynamicallyInvokable]
		Lcid = 4,
		// Token: 0x04001DED RID: 7661
		[__DynamicallyInvokable]
		Retval = 8,
		// Token: 0x04001DEE RID: 7662
		[__DynamicallyInvokable]
		Optional = 16,
		// Token: 0x04001DEF RID: 7663
		ReservedMask = 61440,
		// Token: 0x04001DF0 RID: 7664
		[__DynamicallyInvokable]
		HasDefault = 4096,
		// Token: 0x04001DF1 RID: 7665
		[__DynamicallyInvokable]
		HasFieldMarshal = 8192,
		// Token: 0x04001DF2 RID: 7666
		Reserved3 = 16384,
		// Token: 0x04001DF3 RID: 7667
		Reserved4 = 32768
	}
}
