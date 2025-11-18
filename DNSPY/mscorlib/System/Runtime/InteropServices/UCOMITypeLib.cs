using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009AA RID: 2474
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.ITypeLib instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("00020402-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMITypeLib
	{
		// Token: 0x060062F7 RID: 25335
		[PreserveSig]
		int GetTypeInfoCount();

		// Token: 0x060062F8 RID: 25336
		void GetTypeInfo(int index, out UCOMITypeInfo ppTI);

		// Token: 0x060062F9 RID: 25337
		void GetTypeInfoType(int index, out TYPEKIND pTKind);

		// Token: 0x060062FA RID: 25338
		void GetTypeInfoOfGuid(ref Guid guid, out UCOMITypeInfo ppTInfo);

		// Token: 0x060062FB RID: 25339
		void GetLibAttr(out IntPtr ppTLibAttr);

		// Token: 0x060062FC RID: 25340
		void GetTypeComp(out UCOMITypeComp ppTComp);

		// Token: 0x060062FD RID: 25341
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x060062FE RID: 25342
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

		// Token: 0x060062FF RID: 25343
		void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] UCOMITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgMemId, ref short pcFound);

		// Token: 0x06006300 RID: 25344
		[PreserveSig]
		void ReleaseTLibAttr(IntPtr pTLibAttr);
	}
}
