using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000291 RID: 657
	[ComVisible(true)]
	public abstract class SHA1 : HashAlgorithm
	{
		// Token: 0x06002354 RID: 9044 RVA: 0x000800B9 File Offset: 0x0007E2B9
		protected SHA1()
		{
			this.HashSizeValue = 160;
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x000800CC File Offset: 0x0007E2CC
		public new static SHA1 Create()
		{
			return SHA1.Create("System.Security.Cryptography.SHA1");
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000800D8 File Offset: 0x0007E2D8
		public new static SHA1 Create(string hashName)
		{
			return (SHA1)CryptoConfig.CreateFromName(hashName);
		}
	}
}
