using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000248 RID: 584
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	public abstract class Aes : SymmetricAlgorithm
	{
		// Token: 0x060020D1 RID: 8401 RVA: 0x000728BC File Offset: 0x00070ABC
		protected Aes()
		{
			this.LegalBlockSizesValue = Aes.s_legalBlockSizes;
			this.LegalKeySizesValue = Aes.s_legalKeySizes;
			this.BlockSizeValue = 128;
			this.FeedbackSizeValue = 8;
			this.KeySizeValue = 256;
			this.ModeValue = CipherMode.CBC;
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x00072909 File Offset: 0x00070B09
		public new static Aes Create()
		{
			return Aes.Create("AES");
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x00072915 File Offset: 0x00070B15
		public new static Aes Create(string algorithmName)
		{
			if (algorithmName == null)
			{
				throw new ArgumentNullException("algorithmName");
			}
			return CryptoConfig.CreateFromName(algorithmName) as Aes;
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x00072930 File Offset: 0x00070B30
		// Note: this type is marked as 'beforefieldinit'.
		static Aes()
		{
		}

		// Token: 0x04000BE7 RID: 3047
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(128, 128, 0)
		};

		// Token: 0x04000BE8 RID: 3048
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(128, 256, 64)
		};
	}
}
