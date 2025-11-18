using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000299 RID: 665
	[ComVisible(true)]
	public class SHA512Managed : SHA512
	{
		// Token: 0x06002376 RID: 9078 RVA: 0x000803E9 File Offset: 0x0007E5E9
		public SHA512Managed()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms && AppContextSwitches.UseLegacyFipsThrow)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
			}
			this._impl = SHA512Managed._factory.CreateInstance();
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x0008041F File Offset: 0x0007E61F
		public override void Initialize()
		{
			this._impl.Initialize();
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x0008042C File Offset: 0x0007E62C
		[SecuritySafeCritical]
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			this._impl.TransformBlock(rgb, ibStart, cbSize, null, 0);
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x0008043F File Offset: 0x0007E63F
		[SecuritySafeCritical]
		protected override byte[] HashFinal()
		{
			this._impl.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
			return this._impl.Hash;
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x0008045F File Offset: 0x0007E65F
		protected override void Dispose(bool disposing)
		{
			this._impl.Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x00080473 File Offset: 0x0007E673
		// Note: this type is marked as 'beforefieldinit'.
		static SHA512Managed()
		{
		}

		// Token: 0x04000CE9 RID: 3305
		private static readonly CngHashAlgorithmFactory<SHA512> _factory = new CngHashAlgorithmFactory<SHA512>("System.Security.Cryptography.SHA512Cng");

		// Token: 0x04000CEA RID: 3306
		private SHA512 _impl;
	}
}
