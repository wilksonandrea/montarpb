using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A38 RID: 2616
	[Guid("0000000c-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[__DynamicallyInvokable]
	[ComImport]
	public interface IStream
	{
		// Token: 0x0600666A RID: 26218
		void Read([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] byte[] pv, int cb, IntPtr pcbRead);

		// Token: 0x0600666B RID: 26219
		void Write([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, IntPtr pcbWritten);

		// Token: 0x0600666C RID: 26220
		void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition);

		// Token: 0x0600666D RID: 26221
		[__DynamicallyInvokable]
		void SetSize(long libNewSize);

		// Token: 0x0600666E RID: 26222
		void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

		// Token: 0x0600666F RID: 26223
		[__DynamicallyInvokable]
		void Commit(int grfCommitFlags);

		// Token: 0x06006670 RID: 26224
		[__DynamicallyInvokable]
		void Revert();

		// Token: 0x06006671 RID: 26225
		[__DynamicallyInvokable]
		void LockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x06006672 RID: 26226
		[__DynamicallyInvokable]
		void UnlockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x06006673 RID: 26227
		[__DynamicallyInvokable]
		void Stat(out STATSTG pstatstg, int grfStatFlag);

		// Token: 0x06006674 RID: 26228
		[__DynamicallyInvokable]
		void Clone(out IStream ppstm);
	}
}
