using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006D0 RID: 1744
	[StructLayout(LayoutKind.Sequential)]
	internal class MuiResourceTypeIdIntEntry : IDisposable
	{
		// Token: 0x06005066 RID: 20582 RVA: 0x0011D86C File Offset: 0x0011BA6C
		~MuiResourceTypeIdIntEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x0011D89C File Offset: 0x0011BA9C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06005068 RID: 20584 RVA: 0x0011D8A8 File Offset: 0x0011BAA8
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

		// Token: 0x06005069 RID: 20585 RVA: 0x0011D90E File Offset: 0x0011BB0E
		public MuiResourceTypeIdIntEntry()
		{
		}

		// Token: 0x040022EF RID: 8943
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr StringIds;

		// Token: 0x040022F0 RID: 8944
		public uint StringIdsSize;

		// Token: 0x040022F1 RID: 8945
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr IntegerIds;

		// Token: 0x040022F2 RID: 8946
		public uint IntegerIdsSize;
	}
}
