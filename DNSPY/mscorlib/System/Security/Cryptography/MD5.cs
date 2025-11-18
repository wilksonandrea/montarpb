using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000271 RID: 625
	[ComVisible(true)]
	public abstract class MD5 : HashAlgorithm
	{
		// Token: 0x0600222A RID: 8746 RVA: 0x00078A49 File Offset: 0x00076C49
		protected MD5()
		{
			this.HashSizeValue = 128;
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x00078A5C File Offset: 0x00076C5C
		public new static MD5 Create()
		{
			return MD5.Create("System.Security.Cryptography.MD5");
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x00078A68 File Offset: 0x00076C68
		public new static MD5 Create(string algName)
		{
			return (MD5)CryptoConfig.CreateFromName(algName);
		}
	}
}
