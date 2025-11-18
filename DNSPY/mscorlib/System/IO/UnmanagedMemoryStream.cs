using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020001A9 RID: 425
	public class UnmanagedMemoryStream : Stream
	{
		// Token: 0x06001AA3 RID: 6819 RVA: 0x00059391 File Offset: 0x00057591
		[SecuritySafeCritical]
		protected UnmanagedMemoryStream()
		{
			this._mem = null;
			this._isOpen = false;
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x000593A8 File Offset: 0x000575A8
		[SecuritySafeCritical]
		public UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length)
		{
			this.Initialize(buffer, offset, length, FileAccess.Read, false);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x000593BB File Offset: 0x000575BB
		[SecuritySafeCritical]
		public UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length, FileAccess access)
		{
			this.Initialize(buffer, offset, length, access, false);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x000593CF File Offset: 0x000575CF
		[SecurityCritical]
		internal UnmanagedMemoryStream(SafeBuffer buffer, long offset, long length, FileAccess access, bool skipSecurityCheck)
		{
			this.Initialize(buffer, offset, length, access, skipSecurityCheck);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x000593E4 File Offset: 0x000575E4
		[SecuritySafeCritical]
		protected void Initialize(SafeBuffer buffer, long offset, long length, FileAccess access)
		{
			this.Initialize(buffer, offset, length, access, false);
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x000593F4 File Offset: 0x000575F4
		[SecurityCritical]
		internal unsafe void Initialize(SafeBuffer buffer, long offset, long length, FileAccess access, bool skipSecurityCheck)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (length < 0L)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.ByteLength < (ulong)(offset + length))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSafeBufferOffLen"));
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (this._isOpen)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CalledTwice"));
			}
			if (!skipSecurityCheck)
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			}
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				buffer.AcquirePointer(ref ptr);
				if (ptr + offset + length < ptr)
				{
					throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_UnmanagedMemStreamWrapAround"));
				}
			}
			finally
			{
				if (ptr != null)
				{
					buffer.ReleasePointer();
				}
			}
			this._offset = offset;
			this._buffer = buffer;
			this._length = length;
			this._capacity = length;
			this._access = access;
			this._isOpen = true;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00059510 File Offset: 0x00057710
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe UnmanagedMemoryStream(byte* pointer, long length)
		{
			this.Initialize(pointer, length, length, FileAccess.Read, false);
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x00059523 File Offset: 0x00057723
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe UnmanagedMemoryStream(byte* pointer, long length, long capacity, FileAccess access)
		{
			this.Initialize(pointer, length, capacity, access, false);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x00059537 File Offset: 0x00057737
		[SecurityCritical]
		internal unsafe UnmanagedMemoryStream(byte* pointer, long length, long capacity, FileAccess access, bool skipSecurityCheck)
		{
			this.Initialize(pointer, length, capacity, access, skipSecurityCheck);
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x0005954C File Offset: 0x0005774C
		[SecurityCritical]
		[CLSCompliant(false)]
		protected unsafe void Initialize(byte* pointer, long length, long capacity, FileAccess access)
		{
			this.Initialize(pointer, length, capacity, access, false);
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x0005955C File Offset: 0x0005775C
		[SecurityCritical]
		internal unsafe void Initialize(byte* pointer, long length, long capacity, FileAccess access, bool skipSecurityCheck)
		{
			if (pointer == null)
			{
				throw new ArgumentNullException("pointer");
			}
			if (length < 0L || capacity < 0L)
			{
				throw new ArgumentOutOfRangeException((length < 0L) ? "length" : "capacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (length > capacity)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_LengthGreaterThanCapacity"));
			}
			if (pointer + capacity < pointer)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_UnmanagedMemStreamWrapAround"));
			}
			if (access < FileAccess.Read || access > FileAccess.ReadWrite)
			{
				throw new ArgumentOutOfRangeException("access", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			if (this._isOpen)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CalledTwice"));
			}
			if (!skipSecurityCheck)
			{
				new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			}
			this._mem = pointer;
			this._offset = 0L;
			this._length = length;
			this._capacity = capacity;
			this._access = access;
			this._isOpen = true;
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06001AAE RID: 6830 RVA: 0x0005964C File Offset: 0x0005784C
		public override bool CanRead
		{
			get
			{
				return this._isOpen && (this._access & FileAccess.Read) > (FileAccess)0;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x00059663 File Offset: 0x00057863
		public override bool CanSeek
		{
			get
			{
				return this._isOpen;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x0005966B File Offset: 0x0005786B
		public override bool CanWrite
		{
			get
			{
				return this._isOpen && (this._access & FileAccess.Write) > (FileAccess)0;
			}
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x00059682 File Offset: 0x00057882
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			this._isOpen = false;
			this._mem = null;
			base.Dispose(disposing);
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0005969A File Offset: 0x0005789A
		public override void Flush()
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x000596AC File Offset: 0x000578AC
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			Task task;
			try
			{
				this.Flush();
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException(ex);
			}
			return task;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x000596F4 File Offset: 0x000578F4
		public override long Length
		{
			get
			{
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				return this.InternalGetLength();
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x00059709 File Offset: 0x00057909
		public long Capacity
		{
			get
			{
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				return this._capacity;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x0005971E File Offset: 0x0005791E
		// (set) Token: 0x06001AB7 RID: 6839 RVA: 0x00059734 File Offset: 0x00057934
		public override long Position
		{
			get
			{
				if (!this.CanSeek)
				{
					__Error.StreamIsClosed();
				}
				return this._position;
			}
			[SecuritySafeCritical]
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (!this.CanSeek)
				{
					__Error.StreamIsClosed();
				}
				if (value > 2147483647L || this._mem + value < this._mem)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_StreamLength"));
				}
				this._position = value;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x000597A0 File Offset: 0x000579A0
		// (set) Token: 0x06001AB9 RID: 6841 RVA: 0x00059800 File Offset: 0x00057A00
		[CLSCompliant(false)]
		public unsafe byte* PositionPointer
		{
			[SecurityCritical]
			get
			{
				if (this._buffer != null)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_UmsSafeBuffer"));
				}
				long position = this._position;
				if (position > this._capacity)
				{
					throw new IndexOutOfRangeException(Environment.GetResourceString("IndexOutOfRange_UMSPosition"));
				}
				byte* ptr = this._mem + position;
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				return ptr;
			}
			[SecurityCritical]
			set
			{
				if (this._buffer != null)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_UmsSafeBuffer"));
				}
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				if (new IntPtr((long)(value - this._mem)).ToInt64() > 9223372036854775807L)
				{
					throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_UnmanagedMemStreamLength"));
				}
				if (value < this._mem)
				{
					throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
				}
				this._position = (long)(value - this._mem);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x00059894 File Offset: 0x00057A94
		internal unsafe byte* Pointer
		{
			[SecurityCritical]
			get
			{
				if (this._buffer != null)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_UmsSafeBuffer"));
				}
				return this._mem;
			}
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x000598B4 File Offset: 0x00057AB4
		[SecuritySafeCritical]
		public unsafe override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
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
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this.CanRead)
			{
				__Error.ReadNotSupported();
			}
			long position = this._position;
			long num = this.InternalVolatileGetLength();
			long num2 = num - position;
			if (num2 > (long)count)
			{
				num2 = (long)count;
			}
			if (num2 <= 0L)
			{
				return 0;
			}
			int num3 = (int)num2;
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (this._buffer != null)
			{
				byte* ptr = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._buffer.AcquirePointer(ref ptr);
					Buffer.Memcpy(buffer, offset, ptr + position + this._offset, 0, num3);
					goto IL_100;
				}
				finally
				{
					if (ptr != null)
					{
						this._buffer.ReleasePointer();
					}
				}
			}
			Buffer.Memcpy(buffer, offset, this._mem + position, 0, num3);
			IL_100:
			this._position = position + num2;
			return num3;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x000599DC File Offset: 0x00057BDC
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
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
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation<int>(cancellationToken);
			}
			Task<int> task;
			try
			{
				int num = this.Read(buffer, offset, count);
				Task<int> lastReadTask = this._lastReadTask;
				Task<int> task2;
				if (lastReadTask == null || lastReadTask.Result != num)
				{
					task = (this._lastReadTask = Task.FromResult<int>(num));
					task2 = task;
				}
				else
				{
					task2 = lastReadTask;
				}
				task = task2;
			}
			catch (Exception ex)
			{
				task = Task.FromException<int>(ex);
			}
			return task;
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00059AAC File Offset: 0x00057CAC
		[SecuritySafeCritical]
		public unsafe override int ReadByte()
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this.CanRead)
			{
				__Error.ReadNotSupported();
			}
			long position = this._position;
			long num = this.InternalVolatileGetLength();
			if (position >= num)
			{
				return -1;
			}
			this._position = position + 1L;
			if (this._buffer != null)
			{
				byte* ptr = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._buffer.AcquirePointer(ref ptr);
					return (int)(ptr + position)[this._offset];
				}
				finally
				{
					if (ptr != null)
					{
						this._buffer.ReleasePointer();
					}
				}
			}
			return (int)this._mem[position];
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00059B4C File Offset: 0x00057D4C
		public override long Seek(long offset, SeekOrigin loc)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (offset > 9223372036854775807L)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_UnmanagedMemStreamLength"));
			}
			switch (loc)
			{
			case SeekOrigin.Begin:
				if (offset < 0L)
				{
					throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
				}
				this._position = offset;
				break;
			case SeekOrigin.Current:
			{
				long position = this._position;
				if (offset + position < 0L)
				{
					throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
				}
				this._position = offset + position;
				break;
			}
			case SeekOrigin.End:
			{
				long num = this.InternalGetLength();
				if (num + offset < 0L)
				{
					throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
				}
				this._position = num + offset;
				break;
			}
			default:
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSeekOrigin"));
			}
			return this._position;
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00059C24 File Offset: 0x00057E24
		[SecuritySafeCritical]
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("length", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (this._buffer != null)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UmsSafeBuffer"));
			}
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this.CanWrite)
			{
				__Error.WriteNotSupported();
			}
			if (value > this._capacity)
			{
				throw new IOException(Environment.GetResourceString("IO.IO_FixedCapacity"));
			}
			long position = this._position;
			long num = this.InternalGetLength();
			if (value > num)
			{
				Buffer.ZeroMemory(this._mem + num, value - num);
			}
			this.InternalVolatileSetLength(value);
			if (position > value)
			{
				this._position = value;
			}
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00059CCC File Offset: 0x00057ECC
		[SecuritySafeCritical]
		public unsafe override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
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
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this.CanWrite)
			{
				__Error.WriteNotSupported();
			}
			long position = this._position;
			long num = this.InternalGetLength();
			long num2 = position + (long)count;
			if (num2 < 0L)
			{
				throw new IOException(Environment.GetResourceString("IO.IO_StreamTooLong"));
			}
			if (num2 > this._capacity)
			{
				throw new NotSupportedException(Environment.GetResourceString("IO.IO_FixedCapacity"));
			}
			if (this._buffer == null)
			{
				if (position > num)
				{
					Buffer.ZeroMemory(this._mem + num, position - num);
				}
				if (num2 > num)
				{
					this.InternalVolatileSetLength(num2);
				}
			}
			if (this._buffer != null)
			{
				long num3 = this._capacity - position;
				if (num3 < (long)count)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_BufferTooSmall"));
				}
				byte* ptr = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._buffer.AcquirePointer(ref ptr);
					Buffer.Memcpy(ptr + position + this._offset, 0, buffer, offset, count);
					goto IL_15D;
				}
				finally
				{
					if (ptr != null)
					{
						this._buffer.ReleasePointer();
					}
				}
			}
			Buffer.Memcpy(this._mem + position, 0, buffer, offset, count);
			IL_15D:
			this._position = num2;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00059E50 File Offset: 0x00058050
		[ComVisible(false)]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
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
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			Task task;
			try
			{
				this.Write(buffer, offset, count);
				task = Task.CompletedTask;
			}
			catch (Exception ex)
			{
				task = Task.FromException<int>(ex);
			}
			return task;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00059F00 File Offset: 0x00058100
		[SecuritySafeCritical]
		public unsafe override void WriteByte(byte value)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this.CanWrite)
			{
				__Error.WriteNotSupported();
			}
			long position = this._position;
			long num = this.InternalGetLength();
			long num2 = position + 1L;
			if (position >= num)
			{
				if (num2 < 0L)
				{
					throw new IOException(Environment.GetResourceString("IO.IO_StreamTooLong"));
				}
				if (num2 > this._capacity)
				{
					throw new NotSupportedException(Environment.GetResourceString("IO.IO_FixedCapacity"));
				}
				if (this._buffer == null)
				{
					if (position > num)
					{
						Buffer.ZeroMemory(this._mem + num, position - num);
					}
					this.InternalVolatileSetLength(num2);
				}
			}
			if (this._buffer != null)
			{
				byte* ptr = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._buffer.AcquirePointer(ref ptr);
					(ptr + position)[this._offset] = value;
					goto IL_CC;
				}
				finally
				{
					if (ptr != null)
					{
						this._buffer.ReleasePointer();
					}
				}
			}
			this._mem[position] = value;
			IL_CC:
			this._position = num2;
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x00059FF0 File Offset: 0x000581F0
		[SecuritySafeCritical]
		private unsafe long InternalGetLength()
		{
			fixed (long* ptr = &this._length)
			{
				long* ptr2 = ptr;
				return (long)((ulong)(*(uint*)ptr2));
			}
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x0005A00C File Offset: 0x0005820C
		[SecuritySafeCritical]
		private unsafe long InternalVolatileGetLength()
		{
			fixed (long* ptr = &this._length)
			{
				long* ptr2 = ptr;
				return (long)((ulong)Volatile.Read(ref *(uint*)ptr2));
			}
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x0005A02C File Offset: 0x0005822C
		[SecuritySafeCritical]
		private unsafe void InternalVolatileSetLength(long value)
		{
			fixed (long* ptr = &this._length)
			{
				long* ptr2 = ptr;
				Volatile.Write(ref *(uint*)ptr2, (uint)value);
			}
		}

		// Token: 0x04000938 RID: 2360
		private const long UnmanagedMemStreamMaxLength = 9223372036854775807L;

		// Token: 0x04000939 RID: 2361
		[SecurityCritical]
		private SafeBuffer _buffer;

		// Token: 0x0400093A RID: 2362
		[SecurityCritical]
		private unsafe byte* _mem;

		// Token: 0x0400093B RID: 2363
		private long _length;

		// Token: 0x0400093C RID: 2364
		private long _capacity;

		// Token: 0x0400093D RID: 2365
		private long _position;

		// Token: 0x0400093E RID: 2366
		private long _offset;

		// Token: 0x0400093F RID: 2367
		private FileAccess _access;

		// Token: 0x04000940 RID: 2368
		internal bool _isOpen;

		// Token: 0x04000941 RID: 2369
		[NonSerialized]
		private Task<int> _lastReadTask;
	}
}
