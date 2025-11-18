using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009C1 RID: 2497
	[Guid("7D13DD37-5A04-393C-BBCA-A5FEA802893D")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(SignatureHelper))]
	[ComVisible(true)]
	public interface _SignatureHelper
	{
		// Token: 0x060063A1 RID: 25505
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x060063A2 RID: 25506
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x060063A3 RID: 25507
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x060063A4 RID: 25508
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
