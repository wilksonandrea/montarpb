using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000282 RID: 642
	public sealed class RSAEncryptionPadding : IEquatable<RSAEncryptionPadding>
	{
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x0007D729 File Offset: 0x0007B929
		public static RSAEncryptionPadding Pkcs1
		{
			get
			{
				return RSAEncryptionPadding.s_pkcs1;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060022E4 RID: 8932 RVA: 0x0007D730 File Offset: 0x0007B930
		public static RSAEncryptionPadding OaepSHA1
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA1;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x0007D737 File Offset: 0x0007B937
		public static RSAEncryptionPadding OaepSHA256
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA256;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060022E6 RID: 8934 RVA: 0x0007D73E File Offset: 0x0007B93E
		public static RSAEncryptionPadding OaepSHA384
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA384;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060022E7 RID: 8935 RVA: 0x0007D745 File Offset: 0x0007B945
		public static RSAEncryptionPadding OaepSHA512
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA512;
			}
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x0007D74C File Offset: 0x0007B94C
		private RSAEncryptionPadding(RSAEncryptionPaddingMode mode, HashAlgorithmName oaepHashAlgorithm)
		{
			this._mode = mode;
			this._oaepHashAlgorithm = oaepHashAlgorithm;
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x0007D762 File Offset: 0x0007B962
		public static RSAEncryptionPadding CreateOaep(HashAlgorithmName hashAlgorithm)
		{
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException(Environment.GetResourceString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
			}
			return new RSAEncryptionPadding(RSAEncryptionPaddingMode.Oaep, hashAlgorithm);
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x0007D78E File Offset: 0x0007B98E
		public RSAEncryptionPaddingMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x0007D796 File Offset: 0x0007B996
		public HashAlgorithmName OaepHashAlgorithm
		{
			get
			{
				return this._oaepHashAlgorithm;
			}
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x0007D79E File Offset: 0x0007B99E
		public override int GetHashCode()
		{
			return RSAEncryptionPadding.CombineHashCodes(this._mode.GetHashCode(), this._oaepHashAlgorithm.GetHashCode());
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x0007D7C7 File Offset: 0x0007B9C7
		private static int CombineHashCodes(int h1, int h2)
		{
			return ((h1 << 5) + h1) ^ h2;
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x0007D7D0 File Offset: 0x0007B9D0
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RSAEncryptionPadding);
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x0007D7DE File Offset: 0x0007B9DE
		public bool Equals(RSAEncryptionPadding other)
		{
			return other != null && this._mode == other._mode && this._oaepHashAlgorithm == other._oaepHashAlgorithm;
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x0007D80A File Offset: 0x0007BA0A
		public static bool operator ==(RSAEncryptionPadding left, RSAEncryptionPadding right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x0007D81B File Offset: 0x0007BA1B
		public static bool operator !=(RSAEncryptionPadding left, RSAEncryptionPadding right)
		{
			return !(left == right);
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x0007D827 File Offset: 0x0007BA27
		public override string ToString()
		{
			return this._mode.ToString() + this._oaepHashAlgorithm.Name;
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x0007D84C File Offset: 0x0007BA4C
		// Note: this type is marked as 'beforefieldinit'.
		static RSAEncryptionPadding()
		{
		}

		// Token: 0x04000CAA RID: 3242
		private static readonly RSAEncryptionPadding s_pkcs1 = new RSAEncryptionPadding(RSAEncryptionPaddingMode.Pkcs1, default(HashAlgorithmName));

		// Token: 0x04000CAB RID: 3243
		private static readonly RSAEncryptionPadding s_oaepSHA1 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA1);

		// Token: 0x04000CAC RID: 3244
		private static readonly RSAEncryptionPadding s_oaepSHA256 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA256);

		// Token: 0x04000CAD RID: 3245
		private static readonly RSAEncryptionPadding s_oaepSHA384 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA384);

		// Token: 0x04000CAE RID: 3246
		private static readonly RSAEncryptionPadding s_oaepSHA512 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA512);

		// Token: 0x04000CAF RID: 3247
		private RSAEncryptionPaddingMode _mode;

		// Token: 0x04000CB0 RID: 3248
		private HashAlgorithmName _oaepHashAlgorithm;
	}
}
