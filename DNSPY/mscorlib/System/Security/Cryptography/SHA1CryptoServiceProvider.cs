using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000292 RID: 658
	[ComVisible(true)]
	public sealed class SHA1CryptoServiceProvider : SHA1
	{
		// Token: 0x06002357 RID: 9047 RVA: 0x000800E5 File Offset: 0x0007E2E5
		[SecuritySafeCritical]
		public SHA1CryptoServiceProvider()
		{
			this._safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, 32772);
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x00080102 File Offset: 0x0007E302
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
			{
				this._safeHashHandle.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x0008012B File Offset: 0x0007E32B
		[SecuritySafeCritical]
		public override void Initialize()
		{
			if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
			{
				this._safeHashHandle.Dispose();
			}
			this._safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, 32772);
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x00080162 File Offset: 0x0007E362
		[SecuritySafeCritical]
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			Utils.HashData(this._safeHashHandle, rgb, ibStart, cbSize);
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x00080172 File Offset: 0x0007E372
		[SecuritySafeCritical]
		protected override byte[] HashFinal()
		{
			return Utils.EndHash(this._safeHashHandle);
		}

		// Token: 0x04000CE3 RID: 3299
		[SecurityCritical]
		private SafeHashHandle _safeHashHandle;
	}
}
