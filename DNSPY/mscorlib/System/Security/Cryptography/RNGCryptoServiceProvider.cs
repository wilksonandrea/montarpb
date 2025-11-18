using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000247 RID: 583
	[ComVisible(true)]
	public sealed class RNGCryptoServiceProvider : RandomNumberGenerator
	{
		// Token: 0x060020C8 RID: 8392 RVA: 0x00072810 File Offset: 0x00070A10
		public RNGCryptoServiceProvider()
			: this(null)
		{
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x00072819 File Offset: 0x00070A19
		public RNGCryptoServiceProvider(string str)
			: this(null)
		{
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x00072822 File Offset: 0x00070A22
		public RNGCryptoServiceProvider(byte[] rgb)
			: this(null)
		{
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x0007282B File Offset: 0x00070A2B
		[SecuritySafeCritical]
		public RNGCryptoServiceProvider(CspParameters cspParams)
		{
			if (cspParams != null)
			{
				this.m_safeProvHandle = Utils.AcquireProvHandle(cspParams);
				this.m_ownsHandle = true;
				return;
			}
			this.m_safeProvHandle = Utils.StaticProvHandle;
			this.m_ownsHandle = false;
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x0007285C File Offset: 0x00070A5C
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && this.m_ownsHandle)
			{
				this.m_safeProvHandle.Dispose();
			}
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x0007287B File Offset: 0x00070A7B
		[SecuritySafeCritical]
		public override void GetBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			RNGCryptoServiceProvider.GetBytes(this.m_safeProvHandle, data, data.Length);
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x0007289A File Offset: 0x00070A9A
		[SecuritySafeCritical]
		public override void GetNonZeroBytes(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			RNGCryptoServiceProvider.GetNonZeroBytes(this.m_safeProvHandle, data, data.Length);
		}

		// Token: 0x060020CF RID: 8399
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetBytes(SafeProvHandle hProv, byte[] randomBytes, int count);

		// Token: 0x060020D0 RID: 8400
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetNonZeroBytes(SafeProvHandle hProv, byte[] randomBytes, int count);

		// Token: 0x04000BE5 RID: 3045
		[SecurityCritical]
		private SafeProvHandle m_safeProvHandle;

		// Token: 0x04000BE6 RID: 3046
		private bool m_ownsHandle;
	}
}
