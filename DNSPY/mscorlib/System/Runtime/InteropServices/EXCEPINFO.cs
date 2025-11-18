using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009A0 RID: 2464
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.EXCEPINFO instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct EXCEPINFO
	{
		// Token: 0x04002C64 RID: 11364
		public short wCode;

		// Token: 0x04002C65 RID: 11365
		public short wReserved;

		// Token: 0x04002C66 RID: 11366
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrSource;

		// Token: 0x04002C67 RID: 11367
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrDescription;

		// Token: 0x04002C68 RID: 11368
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrHelpFile;

		// Token: 0x04002C69 RID: 11369
		public int dwHelpContext;

		// Token: 0x04002C6A RID: 11370
		public IntPtr pvReserved;

		// Token: 0x04002C6B RID: 11371
		public IntPtr pfnDeferredFillIn;
	}
}
