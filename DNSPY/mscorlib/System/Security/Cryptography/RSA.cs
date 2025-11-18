using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Util;
using System.Text;

namespace System.Security.Cryptography
{
	// Token: 0x0200027D RID: 637
	[ComVisible(true)]
	public abstract class RSA : AsymmetricAlgorithm
	{
		// Token: 0x0600228E RID: 8846 RVA: 0x0007C4A0 File Offset: 0x0007A6A0
		protected RSA()
		{
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x0007C4A8 File Offset: 0x0007A6A8
		public new static RSA Create()
		{
			return RSA.Create("System.Security.Cryptography.RSA");
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x0007C4B4 File Offset: 0x0007A6B4
		public new static RSA Create(string algName)
		{
			return (RSA)CryptoConfig.CreateFromName(algName);
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0007C4C4 File Offset: 0x0007A6C4
		public static RSA Create(int keySizeInBits)
		{
			RSA rsa = (RSA)CryptoConfig.CreateFromName("RSAPSS");
			rsa.KeySize = keySizeInBits;
			if (rsa.KeySize != keySizeInBits)
			{
				throw new CryptographicException();
			}
			return rsa;
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x0007C4F8 File Offset: 0x0007A6F8
		public static RSA Create(RSAParameters parameters)
		{
			RSA rsa = (RSA)CryptoConfig.CreateFromName("RSAPSS");
			rsa.ImportParameters(parameters);
			return rsa;
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x0007C51D File Offset: 0x0007A71D
		public virtual byte[] Encrypt(byte[] data, RSAEncryptionPadding padding)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x0007C524 File Offset: 0x0007A724
		public virtual byte[] Decrypt(byte[] data, RSAEncryptionPadding padding)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x0007C52B File Offset: 0x0007A72B
		public virtual byte[] SignHash(byte[] hash, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x0007C532 File Offset: 0x0007A732
		public virtual bool VerifyHash(byte[] hash, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x0007C539 File Offset: 0x0007A739
		protected virtual byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x0007C540 File Offset: 0x0007A740
		protected virtual byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			throw RSA.DerivedClassMustOverride();
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x0007C547 File Offset: 0x0007A747
		public byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.SignData(data, 0, data.Length, hashAlgorithm, padding);
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x0007C564 File Offset: 0x0007A764
		public virtual byte[] SignData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0 || offset > data.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > data.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			byte[] array = this.HashData(data, offset, count, hashAlgorithm);
			return this.SignHash(array, hashAlgorithm, padding);
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x0007C5EC File Offset: 0x0007A7EC
		public virtual byte[] SignData(Stream data, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			byte[] array = this.HashData(data, hashAlgorithm);
			return this.SignHash(array, hashAlgorithm, padding);
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x0007C641 File Offset: 0x0007A841
		public bool VerifyData(byte[] data, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			return this.VerifyData(data, 0, data.Length, signature, hashAlgorithm, padding);
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x0007C660 File Offset: 0x0007A860
		public virtual bool VerifyData(byte[] data, int offset, int count, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (offset < 0 || offset > data.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > data.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			byte[] array = this.HashData(data, offset, count, hashAlgorithm);
			return this.VerifyHash(array, signature, hashAlgorithm, padding);
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x0007C6F8 File Offset: 0x0007A8F8
		public bool VerifyData(Stream data, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw RSA.HashAlgorithmNameNullOrEmpty();
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			byte[] array = this.HashData(data, hashAlgorithm);
			return this.VerifyHash(array, signature, hashAlgorithm, padding);
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x0007C75E File Offset: 0x0007A95E
		private static Exception DerivedClassMustOverride()
		{
			return new NotImplementedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x0007C76F File Offset: 0x0007A96F
		internal static Exception HashAlgorithmNameNullOrEmpty()
		{
			return new ArgumentException(Environment.GetResourceString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x0007C785 File Offset: 0x0007A985
		public virtual byte[] DecryptValue(byte[] rgb)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0007C796 File Offset: 0x0007A996
		public virtual byte[] EncryptValue(byte[] rgb)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060022A3 RID: 8867 RVA: 0x0007C7A7 File Offset: 0x0007A9A7
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "RSA";
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x0007C7AE File Offset: 0x0007A9AE
		public override string SignatureAlgorithm
		{
			get
			{
				return "RSA";
			}
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x0007C7B8 File Offset: 0x0007A9B8
		public override void FromXmlString(string xmlString)
		{
			if (xmlString == null)
			{
				throw new ArgumentNullException("xmlString");
			}
			RSAParameters rsaparameters = default(RSAParameters);
			Parser parser = new Parser(xmlString);
			SecurityElement topElement = parser.GetTopElement();
			string text = topElement.SearchForTextOfLocalName("Modulus");
			if (text == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", new object[] { "RSA", "Modulus" }));
			}
			rsaparameters.Modulus = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text));
			string text2 = topElement.SearchForTextOfLocalName("Exponent");
			if (text2 == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidFromXmlString", new object[] { "RSA", "Exponent" }));
			}
			rsaparameters.Exponent = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text2));
			string text3 = topElement.SearchForTextOfLocalName("P");
			if (text3 != null)
			{
				rsaparameters.P = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text3));
			}
			string text4 = topElement.SearchForTextOfLocalName("Q");
			if (text4 != null)
			{
				rsaparameters.Q = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text4));
			}
			string text5 = topElement.SearchForTextOfLocalName("DP");
			if (text5 != null)
			{
				rsaparameters.DP = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text5));
			}
			string text6 = topElement.SearchForTextOfLocalName("DQ");
			if (text6 != null)
			{
				rsaparameters.DQ = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text6));
			}
			string text7 = topElement.SearchForTextOfLocalName("InverseQ");
			if (text7 != null)
			{
				rsaparameters.InverseQ = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text7));
			}
			string text8 = topElement.SearchForTextOfLocalName("D");
			if (text8 != null)
			{
				rsaparameters.D = Convert.FromBase64String(Utils.DiscardWhiteSpaces(text8));
			}
			this.ImportParameters(rsaparameters);
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x0007C95C File Offset: 0x0007AB5C
		public override string ToXmlString(bool includePrivateParameters)
		{
			RSAParameters rsaparameters = this.ExportParameters(includePrivateParameters);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<RSAKeyValue>");
			stringBuilder.Append("<Modulus>" + Convert.ToBase64String(rsaparameters.Modulus) + "</Modulus>");
			stringBuilder.Append("<Exponent>" + Convert.ToBase64String(rsaparameters.Exponent) + "</Exponent>");
			if (includePrivateParameters)
			{
				stringBuilder.Append("<P>" + Convert.ToBase64String(rsaparameters.P) + "</P>");
				stringBuilder.Append("<Q>" + Convert.ToBase64String(rsaparameters.Q) + "</Q>");
				stringBuilder.Append("<DP>" + Convert.ToBase64String(rsaparameters.DP) + "</DP>");
				stringBuilder.Append("<DQ>" + Convert.ToBase64String(rsaparameters.DQ) + "</DQ>");
				stringBuilder.Append("<InverseQ>" + Convert.ToBase64String(rsaparameters.InverseQ) + "</InverseQ>");
				stringBuilder.Append("<D>" + Convert.ToBase64String(rsaparameters.D) + "</D>");
			}
			stringBuilder.Append("</RSAKeyValue>");
			return stringBuilder.ToString();
		}

		// Token: 0x060022A7 RID: 8871
		public abstract RSAParameters ExportParameters(bool includePrivateParameters);

		// Token: 0x060022A8 RID: 8872
		public abstract void ImportParameters(RSAParameters parameters);
	}
}
