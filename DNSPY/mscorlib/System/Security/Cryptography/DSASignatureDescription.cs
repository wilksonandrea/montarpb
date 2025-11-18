using System;

namespace System.Security.Cryptography
{
	// Token: 0x020002A0 RID: 672
	internal class DSASignatureDescription : SignatureDescription
	{
		// Token: 0x06002390 RID: 9104 RVA: 0x0008065E File Offset: 0x0007E85E
		public DSASignatureDescription()
		{
			base.KeyAlgorithm = "System.Security.Cryptography.DSACryptoServiceProvider";
			base.DigestAlgorithm = "System.Security.Cryptography.SHA1CryptoServiceProvider";
			base.FormatterAlgorithm = "System.Security.Cryptography.DSASignatureFormatter";
			base.DeformatterAlgorithm = "System.Security.Cryptography.DSASignatureDeformatter";
		}
	}
}
