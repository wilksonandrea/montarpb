using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009C0 RID: 2496
	[Guid("15F9A479-9397-3A63-ACBD-F51977FB0F02")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(PropertyBuilder))]
	[ComVisible(true)]
	public interface _PropertyBuilder
	{
		// Token: 0x0600639D RID: 25501
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600639E RID: 25502
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x0600639F RID: 25503
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x060063A0 RID: 25504
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
