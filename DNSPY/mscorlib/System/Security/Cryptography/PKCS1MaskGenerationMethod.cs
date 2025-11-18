using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000276 RID: 630
	[ComVisible(true)]
	public class PKCS1MaskGenerationMethod : MaskGenerationMethod
	{
		// Token: 0x06002253 RID: 8787 RVA: 0x000794D7 File Offset: 0x000776D7
		public PKCS1MaskGenerationMethod()
		{
			this.HashNameValue = "SHA1";
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x000794EA File Offset: 0x000776EA
		// (set) Token: 0x06002255 RID: 8789 RVA: 0x000794F2 File Offset: 0x000776F2
		public string HashName
		{
			get
			{
				return this.HashNameValue;
			}
			set
			{
				this.HashNameValue = value;
				if (this.HashNameValue == null)
				{
					this.HashNameValue = "SHA1";
				}
			}
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x00079510 File Offset: 0x00077710
		public override byte[] GenerateMask(byte[] rgbSeed, int cbReturn)
		{
			HashAlgorithm hashAlgorithm = (HashAlgorithm)CryptoConfig.CreateFromName(this.HashNameValue);
			byte[] array = new byte[4];
			byte[] array2 = new byte[cbReturn];
			uint num = 0U;
			for (int i = 0; i < array2.Length; i += hashAlgorithm.Hash.Length)
			{
				Utils.ConvertIntToByteArray(num++, ref array);
				hashAlgorithm.TransformBlock(rgbSeed, 0, rgbSeed.Length, rgbSeed, 0);
				hashAlgorithm.TransformFinalBlock(array, 0, 4);
				byte[] hash = hashAlgorithm.Hash;
				hashAlgorithm.Initialize();
				if (array2.Length - i > hash.Length)
				{
					Buffer.BlockCopy(hash, 0, array2, i, hash.Length);
				}
				else
				{
					Buffer.BlockCopy(hash, 0, array2, i, array2.Length - i);
				}
			}
			return array2;
		}

		// Token: 0x04000C79 RID: 3193
		private string HashNameValue;
	}
}
