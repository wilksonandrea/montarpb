using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200099A RID: 2458
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.PARAMFLAG instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum PARAMFLAG : short
	{
		// Token: 0x04002C4D RID: 11341
		PARAMFLAG_NONE = 0,
		// Token: 0x04002C4E RID: 11342
		PARAMFLAG_FIN = 1,
		// Token: 0x04002C4F RID: 11343
		PARAMFLAG_FOUT = 2,
		// Token: 0x04002C50 RID: 11344
		PARAMFLAG_FLCID = 4,
		// Token: 0x04002C51 RID: 11345
		PARAMFLAG_FRETVAL = 8,
		// Token: 0x04002C52 RID: 11346
		PARAMFLAG_FOPT = 16,
		// Token: 0x04002C53 RID: 11347
		PARAMFLAG_FHASDEFAULT = 32,
		// Token: 0x04002C54 RID: 11348
		PARAMFLAG_FHASCUSTDATA = 64
	}
}
