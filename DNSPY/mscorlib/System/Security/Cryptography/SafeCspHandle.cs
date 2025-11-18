using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200023D RID: 573
	[SecurityCritical]
	internal sealed class SafeCspHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060020A1 RID: 8353 RVA: 0x000725CC File Offset: 0x000707CC
		private SafeCspHandle()
			: base(true)
		{
		}

		// Token: 0x060020A2 RID: 8354
		[DllImport("advapi32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptReleaseContext(IntPtr hProv, int dwFlags);

		// Token: 0x060020A3 RID: 8355 RVA: 0x000725D5 File Offset: 0x000707D5
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeCspHandle.CryptReleaseContext(this.handle, 0);
		}
	}
}
