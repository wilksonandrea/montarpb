using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000297 RID: 663
	[ComVisible(true)]
	public class SHA384Managed : SHA384
	{
		// Token: 0x0600236D RID: 9069 RVA: 0x00080322 File Offset: 0x0007E522
		public SHA384Managed()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms && AppContextSwitches.UseLegacyFipsThrow)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
			}
			this._impl = SHA384Managed._factory.CreateInstance();
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x00080358 File Offset: 0x0007E558
		public override void Initialize()
		{
			this._impl.Initialize();
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x00080365 File Offset: 0x0007E565
		[SecuritySafeCritical]
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			this._impl.TransformBlock(rgb, ibStart, cbSize, null, 0);
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x00080378 File Offset: 0x0007E578
		[SecuritySafeCritical]
		protected override byte[] HashFinal()
		{
			this._impl.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
			return this._impl.Hash;
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x00080398 File Offset: 0x0007E598
		protected override void Dispose(bool disposing)
		{
			this._impl.Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x000803AC File Offset: 0x0007E5AC
		// Note: this type is marked as 'beforefieldinit'.
		static SHA384Managed()
		{
		}

		// Token: 0x04000CE7 RID: 3303
		private static readonly CngHashAlgorithmFactory<SHA384> _factory = new CngHashAlgorithmFactory<SHA384>("System.Security.Cryptography.SHA384Cng");

		// Token: 0x04000CE8 RID: 3304
		private SHA384 _impl;
	}
}
