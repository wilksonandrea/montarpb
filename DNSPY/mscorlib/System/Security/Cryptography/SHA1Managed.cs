using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000293 RID: 659
	[ComVisible(true)]
	public class SHA1Managed : SHA1
	{
		// Token: 0x0600235C RID: 9052 RVA: 0x0008017F File Offset: 0x0007E37F
		public SHA1Managed()
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms && AppContextSwitches.UseLegacyFipsThrow)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NonCompliantFIPSAlgorithm"));
			}
			this._impl = new SHA1CryptoServiceProvider();
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000801B0 File Offset: 0x0007E3B0
		public override void Initialize()
		{
			this._impl.Initialize();
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x000801C0 File Offset: 0x0007E3C0
		protected override void HashCore(byte[] rgb, int ibStart, int cbSize)
		{
			try
			{
				this._impl.TransformBlock(rgb, ibStart, cbSize, null, 0);
			}
			catch (ArgumentException)
			{
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x000801F8 File Offset: 0x0007E3F8
		protected override byte[] HashFinal()
		{
			this._impl.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
			return this._impl.Hash;
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x00080218 File Offset: 0x0007E418
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._impl.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000CE4 RID: 3300
		private SHA1 _impl;
	}
}
