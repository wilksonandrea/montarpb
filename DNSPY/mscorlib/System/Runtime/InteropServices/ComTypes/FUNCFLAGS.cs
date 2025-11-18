using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A4E RID: 2638
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum FUNCFLAGS : short
	{
		// Token: 0x04002DEE RID: 11758
		[__DynamicallyInvokable]
		FUNCFLAG_FRESTRICTED = 1,
		// Token: 0x04002DEF RID: 11759
		[__DynamicallyInvokable]
		FUNCFLAG_FSOURCE = 2,
		// Token: 0x04002DF0 RID: 11760
		[__DynamicallyInvokable]
		FUNCFLAG_FBINDABLE = 4,
		// Token: 0x04002DF1 RID: 11761
		[__DynamicallyInvokable]
		FUNCFLAG_FREQUESTEDIT = 8,
		// Token: 0x04002DF2 RID: 11762
		[__DynamicallyInvokable]
		FUNCFLAG_FDISPLAYBIND = 16,
		// Token: 0x04002DF3 RID: 11763
		[__DynamicallyInvokable]
		FUNCFLAG_FDEFAULTBIND = 32,
		// Token: 0x04002DF4 RID: 11764
		[__DynamicallyInvokable]
		FUNCFLAG_FHIDDEN = 64,
		// Token: 0x04002DF5 RID: 11765
		[__DynamicallyInvokable]
		FUNCFLAG_FUSESGETLASTERROR = 128,
		// Token: 0x04002DF6 RID: 11766
		[__DynamicallyInvokable]
		FUNCFLAG_FDEFAULTCOLLELEM = 256,
		// Token: 0x04002DF7 RID: 11767
		[__DynamicallyInvokable]
		FUNCFLAG_FUIDEFAULT = 512,
		// Token: 0x04002DF8 RID: 11768
		[__DynamicallyInvokable]
		FUNCFLAG_FNONBROWSABLE = 1024,
		// Token: 0x04002DF9 RID: 11769
		[__DynamicallyInvokable]
		FUNCFLAG_FREPLACEABLE = 2048,
		// Token: 0x04002DFA RID: 11770
		[__DynamicallyInvokable]
		FUNCFLAG_FIMMEDIATEBIND = 4096
	}
}
