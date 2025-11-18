using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000983 RID: 2435
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.CONNECTDATA instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct CONNECTDATA
	{
		// Token: 0x04002BED RID: 11245
		[MarshalAs(UnmanagedType.Interface)]
		public object pUnk;

		// Token: 0x04002BEE RID: 11246
		public int dwCookie;
	}
}
