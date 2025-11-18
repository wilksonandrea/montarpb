using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009A6 RID: 2470
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.ITypeInfo instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("00020401-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMITypeInfo
	{
		// Token: 0x060062E4 RID: 25316
		void GetTypeAttr(out IntPtr ppTypeAttr);

		// Token: 0x060062E5 RID: 25317
		void GetTypeComp(out UCOMITypeComp ppTComp);

		// Token: 0x060062E6 RID: 25318
		void GetFuncDesc(int index, out IntPtr ppFuncDesc);

		// Token: 0x060062E7 RID: 25319
		void GetVarDesc(int index, out IntPtr ppVarDesc);

		// Token: 0x060062E8 RID: 25320
		void GetNames(int memid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] string[] rgBstrNames, int cMaxNames, out int pcNames);

		// Token: 0x060062E9 RID: 25321
		void GetRefTypeOfImplType(int index, out int href);

		// Token: 0x060062EA RID: 25322
		void GetImplTypeFlags(int index, out int pImplTypeFlags);

		// Token: 0x060062EB RID: 25323
		void GetIDsOfNames([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 1)] [In] string[] rgszNames, int cNames, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] int[] pMemId);

		// Token: 0x060062EC RID: 25324
		void Invoke([MarshalAs(UnmanagedType.IUnknown)] object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, out object pVarResult, out EXCEPINFO pExcepInfo, out int puArgErr);

		// Token: 0x060062ED RID: 25325
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x060062EE RID: 25326
		void GetDllEntry(int memid, INVOKEKIND invKind, out string pBstrDllName, out string pBstrName, out short pwOrdinal);

		// Token: 0x060062EF RID: 25327
		void GetRefTypeInfo(int hRef, out UCOMITypeInfo ppTI);

		// Token: 0x060062F0 RID: 25328
		void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

		// Token: 0x060062F1 RID: 25329
		void CreateInstance([MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

		// Token: 0x060062F2 RID: 25330
		void GetMops(int memid, out string pBstrMops);

		// Token: 0x060062F3 RID: 25331
		void GetContainingTypeLib(out UCOMITypeLib ppTLB, out int pIndex);

		// Token: 0x060062F4 RID: 25332
		void ReleaseTypeAttr(IntPtr pTypeAttr);

		// Token: 0x060062F5 RID: 25333
		void ReleaseFuncDesc(IntPtr pFuncDesc);

		// Token: 0x060062F6 RID: 25334
		void ReleaseVarDesc(IntPtr pVarDesc);
	}
}
