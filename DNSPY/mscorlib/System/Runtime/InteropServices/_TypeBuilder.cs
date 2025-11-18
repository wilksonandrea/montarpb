using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009C2 RID: 2498
	[Guid("7E5678EE-48B3-3F83-B076-C58543498A58")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(TypeBuilder))]
	[ComVisible(true)]
	public interface _TypeBuilder
	{
		// Token: 0x060063A5 RID: 25509
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x060063A6 RID: 25510
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x060063A7 RID: 25511
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x060063A8 RID: 25512
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
