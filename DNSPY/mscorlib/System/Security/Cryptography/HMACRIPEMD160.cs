using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000264 RID: 612
	[ComVisible(true)]
	public class HMACRIPEMD160 : HMAC
	{
		// Token: 0x060021C3 RID: 8643 RVA: 0x0007779E File Offset: 0x0007599E
		public HMACRIPEMD160()
			: this(Utils.GenerateRandom(64))
		{
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000777AD File Offset: 0x000759AD
		public HMACRIPEMD160(byte[] key)
		{
			this.m_hashName = "RIPEMD160";
			this.m_hash1 = new RIPEMD160Managed();
			this.m_hash2 = new RIPEMD160Managed();
			this.HashSizeValue = 160;
			base.InitializeKey(key);
		}
	}
}
