using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000268 RID: 616
	[ComVisible(true)]
	public class HMACSHA512 : HMAC
	{
		// Token: 0x060021D2 RID: 8658 RVA: 0x00077AFC File Offset: 0x00075CFC
		public HMACSHA512()
			: this(Utils.GenerateRandom(128))
		{
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x00077B10 File Offset: 0x00075D10
		[SecuritySafeCritical]
		public HMACSHA512(byte[] key)
		{
			this.m_hashName = "SHA512";
			this.HashSizeValue = 512;
			base.BlockSizeValue = this.BlockSize;
			if (base.GetType() == typeof(HMACSHA512) && !this.m_useLegacyBlockSize)
			{
				this.m_impl = new NativeHmac(CapiNative.AlgorithmID.Sha512);
			}
			else
			{
				this.m_hash1 = this.InstantiateHash();
				this.m_hash2 = this.InstantiateHash();
			}
			base.InitializeKey(key);
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x00077BA0 File Offset: 0x00075DA0
		private int BlockSize
		{
			get
			{
				if (!this.m_useLegacyBlockSize)
				{
					return 128;
				}
				return 64;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x00077BB2 File Offset: 0x00075DB2
		// (set) Token: 0x060021D6 RID: 8662 RVA: 0x00077BBC File Offset: 0x00075DBC
		public bool ProduceLegacyHmacValues
		{
			get
			{
				return this.m_useLegacyBlockSize;
			}
			set
			{
				this.m_useLegacyBlockSize = value;
				if (this.m_impl != null && value)
				{
					if (this.m_hashing)
					{
						throw new CryptographicException(Environment.GetResourceString("Cryptography_HashNameSet"));
					}
					this.m_impl.Dispose();
					this.m_impl = null;
					this.m_hash1 = this.InstantiateHash();
					this.m_hash2 = this.InstantiateHash();
				}
				base.BlockSizeValue = this.BlockSize;
				if (this.m_impl == null)
				{
					base.InitializeKey(this.KeyValue);
				}
			}
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x00077C40 File Offset: 0x00075E40
		internal sealed override HashAlgorithm InstantiateHash()
		{
			return HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA512Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA512CryptoServiceProvider"));
		}

		// Token: 0x04000C54 RID: 3156
		private bool m_useLegacyBlockSize = Utils._ProduceLegacyHmacValues();

		// Token: 0x02000B4A RID: 2890
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006BA0 RID: 27552 RVA: 0x001745B4 File Offset: 0x001727B4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006BA1 RID: 27553 RVA: 0x001745C0 File Offset: 0x001727C0
			public <>c()
			{
			}

			// Token: 0x06006BA2 RID: 27554 RVA: 0x001745C8 File Offset: 0x001727C8
			internal HashAlgorithm <InstantiateHash>b__8_0()
			{
				return new SHA512Managed();
			}

			// Token: 0x06006BA3 RID: 27555 RVA: 0x001745CF File Offset: 0x001727CF
			internal HashAlgorithm <InstantiateHash>b__8_1()
			{
				return HashAlgorithm.Create("System.Security.Cryptography.SHA512CryptoServiceProvider");
			}

			// Token: 0x040033C8 RID: 13256
			public static readonly HMACSHA512.<>c <>9 = new HMACSHA512.<>c();

			// Token: 0x040033C9 RID: 13257
			public static Func<HashAlgorithm> <>9__8_0;

			// Token: 0x040033CA RID: 13258
			public static Func<HashAlgorithm> <>9__8_1;
		}
	}
}
