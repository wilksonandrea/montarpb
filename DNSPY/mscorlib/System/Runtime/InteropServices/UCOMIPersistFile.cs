using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200098B RID: 2443
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IPersistFile instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("0000010b-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIPersistFile
	{
		// Token: 0x060062BE RID: 25278
		void GetClassID(out Guid pClassID);

		// Token: 0x060062BF RID: 25279
		[PreserveSig]
		int IsDirty();

		// Token: 0x060062C0 RID: 25280
		void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

		// Token: 0x060062C1 RID: 25281
		void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

		// Token: 0x060062C2 RID: 25282
		void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

		// Token: 0x060062C3 RID: 25283
		void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
	}
}
