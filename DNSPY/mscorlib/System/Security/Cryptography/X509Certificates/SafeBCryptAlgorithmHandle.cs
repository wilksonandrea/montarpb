using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C0 RID: 704
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeBCryptAlgorithmHandle : SafeHandle
	{
		// Token: 0x06002513 RID: 9491
		[SecurityCritical]
		[DllImport("bcrypt.dll")]
		private static extern int BCryptCloseAlgorithmProvider([In] IntPtr hAlgorithm, [In] uint dwFlags);

		// Token: 0x06002514 RID: 9492 RVA: 0x00086C7E File Offset: 0x00084E7E
		[SecurityCritical]
		public SafeBCryptAlgorithmHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06002515 RID: 9493 RVA: 0x00086C8C File Offset: 0x00084E8C
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x00086CA0 File Offset: 0x00084EA0
		[SecurityCritical]
		protected sealed override bool ReleaseHandle()
		{
			int num = SafeBCryptAlgorithmHandle.BCryptCloseAlgorithmProvider(this.handle, 0U);
			return num == 0;
		}
	}
}
