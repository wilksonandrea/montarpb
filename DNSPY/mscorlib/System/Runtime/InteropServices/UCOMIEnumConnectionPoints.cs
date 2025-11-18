using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000985 RID: 2437
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IEnumConnectionPoints instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("B196B285-BAB4-101A-B69C-00AA00341D07")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIEnumConnectionPoints
	{
		// Token: 0x0600629A RID: 25242
		[PreserveSig]
		int Next(int celt, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] [Out] UCOMIConnectionPoint[] rgelt, out int pceltFetched);

		// Token: 0x0600629B RID: 25243
		[PreserveSig]
		int Skip(int celt);

		// Token: 0x0600629C RID: 25244
		[PreserveSig]
		int Reset();

		// Token: 0x0600629D RID: 25245
		void Clone(out UCOMIEnumConnectionPoints ppenum);
	}
}
