using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000995 RID: 2453
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum IMPLTYPEFLAGS
	{
		// Token: 0x04002C21 RID: 11297
		IMPLTYPEFLAG_FDEFAULT = 1,
		// Token: 0x04002C22 RID: 11298
		IMPLTYPEFLAG_FSOURCE = 2,
		// Token: 0x04002C23 RID: 11299
		IMPLTYPEFLAG_FRESTRICTED = 4,
		// Token: 0x04002C24 RID: 11300
		IMPLTYPEFLAG_FDEFAULTVTABLE = 8
	}
}
