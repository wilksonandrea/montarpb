using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200023E RID: 574
	[SecurityCritical]
	internal sealed class SafeCspHashHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060020A4 RID: 8356 RVA: 0x000725E3 File Offset: 0x000707E3
		private SafeCspHashHandle()
			: base(true)
		{
		}

		// Token: 0x060020A5 RID: 8357
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptDestroyHash(IntPtr hKey);

		// Token: 0x060020A6 RID: 8358 RVA: 0x000725EC File Offset: 0x000707EC
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeCspHashHandle.CryptDestroyHash(this.handle);
		}
	}
}
