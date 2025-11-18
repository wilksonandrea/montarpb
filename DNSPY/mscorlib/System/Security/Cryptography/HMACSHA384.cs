using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000267 RID: 615
	[ComVisible(true)]
	public class HMACSHA384 : HMAC
	{
		// Token: 0x060021CC RID: 8652 RVA: 0x00077968 File Offset: 0x00075B68
		public HMACSHA384()
			: this(Utils.GenerateRandom(128))
		{
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x0007797C File Offset: 0x00075B7C
		[SecuritySafeCritical]
		public HMACSHA384(byte[] key)
		{
			this.m_hashName = "SHA384";
			this.HashSizeValue = 384;
			base.BlockSizeValue = this.BlockSize;
			if (base.GetType() == typeof(HMACSHA384) && !this.m_useLegacyBlockSize)
			{
				this.m_impl = new NativeHmac(CapiNative.AlgorithmID.Sha384);
			}
			else
			{
				this.m_hash1 = this.InstantiateHash();
				this.m_hash2 = this.InstantiateHash();
			}
			base.InitializeKey(key);
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060021CE RID: 8654 RVA: 0x00077A0C File Offset: 0x00075C0C
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

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060021CF RID: 8655 RVA: 0x00077A1E File Offset: 0x00075C1E
		// (set) Token: 0x060021D0 RID: 8656 RVA: 0x00077A28 File Offset: 0x00075C28
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

		// Token: 0x060021D1 RID: 8657 RVA: 0x00077AAC File Offset: 0x00075CAC
		internal sealed override HashAlgorithm InstantiateHash()
		{
			return HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA384Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA384CryptoServiceProvider"));
		}

		// Token: 0x04000C53 RID: 3155
		private bool m_useLegacyBlockSize = Utils._ProduceLegacyHmacValues();

		// Token: 0x02000B49 RID: 2889
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006B9C RID: 27548 RVA: 0x0017458D File Offset: 0x0017278D
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006B9D RID: 27549 RVA: 0x00174599 File Offset: 0x00172799
			public <>c()
			{
			}

			// Token: 0x06006B9E RID: 27550 RVA: 0x001745A1 File Offset: 0x001727A1
			internal HashAlgorithm <InstantiateHash>b__8_0()
			{
				return new SHA384Managed();
			}

			// Token: 0x06006B9F RID: 27551 RVA: 0x001745A8 File Offset: 0x001727A8
			internal HashAlgorithm <InstantiateHash>b__8_1()
			{
				return HashAlgorithm.Create("System.Security.Cryptography.SHA384CryptoServiceProvider");
			}

			// Token: 0x040033C5 RID: 13253
			public static readonly HMACSHA384.<>c <>9 = new HMACSHA384.<>c();

			// Token: 0x040033C6 RID: 13254
			public static Func<HashAlgorithm> <>9__8_0;

			// Token: 0x040033C7 RID: 13255
			public static Func<HashAlgorithm> <>9__8_1;
		}
	}
}
