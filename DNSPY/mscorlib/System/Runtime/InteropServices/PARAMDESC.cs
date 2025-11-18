using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200099B RID: 2459
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.PARAMDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct PARAMDESC
	{
		// Token: 0x04002C55 RID: 11349
		public IntPtr lpVarValue;

		// Token: 0x04002C56 RID: 11350
		public PARAMFLAG wParamFlags;
	}
}
