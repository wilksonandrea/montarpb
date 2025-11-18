using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200028A RID: 650
	[ComVisible(true)]
	public abstract class Rijndael : SymmetricAlgorithm
	{
		// Token: 0x06002324 RID: 8996 RVA: 0x0007E16D File Offset: 0x0007C36D
		protected Rijndael()
		{
			this.KeySizeValue = 256;
			this.BlockSizeValue = 128;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = Rijndael.s_legalBlockSizes;
			this.LegalKeySizesValue = Rijndael.s_legalKeySizes;
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x0007E1AD File Offset: 0x0007C3AD
		public new static Rijndael Create()
		{
			return Rijndael.Create("System.Security.Cryptography.Rijndael");
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x0007E1B9 File Offset: 0x0007C3B9
		public new static Rijndael Create(string algName)
		{
			return (Rijndael)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x0007E1C6 File Offset: 0x0007C3C6
		// Note: this type is marked as 'beforefieldinit'.
		static Rijndael()
		{
		}

		// Token: 0x04000CC6 RID: 3270
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(128, 256, 64)
		};

		// Token: 0x04000CC7 RID: 3271
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(128, 256, 64)
		};
	}
}
