using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000272 RID: 626
	[ComVisible(true)]
	public sealed class MD5CryptoServiceProvider : MD5
	{
		// Token: 0x0600222D RID: 8749 RVA: 0x00078A75 File Offset: 0x00076C75
		[SecuritySafeCritical]
		public MD5CryptoServiceProvider()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms && AppContextSwitches.UseLegacyFipsThrow)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
			}
			this._safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, 32771);
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x00078AB0 File Offset: 0x00076CB0
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
			{
				this._safeHashHandle.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x00078AD9 File Offset: 0x00076CD9
		[SecuritySafeCritical]
		public override void Initialize()
		{
			if (this._safeHashHandle != null && !this._safeHashHandle.IsClosed)
			{
				this._safeHashHandle.Dispose();
			}
			this._safeHashHandle = Utils.CreateHash(Utils.StaticProvHandle, 32771);
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x00078B10 File Offset: 0x00076D10
		[SecuritySafeCritical]
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			Utils.HashData(this._safeHashHandle, rgb, ibStart, cbSize);
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x00078B20 File Offset: 0x00076D20
		[SecuritySafeCritical]
		protected override byte[] HashFinal()
		{
			return Utils.EndHash(this._safeHashHandle);
		}

		// Token: 0x04000C6A RID: 3178
		[SecurityCritical]
		private SafeHashHandle _safeHashHandle;
	}
}
