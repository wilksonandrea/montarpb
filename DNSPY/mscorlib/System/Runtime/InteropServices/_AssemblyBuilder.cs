using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009B4 RID: 2484
	[Guid("BEBB2505-8B54-3443-AEAD-142A16DD9CC7")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(AssemblyBuilder))]
	[ComVisible(true)]
	public interface _AssemblyBuilder
	{
		// Token: 0x0600636D RID: 25453
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600636E RID: 25454
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x0600636F RID: 25455
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06006370 RID: 25456
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
