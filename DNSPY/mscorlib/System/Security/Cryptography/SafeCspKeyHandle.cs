using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200023F RID: 575
	[SecurityCritical]
	internal sealed class SafeCspKeyHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060020A7 RID: 8359 RVA: 0x000725F9 File Offset: 0x000707F9
		internal SafeCspKeyHandle()
			: base(true)
		{
		}

		// Token: 0x060020A8 RID: 8360
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CryptDestroyKey(IntPtr hKey);

		// Token: 0x060020A9 RID: 8361 RVA: 0x00072602 File Offset: 0x00070802
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return SafeCspKeyHandle.CryptDestroyKey(this.handle);
		}
	}
}
