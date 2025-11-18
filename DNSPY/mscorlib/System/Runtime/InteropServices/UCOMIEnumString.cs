using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000986 RID: 2438
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumString instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("00000101-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIEnumString
	{
		// Token: 0x0600629E RID: 25246
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] [Out] string[] rgelt, out int pceltFetched);

		// Token: 0x0600629F RID: 25247
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x060062A0 RID: 25248
		[PreserveSig]
		int Reset();

		// Token: 0x060062A1 RID: 25249
		void Clone(out UCOMIEnumString ppenum);
	}
}
