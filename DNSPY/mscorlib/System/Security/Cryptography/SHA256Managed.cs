using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000295 RID: 661
	[ComVisible(true)]
	public class SHA256Managed : SHA256
	{
		// Token: 0x06002364 RID: 9060 RVA: 0x0008025B File Offset: 0x0007E45B
		public SHA256Managed()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms && AppContextSwitches.UseLegacyFipsThrow)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
			}
			this._impl = SHA256Managed._factory.CreateInstance();
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x00080291 File Offset: 0x0007E491
		public override void Initialize()
		{
			this._impl.Initialize();
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x0008029E File Offset: 0x0007E49E
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			this._impl.TransformBlock(rgb, ibStart, cbSize, null, 0);
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000802B1 File Offset: 0x0007E4B1
		protected override byte[] HashFinal()
		{
			this._impl.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
			return this._impl.Hash;
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000802D1 File Offset: 0x0007E4D1
		protected override void Dispose(bool disposing)
		{
			this._impl.Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000802E5 File Offset: 0x0007E4E5
		// Note: this type is marked as 'beforefieldinit'.
		static SHA256Managed()
		{
		}

		// Token: 0x04000CE5 RID: 3301
		private static readonly CngHashAlgorithmFactory<SHA256> _factory = new CngHashAlgorithmFactory<SHA256>("System.Security.Cryptography.SHA256Cng");

		// Token: 0x04000CE6 RID: 3302
		private SHA256 _impl;
	}
}
