using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200098F RID: 2447
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IStream instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Guid("0000000c-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIStream
	{
		// Token: 0x060062D7 RID: 25303
		void Read([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] byte[] pv, int cb, IntPtr pcbRead);

		// Token: 0x060062D8 RID: 25304
		void Write([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, IntPtr pcbWritten);

		// Token: 0x060062D9 RID: 25305
		void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition);

		// Token: 0x060062DA RID: 25306
		void SetSize(long libNewSize);

		// Token: 0x060062DB RID: 25307
		void CopyTo(UCOMIStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

		// Token: 0x060062DC RID: 25308
		void Commit(int grfCommitFlags);

		// Token: 0x060062DD RID: 25309
		void Revert();

		// Token: 0x060062DE RID: 25310
		void LockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x060062DF RID: 25311
		void UnlockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x060062E0 RID: 25312
		void Stat(out STATSTG pstatstg, int grfStatFlag);

		// Token: 0x060062E1 RID: 25313
		void Clone(out UCOMIStream ppstm);
	}
}
