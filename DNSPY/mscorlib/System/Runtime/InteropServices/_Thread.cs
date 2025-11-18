using System;
using System.Threading;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000903 RID: 2307
	[Guid("C281C7F1-4AA9-3517-961A-463CFED57E75")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(Thread))]
	[ComVisible(true)]
	public interface _Thread
	{
		// Token: 0x06005E65 RID: 24165
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x06005E66 RID: 24166
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06005E67 RID: 24167
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06005E68 RID: 24168
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
