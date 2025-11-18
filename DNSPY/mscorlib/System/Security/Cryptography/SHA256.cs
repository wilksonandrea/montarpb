using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000294 RID: 660
	[ComVisible(true)]
	public abstract class SHA256 : HashAlgorithm
	{
		// Token: 0x06002361 RID: 9057 RVA: 0x0008022F File Offset: 0x0007E42F
		protected SHA256()
		{
			this.HashSizeValue = 256;
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x00080242 File Offset: 0x0007E442
		public new static SHA256 Create()
		{
			return SHA256.Create("System.Security.Cryptography.SHA256");
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x0008024E File Offset: 0x0007E44E
		public new static SHA256 Create(string hashName)
		{
			return (SHA256)CryptoConfig.CreateFromName(hashName);
		}
	}
}
