using System;
using System.IO;

namespace System.Security.Cryptography
{
	// Token: 0x02000270 RID: 624
	internal sealed class TailStream : Stream
	{
		// Token: 0x0600221B RID: 8731 RVA: 0x00078817 File Offset: 0x00076A17
		public TailStream(int bufferSize)
		{
			this._Buffer = new byte[bufferSize];
			this._BufferSize = bufferSize;
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x00078832 File Offset: 0x00076A32
		public void Clear()
		{
			this.Close();
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x0007883C File Offset: 0x00076A3C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this._Buffer != null)
					{
						Array.Clear(this._Buffer, 0, this._Buffer.Length);
					}
					this._Buffer = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x0007888C File Offset: 0x00076A8C
		public byte[] Buffer
		{
			get
			{
				return (byte[])this._Buffer.Clone();
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x0007889E File Offset: 0x00076A9E
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x000788A1 File Offset: 0x00076AA1
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06002221 RID: 8737 RVA: 0x000788A4 File Offset: 0x00076AA4
		public override bool CanWrite
		{
			get
			{
				return this._Buffer != null;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06002222 RID: 8738 RVA: 0x000788AF File Offset: 0x00076AAF
		public override long Length
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x000788C0 File Offset: 0x00076AC0
		// (set) Token: 0x06002224 RID: 8740 RVA: 0x000788D1 File Offset: 0x00076AD1
		public override long Position
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
			}
			set
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
			}
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x000788E2 File Offset: 0x00076AE2
		public override void Flush()
		{
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x000788E4 File Offset: 0x00076AE4
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x000788F5 File Offset: 0x00076AF5
		public override void SetLength(long value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x00078906 File Offset: 0x00076B06
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x00078918 File Offset: 0x00076B18
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._Buffer == null)
			{
				throw new ObjectDisposedException("TailStream");
			}
			if (count == 0)
			{
				return;
			}
			if (this._BufferFull)
			{
				if (count > this._BufferSize)
				{
					System.Buffer.InternalBlockCopy(buffer, offset + count - this._BufferSize, this._Buffer, 0, this._BufferSize);
					return;
				}
				System.Buffer.InternalBlockCopy(this._Buffer, this._BufferSize - count, this._Buffer, 0, this._BufferSize - count);
				System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferSize - count, count);
				return;
			}
			else
			{
				if (count > this._BufferSize)
				{
					System.Buffer.InternalBlockCopy(buffer, offset + count - this._BufferSize, this._Buffer, 0, this._BufferSize);
					this._BufferFull = true;
					return;
				}
				if (count + this._BufferIndex >= this._BufferSize)
				{
					System.Buffer.InternalBlockCopy(this._Buffer, this._BufferIndex + count - this._BufferSize, this._Buffer, 0, this._BufferSize - count);
					System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferIndex, count);
					this._BufferFull = true;
					return;
				}
				System.Buffer.InternalBlockCopy(buffer, offset, this._Buffer, this._BufferIndex, count);
				this._BufferIndex += count;
				return;
			}
		}

		// Token: 0x04000C66 RID: 3174
		private byte[] _Buffer;

		// Token: 0x04000C67 RID: 3175
		private int _BufferSize;

		// Token: 0x04000C68 RID: 3176
		private int _BufferIndex;

		// Token: 0x04000C69 RID: 3177
		private bool _BufferFull;
	}
}
