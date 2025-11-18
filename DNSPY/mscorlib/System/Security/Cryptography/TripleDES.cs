using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x020002A2 RID: 674
	[ComVisible(true)]
	public abstract class TripleDES : SymmetricAlgorithm
	{
		// Token: 0x060023AE RID: 9134 RVA: 0x00080A23 File Offset: 0x0007EC23
		protected TripleDES()
		{
			this.KeySizeValue = 192;
			this.BlockSizeValue = 64;
			this.FeedbackSizeValue = this.BlockSizeValue;
			this.LegalBlockSizesValue = TripleDES.s_legalBlockSizes;
			this.LegalKeySizesValue = TripleDES.s_legalKeySizes;
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x00080A60 File Offset: 0x0007EC60
		// (set) Token: 0x060023B0 RID: 9136 RVA: 0x00080A90 File Offset: 0x0007EC90
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
					while (TripleDES.IsWeakKey(this.KeyValue));
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
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
				}
				if (TripleDES.IsWeakKey(value))
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKey_Weak"), "TripleDES");
				}
				this.KeyValue = (byte[])value.Clone();
				this.KeySizeValue = value.Length * 8;
			}
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x00080B01 File Offset: 0x0007ED01
		public new static TripleDES Create()
		{
			return TripleDES.Create("System.Security.Cryptography.TripleDES");
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x00080B0D File Offset: 0x0007ED0D
		public new static TripleDES Create(string str)
		{
			return (TripleDES)CryptoConfig.CreateFromName(str);
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x00080B1C File Offset: 0x0007ED1C
		public static bool IsWeakKey(byte[] rgbKey)
		{
			if (!TripleDES.IsLegalKeySize(rgbKey))
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidKeySize"));
			}
			byte[] array = Utils.FixupKeyParity(rgbKey);
			return TripleDES.EqualBytes(array, 0, 8, 8) || (array.Length == 24 && TripleDES.EqualBytes(array, 8, 16, 8));
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x00080B6C File Offset: 0x0007ED6C
		private static bool EqualBytes(byte[] rgbKey, int start1, int start2, int count)
		{
			if (start1 < 0)
			{
				throw new ArgumentOutOfRangeException("start1", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (start2 < 0)
			{
				throw new ArgumentOutOfRangeException("start2", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (start1 + count > rgbKey.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
			}
			if (start2 + count > rgbKey.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
			}
			for (int i = 0; i < count; i++)
			{
				if (rgbKey[start1 + i] != rgbKey[start2 + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x00080BF6 File Offset: 0x0007EDF6
		private static bool IsLegalKeySize(byte[] rgbKey)
		{
			return rgbKey != null && (rgbKey.Length == 16 || rgbKey.Length == 24);
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x00080C0C File Offset: 0x0007EE0C
		// Note: this type is marked as 'beforefieldinit'.
		static TripleDES()
		{
		}

		// Token: 0x04000CF9 RID: 3321
		private static KeySizes[] s_legalBlockSizes = new KeySizes[]
		{
			new KeySizes(64, 64, 0)
		};

		// Token: 0x04000CFA RID: 3322
		private static KeySizes[] s_legalKeySizes = new KeySizes[]
		{
			new KeySizes(128, 192, 64)
		};
	}
}
