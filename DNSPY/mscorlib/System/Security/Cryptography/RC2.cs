using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000277 RID: 631
	[ComVisible(true)]
	public abstract class RC2 : SymmetricAlgorithm
	{
		// Token: 0x06002257 RID: 8791 RVA: 0x000795B8 File Offset: 0x000777B8
		protected RC2()
		{
			this.KeySizeValue = 128;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = RC2.s_legalBlockSizes;
			this.LegalKeySizesValue = RC2.s_legalKeySizes;
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x000795F5 File Offset: 0x000777F5
		// (set) Token: 0x06002259 RID: 8793 RVA: 0x0007960C File Offset: 0x0007780C
		public virtual int EffectiveKeySize
		{
			get
			{
				if (this.EffectiveKeySizeValue == 0)
				{
					return this.KeySizeValue;
				}
				return this.EffectiveKeySizeValue;
			}
			set
			{
				if (value > this.KeySizeValue)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_RC2_EKSKS"));
				}
				if (value == 0)
				{
					this.EffectiveKeySizeValue = value;
					return;
				}
				if (value < 40)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_RC2_EKS40"));
				}
				if (base.ValidKeySize(value))
				{
					this.EffectiveKeySizeValue = value;
					return;
				}
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x00079672 File Offset: 0x00077872
		// (set) Token: 0x0600225B RID: 8795 RVA: 0x0007967A File Offset: 0x0007787A
		public override int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				if (value < this.EffectiveKeySizeValue)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_RC2_EKSKS"));
				}
				base.KeySize = value;
			}
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x0007969C File Offset: 0x0007789C
		public new static RC2 Create()
		{
			return RC2.Create("System.Security.Cryptography.RC2");
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000796A8 File Offset: 0x000778A8
		public new static RC2 Create(string AlgName)
		{
			return (RC2)CryptoConfig.CreateFromName(AlgName);
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x000796B5 File Offset: 0x000778B5
		// Note: this type is marked as 'beforefieldinit'.
		static RC2()
		{
		}

		// Token: 0x04000C7A RID: 3194
		protected int EffectiveKeySizeValue;

		// Token: 0x04000C7B RID: 3195
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x04000C7C RID: 3196
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(40, 1024, 8)
		};
	}
}
