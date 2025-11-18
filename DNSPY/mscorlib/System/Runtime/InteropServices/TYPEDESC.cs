using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200099C RID: 2460
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEDESC
	{
		// Token: 0x04002C57 RID: 11351
		public IntPtr lpValue;

		// Token: 0x04002C58 RID: 11352
		public short vt;
	}
}
