using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000259 RID: 601
	[ComVisible(true)]
	public abstract class DES : SymmetricAlgorithm
	{
		// Token: 0x06002158 RID: 8536 RVA: 0x00075CF9 File Offset: 0x00073EF9
		protected DES()
		{
			this.KeySizeValue = 64;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = DES.s_legalBlockSizes;
			this.LegalKeySizesValue = DES.s_legalKeySizes;
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06002159 RID: 8537 RVA: 0x00075D33 File Offset: 0x00073F33
		// (set) Token: 0x0600215A RID: 8538 RVA: 0x00075D70 File Offset: 0x00073F70
		public override byte[] Key
		{
			get
			{
				if (this.KeyValue == null)
				{
					do
					{
						this.GenerateKey();
					}
					while (DES.IsWeakKey(this.KeyValue) || DES.IsSemiWeakKey(this.KeyValue));
				}
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!base.ValidKeySize(value.Length * 8))
				{
					throw new ArgumentException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
				}
				if (DES.IsWeakKey(value))
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKey_Weak"), "DES");
				}
				if (DES.IsSemiWeakKey(value))
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKey_SemiWeak"), "DES");
				}
				this.KeyValue = (byte[])value.Clone();
				this.KeySizeValue = value.Length * 8;
			}
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x00075DFE File Offset: 0x00073FFE
		public new static DES Create()
		{
			return DES.Create("System.Security.Cryptography.DES");
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x00075E0A File Offset: 0x0007400A
		public new static DES Create(string algName)
		{
			return (DES)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x00075E18 File Offset: 0x00074018
		public static bool IsWeakKey(byte[] rgbKey)
		{
			if (!DES.IsLegalKeySize(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
			byte[] array = Utils.FixupKeyParity(rgbKey);
			ulong num = DES.QuadWordFromBigEndian(array);
			return num == 72340172838076673UL || num == 18374403900871474942UL || num == 2242545357694045710UL || num == 16204198716015505905UL;
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x00075E80 File Offset: 0x00074080
		public static bool IsSemiWeakKey(byte[] rgbKey)
		{
			if (!DES.IsLegalKeySize(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
			byte[] array = Utils.FixupKeyParity(rgbKey);
			ulong num = DES.QuadWordFromBigEndian(array);
			return num == 143554428589179390UL || num == 18303189645120372225UL || num == 2296870857142767345UL || num == 16149873216566784270UL || num == 135110050437988849UL || num == 16141428838415593729UL || num == 2305315235293957886UL || num == 18311634023271562766UL || num == 80784550989267214UL || num == 2234100979542855169UL || num == 16212643094166696446UL || num == 18365959522720284401UL;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x00075F49 File Offset: 0x00074149
		private static bool IsLegalKeySize(byte[] rgbKey)
		{
			return rgbKey != null && rgbKey.Length == 8;
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x00075F58 File Offset: 0x00074158
		private static ulong QuadWordFromBigEndian(byte[] block)
		{
			return ((ulong)block[0] << 56) | ((ulong)block[1] << 48) | ((ulong)block[2] << 40) | ((ulong)block[3] << 32) | ((ulong)block[4] << 24) | ((ulong)block[5] << 16) | ((ulong)block[6] << 8) | (ulong)block[7];
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x00075FA2 File Offset: 0x000741A2
		// Note: this type is marked as 'beforefieldinit'.
		static DES()
		{
		}

		// Token: 0x04000C2E RID: 3118
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x04000C2F RID: 3119
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};
	}
}
