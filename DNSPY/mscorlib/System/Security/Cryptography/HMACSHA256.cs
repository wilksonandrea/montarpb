using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000266 RID: 614
	[ComVisible(true)]
	public class HMACSHA256 : HMAC
	{
		// Token: 0x060021C9 RID: 8649 RVA: 0x00077895 File Offset: 0x00075A95
		public HMACSHA256()
			: this(Utils.GenerateRandom(64))
		{
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x000778A4 File Offset: 0x00075AA4
		public HMACSHA256(byte[] key)
		{
			this.m_hashName = "SHA256";
			this.HashSizeValue = 256;
			if (base.GetType() == typeof(HMACSHA256))
			{
				this.m_impl = new NativeHmac(CapiNative.AlgorithmID.Sha256);
			}
			else
			{
				this.m_hash1 = this.InstantiateHash();
				this.m_hash2 = this.InstantiateHash();
			}
			base.InitializeKey(key);
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x00077918 File Offset: 0x00075B18
		internal sealed override HashAlgorithm InstantiateHash()
		{
			return HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA256Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider"));
		}

		// Token: 0x02000B48 RID: 2888
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006B98 RID: 27544 RVA: 0x00174566 File Offset: 0x00172766
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006B99 RID: 27545 RVA: 0x00174572 File Offset: 0x00172772
			public <>c()
			{
			}

			// Token: 0x06006B9A RID: 27546 RVA: 0x0017457A File Offset: 0x0017277A
			internal HashAlgorithm <InstantiateHash>b__2_0()
			{
				return new SHA256Managed();
			}

			// Token: 0x06006B9B RID: 27547 RVA: 0x00174581 File Offset: 0x00172781
			internal HashAlgorithm <InstantiateHash>b__2_1()
			{
				return HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider");
			}

			// Token: 0x040033C2 RID: 13250
			public static readonly HMACSHA256.<>c <>9 = new HMACSHA256.<>c();

			// Token: 0x040033C3 RID: 13251
			public static Func<HashAlgorithm> <>9__2_0;

			// Token: 0x040033C4 RID: 13252
			public static Func<HashAlgorithm> <>9__2_1;
		}
	}
}
