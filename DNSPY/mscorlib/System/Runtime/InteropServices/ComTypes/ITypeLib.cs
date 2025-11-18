using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A54 RID: 2644
	[Guid("00020402-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface ITypeLib
	{
		// Token: 0x0600668A RID: 26250
		[__DynamicallyInvokable]
		[PreserveSig]
		int GetTypeInfoCount();

		// Token: 0x0600668B RID: 26251
		[__DynamicallyInvokable]
		void GetTypeInfo(int index, out ITypeInfo ppTI);

		// Token: 0x0600668C RID: 26252
		[__DynamicallyInvokable]
		void GetTypeInfoType(int index, out TYPEKIND pTKind);

		// Token: 0x0600668D RID: 26253
		[__DynamicallyInvokable]
		void GetTypeInfoOfGuid(ref Guid guid, out ITypeInfo ppTInfo);

		// Token: 0x0600668E RID: 26254
		void GetLibAttr(out IntPtr ppTLibAttr);

		// Token: 0x0600668F RID: 26255
		[__DynamicallyInvokable]
		void GetTypeComp(out ITypeComp ppTComp);

		// Token: 0x06006690 RID: 26256
		[__DynamicallyInvokable]
		void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

		// Token: 0x06006691 RID: 26257
		[__DynamicallyInvokable]
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

		// Token: 0x06006692 RID: 26258
		[__DynamicallyInvokable]
		void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray)] [Out] ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray)] [Out] int[] rgMemId, ref short pcFound);

		// Token: 0x06006693 RID: 26259
		[PreserveSig]
		void ReleaseTLibAttr(IntPtr pTLibAttr);
	}
}
