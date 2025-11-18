using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000262 RID: 610
	[ComVisible(true)]
	public abstract class HMAC : KeyedHashAlgorithm
	{
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x0007723B File Offset: 0x0007543B
		// (set) Token: 0x060021AF RID: 8623 RVA: 0x00077243 File Offset: 0x00075443
		protected int BlockSizeValue
		{
			get
			{
				return this.blockSizeValue;
			}
			set
			{
				this.blockSizeValue = value;
			}
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x0007724C File Offset: 0x0007544C
		private void UpdateIOPadBuffers()
		{
			if (this.m_inner == null)
			{
				this.m_inner = new byte[this.BlockSizeValue];
			}
			if (this.m_outer == null)
			{
				this.m_outer = new byte[this.BlockSizeValue];
			}
			for (int i = 0; i < this.BlockSizeValue; i++)
			{
				this.m_inner[i] = 54;
				this.m_outer[i] = 92;
			}
			for (int i = 0; i < this.KeyValue.Length; i++)
			{
				byte[] inner = this.m_inner;
				int num = i;
				inner[num] ^= this.KeyValue[i];
				byte[] outer = this.m_outer;
				int num2 = i;
				outer[num2] ^= this.KeyValue[i];
			}
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x000772F8 File Offset: 0x000754F8
		internal void InitializeKey(byte[] key)
		{
			this.m_inner = null;
			this.m_outer = null;
			if (key.Length > this.BlockSizeValue)
			{
				if (this.m_hash1 == null)
				{
					this.m_hash1 = this.InstantiateHash();
				}
				this.KeyValue = this.m_hash1.ComputeHash(key);
			}
			else
			{
				this.KeyValue = (byte[])key.Clone();
			}
			if (this.m_impl == null)
			{
				this.UpdateIOPadBuffers();
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x00077365 File Offset: 0x00075565
		// (set) Token: 0x060021B3 RID: 8627 RVA: 0x00077377 File Offset: 0x00075577
		public override byte[] Key
		{
			get
			{
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (this.m_hashing)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_HashKeySet"));
				}
				this.InitializeKey(value);
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x00077398 File Offset: 0x00075598
		// (set) Token: 0x060021B5 RID: 8629 RVA: 0x000773A0 File Offset: 0x000755A0
		public string HashName
		{
			get
			{
				return this.m_hashName;
			}
			set
			{
				if (this.m_hashing)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_HashNameSet"));
				}
				if (this.m_impl != null && !string.Equals(this.m_hashName, value, StringComparison.OrdinalIgnoreCase))
				{
					this.ClearImpl();
				}
				this.m_hashName = value;
				if (this.m_impl == null)
				{
					this.m_hash1 = HashAlgorithm.Create(this.m_hashName);
					this.m_hash2 = HashAlgorithm.Create(this.m_hashName);
				}
			}
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x00077413 File Offset: 0x00075613
		public new static HMAC Create()
		{
			return HMAC.Create("System.Security.Cryptography.HMAC");
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x0007741F File Offset: 0x0007561F
		public new static HMAC Create(string algorithmName)
		{
			return (HMAC)CryptoConfig.CreateFromName(algorithmName);
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x0007742C File Offset: 0x0007562C
		public override void Initialize()
		{
			if (this.m_impl != null)
			{
				this.m_impl.Reset();
			}
			else
			{
				this.m_hash1.Initialize();
				this.m_hash2.Initialize();
			}
			this.m_hashing = false;
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x00077460 File Offset: 0x00075660
		protected override void HashCore(byte[] rgb, int ib, int cb)
		{
			if (this.m_impl != null)
			{
				if (!this.m_hashing)
				{
					this.m_impl.SetKey(this.KeyValue);
					this.m_hashing = true;
				}
				if (cb > 0)
				{
					this.m_impl.AppendData(rgb, ib, cb);
					return;
				}
			}
			else
			{
				if (!this.m_hashing)
				{
					this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
					this.m_hashing = true;
				}
				this.m_hash1.TransformBlock(rgb, ib, cb, rgb, ib);
			}
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x000774EC File Offset: 0x000756EC
		protected override byte[] HashFinal()
		{
			if (this.m_impl != null)
			{
				if (!this.m_hashing)
				{
					this.m_impl.SetKey(this.KeyValue);
				}
				byte[] array = new byte[this.HashSizeValue >> 3];
				this.m_impl.Finish(array);
				this.m_hashing = false;
				return array;
			}
			if (!this.m_hashing)
			{
				this.m_hash1.TransformBlock(this.m_inner, 0, this.m_inner.Length, this.m_inner, 0);
				this.m_hashing = true;
			}
			this.m_hash1.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			byte[] hashValue = this.m_hash1.HashValue;
			this.m_hash2.TransformBlock(this.m_outer, 0, this.m_outer.Length, this.m_outer, 0);
			this.m_hash2.TransformBlock(hashValue, 0, hashValue.Length, hashValue, 0);
			this.m_hashing = false;
			this.m_hash2.TransformFinalBlock(EmptyArray<byte>.Value, 0, 0);
			return this.m_hash2.HashValue;
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x000775E8 File Offset: 0x000757E8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_impl != null)
				{
					this.m_impl.Dispose();
				}
				if (this.m_hash1 != null)
				{
					((IDisposable)this.m_hash1).Dispose();
				}
				if (this.m_hash2 != null)
				{
					((IDisposable)this.m_hash2).Dispose();
				}
				if (this.m_inner != null)
				{
					Array.Clear(this.m_inner, 0, this.m_inner.Length);
				}
				if (this.m_outer != null)
				{
					Array.Clear(this.m_outer, 0, this.m_outer.Length);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x00077670 File Offset: 0x00075870
		internal virtual HashAlgorithm InstantiateHash()
		{
			return null;
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x00077674 File Offset: 0x00075874
		internal void ClearImpl()
		{
			if (this.m_impl == null)
			{
				return;
			}
			this.m_impl.Dispose();
			this.m_impl = null;
			this.m_hash1 = this.InstantiateHash();
			this.m_hash2 = this.InstantiateHash();
			if (this.KeyValue != null)
			{
				this.UpdateIOPadBuffers();
			}
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x000776C4 File Offset: 0x000758C4
		internal static HashAlgorithm GetHashAlgorithmWithFipsFallback(Func<HashAlgorithm> createStandardHashAlgorithmCallback, Func<HashAlgorithm> createFipsHashAlgorithmCallback)
		{
			if (CryptoConfig.AllowOnlyFipsAlgorithms)
			{
				try
				{
					return createFipsHashAlgorithmCallback();
				}
				catch (PlatformNotSupportedException ex)
				{
					throw new InvalidOperationException(ex.Message, ex);
				}
			}
			return createStandardHashAlgorithmCallback();
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x00077708 File Offset: 0x00075908
		protected HMAC()
		{
		}

		// Token: 0x04000C4B RID: 3147
		private int blockSizeValue = 64;

		// Token: 0x04000C4C RID: 3148
		internal string m_hashName;

		// Token: 0x04000C4D RID: 3149
		internal HashAlgorithm m_hash1;

		// Token: 0x04000C4E RID: 3150
		internal HashAlgorithm m_hash2;

		// Token: 0x04000C4F RID: 3151
		internal NativeHmac m_impl;

		// Token: 0x04000C50 RID: 3152
		private byte[] m_inner;

		// Token: 0x04000C51 RID: 3153
		private byte[] m_outer;

		// Token: 0x04000C52 RID: 3154
		internal bool m_hashing;
	}
}
