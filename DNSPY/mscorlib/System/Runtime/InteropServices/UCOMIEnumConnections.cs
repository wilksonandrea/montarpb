using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000984 RID: 2436
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumConnections instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("B196B287-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIEnumConnections
	{
		// Token: 0x06006296 RID: 25238
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] CONNECTDATA[] rgelt, out int pceltFetched);

		// Token: 0x06006297 RID: 25239
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x06006298 RID: 25240
		[PreserveSig]
		void Reset();

		// Token: 0x06006299 RID: 25241
		void Clone(out UCOMIEnumConnections ppenum);
	}
}
