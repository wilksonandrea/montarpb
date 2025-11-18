using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006D3 RID: 1747
	[StructLayout(LayoutKind.Sequential)]
	internal class MuiResourceMapEntry : IDisposable
	{
		// Token: 0x0600506D RID: 20589 RVA: 0x0011D918 File Offset: 0x0011BB18
		~MuiResourceMapEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x0600506E RID: 20590 RVA: 0x0011D948 File Offset: 0x0011BB48
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600506F RID: 20591 RVA: 0x0011D954 File Offset: 0x0011BB54
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.ResourceTypeIdInt != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.ResourceTypeIdInt);
				this.ResourceTypeIdInt = IntPtr.Zero;
			}
			if (this.ResourceTypeIdString != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.ResourceTypeIdString);
				this.ResourceTypeIdString = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06005070 RID: 20592 RVA: 0x0011D9BA File Offset: 0x0011BBBA
		public MuiResourceMapEntry()
		{
		}

		// Token: 0x040022F8 RID: 8952
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr ResourceTypeIdInt;

		// Token: 0x040022F9 RID: 8953
		public uint ResourceTypeIdIntSize;

		// Token: 0x040022FA RID: 8954
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr ResourceTypeIdString;

		// Token: 0x040022FB RID: 8955
		public uint ResourceTypeIdStringSize;
	}
}
