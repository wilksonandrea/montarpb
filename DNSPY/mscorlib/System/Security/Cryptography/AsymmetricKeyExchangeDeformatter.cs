using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200024A RID: 586
	[ComVisible(true)]
	public abstract class AsymmetricKeyExchangeDeformatter
	{
		// Token: 0x060020E2 RID: 8418 RVA: 0x00072A71 File Offset: 0x00070C71
		protected AsymmetricKeyExchangeDeformatter()
		{
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060020E3 RID: 8419
		// (set) Token: 0x060020E4 RID: 8420
		public abstract string Parameters { get; set; }

		// Token: 0x060020E5 RID: 8421
		public abstract void SetKey(AsymmetricAlgorithm key);

		// Token: 0x060020E6 RID: 8422
		public abstract byte[] DecryptKeyExchange(byte[] rgb);
	}
}
