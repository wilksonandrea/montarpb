using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009BA RID: 2490
	[Guid("A4924B27-6E3B-37F7-9B83-A4501955E6A7")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(ILGenerator))]
	[ComVisible(true)]
	public interface _ILGenerator
	{
		// Token: 0x06006385 RID: 25477
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06006386 RID: 25478
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06006387 RID: 25479
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06006388 RID: 25480
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
