using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020001A3 RID: 419
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class StreamWriter : TextWriter
	{
		// Token: 0x060019E1 RID: 6625 RVA: 0x0005656C File Offset: 0x0005476C
		private void CheckAsyncTaskInProgress()
		{
			Task asyncWriteTask = this._asyncWriteTask;
			if (asyncWriteTask != null && !asyncWriteTask.IsCompleted)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AsyncIOInProgress"));
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x000565A0 File Offset: 0x000547A0
		internal static Encoding UTF8NoBOM
		{
			[FriendAccessAllowed]
			get
			{
				if (StreamWriter._UTF8NoBOM == null)
				{
					UTF8Encoding utf8Encoding = new UTF8Encoding(false, true);
					Thread.MemoryBarrier();
					StreamWriter._UTF8NoBOM = utf8Encoding;
				}
				return StreamWriter._UTF8NoBOM;
			}
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x000565D2 File Offset: 0x000547D2
		internal StreamWriter()
			: base(null)
		{
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x000565DB File Offset: 0x000547DB
		[__DynamicallyInvokable]
		public StreamWriter(Stream stream)
			: this(stream, StreamWriter.UTF8NoBOM, 1024, false)
		{
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x000565EF File Offset: 0x000547EF
		[__DynamicallyInvokable]
		public StreamWriter(Stream stream, Encoding encoding)
			: this(stream, encoding, 1024, false)
		{
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x000565FF File Offset: 0x000547FF
		[__DynamicallyInvokable]
		public StreamWriter(Stream stream, Encoding encoding, int bufferSize)
			: this(stream, encoding, bufferSize, false)
		{
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x0005660C File Offset: 0x0005480C
		[__DynamicallyInvokable]
		public StreamWriter(Stream stream, Encoding encoding, int bufferSize, bool leaveOpen)
			: base(null)
		{
			if (stream == null || encoding == null)
			{
				throw new ArgumentNullException((stream == null) ? "stream" : "encoding");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotWritable"));
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			this.Init(stream, encoding, bufferSize, leaveOpen);
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00056677 File Offset: 0x00054877
		public StreamWriter(string path)
			: this(path, false, StreamWriter.UTF8NoBOM, 1024)
		{
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x0005668B File Offset: 0x0005488B
		public StreamWriter(string path, bool append)
			: this(path, append, StreamWriter.UTF8NoBOM, 1024)
		{
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x0005669F File Offset: 0x0005489F
		public StreamWriter(string path, bool append, Encoding encoding)
			: this(path, append, encoding, 1024)
		{
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x000566AF File Offset: 0x000548AF
		[SecuritySafeCritical]
		public StreamWriter(string path, bool append, Encoding encoding, int bufferSize)
			: this(path, append, encoding, bufferSize, true)
		{
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000566C0 File Offset: 0x000548C0
		[SecurityCritical]
		internal StreamWriter(string path, bool append, Encoding encoding, int bufferSize, bool checkHost)
			: base(null)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			Stream stream = StreamWriter.CreateFile(path, append, checkHost);
			this.Init(stream, encoding, bufferSize, false);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00056738 File Offset: 0x00054938
		[SecuritySafeCritical]
		private void Init(Stream streamArg, Encoding encodingArg, int bufferSize, bool shouldLeaveOpen)
		{
			this.stream = streamArg;
			this.encoding = encodingArg;
			this.encoder = this.encoding.GetEncoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this.charBuffer = new char[bufferSize];
			this.byteBuffer = new byte[this.encoding.GetMaxByteCount(bufferSize)];
			this.charLen = bufferSize;
			if (this.stream.CanSeek && this.stream.Position > 0L)
			{
				this.haveWrittenPreamble = true;
			}
			this.closable = !shouldLeaveOpen;
			if (Mda.StreamWriterBufferedDataLost.Enabled)
			{
				string text = null;
				if (Mda.StreamWriterBufferedDataLost.CaptureAllocatedCallStack)
				{
					text = Environment.GetStackTrace(null, false);
				}
				this.mdaHelper = new StreamWriter.MdaHelper(this, text);
			}
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x000567F0 File Offset: 0x000549F0
		[SecurityCritical]
		private static Stream CreateFile(string path, bool append, bool checkHost)
		{
			FileMode fileMode = (append ? FileMode.Append : FileMode.Create);
			return new FileStream(path, fileMode, FileAccess.Write, FileShare.Read, 4096, FileOptions.SequentialScan, Path.GetFileName(path), false, false, checkHost);
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00056823 File Offset: 0x00054A23
		public override void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x00056834 File Offset: 0x00054A34
		[__DynamicallyInvokable]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.stream != null && (disposing || (this.LeaveOpen && this.stream is __ConsoleStream)))
				{
					this.CheckAsyncTaskInProgress();
					this.Flush(true, true);
					if (this.mdaHelper != null)
					{
						GC.SuppressFinalize(this.mdaHelper);
					}
				}
			}
			finally
			{
				if (!this.LeaveOpen && this.stream != null)
				{
					try
					{
						if (disposing)
						{
							this.stream.Close();
						}
					}
					finally
					{
						this.stream = null;
						this.byteBuffer = null;
						this.charBuffer = null;
						this.encoding = null;
						this.encoder = null;
						this.charLen = 0;
						base.Dispose(disposing);
					}
				}
			}
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x000568F4 File Offset: 0x00054AF4
		[__DynamicallyInvokable]
		public override void Flush()
		{
			this.CheckAsyncTaskInProgress();
			this.Flush(true, true);
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00056904 File Offset: 0x00054B04
		private void Flush(bool flushStream, bool flushEncoder)
		{
			if (this.stream == null)
			{
				__Error.WriterClosed();
			}
			if (this.charPos == 0 && ((!flushStream && !flushEncoder) || CompatibilitySwitches.IsAppEarlierThanWindowsPhone8))
			{
				return;
			}
			if (!this.haveWrittenPreamble)
			{
				this.haveWrittenPreamble = true;
				byte[] preamble = this.encoding.GetPreamble();
				if (preamble.Length != 0)
				{
					this.stream.Write(preamble, 0, preamble.Length);
				}
			}
			int bytes = this.encoder.GetBytes(this.charBuffer, 0, this.charPos, this.byteBuffer, 0, flushEncoder);
			this.charPos = 0;
			if (bytes > 0)
			{
				this.stream.Write(this.byteBuffer, 0, bytes);
			}
			if (flushStream)
			{
				this.stream.Flush();
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x000569B0 File Offset: 0x00054BB0
		// (set) Token: 0x060019F4 RID: 6644 RVA: 0x000569B8 File Offset: 0x00054BB8
		[__DynamicallyInvokable]
		public virtual bool AutoFlush
		{
			[__DynamicallyInvokable]
			get
			{
				return this.autoFlush;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckAsyncTaskInProgress();
				this.autoFlush = value;
				if (value)
				{
					this.Flush(true, false);
				}
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x000569D2 File Offset: 0x00054BD2
		[__DynamicallyInvokable]
		public virtual Stream BaseStream
		{
			[__DynamicallyInvokable]
			get
			{
				return this.stream;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x000569DA File Offset: 0x00054BDA
		internal bool LeaveOpen
		{
			get
			{
				return !this.closable;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (set) Token: 0x060019F7 RID: 6647 RVA: 0x000569E5 File Offset: 0x00054BE5
		internal bool HaveWrittenPreamble
		{
			set
			{
				this.haveWrittenPreamble = value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x000569EE File Offset: 0x00054BEE
		[__DynamicallyInvokable]
		public override Encoding Encoding
		{
			[__DynamicallyInvokable]
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x000569F8 File Offset: 0x00054BF8
		[__DynamicallyInvokable]
		public override void Write(char value)
		{
			this.CheckAsyncTaskInProgress();
			if (this.charPos == this.charLen)
			{
				this.Flush(false, false);
			}
			this.charBuffer[this.charPos] = value;
			this.charPos++;
			if (this.autoFlush)
			{
				this.Flush(true, false);
			}
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x00056A50 File Offset: 0x00054C50
		[__DynamicallyInvokable]
		public override void Write(char[] buffer)
		{
			if (buffer == null)
			{
				return;
			}
			this.CheckAsyncTaskInProgress();
			int num = 0;
			int num2;
			for (int i = buffer.Length; i > 0; i -= num2)
			{
				if (this.charPos == this.charLen)
				{
					this.Flush(false, false);
				}
				num2 = this.charLen - this.charPos;
				if (num2 > i)
				{
					num2 = i;
				}
				Buffer.InternalBlockCopy(buffer, num * 2, this.charBuffer, this.charPos * 2, num2 * 2);
				this.charPos += num2;
				num += num2;
			}
			if (this.autoFlush)
			{
				this.Flush(true, false);
			}
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00056AE0 File Offset: 0x00054CE0
		[__DynamicallyInvokable]
		public override void Write(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			this.CheckAsyncTaskInProgress();
			while (count > 0)
			{
				if (this.charPos == this.charLen)
				{
					this.Flush(false, false);
				}
				int num = this.charLen - this.charPos;
				if (num > count)
				{
					num = count;
				}
				Buffer.InternalBlockCopy(buffer, index * 2, this.charBuffer, this.charPos * 2, num * 2);
				this.charPos += num;
				index += num;
				count -= num;
			}
			if (this.autoFlush)
			{
				this.Flush(true, false);
			}
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00056BC8 File Offset: 0x00054DC8
		[__DynamicallyInvokable]
		public override void Write(string value)
		{
			if (value != null)
			{
				this.CheckAsyncTaskInProgress();
				int i = value.Length;
				int num = 0;
				while (i > 0)
				{
					if (this.charPos == this.charLen)
					{
						this.Flush(false, false);
					}
					int num2 = this.charLen - this.charPos;
					if (num2 > i)
					{
						num2 = i;
					}
					value.CopyTo(num, this.charBuffer, this.charPos, num2);
					this.charPos += num2;
					num += num2;
					i -= num2;
				}
				if (this.autoFlush)
				{
					this.Flush(true, false);
				}
			}
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00056C54 File Offset: 0x00054E54
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(char value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(value);
			}
			if (this.stream == null)
			{
				__Error.WriterClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, false);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00056CC4 File Offset: 0x00054EC4
		private static async Task WriteAsyncInternal(StreamWriter _this, char value, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
		{
			if (charPos == charLen)
			{
				await _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
				charPos = 0;
			}
			charBuffer[charPos] = value;
			charPos++;
			if (appendNewLine)
			{
				for (int i = 0; i < coreNewLine.Length; i++)
				{
					if (charPos == charLen)
					{
						await _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
						charPos = 0;
					}
					charBuffer[charPos] = coreNewLine[i];
					charPos++;
				}
			}
			if (autoFlush)
			{
				await _this.FlushAsyncInternal(true, false, charBuffer, charPos).ConfigureAwait(false);
				charPos = 0;
			}
			_this.CharPos_Prop = charPos;
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00056D44 File Offset: 0x00054F44
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(string value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(value);
			}
			if (value != null)
			{
				if (this.stream == null)
				{
					__Error.WriterClosed();
				}
				this.CheckAsyncTaskInProgress();
				Task task = StreamWriter.WriteAsyncInternal(this, value, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, false);
				this._asyncWriteTask = task;
				return task;
			}
			return Task.CompletedTask;
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x00056DC0 File Offset: 0x00054FC0
		private static async Task WriteAsyncInternal(StreamWriter _this, string value, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
		{
			int count = value.Length;
			int index = 0;
			while (count > 0)
			{
				if (charPos == charLen)
				{
					await _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
					charPos = 0;
				}
				int num = charLen - charPos;
				if (num > count)
				{
					num = count;
				}
				value.CopyTo(index, charBuffer, charPos, num);
				charPos += num;
				index += num;
				count -= num;
			}
			if (appendNewLine)
			{
				for (int i = 0; i < coreNewLine.Length; i++)
				{
					if (charPos == charLen)
					{
						await _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
						charPos = 0;
					}
					charBuffer[charPos] = coreNewLine[i];
					charPos++;
				}
			}
			if (autoFlush)
			{
				await _this.FlushAsyncInternal(true, false, charBuffer, charPos).ConfigureAwait(false);
				charPos = 0;
			}
			_this.CharPos_Prop = charPos;
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00056E40 File Offset: 0x00055040
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteAsync(buffer, index, count);
			}
			if (this.stream == null)
			{
				__Error.WriterClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, buffer, index, count, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, false);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00056F18 File Offset: 0x00055118
		private static async Task WriteAsyncInternal(StreamWriter _this, char[] buffer, int index, int count, char[] charBuffer, int charPos, int charLen, char[] coreNewLine, bool autoFlush, bool appendNewLine)
		{
			while (count > 0)
			{
				if (charPos == charLen)
				{
					await _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
					charPos = 0;
				}
				int num = charLen - charPos;
				if (num > count)
				{
					num = count;
				}
				Buffer.InternalBlockCopy(buffer, index * 2, charBuffer, charPos * 2, num * 2);
				charPos += num;
				index += num;
				count -= num;
			}
			if (appendNewLine)
			{
				for (int i = 0; i < coreNewLine.Length; i++)
				{
					if (charPos == charLen)
					{
						await _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false);
						charPos = 0;
					}
					charBuffer[charPos] = coreNewLine[i];
					charPos++;
				}
			}
			if (autoFlush)
			{
				await _this.FlushAsyncInternal(true, false, charBuffer, charPos).ConfigureAwait(false);
				charPos = 0;
			}
			_this.CharPos_Prop = charPos;
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00056FAC File Offset: 0x000551AC
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync()
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync();
			}
			if (this.stream == null)
			{
				__Error.WriterClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, null, 0, 0, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, true);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00057020 File Offset: 0x00055220
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(char value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(value);
			}
			if (this.stream == null)
			{
				__Error.WriterClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, true);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x00057090 File Offset: 0x00055290
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(string value)
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(value);
			}
			if (this.stream == null)
			{
				__Error.WriterClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, value, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, true);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00057100 File Offset: 0x00055300
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteLineAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.WriteLineAsync(buffer, index, count);
			}
			if (this.stream == null)
			{
				__Error.WriterClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task task = StreamWriter.WriteAsyncInternal(this, buffer, index, count, this.charBuffer, this.charPos, this.charLen, this.CoreNewLine, this.autoFlush, true);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x000571D8 File Offset: 0x000553D8
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task FlushAsync()
		{
			if (base.GetType() != typeof(StreamWriter))
			{
				return base.FlushAsync();
			}
			if (this.stream == null)
			{
				__Error.WriterClosed();
			}
			this.CheckAsyncTaskInProgress();
			Task task = this.FlushAsyncInternal(true, true, this.charBuffer, this.charPos);
			this._asyncWriteTask = task;
			return task;
		}

		// Token: 0x170002E9 RID: 745
		// (set) Token: 0x06001A08 RID: 6664 RVA: 0x00057235 File Offset: 0x00055435
		private int CharPos_Prop
		{
			set
			{
				this.charPos = value;
			}
		}

		// Token: 0x170002EA RID: 746
		// (set) Token: 0x06001A09 RID: 6665 RVA: 0x0005723E File Offset: 0x0005543E
		private bool HaveWrittenPreamble_Prop
		{
			set
			{
				this.haveWrittenPreamble = value;
			}
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00057248 File Offset: 0x00055448
		private Task FlushAsyncInternal(bool flushStream, bool flushEncoder, char[] sCharBuffer, int sCharPos)
		{
			if (sCharPos == 0 && !flushStream && !flushEncoder)
			{
				return Task.CompletedTask;
			}
			Task task = StreamWriter.FlushAsyncInternal(this, flushStream, flushEncoder, sCharBuffer, sCharPos, this.haveWrittenPreamble, this.encoding, this.encoder, this.byteBuffer, this.stream);
			this.charPos = 0;
			return task;
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00057298 File Offset: 0x00055498
		private static async Task FlushAsyncInternal(StreamWriter _this, bool flushStream, bool flushEncoder, char[] charBuffer, int charPos, bool haveWrittenPreamble, Encoding encoding, Encoder encoder, byte[] byteBuffer, Stream stream)
		{
			if (!haveWrittenPreamble)
			{
				_this.HaveWrittenPreamble_Prop = true;
				byte[] preamble = encoding.GetPreamble();
				if (preamble.Length != 0)
				{
					await stream.WriteAsync(preamble, 0, preamble.Length).ConfigureAwait(false);
				}
			}
			int bytes = encoder.GetBytes(charBuffer, 0, charPos, byteBuffer, 0, flushEncoder);
			if (bytes > 0)
			{
				await stream.WriteAsync(byteBuffer, 0, bytes).ConfigureAwait(false);
			}
			if (flushStream)
			{
				await stream.FlushAsync().ConfigureAwait(false);
			}
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00057329 File Offset: 0x00055529
		// Note: this type is marked as 'beforefieldinit'.
		static StreamWriter()
		{
		}

		// Token: 0x0400090B RID: 2315
		internal const int DefaultBufferSize = 1024;

		// Token: 0x0400090C RID: 2316
		private const int DefaultFileStreamBufferSize = 4096;

		// Token: 0x0400090D RID: 2317
		private const int MinBufferSize = 128;

		// Token: 0x0400090E RID: 2318
		private const int DontCopyOnWriteLineThreshold = 512;

		// Token: 0x0400090F RID: 2319
		[__DynamicallyInvokable]
		public new static readonly StreamWriter Null = new StreamWriter(Stream.Null, new UTF8Encoding(false, true), 128, true);

		// Token: 0x04000910 RID: 2320
		private Stream stream;

		// Token: 0x04000911 RID: 2321
		private Encoding encoding;

		// Token: 0x04000912 RID: 2322
		private Encoder encoder;

		// Token: 0x04000913 RID: 2323
		private byte[] byteBuffer;

		// Token: 0x04000914 RID: 2324
		private char[] charBuffer;

		// Token: 0x04000915 RID: 2325
		private int charPos;

		// Token: 0x04000916 RID: 2326
		private int charLen;

		// Token: 0x04000917 RID: 2327
		private bool autoFlush;

		// Token: 0x04000918 RID: 2328
		private bool haveWrittenPreamble;

		// Token: 0x04000919 RID: 2329
		private bool closable;

		// Token: 0x0400091A RID: 2330
		[NonSerialized]
		private StreamWriter.MdaHelper mdaHelper;

		// Token: 0x0400091B RID: 2331
		[NonSerialized]
		private volatile Task _asyncWriteTask;

		// Token: 0x0400091C RID: 2332
		private static volatile Encoding _UTF8NoBOM;

		// Token: 0x02000B20 RID: 2848
		private sealed class MdaHelper
		{
			// Token: 0x06006AFF RID: 27391 RVA: 0x00172062 File Offset: 0x00170262
			internal MdaHelper(StreamWriter sw, string cs)
			{
				this.streamWriter = sw;
				this.allocatedCallstack = cs;
			}

			// Token: 0x06006B00 RID: 27392 RVA: 0x00172078 File Offset: 0x00170278
			protected override void Finalize()
			{
				try
				{
					if (this.streamWriter.charPos != 0 && this.streamWriter.stream != null && this.streamWriter.stream != Stream.Null)
					{
						string text = ((this.streamWriter.stream is FileStream) ? ((FileStream)this.streamWriter.stream).NameInternal : "<unknown>");
						string resourceString = this.allocatedCallstack;
						if (resourceString == null)
						{
							resourceString = Environment.GetResourceString("IO_StreamWriterBufferedDataLostCaptureAllocatedFromCallstackNotEnabled");
						}
						string resourceString2 = Environment.GetResourceString("IO_StreamWriterBufferedDataLost", new object[]
						{
							this.streamWriter.stream.GetType().FullName,
							text,
							resourceString
						});
						Mda.StreamWriterBufferedDataLost.ReportError(resourceString2);
					}
				}
				finally
				{
					base.Finalize();
				}
			}

			// Token: 0x040032F9 RID: 13049
			private StreamWriter streamWriter;

			// Token: 0x040032FA RID: 13050
			private string allocatedCallstack;
		}

		// Token: 0x02000B21 RID: 2849
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAsyncInternal>d__53 : IAsyncStateMachine
		{
			// Token: 0x06006B01 RID: 27393 RVA: 0x0017214C File Offset: 0x0017034C
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter4;
					switch (num)
					{
					case 0:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						break;
					}
					case 1:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter3 = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_163;
					}
					case 2:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter4 = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_236;
					}
					default:
						if (charPos != charLen)
						{
							goto IL_A5;
						}
						configuredTaskAwaiter = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num2 = 0;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__53>(ref configuredTaskAwaiter, ref this);
							return;
						}
						break;
					}
					configuredTaskAwaiter.GetResult();
					charPos = 0;
					IL_A5:
					charBuffer[charPos] = value;
					int num3 = charPos;
					charPos = num3 + 1;
					if (appendNewLine)
					{
						i = 0;
						goto IL_1AB;
					}
					goto IL_1BE;
					IL_163:
					configuredTaskAwaiter3.GetResult();
					charPos = 0;
					IL_171:
					charBuffer[charPos] = coreNewLine[i];
					num3 = charPos;
					charPos = num3 + 1;
					num3 = i;
					i = num3 + 1;
					IL_1AB:
					if (i < coreNewLine.Length)
					{
						if (charPos != charLen)
						{
							goto IL_171;
						}
						configuredTaskAwaiter3 = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							num2 = 1;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__53>(ref configuredTaskAwaiter3, ref this);
							return;
						}
						goto IL_163;
					}
					IL_1BE:
					if (!autoFlush)
					{
						goto IL_244;
					}
					configuredTaskAwaiter4 = _this.FlushAsyncInternal(true, false, charBuffer, charPos).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter4.IsCompleted)
					{
						num2 = 2;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter4;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__53>(ref configuredTaskAwaiter4, ref this);
						return;
					}
					IL_236:
					configuredTaskAwaiter4.GetResult();
					charPos = 0;
					IL_244:
					_this.CharPos_Prop = charPos;
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06006B02 RID: 27394 RVA: 0x001723F8 File Offset: 0x001705F8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040032FB RID: 13051
			public int <>1__state;

			// Token: 0x040032FC RID: 13052
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040032FD RID: 13053
			public int charPos;

			// Token: 0x040032FE RID: 13054
			public int charLen;

			// Token: 0x040032FF RID: 13055
			public StreamWriter _this;

			// Token: 0x04003300 RID: 13056
			public char[] charBuffer;

			// Token: 0x04003301 RID: 13057
			public char value;

			// Token: 0x04003302 RID: 13058
			public bool appendNewLine;

			// Token: 0x04003303 RID: 13059
			public char[] coreNewLine;

			// Token: 0x04003304 RID: 13060
			public bool autoFlush;

			// Token: 0x04003305 RID: 13061
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04003306 RID: 13062
			private int <i>5__2;
		}

		// Token: 0x02000B22 RID: 2850
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAsyncInternal>d__55 : IAsyncStateMachine
		{
			// Token: 0x06006B03 RID: 27395 RVA: 0x00172408 File Offset: 0x00170608
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter4;
					switch (num)
					{
					case 0:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						break;
					}
					case 1:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter3 = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_1CF;
					}
					case 2:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter4 = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_2A6;
					}
					default:
						count = value.Length;
						index = 0;
						goto IL_128;
					}
					IL_B4:
					configuredTaskAwaiter.GetResult();
					charPos = 0;
					IL_C2:
					int num3 = charLen - charPos;
					if (num3 > count)
					{
						num3 = count;
					}
					value.CopyTo(index, charBuffer, charPos, num3);
					charPos += num3;
					index += num3;
					count -= num3;
					IL_128:
					if (count <= 0)
					{
						if (appendNewLine)
						{
							i = 0;
							goto IL_21B;
						}
						goto IL_22E;
					}
					else
					{
						if (charPos != charLen)
						{
							goto IL_C2;
						}
						configuredTaskAwaiter = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num2 = 0;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__55>(ref configuredTaskAwaiter, ref this);
							return;
						}
						goto IL_B4;
					}
					IL_1CF:
					configuredTaskAwaiter3.GetResult();
					charPos = 0;
					IL_1DD:
					charBuffer[charPos] = coreNewLine[i];
					int num4 = charPos;
					charPos = num4 + 1;
					num4 = i;
					i = num4 + 1;
					IL_21B:
					if (i < coreNewLine.Length)
					{
						if (charPos != charLen)
						{
							goto IL_1DD;
						}
						configuredTaskAwaiter3 = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							num2 = 1;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__55>(ref configuredTaskAwaiter3, ref this);
							return;
						}
						goto IL_1CF;
					}
					IL_22E:
					if (!autoFlush)
					{
						goto IL_2B4;
					}
					configuredTaskAwaiter4 = _this.FlushAsyncInternal(true, false, charBuffer, charPos).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter4.IsCompleted)
					{
						num2 = 2;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter4;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__55>(ref configuredTaskAwaiter4, ref this);
						return;
					}
					IL_2A6:
					configuredTaskAwaiter4.GetResult();
					charPos = 0;
					IL_2B4:
					_this.CharPos_Prop = charPos;
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06006B04 RID: 27396 RVA: 0x00172724 File Offset: 0x00170924
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003307 RID: 13063
			public int <>1__state;

			// Token: 0x04003308 RID: 13064
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003309 RID: 13065
			public string value;

			// Token: 0x0400330A RID: 13066
			public int charPos;

			// Token: 0x0400330B RID: 13067
			public int charLen;

			// Token: 0x0400330C RID: 13068
			public StreamWriter _this;

			// Token: 0x0400330D RID: 13069
			public char[] charBuffer;

			// Token: 0x0400330E RID: 13070
			public bool appendNewLine;

			// Token: 0x0400330F RID: 13071
			public char[] coreNewLine;

			// Token: 0x04003310 RID: 13072
			public bool autoFlush;

			// Token: 0x04003311 RID: 13073
			private int <count>5__2;

			// Token: 0x04003312 RID: 13074
			private int <index>5__3;

			// Token: 0x04003313 RID: 13075
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04003314 RID: 13076
			private int <i>5__4;
		}

		// Token: 0x02000B23 RID: 2851
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAsyncInternal>d__57 : IAsyncStateMachine
		{
			// Token: 0x06006B05 RID: 27397 RVA: 0x00172734 File Offset: 0x00170934
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter4;
					switch (num)
					{
					case 0:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						break;
					}
					case 1:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter3 = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_1BD;
					}
					case 2:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter4 = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_294;
					}
					default:
						goto IL_116;
					}
					IL_9C:
					configuredTaskAwaiter.GetResult();
					charPos = 0;
					IL_AA:
					int num3 = charLen - charPos;
					if (num3 > count)
					{
						num3 = count;
					}
					Buffer.InternalBlockCopy(buffer, index * 2, charBuffer, charPos * 2, num3 * 2);
					charPos += num3;
					index += num3;
					count -= num3;
					IL_116:
					if (count <= 0)
					{
						if (appendNewLine)
						{
							i = 0;
							goto IL_209;
						}
						goto IL_21C;
					}
					else
					{
						if (charPos != charLen)
						{
							goto IL_AA;
						}
						configuredTaskAwaiter = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num2 = 0;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__57>(ref configuredTaskAwaiter, ref this);
							return;
						}
						goto IL_9C;
					}
					IL_1BD:
					configuredTaskAwaiter3.GetResult();
					charPos = 0;
					IL_1CB:
					charBuffer[charPos] = coreNewLine[i];
					int num4 = charPos;
					charPos = num4 + 1;
					num4 = i;
					i = num4 + 1;
					IL_209:
					if (i < coreNewLine.Length)
					{
						if (charPos != charLen)
						{
							goto IL_1CB;
						}
						configuredTaskAwaiter3 = _this.FlushAsyncInternal(false, false, charBuffer, charPos).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							num2 = 1;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__57>(ref configuredTaskAwaiter3, ref this);
							return;
						}
						goto IL_1BD;
					}
					IL_21C:
					if (!autoFlush)
					{
						goto IL_2A2;
					}
					configuredTaskAwaiter4 = _this.FlushAsyncInternal(true, false, charBuffer, charPos).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter4.IsCompleted)
					{
						num2 = 2;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter4;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<WriteAsyncInternal>d__57>(ref configuredTaskAwaiter4, ref this);
						return;
					}
					IL_294:
					configuredTaskAwaiter4.GetResult();
					charPos = 0;
					IL_2A2:
					_this.CharPos_Prop = charPos;
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06006B06 RID: 27398 RVA: 0x00172A40 File Offset: 0x00170C40
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003315 RID: 13077
			public int <>1__state;

			// Token: 0x04003316 RID: 13078
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003317 RID: 13079
			public int charPos;

			// Token: 0x04003318 RID: 13080
			public int charLen;

			// Token: 0x04003319 RID: 13081
			public StreamWriter _this;

			// Token: 0x0400331A RID: 13082
			public char[] charBuffer;

			// Token: 0x0400331B RID: 13083
			public int count;

			// Token: 0x0400331C RID: 13084
			public char[] buffer;

			// Token: 0x0400331D RID: 13085
			public int index;

			// Token: 0x0400331E RID: 13086
			public bool appendNewLine;

			// Token: 0x0400331F RID: 13087
			public char[] coreNewLine;

			// Token: 0x04003320 RID: 13088
			public bool autoFlush;

			// Token: 0x04003321 RID: 13089
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04003322 RID: 13090
			private int <i>5__2;
		}

		// Token: 0x02000B24 RID: 2852
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FlushAsyncInternal>d__68 : IAsyncStateMachine
		{
			// Token: 0x06006B07 RID: 27399 RVA: 0x00172A50 File Offset: 0x00170C50
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter4;
					switch (num)
					{
					case 0:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						break;
					}
					case 1:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter3 = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_147;
					}
					case 2:
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter4 = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_1B9;
					}
					default:
					{
						if (haveWrittenPreamble)
						{
							goto IL_AF;
						}
						_this.HaveWrittenPreamble_Prop = true;
						byte[] preamble = encoding.GetPreamble();
						if (preamble.Length == 0)
						{
							goto IL_AF;
						}
						configuredTaskAwaiter = stream.WriteAsync(preamble, 0, preamble.Length).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter.IsCompleted)
						{
							num2 = 0;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<FlushAsyncInternal>d__68>(ref configuredTaskAwaiter, ref this);
							return;
						}
						break;
					}
					}
					configuredTaskAwaiter.GetResult();
					IL_AF:
					int bytes = encoder.GetBytes(charBuffer, 0, charPos, byteBuffer, 0, flushEncoder);
					if (bytes <= 0)
					{
						goto IL_14E;
					}
					configuredTaskAwaiter3 = stream.WriteAsync(byteBuffer, 0, bytes).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter3.IsCompleted)
					{
						num2 = 1;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<FlushAsyncInternal>d__68>(ref configuredTaskAwaiter3, ref this);
						return;
					}
					IL_147:
					configuredTaskAwaiter3.GetResult();
					IL_14E:
					if (!flushStream)
					{
						goto IL_1C0;
					}
					configuredTaskAwaiter4 = stream.FlushAsync().ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter4.IsCompleted)
					{
						num2 = 2;
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter4;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, StreamWriter.<FlushAsyncInternal>d__68>(ref configuredTaskAwaiter4, ref this);
						return;
					}
					IL_1B9:
					configuredTaskAwaiter4.GetResult();
					IL_1C0:;
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06006B08 RID: 27400 RVA: 0x00172C68 File Offset: 0x00170E68
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003323 RID: 13091
			public int <>1__state;

			// Token: 0x04003324 RID: 13092
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04003325 RID: 13093
			public bool haveWrittenPreamble;

			// Token: 0x04003326 RID: 13094
			public StreamWriter _this;

			// Token: 0x04003327 RID: 13095
			public Encoding encoding;

			// Token: 0x04003328 RID: 13096
			public Stream stream;

			// Token: 0x04003329 RID: 13097
			public Encoder encoder;

			// Token: 0x0400332A RID: 13098
			public char[] charBuffer;

			// Token: 0x0400332B RID: 13099
			public int charPos;

			// Token: 0x0400332C RID: 13100
			public byte[] byteBuffer;

			// Token: 0x0400332D RID: 13101
			public bool flushEncoder;

			// Token: 0x0400332E RID: 13102
			public bool flushStream;

			// Token: 0x0400332F RID: 13103
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
