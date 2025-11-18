using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000263 RID: 611
	[ComVisible(true)]
	public class HMACMD5 : HMAC
	{
		// Token: 0x060021C0 RID: 8640 RVA: 0x00077718 File Offset: 0x00075918
		public HMACMD5()
			: this(Utils.GenerateRandom(64))
		{
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x00077728 File Offset: 0x00075928
		public HMACMD5(byte[] key)
		{
			this.m_hashName = "MD5";
			this.HashSizeValue = 128;
			if (base.GetType() == typeof(HMACMD5))
			{
				this.m_impl = new NativeHmac(CapiNative.AlgorithmID.Md5);
			}
			else
			{
				this.m_hash1 = new MD5CryptoServiceProvider();
				this.m_hash2 = new MD5CryptoServiceProvider();
			}
			base.InitializeKey(key);
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x00077797 File Offset: 0x00075997
		internal sealed override HashAlgorithm InstantiateHash()
		{
			return new MD5CryptoServiceProvider();
		}
	}
}
