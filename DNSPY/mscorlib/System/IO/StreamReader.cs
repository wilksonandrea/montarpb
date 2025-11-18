using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020001A2 RID: 418
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class StreamReader : TextReader
	{
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x00055444 File Offset: 0x00053644
		internal static int DefaultBufferSize
		{
			get
			{
				return 1024;
			}
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0005544C File Offset: 0x0005364C
		private void CheckAsyncTaskInProgress()
		{
			Task asyncReadTask = this._asyncReadTask;
			if (asyncReadTask != null && !asyncReadTask.IsCompleted)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsyncIOInProgress"));
			}
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0005547D File Offset: 0x0005367D
		internal StreamReader()
		{
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00055485 File Offset: 0x00053685
		[__DynamicallyInvokable]
		public StreamReader(Stream stream)
			: this(stream, true)
		{
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0005548F File Offset: 0x0005368F
		[__DynamicallyInvokable]
		public StreamReader(Stream stream, bool detectEncodingFromByteOrderMarks)
			: this(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize, false)
		{
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x000554A4 File Offset: 0x000536A4
		[__DynamicallyInvokable]
		public StreamReader(Stream stream, Encoding encoding)
			: this(stream, encoding, true, StreamReader.DefaultBufferSize, false)
		{
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x000554B5 File Offset: 0x000536B5
		[__DynamicallyInvokable]
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks)
			: this(stream, encoding, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize, false)
		{
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x000554C6 File Offset: 0x000536C6
		[__DynamicallyInvokable]
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
			: this(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, false)
		{
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x000554D4 File Offset: 0x000536D4
		[__DynamicallyInvokable]
		public StreamReader(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool leaveOpen)
		{
			if (stream == null || encoding == null)
			{
				throw new ArgumentNullException((stream == null) ? "stream" : "encoding");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotReadable"));
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			this.Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, leaveOpen);
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00055541 File Offset: 0x00053741
		public StreamReader(string path)
			: this(path, true)
		{
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x0005554B File Offset: 0x0005374B
		public StreamReader(string path, bool detectEncodingFromByteOrderMarks)
			: this(path, Encoding.UTF8, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize)
		{
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0005555F File Offset: 0x0005375F
		public StreamReader(string path, Encoding encoding)
			: this(path, encoding, true, StreamReader.DefaultBufferSize)
		{
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0005556F File Offset: 0x0005376F
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks)
			: this(path, encoding, detectEncodingFromByteOrderMarks, StreamReader.DefaultBufferSize)
		{
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0005557F File Offset: 0x0005377F
		[SecuritySafeCritical]
		public StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize)
			: this(path, encoding, detectEncodingFromByteOrderMarks, bufferSize, true)
		{
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x00055590 File Offset: 0x00053790
		[SecurityCritical]
		internal StreamReader(string path, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool checkHost)
		{
			if (path == null || encoding == null)
			{
				throw new ArgumentNullException((path == null) ? "path" : "encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan, Path.GetFileName(path), false, false, checkHost);
			this.Init(stream, encoding, detectEncodingFromByteOrderMarks, bufferSize, false);
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0005561C File Offset: 0x0005381C
		private void Init(Stream stream, Encoding encoding, bool detectEncodingFromByteOrderMarks, int bufferSize, bool leaveOpen)
		{
			this.stream = stream;
			this.encoding = encoding;
			this.decoder = encoding.GetDecoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this.byteBuffer = new byte[bufferSize];
			this._maxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
			this.charBuffer = new char[this._maxCharsPerBuffer];
			this.byteLen = 0;
			this.bytePos = 0;
			this._detectEncoding = detectEncodingFromByteOrderMarks;
			this._preamble = encoding.GetPreamble();
			this._checkPreamble = this._preamble.Length != 0;
			this._isBlocked = false;
			this._closable = !leaveOpen;
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x000556C2 File Offset: 0x000538C2
		internal void Init(Stream stream)
		{
			this.stream = stream;
			this._closable = true;
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x000556D2 File Offset: 0x000538D2
		public override void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x000556DC File Offset: 0x000538DC
		[__DynamicallyInvokable]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this.LeaveOpen && disposing && this.stream != null)
				{
					this.stream.Close();
				}
			}
			finally
			{
				if (!this.LeaveOpen && this.stream != null)
				{
					this.stream = null;
					this.encoding = null;
					this.decoder = null;
					this.byteBuffer = null;
					this.charBuffer = null;
					this.charPos = 0;
					this.charLen = 0;
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x00055764 File Offset: 0x00053964
		[__DynamicallyInvokable]
		public virtual Encoding CurrentEncoding
		{
			[__DynamicallyInvokable]
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x0005576C File Offset: 0x0005396C
		[__DynamicallyInvokable]
		public virtual Stream BaseStream
		{
			[__DynamicallyInvokable]
			get
			{
				return this.stream;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x00055774 File Offset: 0x00053974
		internal bool LeaveOpen
		{
			get
			{
				return !this._closable;
			}
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x0005577F File Offset: 0x0005397F
		[__DynamicallyInvokable]
		public void DiscardBufferedData()
		{
			this.CheckAsyncTaskInProgress();
			this.byteLen = 0;
			this.charLen = 0;
			this.charPos = 0;
			if (this.encoding != null)
			{
				this.decoder = this.encoding.GetDecoder();
			}
			this._isBlocked = false;
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x000557BC File Offset: 0x000539BC
		[__DynamicallyInvokable]
		public bool EndOfStream
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.stream == null)
				{
					__Error.ReaderClosed();
				}
				this.CheckAsyncTaskInProgress();
				if (this.charPos < this.charLen)
				{
					return false;
				}
				int num = this.ReadBuffer();
				return num == 0;
			}
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x000557F8 File Offset: 0x000539F8
		[__DynamicallyInvokable]
		public override int Peek()
		{
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			if (this.charPos == this.charLen && (this._isBlocked || this.ReadBuffer() == 0))
			{
				return -1;
			}
			return (int)this.charBuffer[this.charPos];
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x00055848 File Offset: 0x00053A48
		[__DynamicallyInvokable]
		public override int Read()
		{
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			if (this.charPos == this.charLen && this.ReadBuffer() == 0)
			{
				return -1;
			}
			int num = (int)this.charBuffer[this.charPos];
			this.charPos++;
			return num;
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x000558A0 File Offset: 0x00053AA0
		[__DynamicallyInvokable]
		public override int Read([In] [Out] char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			int num = 0;
			bool flag = false;
			while (count > 0)
			{
				int num2 = this.charLen - this.charPos;
				if (num2 == 0)
				{
					num2 = this.ReadBuffer(buffer, index + num, count, out flag);
				}
				if (num2 == 0)
				{
					break;
				}
				if (num2 > count)
				{
					num2 = count;
				}
				if (!flag)
				{
					Buffer.InternalBlockCopy(this.charBuffer, this.charPos * 2, buffer, (index + num) * 2, num2 * 2);
					this.charPos += num2;
				}
				num += num2;
				count -= num2;
				if (this._isBlocked)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0005598C File Offset: 0x00053B8C
		[__DynamicallyInvokable]
		public override string ReadToEnd()
		{
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			StringBuilder stringBuilder = new StringBuilder(this.charLen - this.charPos);
			do
			{
				stringBuilder.Append(this.charBuffer, this.charPos, this.charLen - this.charPos);
				this.charPos = this.charLen;
				this.ReadBuffer();
			}
			while (this.charLen > 0);
			return stringBuilder.ToString();
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x00055A04 File Offset: 0x00053C04
		[__DynamicallyInvokable]
		public override int ReadBlock([In] [Out] char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			return base.ReadBlock(buffer, index, count);
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x00055A85 File Offset: 0x00053C85
		private void CompressBuffer(int n)
		{
			Buffer.InternalBlockCopy(this.byteBuffer, n, this.byteBuffer, 0, this.byteLen - n);
			this.byteLen -= n;
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00055AB0 File Offset: 0x00053CB0
		private void DetectEncoding()
		{
			if (this.byteLen < 2)
			{
				return;
			}
			this._detectEncoding = false;
			bool flag = false;
			if (this.byteBuffer[0] == 254 && this.byteBuffer[1] == 255)
			{
				this.encoding = new UnicodeEncoding(true, true);
				this.CompressBuffer(2);
				flag = true;
			}
			else if (this.byteBuffer[0] == 255 && this.byteBuffer[1] == 254)
			{
				if (this.byteLen < 4 || this.byteBuffer[2] != 0 || this.byteBuffer[3] != 0)
				{
					this.encoding = new UnicodeEncoding(false, true);
					this.CompressBuffer(2);
					flag = true;
				}
				else
				{
					this.encoding = new UTF32Encoding(false, true);
					this.CompressBuffer(4);
					flag = true;
				}
			}
			else if (this.byteLen >= 3 && this.byteBuffer[0] == 239 && this.byteBuffer[1] == 187 && this.byteBuffer[2] == 191)
			{
				this.encoding = Encoding.UTF8;
				this.CompressBuffer(3);
				flag = true;
			}
			else if (this.byteLen >= 4 && this.byteBuffer[0] == 0 && this.byteBuffer[1] == 0 && this.byteBuffer[2] == 254 && this.byteBuffer[3] == 255)
			{
				this.encoding = new UTF32Encoding(true, true);
				this.CompressBuffer(4);
				flag = true;
			}
			else if (this.byteLen == 2)
			{
				this._detectEncoding = true;
			}
			if (flag)
			{
				this.decoder = this.encoding.GetDecoder();
				this._maxCharsPerBuffer = this.encoding.GetMaxCharCount(this.byteBuffer.Length);
				this.charBuffer = new char[this._maxCharsPerBuffer];
			}
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00055C68 File Offset: 0x00053E68
		private bool IsPreamble()
		{
			if (!this._checkPreamble)
			{
				return this._checkPreamble;
			}
			int num = ((this.byteLen >= this._preamble.Length) ? (this._preamble.Length - this.bytePos) : (this.byteLen - this.bytePos));
			int i = 0;
			while (i < num)
			{
				if (this.byteBuffer[this.bytePos] != this._preamble[this.bytePos])
				{
					this.bytePos = 0;
					this._checkPreamble = false;
					break;
				}
				i++;
				this.bytePos++;
			}
			if (this._checkPreamble && this.bytePos == this._preamble.Length)
			{
				this.CompressBuffer(this._preamble.Length);
				this.bytePos = 0;
				this._checkPreamble = false;
				this._detectEncoding = false;
			}
			return this._checkPreamble;
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x00055D3C File Offset: 0x00053F3C
		internal virtual int ReadBuffer()
		{
			this.charLen = 0;
			this.charPos = 0;
			if (!this._checkPreamble)
			{
				this.byteLen = 0;
			}
			for (;;)
			{
				if (this._checkPreamble)
				{
					int num = this.stream.Read(this.byteBuffer, this.bytePos, this.byteBuffer.Length - this.bytePos);
					if (num == 0)
					{
						break;
					}
					this.byteLen += num;
				}
				else
				{
					this.byteLen = this.stream.Read(this.byteBuffer, 0, this.byteBuffer.Length);
					if (this.byteLen == 0)
					{
						goto Block_5;
					}
				}
				this._isBlocked = this.byteLen < this.byteBuffer.Length;
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this.byteLen >= 2)
					{
						this.DetectEncoding();
					}
					this.charLen += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, this.charLen);
				}
				if (this.charLen != 0)
				{
					goto Block_9;
				}
			}
			if (this.byteLen > 0)
			{
				this.charLen += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, this.charLen);
				this.bytePos = (this.byteLen = 0);
			}
			return this.charLen;
			Block_5:
			return this.charLen;
			Block_9:
			return this.charLen;
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00055EA4 File Offset: 0x000540A4
		private int ReadBuffer(char[] userBuffer, int userOffset, int desiredChars, out bool readToUserBuffer)
		{
			this.charLen = 0;
			this.charPos = 0;
			if (!this._checkPreamble)
			{
				this.byteLen = 0;
			}
			int num = 0;
			readToUserBuffer = desiredChars >= this._maxCharsPerBuffer;
			for (;;)
			{
				if (this._checkPreamble)
				{
					int num2 = this.stream.Read(this.byteBuffer, this.bytePos, this.byteBuffer.Length - this.bytePos);
					if (num2 == 0)
					{
						break;
					}
					this.byteLen += num2;
				}
				else
				{
					this.byteLen = this.stream.Read(this.byteBuffer, 0, this.byteBuffer.Length);
					if (this.byteLen == 0)
					{
						goto IL_1B1;
					}
				}
				this._isBlocked = this.byteLen < this.byteBuffer.Length;
				if (!this.IsPreamble())
				{
					if (this._detectEncoding && this.byteLen >= 2)
					{
						this.DetectEncoding();
						readToUserBuffer = desiredChars >= this._maxCharsPerBuffer;
					}
					this.charPos = 0;
					if (readToUserBuffer)
					{
						num += this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, userBuffer, userOffset + num);
						this.charLen = 0;
					}
					else
					{
						num = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, num);
						this.charLen += num;
					}
				}
				if (num != 0)
				{
					goto IL_1B1;
				}
			}
			if (this.byteLen > 0)
			{
				if (readToUserBuffer)
				{
					num = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, userBuffer, userOffset + num);
					this.charLen = 0;
				}
				else
				{
					num = this.decoder.GetChars(this.byteBuffer, 0, this.byteLen, this.charBuffer, num);
					this.charLen += num;
				}
			}
			return num;
			IL_1B1:
			this._isBlocked &= num < desiredChars;
			return num;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00056074 File Offset: 0x00054274
		[__DynamicallyInvokable]
		public override string ReadLine()
		{
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			if (this.charPos == this.charLen && this.ReadBuffer() == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = null;
			int num;
			char c;
			for (;;)
			{
				num = this.charPos;
				do
				{
					c = this.charBuffer[num];
					if (c == '\r' || c == '\n')
					{
						goto IL_4A;
					}
					num++;
				}
				while (num < this.charLen);
				num = this.charLen - this.charPos;
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(num + 80);
				}
				stringBuilder.Append(this.charBuffer, this.charPos, num);
				if (this.ReadBuffer() <= 0)
				{
					goto Block_11;
				}
			}
			IL_4A:
			string text;
			if (stringBuilder != null)
			{
				stringBuilder.Append(this.charBuffer, this.charPos, num - this.charPos);
				text = stringBuilder.ToString();
			}
			else
			{
				text = new string(this.charBuffer, this.charPos, num - this.charPos);
			}
			this.charPos = num + 1;
			if (c == '\r' && (this.charPos < this.charLen || this.ReadBuffer() > 0) && this.charBuffer[this.charPos] == '\n')
			{
				this.charPos++;
			}
			return text;
			Block_11:
			return stringBuilder.ToString();
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x000561A4 File Offset: 0x000543A4
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task<string> ReadLineAsync()
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadLineAsync();
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task<string> task = this.ReadLineAsyncInternal();
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x000561F4 File Offset: 0x000543F4
		private async Task<string> ReadLineAsyncInternal()
		{
			bool flag = this.CharPos_Prop == this.CharLen_Prop;
			bool flag2 = flag;
			if (flag2)
			{
				int num = await this.ReadBufferAsync().ConfigureAwait(false);
				flag2 = num == 0;
			}
			string text;
			if (flag2)
			{
				text = null;
			}
			else
			{
				StringBuilder sb = null;
				char[] charBuffer_Prop;
				int charLen_Prop;
				int num3;
				int num2;
				char c;
				for (;;)
				{
					charBuffer_Prop = this.CharBuffer_Prop;
					charLen_Prop = this.CharLen_Prop;
					num2 = (num3 = this.CharPos_Prop);
					do
					{
						c = charBuffer_Prop[num3];
						if (c == '\r' || c == '\n')
						{
							goto IL_EB;
						}
						num3++;
					}
					while (num3 < charLen_Prop);
					num3 = charLen_Prop - num2;
					if (sb == null)
					{
						sb = new StringBuilder(num3 + 80);
					}
					sb.Append(charBuffer_Prop, num2, num3);
					if (await this.ReadBufferAsync().ConfigureAwait(false) <= 0)
					{
						goto Block_11;
					}
				}
				IL_EB:
				string s;
				if (sb != null)
				{
					sb.Append(charBuffer_Prop, num2, num3 - num2);
					s = sb.ToString();
				}
				else
				{
					s = new string(charBuffer_Prop, num2, num3 - num2);
				}
				int num4 = num3 + 1;
				this.CharPos_Prop = num4;
				bool flag3 = c == '\r';
				if (flag3)
				{
					bool flag4 = num4 < charLen_Prop;
					if (!flag4)
					{
						flag4 = await this.ReadBufferAsync().ConfigureAwait(false) > 0;
					}
					flag3 = flag4;
				}
				if (flag3)
				{
					num2 = this.CharPos_Prop;
					if (this.CharBuffer_Prop[num2] == '\n')
					{
						this.CharPos_Prop = num2 + 1;
					}
				}
				return s;
				Block_11:
				text = sb.ToString();
			}
			return text;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00056238 File Offset: 0x00054438
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task<string> ReadToEndAsync()
		{
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadToEndAsync();
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task<string> task = this.ReadToEndAsyncInternal();
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00056288 File Offset: 0x00054488
		private async Task<string> ReadToEndAsyncInternal()
		{
			StringBuilder sb = new StringBuilder(this.CharLen_Prop - this.CharPos_Prop);
			do
			{
				int charPos_Prop = this.CharPos_Prop;
				sb.Append(this.CharBuffer_Prop, charPos_Prop, this.CharLen_Prop - charPos_Prop);
				this.CharPos_Prop = this.CharLen_Prop;
				await this.ReadBufferAsync().ConfigureAwait(false);
			}
			while (this.CharLen_Prop > 0);
			return sb.ToString();
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x000562CC File Offset: 0x000544CC
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadAsync(buffer, index, count);
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task<int> task = this.ReadAsyncInternal(buffer, index, count);
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0005637C File Offset: 0x0005457C
		internal override async Task<int> ReadAsyncInternal(char[] buffer, int index, int count)
		{
			bool flag = this.CharPos_Prop == this.CharLen_Prop;
			bool flag2 = flag;
			if (flag2)
			{
				int num = await this.ReadBufferAsync().ConfigureAwait(false);
				flag2 = num == 0;
			}
			int num2;
			if (flag2)
			{
				num2 = 0;
			}
			else
			{
				int charsRead = 0;
				bool readToUserBuffer = false;
				byte[] tmpByteBuffer = this.ByteBuffer_Prop;
				Stream tmpStream = this.Stream_Prop;
				while (count > 0)
				{
					int i = this.CharLen_Prop - this.CharPos_Prop;
					if (i == 0)
					{
						this.CharLen_Prop = 0;
						this.CharPos_Prop = 0;
						if (!this.CheckPreamble_Prop)
						{
							this.ByteLen_Prop = 0;
						}
						readToUserBuffer = count >= this.MaxCharsPerBuffer_Prop;
						do
						{
							if (this.CheckPreamble_Prop)
							{
								int bytePos_Prop = this.BytePos_Prop;
								int num3 = await tmpStream.ReadAsync(tmpByteBuffer, bytePos_Prop, tmpByteBuffer.Length - bytePos_Prop).ConfigureAwait(false);
								if (num3 == 0)
								{
									goto Block_6;
								}
								this.ByteLen_Prop += num3;
							}
							else
							{
								this.ByteLen_Prop = await tmpStream.ReadAsync(tmpByteBuffer, 0, tmpByteBuffer.Length).ConfigureAwait(false);
								if (this.ByteLen_Prop == 0)
								{
									goto Block_9;
								}
							}
							this.IsBlocked_Prop = this.ByteLen_Prop < tmpByteBuffer.Length;
							if (!this.IsPreamble())
							{
								if (this.DetectEncoding_Prop && this.ByteLen_Prop >= 2)
								{
									this.DetectEncoding();
									readToUserBuffer = count >= this.MaxCharsPerBuffer_Prop;
								}
								this.CharPos_Prop = 0;
								if (readToUserBuffer)
								{
									i += this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, buffer, index + charsRead);
									this.CharLen_Prop = 0;
								}
								else
								{
									i = this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, 0);
									this.CharLen_Prop += i;
								}
							}
						}
						while (i == 0);
						IL_3EE:
						if (i != 0)
						{
							goto IL_3F9;
						}
						break;
						Block_9:
						this.IsBlocked_Prop = true;
						goto IL_3EE;
						Block_6:
						if (this.ByteLen_Prop > 0)
						{
							if (readToUserBuffer)
							{
								i = this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, buffer, index + charsRead);
								this.CharLen_Prop = 0;
							}
							else
							{
								i = this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, 0);
								this.CharLen_Prop += i;
							}
						}
						this.IsBlocked_Prop = true;
						goto IL_3EE;
					}
					IL_3F9:
					if (i > count)
					{
						i = count;
					}
					if (!readToUserBuffer)
					{
						Buffer.InternalBlockCopy(this.CharBuffer_Prop, this.CharPos_Prop * 2, buffer, (index + charsRead) * 2, i * 2);
						this.CharPos_Prop += i;
					}
					charsRead += i;
					count -= i;
					if (this.IsBlocked_Prop)
					{
						break;
					}
				}
				num2 = charsRead;
			}
			return num2;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x000563D8 File Offset: 0x000545D8
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (base.GetType() != typeof(StreamReader))
			{
				return base.ReadBlockAsync(buffer, index, count);
			}
			if (this.stream == null)
			{
				__Error.ReaderClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task<int> task = base.ReadBlockAsync(buffer, index, count);
			this._asyncReadTask = task;
			return task;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x00056485 File Offset: 0x00054685
		// (set) Token: 0x060019CE RID: 6606 RVA: 0x0005648D File Offset: 0x0005468D
		private int CharLen_Prop
		{
			get
			{
				return this.charLen;
			}
			set
			{
				this.charLen = value;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x00056496 File Offset: 0x00054696
		// (set) Token: 0x060019D0 RID: 6608 RVA: 0x0005649E File Offset: 0x0005469E
		private int CharPos_Prop
		{
			get
			{
				return this.charPos;
			}
			set
			{
				this.charPos = value;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x000564A7 File Offset: 0x000546A7
		// (set) Token: 0x060019D2 RID: 6610 RVA: 0x000564AF File Offset: 0x000546AF
		private int ByteLen_Prop
		{
			get
			{
				return this.byteLen;
			}
			set
			{
				this.byteLen = value;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x000564B8 File Offset: 0x000546B8
		// (set) Token: 0x060019D4 RID: 6612 RVA: 0x000564C0 File Offset: 0x000546C0
		private int BytePos_Prop
		{
			get
			{
				return this.bytePos;
			}
			set
			{
				this.bytePos = value;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x000564C9 File Offset: 0x000546C9
		private byte[] Preamble_Prop
		{
			get
			{
				return this._preamble;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x000564D1 File Offset: 0x000546D1
		private bool CheckPreamble_Prop
		{
			get
			{
				return this._checkPreamble;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x000564D9 File Offset: 0x000546D9
		private Decoder Decoder_Prop
		{
			get
			{
				return this.decoder;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x000564E1 File Offset: 0x000546E1
		private bool DetectEncoding_Prop
		{
			get
			{
				return this._detectEncoding;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x000564E9 File Offset: 0x000546E9
		private char[] CharBuffer_Prop
		{
			get
			{
				return this.charBuffer;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x000564F1 File Offset: 0x000546F1
		private byte[] ByteBuffer_Prop
		{
			get
			{
				return this.byteBuffer;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x000564F9 File Offset: 0x000546F9
		// (set) Token: 0x060019DC RID: 6620 RVA: 0x00056501 File Offset: 0x00054701
		private bool IsBlocked_Prop
		{
			get
			{
				return this._isBlocked;
			}
			set
			{
				this._isBlocked = value;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x0005650A File Offset: 0x0005470A
		private Stream Stream_Prop
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x00056512 File Offset: 0x00054712
		private int MaxCharsPerBuffer_Prop
		{
			get
			{
				return this._maxCharsPerBuffer;
			}
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x0005651C File Offset: 0x0005471C
		private async Task<int> ReadBufferAsync()
		{
			this.CharLen_Prop = 0;
			this.CharPos_Prop = 0;
			byte[] tmpByteBuffer = this.ByteBuffer_Prop;
			Stream tmpStream = this.Stream_Prop;
			if (!this.CheckPreamble_Prop)
			{
				this.ByteLen_Prop = 0;
			}
			for (;;)
			{
				if (this.CheckPreamble_Prop)
				{
					int bytePos_Prop = this.BytePos_Prop;
					int num = await tmpStream.ReadAsync(tmpByteBuffer, bytePos_Prop, tmpByteBuffer.Length - bytePos_Prop).ConfigureAwait(false);
					int num2 = num;
					if (num2 == 0)
					{
						break;
					}
					this.ByteLen_Prop += num2;
				}
				else
				{
					this.ByteLen_Prop = await tmpStream.ReadAsync(tmpByteBuffer, 0, tmpByteBuffer.Length).ConfigureAwait(false);
					if (this.ByteLen_Prop == 0)
					{
						goto Block_5;
					}
				}
				this.IsBlocked_Prop = this.ByteLen_Prop < tmpByteBuffer.Length;
				if (!this.IsPreamble())
				{
					if (this.DetectEncoding_Prop && this.ByteLen_Prop >= 2)
					{
						this.DetectEncoding();
					}
					this.CharLen_Prop += this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, this.CharLen_Prop);
				}
				if (this.CharLen_Prop != 0)
				{
					goto Block_9;
				}
			}
			if (this.ByteLen_Prop > 0)
			{
				this.CharLen_Prop += this.Decoder_Prop.GetChars(tmpByteBuffer, 0, this.ByteLen_Prop, this.CharBuffer_Prop, this.CharLen_Prop);
				this.BytePos_Prop = 0;
				this.ByteLen_Prop = 0;
			}
			return this.CharLen_Prop;
			Block_5:
			return this.CharLen_Prop;
			Block_9:
			return this.CharLen_Prop;
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x0005655F File Offset: 0x0005475F
		// Note: this type is marked as 'beforefieldinit'.
		static StreamReader()
		{
		}

		// Token: 0x040008F8 RID: 2296
		[__DynamicallyInvokable]
		public new static readonly StreamReader Null = new StreamReader.NullStreamReader();

		// Token: 0x040008F9 RID: 2297
		private const int DefaultFileStreamBufferSize = 4096;

		// Token: 0x040008FA RID: 2298
		private const int MinBufferSize = 128;

		// Token: 0x040008FB RID: 2299
		private Stream stream;

		// Token: 0x040008FC RID: 2300
		private Encoding encoding;

		// Token: 0x040008FD RID: 2301
		private Decoder decoder;

		// Token: 0x040008FE RID: 2302
		private byte[] byteBuffer;

		// Token: 0x040008FF RID: 2303
		private char[] charBuffer;

		// Token: 0x04000900 RID: 2304
		private byte[] _preamble;

		// Token: 0x04000901 RID: 2305
		private int charPos;

		// Token: 0x04000902 RID: 2306
		private int charLen;

		// Token: 0x04000903 RID: 2307
		private int byteLen;

		// Token: 0x04000904 RID: 2308
		private int bytePos;

		// Token: 0x04000905 RID: 2309
		private int _maxCharsPerBuffer;

		// Token: 0x04000906 RID: 2310
		private bool _detectEncoding;

		// Token: 0x04000907 RID: 2311
		private bool _checkPreamble;

		// Token: 0x04000908 RID: 2312
		private bool _isBlocked;

		// Token: 0x04000909 RID: 2313
		private bool _closable;

		// Token: 0x0400090A RID: 2314
		[NonSerialized]
		private volatile Task _asyncReadTask;

		// Token: 0x02000B1B RID: 2843
		private class NullStreamReader : StreamReader
		{
			// Token: 0x06006AED RID: 27373 RVA: 0x001713A2 File Offset: 0x0016F5A2
			internal NullStreamReader()
			{
				base.Init(Stream.Null);
			}

			// Token: 0x17001218 RID: 4632
			// (get) Token: 0x06006AEE RID: 27374 RVA: 0x001713B5 File Offset: 0x0016F5B5
			public override Stream BaseStream
			{
				get
				{
					return Stream.Null;
				}
			}

			// Token: 0x17001219 RID: 4633
			// (get) Token: 0x06006AEF RID: 27375 RVA: 0x001713BC File Offset: 0x0016F5BC
			public override Encoding CurrentEncoding
			{
				get
				{
					return Encoding.Unicode;
				}
			}

			// Token: 0x06006AF0 RID: 27376 RVA: 0x001713C3 File Offset: 0x0016F5C3
			protected override void Dispose(bool disposing)
			{
			}

			// Token: 0x06006AF1 RID: 27377 RVA: 0x001713C5 File Offset: 0x0016F5C5
			public override int Peek()
			{
				return -1;
			}

			// Token: 0x06006AF2 RID: 27378 RVA: 0x001713C8 File Offset: 0x0016F5C8
			public override int Read()
			{
				return -1;
			}

			// Token: 0x06006AF3 RID: 27379 RVA: 0x001713CB File Offset: 0x0016F5CB
			public override int Read(char[] buffer, int index, int count)
			{
				return 0;
			}

			// Token: 0x06006AF4 RID: 27380 RVA: 0x001713CE File Offset: 0x0016F5CE
			public override string ReadLine()
			{
				return null;
			}

			// Token: 0x06006AF5 RID: 27381 RVA: 0x001713D1 File Offset: 0x0016F5D1
			public override string ReadToEnd()
			{
				return string.Empty;
			}

			// Token: 0x06006AF6 RID: 27382 RVA: 0x001713D8 File Offset: 0x0016F5D8
			internal override int ReadBuffer()
			{
				return 0;
			}
		}

		// Token: 0x02000B1C RID: 2844
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadLineAsyncInternal>d__60 : IAsyncStateMachine
		{
			// Token: 0x06006AF7 RID: 27383 RVA: 0x001713DC File Offset: 0x0016F5DC
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				StreamReader streamReader = this;
				string text;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter3;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
					bool flag2;
					switch (num)
					{
					case 0:
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num2 = -1;
						break;
					}
					case 1:
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter3 = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_1C0;
					}
					case 2:
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter4 = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_2A4;
					}
					default:
					{
						bool flag = streamReader.CharPos_Prop == streamReader.CharLen_Prop;
						flag2 = flag;
						if (!flag2)
						{
							goto IL_A7;
						}
						configuredTaskAwaiter = streamReader.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num2 = 0;
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadLineAsyncInternal>d__60>(ref configuredTaskAwaiter, ref this);
							return;
						}
						break;
					}
					}
					int result = configuredTaskAwaiter.GetResult();
					flag2 = result == 0;
					IL_A7:
					if (flag2)
					{
						text = null;
						goto IL_2E3;
					}
					sb = null;
					IL_B9:
					char[] charBuffer_Prop = streamReader.CharBuffer_Prop;
					int charLen_Prop = streamReader.CharLen_Prop;
					int num3 = streamReader.CharPos_Prop;
					int num4 = num3;
					char c;
					for (;;)
					{
						c = charBuffer_Prop[num4];
						if (c == '\r' || c == '\n')
						{
							break;
						}
						num4++;
						if (num4 >= charLen_Prop)
						{
							goto Block_14;
						}
					}
					if (sb != null)
					{
						sb.Append(charBuffer_Prop, num3, num4 - num3);
						s = sb.ToString();
					}
					else
					{
						s = new string(charBuffer_Prop, num3, num4 - num3);
					}
					num3 = (streamReader.CharPos_Prop = num4 + 1);
					bool flag3 = c == '\r';
					bool flag4 = flag3;
					if (!flag4)
					{
						goto IL_1D4;
					}
					bool flag5 = num3 < charLen_Prop;
					bool flag6 = flag5;
					if (flag6)
					{
						goto IL_1D0;
					}
					configuredTaskAwaiter3 = streamReader.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter3.IsCompleted)
					{
						num2 = 1;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadLineAsyncInternal>d__60>(ref configuredTaskAwaiter3, ref this);
						return;
					}
					goto IL_1C0;
					Block_14:
					num4 = charLen_Prop - num3;
					if (sb == null)
					{
						sb = new StringBuilder(num4 + 80);
					}
					sb.Append(charBuffer_Prop, num3, num4);
					configuredTaskAwaiter4 = streamReader.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter4.IsCompleted)
					{
						num2 = 2;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter4;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadLineAsyncInternal>d__60>(ref configuredTaskAwaiter4, ref this);
						return;
					}
					goto IL_2A4;
					IL_1C0:
					int result2 = configuredTaskAwaiter3.GetResult();
					flag6 = result2 > 0;
					IL_1D0:
					flag4 = flag6;
					IL_1D4:
					if (flag4)
					{
						num3 = streamReader.CharPos_Prop;
						if (streamReader.CharBuffer_Prop[num3] == '\n')
						{
							streamReader.CharPos_Prop = num3 + 1;
						}
					}
					text = s;
					goto IL_2E3;
					IL_2A4:
					int result3 = configuredTaskAwaiter4.GetResult();
					if (result3 > 0)
					{
						goto IL_B9;
					}
					text = sb.ToString();
				}
				catch (Exception ex)
				{
					num2 = -2;
					sb = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				IL_2E3:
				num2 = -2;
				sb = null;
				this.<>t__builder.SetResult(text);
			}

			// Token: 0x06006AF8 RID: 27384 RVA: 0x00171704 File Offset: 0x0016F904
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040032DC RID: 13020
			public int <>1__state;

			// Token: 0x040032DD RID: 13021
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x040032DE RID: 13022
			public StreamReader <>4__this;

			// Token: 0x040032DF RID: 13023
			private StringBuilder <sb>5__2;

			// Token: 0x040032E0 RID: 13024
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040032E1 RID: 13025
			private string <s>5__3;
		}

		// Token: 0x02000B1D RID: 2845
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadToEndAsyncInternal>d__62 : IAsyncStateMachine
		{
			// Token: 0x06006AF9 RID: 27385 RVA: 0x00171714 File Offset: 0x0016F914
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				StreamReader streamReader = this;
				string text;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter;
					if (num == 0)
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_B8;
					}
					sb = new StringBuilder(streamReader.CharLen_Prop - streamReader.CharPos_Prop);
					IL_2C:
					int charPos_Prop = streamReader.CharPos_Prop;
					sb.Append(streamReader.CharBuffer_Prop, charPos_Prop, streamReader.CharLen_Prop - charPos_Prop);
					streamReader.CharPos_Prop = streamReader.CharLen_Prop;
					configuredTaskAwaiter = streamReader.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						num2 = 0;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadToEndAsyncInternal>d__62>(ref configuredTaskAwaiter, ref this);
						return;
					}
					IL_B8:
					configuredTaskAwaiter.GetResult();
					if (streamReader.CharLen_Prop > 0)
					{
						goto IL_2C;
					}
					text = sb.ToString();
				}
				catch (Exception ex)
				{
					num2 = -2;
					sb = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				sb = null;
				this.<>t__builder.SetResult(text);
			}

			// Token: 0x06006AFA RID: 27386 RVA: 0x00171848 File Offset: 0x0016FA48
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040032E2 RID: 13026
			public int <>1__state;

			// Token: 0x040032E3 RID: 13027
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x040032E4 RID: 13028
			public StreamReader <>4__this;

			// Token: 0x040032E5 RID: 13029
			private StringBuilder <sb>5__2;

			// Token: 0x040032E6 RID: 13030
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000B1E RID: 2846
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAsyncInternal>d__64 : IAsyncStateMachine
		{
			// Token: 0x06006AFB RID: 27387 RVA: 0x00171858 File Offset: 0x0016FA58
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				StreamReader streamReader = this;
				int num3;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter3;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
					bool flag2;
					switch (num)
					{
					case 0:
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num2 = -1;
						break;
					}
					case 1:
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter3 = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_1BB;
					}
					case 2:
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter4 = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_2E7;
					}
					default:
					{
						bool flag = streamReader.CharPos_Prop == streamReader.CharLen_Prop;
						flag2 = flag;
						if (!flag2)
						{
							goto IL_A7;
						}
						configuredTaskAwaiter = streamReader.ReadBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num2 = 0;
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadAsyncInternal>d__64>(ref configuredTaskAwaiter, ref this);
							return;
						}
						break;
					}
					}
					int result = configuredTaskAwaiter.GetResult();
					flag2 = result == 0;
					IL_A7:
					if (flag2)
					{
						num3 = 0;
						goto IL_4C8;
					}
					charsRead = 0;
					readToUserBuffer = false;
					tmpByteBuffer = streamReader.ByteBuffer_Prop;
					tmpStream = streamReader.Stream_Prop;
					goto IL_48C;
					IL_12F:
					if (streamReader.CheckPreamble_Prop)
					{
						int bytePos_Prop = streamReader.BytePos_Prop;
						configuredTaskAwaiter3 = tmpStream.ReadAsync(tmpByteBuffer, bytePos_Prop, tmpByteBuffer.Length - bytePos_Prop).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							num2 = 1;
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadAsyncInternal>d__64>(ref configuredTaskAwaiter3, ref this);
							return;
						}
					}
					else
					{
						configuredTaskAwaiter4 = tmpStream.ReadAsync(tmpByteBuffer, 0, tmpByteBuffer.Length).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter4.IsCompleted)
						{
							num2 = 2;
							ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter4;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadAsyncInternal>d__64>(ref configuredTaskAwaiter4, ref this);
							return;
						}
						goto IL_2E7;
					}
					IL_1BB:
					int result2 = configuredTaskAwaiter3.GetResult();
					int num4 = result2;
					if (num4 == 0)
					{
						if (streamReader.ByteLen_Prop > 0)
						{
							if (readToUserBuffer)
							{
								i = streamReader.Decoder_Prop.GetChars(tmpByteBuffer, 0, streamReader.ByteLen_Prop, buffer, index + charsRead);
								streamReader.CharLen_Prop = 0;
							}
							else
							{
								i = streamReader.Decoder_Prop.GetChars(tmpByteBuffer, 0, streamReader.ByteLen_Prop, streamReader.CharBuffer_Prop, 0);
								streamReader.CharLen_Prop += i;
							}
						}
						streamReader.IsBlocked_Prop = true;
						goto IL_3EE;
					}
					streamReader.ByteLen_Prop += num4;
					goto IL_30C;
					IL_2E7:
					int result3 = configuredTaskAwaiter4.GetResult();
					streamReader.ByteLen_Prop = result3;
					if (streamReader.ByteLen_Prop == 0)
					{
						streamReader.IsBlocked_Prop = true;
						goto IL_3EE;
					}
					IL_30C:
					streamReader.IsBlocked_Prop = streamReader.ByteLen_Prop < tmpByteBuffer.Length;
					if (!streamReader.IsPreamble())
					{
						if (streamReader.DetectEncoding_Prop && streamReader.ByteLen_Prop >= 2)
						{
							streamReader.DetectEncoding();
							readToUserBuffer = count >= streamReader.MaxCharsPerBuffer_Prop;
						}
						streamReader.CharPos_Prop = 0;
						if (readToUserBuffer)
						{
							i += streamReader.Decoder_Prop.GetChars(tmpByteBuffer, 0, streamReader.ByteLen_Prop, buffer, index + charsRead);
							streamReader.CharLen_Prop = 0;
						}
						else
						{
							i = streamReader.Decoder_Prop.GetChars(tmpByteBuffer, 0, streamReader.ByteLen_Prop, streamReader.CharBuffer_Prop, 0);
							streamReader.CharLen_Prop += i;
						}
					}
					if (i == 0)
					{
						goto IL_12F;
					}
					IL_3EE:
					if (i == 0)
					{
						goto IL_498;
					}
					IL_3F9:
					if (i > count)
					{
						i = count;
					}
					if (!readToUserBuffer)
					{
						Buffer.InternalBlockCopy(streamReader.CharBuffer_Prop, streamReader.CharPos_Prop * 2, buffer, (index + charsRead) * 2, i * 2);
						streamReader.CharPos_Prop += i;
					}
					charsRead += i;
					count -= i;
					if (streamReader.IsBlocked_Prop)
					{
						goto IL_498;
					}
					IL_48C:
					if (count > 0)
					{
						i = streamReader.CharLen_Prop - streamReader.CharPos_Prop;
						if (i == 0)
						{
							streamReader.CharLen_Prop = 0;
							streamReader.CharPos_Prop = 0;
							if (!streamReader.CheckPreamble_Prop)
							{
								streamReader.ByteLen_Prop = 0;
							}
							readToUserBuffer = count >= streamReader.MaxCharsPerBuffer_Prop;
							goto IL_12F;
						}
						goto IL_3F9;
					}
					IL_498:
					num3 = charsRead;
				}
				catch (Exception ex)
				{
					num2 = -2;
					tmpByteBuffer = null;
					tmpStream = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				IL_4C8:
				num2 = -2;
				tmpByteBuffer = null;
				tmpStream = null;
				this.<>t__builder.SetResult(num3);
			}

			// Token: 0x06006AFC RID: 27388 RVA: 0x00171D6C File Offset: 0x0016FF6C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040032E7 RID: 13031
			public int <>1__state;

			// Token: 0x040032E8 RID: 13032
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x040032E9 RID: 13033
			public StreamReader <>4__this;

			// Token: 0x040032EA RID: 13034
			public int count;

			// Token: 0x040032EB RID: 13035
			public char[] buffer;

			// Token: 0x040032EC RID: 13036
			public int index;

			// Token: 0x040032ED RID: 13037
			private int <charsRead>5__2;

			// Token: 0x040032EE RID: 13038
			private bool <readToUserBuffer>5__3;

			// Token: 0x040032EF RID: 13039
			private byte[] <tmpByteBuffer>5__4;

			// Token: 0x040032F0 RID: 13040
			private Stream <tmpStream>5__5;

			// Token: 0x040032F1 RID: 13041
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040032F2 RID: 13042
			private int <n>5__6;
		}

		// Token: 0x02000B1F RID: 2847
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadBufferAsync>d__97 : IAsyncStateMachine
		{
			// Token: 0x06006AFD RID: 27389 RVA: 0x00171D7C File Offset: 0x0016FF7C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				StreamReader streamReader = this;
				int num4;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter;
					if (num == 0)
					{
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_D9;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter3;
					if (num == 1)
					{
						configuredTaskAwaiter3 = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_1C7;
					}
					streamReader.CharLen_Prop = 0;
					streamReader.CharPos_Prop = 0;
					tmpByteBuffer = streamReader.ByteBuffer_Prop;
					tmpStream = streamReader.Stream_Prop;
					if (!streamReader.CheckPreamble_Prop)
					{
						streamReader.ByteLen_Prop = 0;
					}
					IL_50:
					if (streamReader.CheckPreamble_Prop)
					{
						int bytePos_Prop = streamReader.BytePos_Prop;
						configuredTaskAwaiter = tmpStream.ReadAsync(tmpByteBuffer, bytePos_Prop, tmpByteBuffer.Length - bytePos_Prop).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num2 = 0;
							configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadBufferAsync>d__97>(ref configuredTaskAwaiter, ref this);
							return;
						}
					}
					else
					{
						configuredTaskAwaiter3 = tmpStream.ReadAsync(tmpByteBuffer, 0, tmpByteBuffer.Length).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							num2 = 1;
							configuredTaskAwaiter2 = configuredTaskAwaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, StreamReader.<ReadBufferAsync>d__97>(ref configuredTaskAwaiter3, ref this);
							return;
						}
						goto IL_1C7;
					}
					IL_D9:
					int result = configuredTaskAwaiter.GetResult();
					int num3 = result;
					if (num3 == 0)
					{
						if (streamReader.ByteLen_Prop > 0)
						{
							streamReader.CharLen_Prop += streamReader.Decoder_Prop.GetChars(tmpByteBuffer, 0, streamReader.ByteLen_Prop, streamReader.CharBuffer_Prop, streamReader.CharLen_Prop);
							streamReader.BytePos_Prop = 0;
							streamReader.ByteLen_Prop = 0;
						}
						num4 = streamReader.CharLen_Prop;
						goto IL_28D;
					}
					streamReader.ByteLen_Prop += num3;
					goto IL_1EC;
					IL_1C7:
					int result2 = configuredTaskAwaiter3.GetResult();
					streamReader.ByteLen_Prop = result2;
					if (streamReader.ByteLen_Prop == 0)
					{
						num4 = streamReader.CharLen_Prop;
						goto IL_28D;
					}
					IL_1EC:
					streamReader.IsBlocked_Prop = streamReader.ByteLen_Prop < tmpByteBuffer.Length;
					if (!streamReader.IsPreamble())
					{
						if (streamReader.DetectEncoding_Prop && streamReader.ByteLen_Prop >= 2)
						{
							streamReader.DetectEncoding();
						}
						streamReader.CharLen_Prop += streamReader.Decoder_Prop.GetChars(tmpByteBuffer, 0, streamReader.ByteLen_Prop, streamReader.CharBuffer_Prop, streamReader.CharLen_Prop);
					}
					if (streamReader.CharLen_Prop == 0)
					{
						goto IL_50;
					}
					num4 = streamReader.CharLen_Prop;
				}
				catch (Exception ex)
				{
					num2 = -2;
					tmpByteBuffer = null;
					tmpStream = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				IL_28D:
				num2 = -2;
				tmpByteBuffer = null;
				tmpStream = null;
				this.<>t__builder.SetResult(num4);
			}

			// Token: 0x06006AFE RID: 27390 RVA: 0x00172054 File Offset: 0x00170254
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040032F3 RID: 13043
			public int <>1__state;

			// Token: 0x040032F4 RID: 13044
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x040032F5 RID: 13045
			public StreamReader <>4__this;

			// Token: 0x040032F6 RID: 13046
			private byte[] <tmpByteBuffer>5__2;

			// Token: 0x040032F7 RID: 13047
			private Stream <tmpStream>5__3;

			// Token: 0x040032F8 RID: 13048
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
