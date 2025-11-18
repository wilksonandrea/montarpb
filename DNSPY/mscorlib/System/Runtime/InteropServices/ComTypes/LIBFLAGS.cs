using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A52 RID: 2642
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum LIBFLAGS : short
	{
		// Token: 0x04002E0F RID: 11791
		[__DynamicallyInvokable]
		LIBFLAG_FRESTRICTED = 1,
		// Token: 0x04002E10 RID: 11792
		[__DynamicallyInvokable]
		LIBFLAG_FCONTROL = 2,
		// Token: 0x04002E11 RID: 11793
		[__DynamicallyInvokable]
		LIBFLAG_FHIDDEN = 4,
		// Token: 0x04002E12 RID: 11794
		[__DynamicallyInvokable]
		LIBFLAG_FHASDISKIMAGE = 8
	}
}
