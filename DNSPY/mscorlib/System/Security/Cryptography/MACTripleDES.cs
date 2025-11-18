using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200026F RID: 623
	[ComVisible(true)]
	public class MACTripleDES : KeyedHashAlgorithm
	{
		// Token: 0x06002212 RID: 8722 RVA: 0x0007854C File Offset: 0x0007674C
		public MACTripleDES()
		{
			this.KeyValue = new byte[24];
			Utils.StaticRandomNumberGenerator.GetBytes(this.KeyValue);
			this.des = TripleDES.Create();
			this.HashSizeValue = this.des.BlockSize;
			this.m_bytesPerBlock = this.des.BlockSize / 8;
			this.des.IV = new byte[this.m_bytesPerBlock];
			this.des.Padding = PaddingMode.Zeros;
			this.m_encryptor = null;
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x000785D4 File Offset: 0x000767D4
		public MACTripleDES(byte[] rgbKey)
			: this("System.Security.Cryptography.TripleDES", rgbKey)
		{
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x000785E4 File Offset: 0x000767E4
		public MACTripleDES(string strTripleDES, byte[] rgbKey)
		{
			if (rgbKey == null)
			{
				throw new ArgumentNullException("rgbKey");
			}
			if (strTripleDES == null)
			{
				this.des = TripleDES.Create();
			}
			else
			{
				this.des = TripleDES.Create(strTripleDES);
			}
			this.HashSizeValue = this.des.BlockSize;
			this.KeyValue = (byte[])rgbKey.Clone();
			this.m_bytesPerBlock = this.des.BlockSize / 8;
			this.des.IV = new byte[this.m_bytesPerBlock];
			this.des.Padding = PaddingMode.Zeros;
			this.m_encryptor = null;
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x0007867F File Offset: 0x0007687F
		public override void Initialize()
		{
			this.m_encryptor = null;
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x00078688 File Offset: 0x00076888
		// (set) Token: 0x06002217 RID: 8727 RVA: 0x00078695 File Offset: 0x00076895
		[ComVisible(false)]
		public PaddingMode Padding
		{
			get
			{
				return this.des.Padding;
			}
			set
			{
				if (value < PaddingMode.None || PaddingMode.ISO10126 < value)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidPaddingMode"));
				}
				this.des.Padding = value;
			}
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000786BC File Offset: 0x000768BC
		protected override void HashCore(byte[] rgbData, int ibStart, int cbSize)
		{
			if (this.m_encryptor == null)
			{
				this.des.Key = this.Key;
				this.m_encryptor = this.des.CreateEncryptor();
				this._ts = new TailStream(this.des.BlockSize / 8);
				this._cs = new CryptoStream(this._ts, this.m_encryptor, CryptoStreamMode.Write);
			}
			this._cs.Write(rgbData, ibStart, cbSize);
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x00078734 File Offset: 0x00076934
		protected override byte[] HashFinal()
		{
			if (this.m_encryptor == null)
			{
				this.des.Key = this.Key;
				this.m_encryptor = this.des.CreateEncryptor();
				this._ts = new TailStream(this.des.BlockSize / 8);
				this._cs = new CryptoStream(this._ts, this.m_encryptor, CryptoStreamMode.Write);
			}
			this._cs.FlushFinalBlock();
			return this._ts.Buffer;
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x000787B4 File Offset: 0x000769B4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.des != null)
				{
					this.des.Clear();
				}
				if (this.m_encryptor != null)
				{
					this.m_encryptor.Dispose();
				}
				if (this._cs != null)
				{
					this._cs.Clear();
				}
				if (this._ts != null)
				{
					this._ts.Clear();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000C60 RID: 3168
		private ICryptoTransform m_encryptor;

		// Token: 0x04000C61 RID: 3169
		private CryptoStream _cs;

		// Token: 0x04000C62 RID: 3170
		private TailStream _ts;

		// Token: 0x04000C63 RID: 3171
		private const int m_bitsPerByte = 8;

		// Token: 0x04000C64 RID: 3172
		private int m_bytesPerBlock;

		// Token: 0x04000C65 RID: 3173
		private TripleDES des;
	}
}
