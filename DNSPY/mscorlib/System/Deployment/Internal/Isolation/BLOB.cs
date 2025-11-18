using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000676 RID: 1654
	internal struct BLOB : IDisposable
	{
		// Token: 0x06004F35 RID: 20277 RVA: 0x0011C197 File Offset: 0x0011A397
		[SecuritySafeCritical]
		public void Dispose()
		{
			if (this.BlobData != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.BlobData);
				this.BlobData = IntPtr.Zero;
			}
		}

		// Token: 0x040021E7 RID: 8679
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x040021E8 RID: 8680
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr BlobData;
	}
}
