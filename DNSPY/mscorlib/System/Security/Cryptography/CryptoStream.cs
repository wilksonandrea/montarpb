using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Security.Cryptography
{
	// Token: 0x02000258 RID: 600
	[ComVisible(true)]
	public class CryptoStream : Stream, IDisposable
	{
		// Token: 0x06002141 RID: 8513 RVA: 0x00075193 File Offset: 0x00073393
		public CryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode)
			: this(stream, transform, mode, false)
		{
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000751A0 File Offset: 0x000733A0
		public CryptoStream(Stream stream, ICryptoTransform transform, CryptoStreamMode mode, bool leaveOpen)
		{
			this._stream = stream;
			this._transformMode = mode;
			this._Transform = transform;
			this._leaveOpen = leaveOpen;
			CryptoStreamMode transformMode = this._transformMode;
			if (transformMode != CryptoStreamMode.Read)
			{
				if (transformMode != CryptoStreamMode.Write)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
				}
				if (!this._stream.CanWrite)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotWritable"), "stream");
				}
				this._canWrite = true;
			}
			else
			{
				if (!this._stream.CanRead)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotReadable"), "stream");
				}
				this._canRead = true;
			}
			this.InitializeBuffer();
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x0007524C File Offset: 0x0007344C
		public override bool CanRead
		{
			get
			{
				return this._canRead;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06002144 RID: 8516 RVA: 0x00075254 File Offset: 0x00073454
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x00075257 File Offset: 0x00073457
		public override bool CanWrite
		{
			get
			{
				return this._canWrite;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06002146 RID: 8518 RVA: 0x0007525F File Offset: 0x0007345F
		public override long Length
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06002147 RID: 8519 RVA: 0x00075270 File Offset: 0x00073470
		// (set) Token: 0x06002148 RID: 8520 RVA: 0x00075281 File Offset: 0x00073481
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

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06002149 RID: 8521 RVA: 0x00075292 File Offset: 0x00073492
		public bool HasFlushedFinalBlock
		{
			get
			{
				return this._finalBlockTransformed;
			}
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x0007529C File Offset: 0x0007349C
		public void FlushFinalBlock()
		{
			if (this._finalBlockTransformed)
			{
				throw new NotSupportedException(Environment.GetResourceString("Cryptography_CryptoStream_FlushFinalBlockTwice"));
			}
			byte[] array = this._Transform.TransformFinalBlock(this._InputBuffer, 0, this._InputBufferIndex);
			this._finalBlockTransformed = true;
			if (this._canWrite && this._OutputBufferIndex > 0)
			{
				this._stream.Write(this._OutputBuffer, 0, this._OutputBufferIndex);
				this._OutputBufferIndex = 0;
			}
			if (this._canWrite)
			{
				this._stream.Write(array, 0, array.Length);
			}
			CryptoStream cryptoStream = this._stream as CryptoStream;
			if (cryptoStream != null)
			{
				if (!cryptoStream.HasFlushedFinalBlock)
				{
					cryptoStream.FlushFinalBlock();
				}
			}
			else
			{
				this._stream.Flush();
			}
			if (this._InputBuffer != null)
			{
				Array.Clear(this._InputBuffer, 0, this._InputBuffer.Length);
			}
			if (this._OutputBuffer != null)
			{
				Array.Clear(this._OutputBuffer, 0, this._OutputBuffer.Length);
			}
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x0007538B File Offset: 0x0007358B
		public override void Flush()
		{
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x0007538D File Offset: 0x0007358D
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (base.GetType() != typeof(CryptoStream))
			{
				return base.FlushAsync(cancellationToken);
			}
			if (!cancellationToken.IsCancellationRequested)
			{
				return Task.CompletedTask;
			}
			return Task.FromCancellation(cancellationToken);
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x000753C3 File Offset: 0x000735C3
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x000753D4 File Offset: 0x000735D4
		public override void SetLength(long value)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x000753E8 File Offset: 0x000735E8
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			int i = count;
			int num = offset;
			if (this._OutputBufferIndex != 0)
			{
				if (this._OutputBufferIndex > count)
				{
					Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, offset, count);
					Buffer.InternalBlockCopy(this._OutputBuffer, count, this._OutputBuffer, 0, this._OutputBufferIndex - count);
					this._OutputBufferIndex -= count;
					return count;
				}
				Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, offset, this._OutputBufferIndex);
				i -= this._OutputBufferIndex;
				num += this._OutputBufferIndex;
				this._OutputBufferIndex = 0;
			}
			if (this._finalBlockTransformed)
			{
				return count - i;
			}
			if (i > this._OutputBlockSize && this._Transform.CanTransformMultipleBlocks)
			{
				int num2 = i / this._OutputBlockSize;
				int num3 = num2 * this._InputBlockSize;
				byte[] array = new byte[num3];
				Buffer.InternalBlockCopy(this._InputBuffer, 0, array, 0, this._InputBufferIndex);
				int num4 = this._InputBufferIndex;
				num4 += this._stream.Read(array, this._InputBufferIndex, num3 - this._InputBufferIndex);
				this._InputBufferIndex = 0;
				if (num4 <= this._InputBlockSize)
				{
					this._InputBuffer = array;
					this._InputBufferIndex = num4;
				}
				else
				{
					int num5 = num4 / this._InputBlockSize * this._InputBlockSize;
					int num6 = num4 - num5;
					if (num6 != 0)
					{
						this._InputBufferIndex = num6;
						Buffer.InternalBlockCopy(array, num5, this._InputBuffer, 0, num6);
					}
					byte[] array2 = new byte[num5 / this._InputBlockSize * this._OutputBlockSize];
					int num7 = this._Transform.TransformBlock(array, 0, num5, array2, 0);
					Buffer.InternalBlockCopy(array2, 0, buffer, num, num7);
					Array.Clear(array, 0, array.Length);
					Array.Clear(array2, 0, array2.Length);
					i -= num7;
					num += num7;
				}
			}
			while (i > 0)
			{
				while (this._InputBufferIndex < this._InputBlockSize)
				{
					int num4 = this._stream.Read(this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex);
					if (num4 != 0)
					{
						this._InputBufferIndex += num4;
					}
					else
					{
						byte[] array3 = this._Transform.TransformFinalBlock(this._InputBuffer, 0, this._InputBufferIndex);
						this._OutputBuffer = array3;
						this._OutputBufferIndex = array3.Length;
						this._finalBlockTransformed = true;
						if (i < this._OutputBufferIndex)
						{
							Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, num, i);
							this._OutputBufferIndex -= i;
							Buffer.InternalBlockCopy(this._OutputBuffer, i, this._OutputBuffer, 0, this._OutputBufferIndex);
							return count;
						}
						Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, num, this._OutputBufferIndex);
						i -= this._OutputBufferIndex;
						this._OutputBufferIndex = 0;
						return count - i;
					}
				}
				int num7 = this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0);
				this._InputBufferIndex = 0;
				if (i < num7)
				{
					Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, num, i);
					this._OutputBufferIndex = num7 - i;
					Buffer.InternalBlockCopy(this._OutputBuffer, i, this._OutputBuffer, 0, this._OutputBufferIndex);
					return count;
				}
				Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, num, num7);
				num += num7;
				i -= num7;
			}
			return count;
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x00075770 File Offset: 0x00073970
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (base.GetType() != typeof(CryptoStream))
			{
				return base.ReadAsync(buffer, offset, count, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation<int>(cancellationToken);
			}
			return this.ReadAsyncInternal(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x00075820 File Offset: 0x00073A20
		private async Task<int> ReadAsyncInternal(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			await default(CryptoStream.HopToThreadPoolAwaitable);
			SemaphoreSlim sem = base.EnsureAsyncActiveSemaphoreInitialized();
			await sem.WaitAsync().ConfigureAwait(false);
			int num;
			try
			{
				int bytesToDeliver = count;
				int currentOutputIndex = offset;
				if (this._OutputBufferIndex != 0)
				{
					if (this._OutputBufferIndex > count)
					{
						Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, offset, count);
						Buffer.InternalBlockCopy(this._OutputBuffer, count, this._OutputBuffer, 0, this._OutputBufferIndex - count);
						this._OutputBufferIndex -= count;
						return count;
					}
					Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, offset, this._OutputBufferIndex);
					bytesToDeliver -= this._OutputBufferIndex;
					currentOutputIndex += this._OutputBufferIndex;
					this._OutputBufferIndex = 0;
				}
				if (this._finalBlockTransformed)
				{
					num = count - bytesToDeliver;
				}
				else
				{
					if (bytesToDeliver > this._OutputBlockSize && this._Transform.CanTransformMultipleBlocks)
					{
						int num2 = bytesToDeliver / this._OutputBlockSize * this._InputBlockSize;
						byte[] tempInputBuffer = new byte[num2];
						Buffer.InternalBlockCopy(this._InputBuffer, 0, tempInputBuffer, 0, this._InputBufferIndex);
						int inputBufferIndex = this._InputBufferIndex;
						int num3 = inputBufferIndex + await this._stream.ReadAsync(tempInputBuffer, this._InputBufferIndex, num2 - this._InputBufferIndex, cancellationToken).ConfigureAwait(false);
						this._InputBufferIndex = 0;
						if (num3 <= this._InputBlockSize)
						{
							this._InputBuffer = tempInputBuffer;
							this._InputBufferIndex = num3;
						}
						else
						{
							int num4 = num3 / this._InputBlockSize * this._InputBlockSize;
							int num5 = num3 - num4;
							if (num5 != 0)
							{
								this._InputBufferIndex = num5;
								Buffer.InternalBlockCopy(tempInputBuffer, num4, this._InputBuffer, 0, num5);
							}
							byte[] array = new byte[num4 / this._InputBlockSize * this._OutputBlockSize];
							int num6 = this._Transform.TransformBlock(tempInputBuffer, 0, num4, array, 0);
							Buffer.InternalBlockCopy(array, 0, buffer, currentOutputIndex, num6);
							Array.Clear(tempInputBuffer, 0, tempInputBuffer.Length);
							Array.Clear(array, 0, array.Length);
							bytesToDeliver -= num6;
							currentOutputIndex += num6;
							tempInputBuffer = null;
						}
					}
					while (bytesToDeliver > 0)
					{
						while (this._InputBufferIndex < this._InputBlockSize)
						{
							int num3 = await this._stream.ReadAsync(this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex, cancellationToken).ConfigureAwait(false);
							if (num3 != 0)
							{
								this._InputBufferIndex += num3;
							}
							else
							{
								byte[] array2 = this._Transform.TransformFinalBlock(this._InputBuffer, 0, this._InputBufferIndex);
								this._OutputBuffer = array2;
								this._OutputBufferIndex = array2.Length;
								this._finalBlockTransformed = true;
								if (bytesToDeliver < this._OutputBufferIndex)
								{
									Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, currentOutputIndex, bytesToDeliver);
									this._OutputBufferIndex -= bytesToDeliver;
									Buffer.InternalBlockCopy(this._OutputBuffer, bytesToDeliver, this._OutputBuffer, 0, this._OutputBufferIndex);
									return count;
								}
								Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, currentOutputIndex, this._OutputBufferIndex);
								bytesToDeliver -= this._OutputBufferIndex;
								this._OutputBufferIndex = 0;
								return count - bytesToDeliver;
							}
						}
						int num6 = this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0);
						this._InputBufferIndex = 0;
						if (bytesToDeliver < num6)
						{
							Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, currentOutputIndex, bytesToDeliver);
							this._OutputBufferIndex = num6 - bytesToDeliver;
							Buffer.InternalBlockCopy(this._OutputBuffer, bytesToDeliver, this._OutputBuffer, 0, this._OutputBufferIndex);
							return count;
						}
						Buffer.InternalBlockCopy(this._OutputBuffer, 0, buffer, currentOutputIndex, num6);
						currentOutputIndex += num6;
						bytesToDeliver -= num6;
					}
					num = count;
				}
			}
			finally
			{
				sem.Release();
			}
			return num;
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x00075884 File Offset: 0x00073A84
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			int i = count;
			int num = offset;
			if (this._InputBufferIndex > 0)
			{
				if (count < this._InputBlockSize - this._InputBufferIndex)
				{
					Buffer.InternalBlockCopy(buffer, offset, this._InputBuffer, this._InputBufferIndex, count);
					this._InputBufferIndex += count;
					return;
				}
				Buffer.InternalBlockCopy(buffer, offset, this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex);
				num += this._InputBlockSize - this._InputBufferIndex;
				i -= this._InputBlockSize - this._InputBufferIndex;
				this._InputBufferIndex = this._InputBlockSize;
			}
			if (this._OutputBufferIndex > 0)
			{
				this._stream.Write(this._OutputBuffer, 0, this._OutputBufferIndex);
				this._OutputBufferIndex = 0;
			}
			if (this._InputBufferIndex == this._InputBlockSize)
			{
				int num2 = this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0);
				this._stream.Write(this._OutputBuffer, 0, num2);
				this._InputBufferIndex = 0;
			}
			while (i > 0)
			{
				if (i < this._InputBlockSize)
				{
					Buffer.InternalBlockCopy(buffer, num, this._InputBuffer, 0, i);
					this._InputBufferIndex += i;
					return;
				}
				if (this._Transform.CanTransformMultipleBlocks)
				{
					int num3 = i / this._InputBlockSize;
					int num4 = num3 * this._InputBlockSize;
					byte[] array = new byte[num3 * this._OutputBlockSize];
					int num2 = this._Transform.TransformBlock(buffer, num, num4, array, 0);
					this._stream.Write(array, 0, num2);
					num += num4;
					i -= num4;
				}
				else
				{
					int num2 = this._Transform.TransformBlock(buffer, num, this._InputBlockSize, this._OutputBuffer, 0);
					this._stream.Write(this._OutputBuffer, 0, num2);
					num += this._InputBlockSize;
					i -= this._InputBlockSize;
				}
			}
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x00075ACC File Offset: 0x00073CCC
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!this.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (base.GetType() != typeof(CryptoStream))
			{
				return base.WriteAsync(buffer, offset, count, cancellationToken);
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			return this.WriteAsyncInternal(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x00075B7C File Offset: 0x00073D7C
		private async Task WriteAsyncInternal(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			await default(CryptoStream.HopToThreadPoolAwaitable);
			SemaphoreSlim sem = base.EnsureAsyncActiveSemaphoreInitialized();
			await sem.WaitAsync().ConfigureAwait(false);
			try
			{
				int bytesToWrite = count;
				int currentInputIndex = offset;
				if (this._InputBufferIndex > 0)
				{
					if (count < this._InputBlockSize - this._InputBufferIndex)
					{
						Buffer.InternalBlockCopy(buffer, offset, this._InputBuffer, this._InputBufferIndex, count);
						this._InputBufferIndex += count;
						return;
					}
					Buffer.InternalBlockCopy(buffer, offset, this._InputBuffer, this._InputBufferIndex, this._InputBlockSize - this._InputBufferIndex);
					currentInputIndex += this._InputBlockSize - this._InputBufferIndex;
					bytesToWrite -= this._InputBlockSize - this._InputBufferIndex;
					this._InputBufferIndex = this._InputBlockSize;
				}
				if (this._OutputBufferIndex > 0)
				{
					await this._stream.WriteAsync(this._OutputBuffer, 0, this._OutputBufferIndex, cancellationToken).ConfigureAwait(false);
					this._OutputBufferIndex = 0;
				}
				if (this._InputBufferIndex == this._InputBlockSize)
				{
					int num = this._Transform.TransformBlock(this._InputBuffer, 0, this._InputBlockSize, this._OutputBuffer, 0);
					await this._stream.WriteAsync(this._OutputBuffer, 0, num, cancellationToken).ConfigureAwait(false);
					this._InputBufferIndex = 0;
				}
				while (bytesToWrite > 0)
				{
					if (bytesToWrite < this._InputBlockSize)
					{
						Buffer.InternalBlockCopy(buffer, currentInputIndex, this._InputBuffer, 0, bytesToWrite);
						this._InputBufferIndex += bytesToWrite;
						break;
					}
					if (this._Transform.CanTransformMultipleBlocks)
					{
						int num2 = bytesToWrite / this._InputBlockSize;
						int numWholeBlocksInBytes = num2 * this._InputBlockSize;
						byte[] array = new byte[num2 * this._OutputBlockSize];
						int num = this._Transform.TransformBlock(buffer, currentInputIndex, numWholeBlocksInBytes, array, 0);
						await this._stream.WriteAsync(array, 0, num, cancellationToken).ConfigureAwait(false);
						currentInputIndex += numWholeBlocksInBytes;
						bytesToWrite -= numWholeBlocksInBytes;
					}
					else
					{
						int num = this._Transform.TransformBlock(buffer, currentInputIndex, this._InputBlockSize, this._OutputBuffer, 0);
						await this._stream.WriteAsync(this._OutputBuffer, 0, num, cancellationToken).ConfigureAwait(false);
						currentInputIndex += this._InputBlockSize;
						bytesToWrite -= this._InputBlockSize;
					}
				}
			}
			finally
			{
				sem.Release();
			}
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x00075BE0 File Offset: 0x00073DE0
		public void Clear()
		{
			this.Close();
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00075BE8 File Offset: 0x00073DE8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (!this._finalBlockTransformed)
					{
						this.FlushFinalBlock();
					}
					if (!this._leaveOpen)
					{
						this._stream.Close();
					}
				}
			}
			finally
			{
				try
				{
					this._finalBlockTransformed = true;
					if (this._InputBuffer != null)
					{
						Array.Clear(this._InputBuffer, 0, this._InputBuffer.Length);
					}
					if (this._OutputBuffer != null)
					{
						Array.Clear(this._OutputBuffer, 0, this._OutputBuffer.Length);
					}
					this._InputBuffer = null;
					this._OutputBuffer = null;
					this._canRead = false;
					this._canWrite = false;
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x00075CA0 File Offset: 0x00073EA0
		private void InitializeBuffer()
		{
			if (this._Transform != null)
			{
				this._InputBlockSize = this._Transform.InputBlockSize;
				this._InputBuffer = new byte[this._InputBlockSize];
				this._OutputBlockSize = this._Transform.OutputBlockSize;
				this._OutputBuffer = new byte[this._OutputBlockSize];
			}
		}

		// Token: 0x04000C21 RID: 3105
		private Stream _stream;

		// Token: 0x04000C22 RID: 3106
		private ICryptoTransform _Transform;

		// Token: 0x04000C23 RID: 3107
		private byte[] _InputBuffer;

		// Token: 0x04000C24 RID: 3108
		private int _InputBufferIndex;

		// Token: 0x04000C25 RID: 3109
		private int _InputBlockSize;

		// Token: 0x04000C26 RID: 3110
		private byte[] _OutputBuffer;

		// Token: 0x04000C27 RID: 3111
		private int _OutputBufferIndex;

		// Token: 0x04000C28 RID: 3112
		private int _OutputBlockSize;

		// Token: 0x04000C29 RID: 3113
		private CryptoStreamMode _transformMode;

		// Token: 0x04000C2A RID: 3114
		private bool _canRead;

		// Token: 0x04000C2B RID: 3115
		private bool _canWrite;

		// Token: 0x04000C2C RID: 3116
		private bool _finalBlockTransformed;

		// Token: 0x04000C2D RID: 3117
		private bool _leaveOpen;

		// Token: 0x02000B45 RID: 2885
		private struct HopToThreadPoolAwaitable : INotifyCompletion
		{
			// Token: 0x06006B90 RID: 27536 RVA: 0x00173836 File Offset: 0x00171A36
			public CryptoStream.HopToThreadPoolAwaitable GetAwaiter()
			{
				return this;
			}

			// Token: 0x17001224 RID: 4644
			// (get) Token: 0x06006B91 RID: 27537 RVA: 0x0017383E File Offset: 0x00171A3E
			public bool IsCompleted
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006B92 RID: 27538 RVA: 0x00173841 File Offset: 0x00171A41
			public void OnCompleted(Action continuation)
			{
				Task.Run(continuation);
			}

			// Token: 0x06006B93 RID: 27539 RVA: 0x0017384A File Offset: 0x00171A4A
			public void GetResult()
			{
			}
		}

		// Token: 0x02000B46 RID: 2886
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAsyncInternal>d__36 : IAsyncStateMachine
		{
			// Token: 0x06006B94 RID: 27540 RVA: 0x0017384C File Offset: 0x00171A4C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				CryptoStream cryptoStream = this;
				int num3;
				try
				{
					CryptoStream.HopToThreadPoolAwaitable hopToThreadPoolAwaitable;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
					switch (num)
					{
					case 0:
					{
						CryptoStream.HopToThreadPoolAwaitable hopToThreadPoolAwaitable2;
						hopToThreadPoolAwaitable = hopToThreadPoolAwaitable2;
						hopToThreadPoolAwaitable2 = default(CryptoStream.HopToThreadPoolAwaitable);
						num = (num2 = -1);
						break;
					}
					case 1:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (num2 = -1);
						goto IL_F4;
					}
					case 2:
					case 3:
						IL_FB:
						try
						{
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter3;
							int num6;
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter5;
							if (num != 2)
							{
								if (num == 3)
								{
									ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
									configuredTaskAwaiter3 = configuredTaskAwaiter4;
									configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
									num = (num2 = -1);
									goto IL_488;
								}
								bytesToDeliver = count;
								currentOutputIndex = offset;
								if (cryptoStream._OutputBufferIndex != 0)
								{
									if (cryptoStream._OutputBufferIndex > count)
									{
										Buffer.InternalBlockCopy(cryptoStream._OutputBuffer, 0, buffer, offset, count);
										Buffer.InternalBlockCopy(cryptoStream._OutputBuffer, count, cryptoStream._OutputBuffer, 0, cryptoStream._OutputBufferIndex - count);
										cryptoStream._OutputBufferIndex -= count;
										num3 = count;
										goto IL_6A9;
									}
									Buffer.InternalBlockCopy(cryptoStream._OutputBuffer, 0, buffer, offset, cryptoStream._OutputBufferIndex);
									bytesToDeliver -= cryptoStream._OutputBufferIndex;
									currentOutputIndex += cryptoStream._OutputBufferIndex;
									cryptoStream._OutputBufferIndex = 0;
								}
								if (cryptoStream._finalBlockTransformed)
								{
									num3 = count - bytesToDeliver;
									goto IL_6A9;
								}
								if (bytesToDeliver <= cryptoStream._OutputBlockSize || !cryptoStream._Transform.CanTransformMultipleBlocks)
								{
									goto IL_57F;
								}
								int num4 = bytesToDeliver / cryptoStream._OutputBlockSize;
								int num5 = num4 * cryptoStream._InputBlockSize;
								tempInputBuffer = new byte[num5];
								Buffer.InternalBlockCopy(cryptoStream._InputBuffer, 0, tempInputBuffer, 0, cryptoStream._InputBufferIndex);
								num6 = cryptoStream._InputBufferIndex;
								inputBufferIndex = num6;
								configuredTaskAwaiter5 = cryptoStream._stream.ReadAsync(tempInputBuffer, cryptoStream._InputBufferIndex, num5 - cryptoStream._InputBufferIndex, cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter5.IsCompleted)
								{
									num = (num2 = 2);
									ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter4 = configuredTaskAwaiter5;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, CryptoStream.<ReadAsyncInternal>d__36>(ref configuredTaskAwaiter5, ref this);
									return;
								}
							}
							else
							{
								ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
								configuredTaskAwaiter5 = configuredTaskAwaiter4;
								configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
								num = (num2 = -1);
							}
							int result = configuredTaskAwaiter5.GetResult();
							num6 = inputBufferIndex + result;
							cryptoStream._InputBufferIndex = 0;
							if (num6 <= cryptoStream._InputBlockSize)
							{
								cryptoStream._InputBuffer = tempInputBuffer;
								cryptoStream._InputBufferIndex = num6;
								goto IL_57F;
							}
							int num7 = num6 / cryptoStream._InputBlockSize * cryptoStream._InputBlockSize;
							int num8 = num6 - num7;
							if (num8 != 0)
							{
								cryptoStream._InputBufferIndex = num8;
								Buffer.InternalBlockCopy(tempInputBuffer, num7, cryptoStream._InputBuffer, 0, num8);
							}
							byte[] array = new byte[num7 / cryptoStream._InputBlockSize * cryptoStream._OutputBlockSize];
							int num9 = cryptoStream._Transform.TransformBlock(tempInputBuffer, 0, num7, array, 0);
							Buffer.InternalBlockCopy(array, 0, buffer, currentOutputIndex, num9);
							Array.Clear(tempInputBuffer, 0, tempInputBuffer.Length);
							Array.Clear(array, 0, array.Length);
							bytesToDeliver -= num9;
							currentOutputIndex += num9;
							tempInputBuffer = null;
							goto IL_57F;
							IL_488:
							int result2 = configuredTaskAwaiter3.GetResult();
							num6 = result2;
							if (num6 != 0)
							{
								cryptoStream._InputBufferIndex += num6;
							}
							else
							{
								byte[] array2 = cryptoStream._Transform.TransformFinalBlock(cryptoStream._InputBuffer, 0, cryptoStream._InputBufferIndex);
								cryptoStream._OutputBuffer = array2;
								cryptoStream._OutputBufferIndex = array2.Length;
								cryptoStream._finalBlockTransformed = true;
								if (bytesToDeliver < cryptoStream._OutputBufferIndex)
								{
									Buffer.InternalBlockCopy(cryptoStream._OutputBuffer, 0, buffer, currentOutputIndex, bytesToDeliver);
									cryptoStream._OutputBufferIndex -= bytesToDeliver;
									Buffer.InternalBlockCopy(cryptoStream._OutputBuffer, bytesToDeliver, cryptoStream._OutputBuffer, 0, cryptoStream._OutputBufferIndex);
									num3 = count;
									goto IL_6A9;
								}
								Buffer.InternalBlockCopy(cryptoStream._OutputBuffer, 0, buffer, currentOutputIndex, cryptoStream._OutputBufferIndex);
								bytesToDeliver -= cryptoStream._OutputBufferIndex;
								cryptoStream._OutputBufferIndex = 0;
								num3 = count - bytesToDeliver;
								goto IL_6A9;
							}
							IL_4AB:
							if (cryptoStream._InputBufferIndex >= cryptoStream._InputBlockSize)
							{
								num9 = cryptoStream._Transform.TransformBlock(cryptoStream._InputBuffer, 0, cryptoStream._InputBlockSize, cryptoStream._OutputBuffer, 0);
								cryptoStream._InputBufferIndex = 0;
								if (bytesToDeliver < num9)
								{
									Buffer.InternalBlockCopy(cryptoStream._OutputBuffer, 0, buffer, currentOutputIndex, bytesToDeliver);
									cryptoStream._OutputBufferIndex = num9 - bytesToDeliver;
									Buffer.InternalBlockCopy(cryptoStream._OutputBuffer, bytesToDeliver, cryptoStream._OutputBuffer, 0, cryptoStream._OutputBufferIndex);
									num3 = count;
									goto IL_6A9;
								}
								Buffer.InternalBlockCopy(cryptoStream._OutputBuffer, 0, buffer, currentOutputIndex, num9);
								currentOutputIndex += num9;
								bytesToDeliver -= num9;
							}
							else
							{
								configuredTaskAwaiter3 = cryptoStream._stream.ReadAsync(cryptoStream._InputBuffer, cryptoStream._InputBufferIndex, cryptoStream._InputBlockSize - cryptoStream._InputBufferIndex, cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter3.IsCompleted)
								{
									num = (num2 = 3);
									ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter4 = configuredTaskAwaiter3;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, CryptoStream.<ReadAsyncInternal>d__36>(ref configuredTaskAwaiter3, ref this);
									return;
								}
								goto IL_488;
							}
							IL_57F:
							if (bytesToDeliver <= 0)
							{
								num3 = count;
								goto IL_6A9;
							}
							goto IL_4AB;
						}
						finally
						{
							if (num < 0)
							{
								sem.Release();
							}
						}
						break;
					default:
						hopToThreadPoolAwaitable = default(CryptoStream.HopToThreadPoolAwaitable).GetAwaiter();
						if (!hopToThreadPoolAwaitable.IsCompleted)
						{
							num = (num2 = 0);
							CryptoStream.HopToThreadPoolAwaitable hopToThreadPoolAwaitable2 = hopToThreadPoolAwaitable;
							this.<>t__builder.AwaitOnCompleted<CryptoStream.HopToThreadPoolAwaitable, CryptoStream.<ReadAsyncInternal>d__36>(ref hopToThreadPoolAwaitable, ref this);
							return;
						}
						break;
					}
					hopToThreadPoolAwaitable.GetResult();
					sem = cryptoStream.EnsureAsyncActiveSemaphoreInitialized();
					configuredTaskAwaiter = sem.WaitAsync().ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						num = (num2 = 1);
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, CryptoStream.<ReadAsyncInternal>d__36>(ref configuredTaskAwaiter, ref this);
						return;
					}
					IL_F4:
					configuredTaskAwaiter.GetResult();
					goto IL_FB;
				}
				catch (Exception ex)
				{
					num2 = -2;
					sem = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				IL_6A9:
				num2 = -2;
				sem = null;
				this.<>t__builder.SetResult(num3);
			}

			// Token: 0x06006B95 RID: 27541 RVA: 0x00173F54 File Offset: 0x00172154
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040033A6 RID: 13222
			public int <>1__state;

			// Token: 0x040033A7 RID: 13223
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x040033A8 RID: 13224
			public CryptoStream <>4__this;

			// Token: 0x040033A9 RID: 13225
			public int count;

			// Token: 0x040033AA RID: 13226
			public int offset;

			// Token: 0x040033AB RID: 13227
			public byte[] buffer;

			// Token: 0x040033AC RID: 13228
			public CancellationToken cancellationToken;

			// Token: 0x040033AD RID: 13229
			private SemaphoreSlim <sem>5__2;

			// Token: 0x040033AE RID: 13230
			private CryptoStream.HopToThreadPoolAwaitable <>u__1;

			// Token: 0x040033AF RID: 13231
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x040033B0 RID: 13232
			private int <bytesToDeliver>5__3;

			// Token: 0x040033B1 RID: 13233
			private int <currentOutputIndex>5__4;

			// Token: 0x040033B2 RID: 13234
			private byte[] <tempInputBuffer>5__5;

			// Token: 0x040033B3 RID: 13235
			private int <>7__wrap5;

			// Token: 0x040033B4 RID: 13236
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__3;
		}

		// Token: 0x02000B47 RID: 2887
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAsyncInternal>d__39 : IAsyncStateMachine
		{
			// Token: 0x06006B96 RID: 27542 RVA: 0x00173F64 File Offset: 0x00172164
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				CryptoStream cryptoStream = this;
				try
				{
					CryptoStream.HopToThreadPoolAwaitable hopToThreadPoolAwaitable;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
					switch (num)
					{
					case 0:
					{
						CryptoStream.HopToThreadPoolAwaitable hopToThreadPoolAwaitable2;
						hopToThreadPoolAwaitable = hopToThreadPoolAwaitable2;
						hopToThreadPoolAwaitable2 = default(CryptoStream.HopToThreadPoolAwaitable);
						num = (num2 = -1);
						break;
					}
					case 1:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (num2 = -1);
						goto IL_FC;
					}
					case 2:
					case 3:
					case 4:
					case 5:
						IL_103:
						try
						{
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter4;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter5;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter6;
							switch (num)
							{
							case 2:
							{
								ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
								configuredTaskAwaiter3 = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								num = (num2 = -1);
								break;
							}
							case 3:
							{
								ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
								configuredTaskAwaiter4 = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								num = (num2 = -1);
								goto IL_336;
							}
							case 4:
							{
								ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
								configuredTaskAwaiter5 = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								num = (num2 = -1);
								goto IL_42B;
							}
							case 5:
							{
								ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
								configuredTaskAwaiter6 = configuredTaskAwaiter2;
								configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								num = (num2 = -1);
								goto IL_4F8;
							}
							default:
								bytesToWrite = count;
								currentInputIndex = offset;
								if (cryptoStream._InputBufferIndex > 0)
								{
									if (count < cryptoStream._InputBlockSize - cryptoStream._InputBufferIndex)
									{
										Buffer.InternalBlockCopy(buffer, offset, cryptoStream._InputBuffer, cryptoStream._InputBufferIndex, count);
										cryptoStream._InputBufferIndex += count;
										goto IL_599;
									}
									Buffer.InternalBlockCopy(buffer, offset, cryptoStream._InputBuffer, cryptoStream._InputBufferIndex, cryptoStream._InputBlockSize - cryptoStream._InputBufferIndex);
									currentInputIndex += cryptoStream._InputBlockSize - cryptoStream._InputBufferIndex;
									bytesToWrite -= cryptoStream._InputBlockSize - cryptoStream._InputBufferIndex;
									cryptoStream._InputBufferIndex = cryptoStream._InputBlockSize;
								}
								if (cryptoStream._OutputBufferIndex <= 0)
								{
									goto IL_28F;
								}
								configuredTaskAwaiter3 = cryptoStream._stream.WriteAsync(cryptoStream._OutputBuffer, 0, cryptoStream._OutputBufferIndex, cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter3.IsCompleted)
								{
									num = (num2 = 2);
									ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter3;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, CryptoStream.<WriteAsyncInternal>d__39>(ref configuredTaskAwaiter3, ref this);
									return;
								}
								break;
							}
							configuredTaskAwaiter3.GetResult();
							cryptoStream._OutputBufferIndex = 0;
							IL_28F:
							if (cryptoStream._InputBufferIndex != cryptoStream._InputBlockSize)
							{
								goto IL_55A;
							}
							int num3 = cryptoStream._Transform.TransformBlock(cryptoStream._InputBuffer, 0, cryptoStream._InputBlockSize, cryptoStream._OutputBuffer, 0);
							configuredTaskAwaiter4 = cryptoStream._stream.WriteAsync(cryptoStream._OutputBuffer, 0, num3, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter4.IsCompleted)
							{
								num = (num2 = 3);
								ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter4;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, CryptoStream.<WriteAsyncInternal>d__39>(ref configuredTaskAwaiter4, ref this);
								return;
							}
							IL_336:
							configuredTaskAwaiter4.GetResult();
							cryptoStream._InputBufferIndex = 0;
							goto IL_55A;
							IL_42B:
							configuredTaskAwaiter5.GetResult();
							currentInputIndex += numWholeBlocksInBytes;
							bytesToWrite -= numWholeBlocksInBytes;
							goto IL_55A;
							IL_4F8:
							configuredTaskAwaiter6.GetResult();
							currentInputIndex += cryptoStream._InputBlockSize;
							bytesToWrite -= cryptoStream._InputBlockSize;
							IL_55A:
							if (bytesToWrite <= 0)
							{
								goto IL_599;
							}
							if (bytesToWrite < cryptoStream._InputBlockSize)
							{
								Buffer.InternalBlockCopy(buffer, currentInputIndex, cryptoStream._InputBuffer, 0, bytesToWrite);
								cryptoStream._InputBufferIndex += bytesToWrite;
								goto IL_599;
							}
							if (cryptoStream._Transform.CanTransformMultipleBlocks)
							{
								int num4 = bytesToWrite / cryptoStream._InputBlockSize;
								numWholeBlocksInBytes = num4 * cryptoStream._InputBlockSize;
								byte[] array = new byte[num4 * cryptoStream._OutputBlockSize];
								num3 = cryptoStream._Transform.TransformBlock(buffer, currentInputIndex, numWholeBlocksInBytes, array, 0);
								configuredTaskAwaiter5 = cryptoStream._stream.WriteAsync(array, 0, num3, cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter5.IsCompleted)
								{
									num = (num2 = 4);
									ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter5;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, CryptoStream.<WriteAsyncInternal>d__39>(ref configuredTaskAwaiter5, ref this);
									return;
								}
								goto IL_42B;
							}
							else
							{
								num3 = cryptoStream._Transform.TransformBlock(buffer, currentInputIndex, cryptoStream._InputBlockSize, cryptoStream._OutputBuffer, 0);
								configuredTaskAwaiter6 = cryptoStream._stream.WriteAsync(cryptoStream._OutputBuffer, 0, num3, cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!configuredTaskAwaiter6.IsCompleted)
								{
									num = (num2 = 5);
									ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter6;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, CryptoStream.<WriteAsyncInternal>d__39>(ref configuredTaskAwaiter6, ref this);
									return;
								}
								goto IL_4F8;
							}
						}
						finally
						{
							if (num < 0)
							{
								sem.Release();
							}
						}
						break;
					default:
						hopToThreadPoolAwaitable = default(CryptoStream.HopToThreadPoolAwaitable).GetAwaiter();
						if (!hopToThreadPoolAwaitable.IsCompleted)
						{
							num = (num2 = 0);
							CryptoStream.HopToThreadPoolAwaitable hopToThreadPoolAwaitable2 = hopToThreadPoolAwaitable;
							this.<>t__builder.AwaitOnCompleted<CryptoStream.HopToThreadPoolAwaitable, CryptoStream.<WriteAsyncInternal>d__39>(ref hopToThreadPoolAwaitable, ref this);
							return;
						}
						break;
					}
					hopToThreadPoolAwaitable.GetResult();
					sem = cryptoStream.EnsureAsyncActiveSemaphoreInitialized();
					configuredTaskAwaiter = sem.WaitAsync().ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						num = (num2 = 1);
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, CryptoStream.<WriteAsyncInternal>d__39>(ref configuredTaskAwaiter, ref this);
						return;
					}
					IL_FC:
					configuredTaskAwaiter.GetResult();
					goto IL_103;
				}
				catch (Exception ex)
				{
					num2 = -2;
					sem = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				IL_599:
				num2 = -2;
				sem = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06006B97 RID: 27543 RVA: 0x00174558 File Offset: 0x00172758
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040033B5 RID: 13237
			public int <>1__state;

			// Token: 0x040033B6 RID: 13238
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040033B7 RID: 13239
			public CryptoStream <>4__this;

			// Token: 0x040033B8 RID: 13240
			public int count;

			// Token: 0x040033B9 RID: 13241
			public int offset;

			// Token: 0x040033BA RID: 13242
			public byte[] buffer;

			// Token: 0x040033BB RID: 13243
			public CancellationToken cancellationToken;

			// Token: 0x040033BC RID: 13244
			private SemaphoreSlim <sem>5__2;

			// Token: 0x040033BD RID: 13245
			private CryptoStream.HopToThreadPoolAwaitable <>u__1;

			// Token: 0x040033BE RID: 13246
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x040033BF RID: 13247
			private int <bytesToWrite>5__3;

			// Token: 0x040033C0 RID: 13248
			private int <currentInputIndex>5__4;

			// Token: 0x040033C1 RID: 13249
			private int <numWholeBlocksInBytes>5__5;
		}
	}
}
