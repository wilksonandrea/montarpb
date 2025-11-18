using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200024B RID: 587
	[ComVisible(true)]
	public abstract class AsymmetricKeyExchangeFormatter
	{
		// Token: 0x060020E7 RID: 8423 RVA: 0x00072A79 File Offset: 0x00070C79
		protected AsymmetricKeyExchangeFormatter()
		{
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060020E8 RID: 8424
		public abstract string Parameters { get; }

		// Token: 0x060020E9 RID: 8425
		public abstract void SetKey(AsymmetricAlgorithm key);

		// Token: 0x060020EA RID: 8426
		public abstract byte[] CreateKeyExchange(byte[] data);

		// Token: 0x060020EB RID: 8427
		public abstract byte[] CreateKeyExchange(byte[] data, Type symAlgType);
	}
}
