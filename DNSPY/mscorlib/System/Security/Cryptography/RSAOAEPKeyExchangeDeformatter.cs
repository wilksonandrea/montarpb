using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000284 RID: 644
	[ComVisible(true)]
	public class RSAOAEPKeyExchangeDeformatter : AsymmetricKeyExchangeDeformatter
	{
		// Token: 0x060022F4 RID: 8948 RVA: 0x0007D8A9 File Offset: 0x0007BAA9
		public RSAOAEPKeyExchangeDeformatter()
		{
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x0007D8B1 File Offset: 0x0007BAB1
		public RSAOAEPKeyExchangeDeformatter(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060022F6 RID: 8950 RVA: 0x0007D8D3 File Offset: 0x0007BAD3
		// (set) Token: 0x060022F7 RID: 8951 RVA: 0x0007D8D6 File Offset: 0x0007BAD6
		public override string Parameters
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x0007D8D8 File Offset: 0x0007BAD8
		[SecuritySafeCritical]
		public override byte[] DecryptKeyExchange(byte[] rgbData)
		{
			if (this._rsaKey == null)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_MissingKey"));
			}
			if (this.OverridesDecrypt)
			{
				return this._rsaKey.Decrypt(rgbData, RSAEncryptionPadding.OaepSHA1);
			}
			return Utils.RsaOaepDecrypt(this._rsaKey, SHA1.Create(), new PKCS1MaskGenerationMethod(), rgbData);
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x0007D92D File Offset: 0x0007BB2D
		public override void SetKey(AsymmetricAlgorithm key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this._rsaKey = (RSA)key;
			this._rsaOverridesDecrypt = null;
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x060022FA RID: 8954 RVA: 0x0007D958 File Offset: 0x0007BB58
		private bool OverridesDecrypt
		{
			get
			{
				if (this._rsaOverridesDecrypt == null)
				{
					this._rsaOverridesDecrypt = new bool?(Utils.DoesRsaKeyOverride(this._rsaKey, "Decrypt", new Type[]
					{
						typeof(byte[]),
						typeof(RSAEncryptionPadding)
					}));
				}
				return this._rsaOverridesDecrypt.Value;
			}
		}

		// Token: 0x04000CB4 RID: 3252
		private RSA _rsaKey;

		// Token: 0x04000CB5 RID: 3253
		private bool? _rsaOverridesDecrypt;
	}
}
