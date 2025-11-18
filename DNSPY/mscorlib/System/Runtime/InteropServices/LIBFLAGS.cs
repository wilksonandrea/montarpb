using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009A8 RID: 2472
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.LIBFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum LIBFLAGS : short
	{
		// Token: 0x04002CA3 RID: 11427
		LIBFLAG_FRESTRICTED = 1,
		// Token: 0x04002CA4 RID: 11428
		LIBFLAG_FCONTROL = 2,
		// Token: 0x04002CA5 RID: 11429
		LIBFLAG_FHIDDEN = 4,
		// Token: 0x04002CA6 RID: 11430
		LIBFLAG_FHASDISKIMAGE = 8
	}
}
