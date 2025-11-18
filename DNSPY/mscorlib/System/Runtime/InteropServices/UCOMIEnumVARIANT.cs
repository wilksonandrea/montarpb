using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000987 RID: 2439
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumVARIANT instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("00020404-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIEnumVARIANT
	{
		// Token: 0x060062A2 RID: 25250
		[PreserveSig]
		int Next(int celt, int rgvar, int pceltFetched);

		// Token: 0x060062A3 RID: 25251
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x060062A4 RID: 25252
		[PreserveSig]
		int Reset();

		// Token: 0x060062A5 RID: 25253
		void Clone(int ppenum);
	}
}
