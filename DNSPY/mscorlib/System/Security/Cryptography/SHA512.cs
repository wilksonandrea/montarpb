using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000298 RID: 664
	[ComVisible(true)]
	public abstract class SHA512 : HashAlgorithm
	{
		// Token: 0x06002373 RID: 9075 RVA: 0x000803BD File Offset: 0x0007E5BD
		protected SHA512()
		{
			this.HashSizeValue = 512;
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000803D0 File Offset: 0x0007E5D0
		public new static SHA512 Create()
		{
			return SHA512.Create("System.Security.Cryptography.SHA512");
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000803DC File Offset: 0x0007E5DC
		public new static SHA512 Create(string hashName)
		{
			return (SHA512)CryptoConfig.CreateFromName(hashName);
		}
	}
}
