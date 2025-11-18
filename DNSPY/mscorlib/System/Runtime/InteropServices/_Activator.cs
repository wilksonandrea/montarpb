using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000901 RID: 2305
	[Guid("03973551-57A1-3900-A2B5-9083E3FF2943")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(Activator))]
	[ComVisible(true)]
	public interface _Activator
	{
		// Token: 0x06005E5D RID: 24157
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005E5E RID: 24158
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005E5F RID: 24159
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005E60 RID: 24160
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
