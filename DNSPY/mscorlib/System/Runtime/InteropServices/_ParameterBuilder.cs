using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009BF RID: 2495
	[Guid("36329EBA-F97A-3565-BC07-0ED5C6EF19FC")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(ParameterBuilder))]
	[ComVisible(true)]
	public interface _ParameterBuilder
	{
		// Token: 0x06006399 RID: 25497
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600639A RID: 25498
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x0600639B RID: 25499
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x0600639C RID: 25500
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
