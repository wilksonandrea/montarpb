using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000989 RID: 2441
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.FILETIME instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	public struct FILETIME
	{
		// Token: 0x04002BEF RID: 11247
		public int dwLowDateTime;

		// Token: 0x04002BF0 RID: 11248
		public int dwHighDateTime;
	}
}
