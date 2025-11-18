using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200090D RID: 2317
	[Guid("993634C4-E47A-32CC-BE08-85F567DC27D6")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(ParameterInfo))]
	[ComVisible(true)]
	public interface _ParameterInfo
	{
		// Token: 0x06005FDF RID: 24543
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005FE0 RID: 24544
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005FE1 RID: 24545
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005FE2 RID: 24546
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
