using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200026E RID: 622
	[ComVisible(true)]
	public abstract class KeyedHashAlgorithm : HashAlgorithm
	{
		// Token: 0x0600220C RID: 8716 RVA: 0x000784BC File Offset: 0x000766BC
		protected KeyedHashAlgorithm()
		{
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x000784C4 File Offset: 0x000766C4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.KeyValue != null)
				{
					Array.Clear(this.KeyValue, 0, this.KeyValue.Length);
				}
				this.KeyValue = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x000784F3 File Offset: 0x000766F3
		// (set) Token: 0x0600220F RID: 8719 RVA: 0x00078505 File Offset: 0x00076705
		public virtual byte[] Key
		{
			get
			{
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (this.State != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_HashKeySet"));
				}
				this.KeyValue = (byte[])value.Clone();
			}
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x00078530 File Offset: 0x00076730
		public new static KeyedHashAlgorithm Create()
		{
			return KeyedHashAlgorithm.Create("System.Security.Cryptography.KeyedHashAlgorithm");
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x0007853C File Offset: 0x0007673C
		public new static KeyedHashAlgorithm Create(string algName)
		{
			return (KeyedHashAlgorithm)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x04000C5F RID: 3167
		protected byte[] KeyValue;
	}
}
