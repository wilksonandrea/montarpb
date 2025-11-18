using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000902 RID: 2306
	[Guid("917B14D0-2D9E-38B8-92A9-381ACF52F7C0")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(Attribute))]
	[ComVisible(true)]
	public interface _Attribute
	{
		// Token: 0x06005E61 RID: 24161
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005E62 RID: 24162
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005E63 RID: 24163
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005E64 RID: 24164
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
