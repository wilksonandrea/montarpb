using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.IO.IsolatedStorage
{
	// Token: 0x020001B7 RID: 439
	[SecurityCritical]
	internal sealed class SafeIsolatedStorageFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001BC5 RID: 7109
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void Close(IntPtr file);

		// Token: 0x06001BC6 RID: 7110 RVA: 0x0005F440 File Offset: 0x0005D640
		private SafeIsolatedStorageFileHandle()
			: base(true)
		{
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x0005F454 File Offset: 0x0005D654
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeIsolatedStorageFileHandle.Close(this.handle);
			return true;
		}
	}
}
