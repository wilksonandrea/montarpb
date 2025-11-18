using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200029B RID: 667
	internal abstract class RSAPKCS1SignatureDescription : SignatureDescription
	{
		// Token: 0x06002389 RID: 9097 RVA: 0x00080598 File Offset: 0x0007E798
		protected RSAPKCS1SignatureDescription(string hashAlgorithm, string digestAlgorithm)
		{
			base.KeyAlgorithm = "System.Security.Cryptography.RSA";
			base.DigestAlgorithm = digestAlgorithm;
			base.FormatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureFormatter";
			base.DeformatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureDeformatter";
			this._hashAlgorithm = hashAlgorithm;
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x000805D0 File Offset: 0x0007E7D0
		public sealed override AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = base.CreateDeformatter(key);
			asymmetricSignatureDeformatter.SetHashAlgorithm(this._hashAlgorithm);
			return asymmetricSignatureDeformatter;
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x000805F4 File Offset: 0x0007E7F4
		public sealed override AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
		{
			AsymmetricSignatureFormatter asymmetricSignatureFormatter = base.CreateFormatter(key);
			asymmetricSignatureFormatter.SetHashAlgorithm(this._hashAlgorithm);
			return asymmetricSignatureFormatter;
		}

		// Token: 0x04000CEF RID: 3311
		private string _hashAlgorithm;
	}
}
