using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000265 RID: 613
	[ComVisible(true)]
	public class HMACSHA1 : HMAC
	{
		// Token: 0x060021C5 RID: 8645 RVA: 0x000777E8 File Offset: 0x000759E8
		public HMACSHA1()
			: this(Utils.GenerateRandom(64))
		{
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x000777F7 File Offset: 0x000759F7
		public HMACSHA1(byte[] key)
			: this(key, false)
		{
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x00077804 File Offset: 0x00075A04
		public HMACSHA1(byte[] key, bool useManagedSha1)
		{
			this.m_hashName = "SHA1";
			this.HashSizeValue = 160;
			if (base.GetType() == typeof(HMACSHA1))
			{
				this.m_impl = new NativeHmac(CapiNative.AlgorithmID.Sha1);
			}
			else if (useManagedSha1)
			{
				this.m_hash1 = new SHA1Managed();
				this.m_hash2 = new SHA1Managed();
			}
			else
			{
				this.m_hash1 = new SHA1CryptoServiceProvider();
				this.m_hash2 = new SHA1CryptoServiceProvider();
			}
			base.InitializeKey(key);
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x0007788E File Offset: 0x00075A8E
		internal sealed override HashAlgorithm InstantiateHash()
		{
			return new SHA1CryptoServiceProvider();
		}
	}
}
