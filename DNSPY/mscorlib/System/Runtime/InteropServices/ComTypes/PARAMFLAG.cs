using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A43 RID: 2627
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum PARAMFLAG : short
	{
		// Token: 0x04002DB1 RID: 11697
		[__DynamicallyInvokable]
		PARAMFLAG_NONE = 0,
		// Token: 0x04002DB2 RID: 11698
		[__DynamicallyInvokable]
		PARAMFLAG_FIN = 1,
		// Token: 0x04002DB3 RID: 11699
		[__DynamicallyInvokable]
		PARAMFLAG_FOUT = 2,
		// Token: 0x04002DB4 RID: 11700
		[__DynamicallyInvokable]
		PARAMFLAG_FLCID = 4,
		// Token: 0x04002DB5 RID: 11701
		[__DynamicallyInvokable]
		PARAMFLAG_FRETVAL = 8,
		// Token: 0x04002DB6 RID: 11702
		[__DynamicallyInvokable]
		PARAMFLAG_FOPT = 16,
		// Token: 0x04002DB7 RID: 11703
		[__DynamicallyInvokable]
		PARAMFLAG_FHASDEFAULT = 32,
		// Token: 0x04002DB8 RID: 11704
		[__DynamicallyInvokable]
		PARAMFLAG_FHASCUSTDATA = 64
	}
}
