using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000296 RID: 662
	[ComVisible(true)]
	public abstract class SHA384 : HashAlgorithm
	{
		// Token: 0x0600236A RID: 9066 RVA: 0x000802F6 File Offset: 0x0007E4F6
		protected SHA384()
		{
			this.HashSizeValue = 384;
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x00080309 File Offset: 0x0007E509
		public new static SHA384 Create()
		{
			return SHA384.Create("System.Security.Cryptography.SHA384");
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x00080315 File Offset: 0x0007E515
		public new static SHA384 Create(string hashName)
		{
			return (SHA384)CryptoConfig.CreateFromName(hashName);
		}
	}
}
