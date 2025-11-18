using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000991 RID: 2449
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.BINDPTR instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
	public struct BINDPTR
	{
		// Token: 0x04002C03 RID: 11267
		[FieldOffset(0)]
		public IntPtr lpfuncdesc;

		// Token: 0x04002C04 RID: 11268
		[FieldOffset(0)]
		public IntPtr lpvardesc;

		// Token: 0x04002C05 RID: 11269
		[FieldOffset(0)]
		public IntPtr lptcomp;
	}
}
