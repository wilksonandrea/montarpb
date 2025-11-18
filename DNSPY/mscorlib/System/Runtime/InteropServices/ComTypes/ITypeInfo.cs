using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A50 RID: 2640
	[Guid("00020401-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface ITypeInfo
	{
		// Token: 0x06006677 RID: 26231
		void GetTypeAttr(out IntPtr ppTypeAttr);

		// Token: 0x06006678 RID: 26232
		[__DynamicallyInvokable]
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x06006679 RID: 26233
		void GetFuncDesc(int index, out IntPtr ppFuncDesc);

		// Token: 0x0600667A RID: 26234
		void GetVarDesc(int index, out IntPtr ppVarDesc);

		// Token: 0x0600667B RID: 26235
		[__DynamicallyInvokable]
		void GetNames(int memid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] string[] rgBstrNames, int cMaxNames, out int pcNames);

		// Token: 0x0600667C RID: 26236
		[__DynamicallyInvokable]
		void GetRefTypeOfImplType(int index, out int href);

		// Token: 0x0600667D RID: 26237
		[__DynamicallyInvokable]
		void GetImplTypeFlags(int index, out IMPLTYPEFLAGS pImplTypeFlags);

		// Token: 0x0600667E RID: 26238
		[__DynamicallyInvokable]
		void GetIDsOfNames([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 1)] [In] string[] rgszNames, int cNames, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] int[] pMemId);

		// Token: 0x0600667F RID: 26239
		void Invoke([MarshalAs(UnmanagedType.IUnknown)] object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, out int puArgErr);

		// Token: 0x06006680 RID: 26240
		[__DynamicallyInvokable]
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x06006681 RID: 26241
		void GetDllEntry(int memid, INVOKEKIND invKind, IntPtr pBstrDllName, IntPtr pBstrName, IntPtr pwOrdinal);

		// Token: 0x06006682 RID: 26242
		[__DynamicallyInvokable]
		void GetRefTypeInfo(int hRef, out ITypeInfo ppTI);

		// Token: 0x06006683 RID: 26243
		void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

		// Token: 0x06006684 RID: 26244
		[__DynamicallyInvokable]
		void CreateInstance([MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

		// Token: 0x06006685 RID: 26245
		[__DynamicallyInvokable]
		void GetMops(int memid, out string pBstrMops);

		// Token: 0x06006686 RID: 26246
		[__DynamicallyInvokable]
		void GetContainingTypeLib(out ITypeLib ppTLB, out int pIndex);

		// Token: 0x06006687 RID: 26247
		[PreserveSig]
		void ReleaseTypeAttr(IntPtr pTypeAttr);

		// Token: 0x06006688 RID: 26248
		[PreserveSig]
		void ReleaseFuncDesc(IntPtr pFuncDesc);

		// Token: 0x06006689 RID: 26249
		[PreserveSig]
		void ReleaseVarDesc(IntPtr pVarDesc);
	}
}
