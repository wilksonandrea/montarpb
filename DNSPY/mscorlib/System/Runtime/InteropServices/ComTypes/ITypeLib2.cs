using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A55 RID: 2645
	[Guid("00020411-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface ITypeLib2 : ITypeLib
	{
		// Token: 0x06006694 RID: 26260
		[__DynamicallyInvokable]
		[PreserveSig]
		int GetTypeInfoCount();

		// Token: 0x06006695 RID: 26261
		[__DynamicallyInvokable]
		void GetTypeInfo(int index, out ITypeInfo ppTI);

		// Token: 0x06006696 RID: 26262
		[__DynamicallyInvokable]
		void GetTypeInfoType(int index, out TYPEKIND pTKind);

		// Token: 0x06006697 RID: 26263
		[__DynamicallyInvokable]
		void GetTypeInfoOfGuid(ref Guid guid, out ITypeInfo ppTInfo);

		// Token: 0x06006698 RID: 26264
		void GetLibAttr(out IntPtr ppTLibAttr);

		// Token: 0x06006699 RID: 26265
		[__DynamicallyInvokable]
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x0600669A RID: 26266
		[__DynamicallyInvokable]
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x0600669B RID: 26267
		[__DynamicallyInvokable]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

		// Token: 0x0600669C RID: 26268
		[__DynamicallyInvokable]
		void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgMemId, ref short pcFound);

		// Token: 0x0600669D RID: 26269
		[PreserveSig]
		void ReleaseTLibAttr(IntPtr pTLibAttr);

		// Token: 0x0600669E RID: 26270
		[__DynamicallyInvokable]
		void GetCustData(ref Guid guid, out object pVarVal);

		// Token: 0x0600669F RID: 26271
		[LCIDConversion(1)]
		[__DynamicallyInvokable]
		void GetDocumentation2(int index, out string pbstrHelpString, out int pdwHelpStringContext, out string pbstrHelpStringDll);

		// Token: 0x060066A0 RID: 26272
		void GetLibStatistics(IntPtr pcUniqueNames, out int pcchUniqueNames);

		// Token: 0x060066A1 RID: 26273
		void GetAllCustData(IntPtr pCustData);
	}
}
