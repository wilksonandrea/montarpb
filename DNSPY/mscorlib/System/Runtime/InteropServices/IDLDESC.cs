using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000999 RID: 2457
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IDLDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct IDLDESC
	{
		// Token: 0x04002C4A RID: 11338
		public int dwReserved;

		// Token: 0x04002C4B RID: 11339
		public IDLFLAG wIDLFlags;
	}
}
