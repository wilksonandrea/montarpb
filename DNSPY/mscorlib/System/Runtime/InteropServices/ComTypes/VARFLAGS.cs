using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A4F RID: 2639
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum VARFLAGS : short
	{
		// Token: 0x04002DFC RID: 11772
		[__DynamicallyInvokable]
		VARFLAG_FREADONLY = 1,
		// Token: 0x04002DFD RID: 11773
		[__DynamicallyInvokable]
		VARFLAG_FSOURCE = 2,
		// Token: 0x04002DFE RID: 11774
		[__DynamicallyInvokable]
		VARFLAG_FBINDABLE = 4,
		// Token: 0x04002DFF RID: 11775
		[__DynamicallyInvokable]
		VARFLAG_FREQUESTEDIT = 8,
		// Token: 0x04002E00 RID: 11776
		[__DynamicallyInvokable]
		VARFLAG_FDISPLAYBIND = 16,
		// Token: 0x04002E01 RID: 11777
		[__DynamicallyInvokable]
		VARFLAG_FDEFAULTBIND = 32,
		// Token: 0x04002E02 RID: 11778
		[__DynamicallyInvokable]
		VARFLAG_FHIDDEN = 64,
		// Token: 0x04002E03 RID: 11779
		[__DynamicallyInvokable]
		VARFLAG_FRESTRICTED = 128,
		// Token: 0x04002E04 RID: 11780
		[__DynamicallyInvokable]
		VARFLAG_FDEFAULTCOLLELEM = 256,
		// Token: 0x04002E05 RID: 11781
		[__DynamicallyInvokable]
		VARFLAG_FUIDEFAULT = 512,
		// Token: 0x04002E06 RID: 11782
		[__DynamicallyInvokable]
		VARFLAG_FNONBROWSABLE = 1024,
		// Token: 0x04002E07 RID: 11783
		[__DynamicallyInvokable]
		VARFLAG_FREPLACEABLE = 2048,
		// Token: 0x04002E08 RID: 11784
		[__DynamicallyInvokable]
		VARFLAG_FIMMEDIATEBIND = 4096
	}
}
