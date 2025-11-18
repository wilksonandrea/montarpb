using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006CD RID: 1741
	[StructLayout(LayoutKind.Sequential)]
	internal class MuiResourceTypeIdStringEntry : IDisposable
	{
		// Token: 0x0600505F RID: 20575 RVA: 0x0011D7C0 File Offset: 0x0011B9C0
		~MuiResourceTypeIdStringEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06005060 RID: 20576 RVA: 0x0011D7F0 File Offset: 0x0011B9F0
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x0011D7FC File Offset: 0x0011B9FC
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.StringIds != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.StringIds);
				this.StringIds = IntPtr.Zero;
			}
			if (this.IntegerIds != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.IntegerIds);
				this.IntegerIds = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x0011D862 File Offset: 0x0011BA62
		public MuiResourceTypeIdStringEntry()
		{
		}

		// Token: 0x040022E6 RID: 8934
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr StringIds;

		// Token: 0x040022E7 RID: 8935
		public uint StringIdsSize;

		// Token: 0x040022E8 RID: 8936
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr IntegerIds;

		// Token: 0x040022E9 RID: 8937
		public uint IntegerIdsSize;
	}
}
