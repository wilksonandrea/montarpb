using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200027A RID: 634
	[ComVisible(true)]
	public abstract class RIPEMD160 : HashAlgorithm
	{
		// Token: 0x0600227E RID: 8830 RVA: 0x00079FE4 File Offset: 0x000781E4
		protected RIPEMD160()
		{
			this.HashSizeValue = 160;
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x00079FF7 File Offset: 0x000781F7
		public new static RIPEMD160 Create()
		{
			return RIPEMD160.Create("System.Security.Cryptography.RIPEMD160");
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x0007A003 File Offset: 0x00078203
		public new static RIPEMD160 Create(string hashName)
		{
			return (RIPEMD160)CryptoConfig.CreateFromName(hashName);
		}
	}
}
