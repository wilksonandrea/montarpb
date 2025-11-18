using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200099F RID: 2463
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.DISPPARAMS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct DISPPARAMS
	{
		// Token: 0x04002C60 RID: 11360
		public IntPtr rgvarg;

		// Token: 0x04002C61 RID: 11361
		public IntPtr rgdispidNamedArgs;

		// Token: 0x04002C62 RID: 11362
		public int cArgs;

		// Token: 0x04002C63 RID: 11363
		public int cNamedArgs;
	}
}
