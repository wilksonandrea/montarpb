using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	// Token: 0x020001A1 RID: 417
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Stream : MarshalByRefObject, IDisposable
	{
		// Token: 0x0600196D RID: 6509 RVA: 0x00054B4F File Offset: 0x00052D4F
		internal SemaphoreSlim EnsureAsyncActiveSemaphoreInitialized()
		{
			return LazyInitializer.EnsureInitialized<SemaphoreSlim>(ref this._asyncActiveSemaphore, () => new SemaphoreSlim(1, 1));
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600196E RID: 6510
		[__DynamicallyInvokable]
		public abstract bool CanRead
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600196F RID: 6511
		[__DynamicallyInvokable]
		public abstract bool CanSeek
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06001970 RID: 6512 RVA: 0x00054B7B File Offset: 0x00052D7B
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual bool CanTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06001971 RID: 6513
		[__DynamicallyInvokable]
		public abstract bool CanWrite
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06001972 RID: 6514
		[__DynamicallyInvokable]
		public abstract long Length
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001973 RID: 6515
		// (set) Token: 0x06001974 RID: 6516
		[__DynamicallyInvokable]
		public abstract long Position
		{
			[__DynamicallyInvokable]
			get;
			[__DynamicallyInvokable]
			set;
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x00054B7E File Offset: 0x00052D7E
		// (set) Token: 0x06001976 RID: 6518 RVA: 0x00054B8F File Offset: 0x00052D8F
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual int ReadTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
			}
			[__DynamicallyInvokable]
			set
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001977 RID: 6519 RVA: 0x00054BA0 File Offset: 0x00052DA0
		// (set) Token: 0x06001978 RID: 6520 RVA: 0x00054BB1 File Offset: 0x00052DB1
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual int WriteTimeout
		{
			[__DynamicallyInvokable]
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
			}
			[__DynamicallyInvokable]
			set
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TimeoutsNotSupported"));
			}
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00054BC2 File Offset: 0x00052DC2
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task CopyToAsync(Stream destination)
		{
			return this.CopyToAsync(destination, 81920);
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x00054BD0 File Offset: 0x00052DD0
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task CopyToAsync(Stream destination, int bufferSize)
		{
			return this.CopyToAsync(destination, bufferSize, CancellationToken.None);
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00054BE0 File Offset: 0x00052DE0
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (!this.CanRead && !this.CanWrite)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_StreamClosed"));
			}
			if (!destination.CanRead && !destination.CanWrite)
			{
				throw new ObjectDisposedException("destination", Environment.GetResourceString("ObjectDisposed_StreamClosed"));
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
			}
			if (!destination.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
			}
			return this.CopyToAsyncInternal(destination, bufferSize, cancellationToken);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00054C94 File Offset: 0x00052E94
		private async Task CopyToAsyncInternal(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			byte[] buffer = new byte[bufferSize];
			int num;
			while ((num = await this.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) != 0)
			{
				await destination.WriteAsync(buffer, 0, num, cancellationToken).ConfigureAwait(false);
			}
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x00054CF0 File Offset: 0x00052EF0
		[__DynamicallyInvokable]
		public void CopyTo(Stream destination)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (!this.CanRead && !this.CanWrite)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_StreamClosed"));
			}
			if (!destination.CanRead && !destination.CanWrite)
			{
				throw new ObjectDisposedException("destination", Environment.GetResourceString("ObjectDisposed_StreamClosed"));
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
			}
			if (!destination.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
			}
			this.InternalCopyTo(destination, 81920);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00054D90 File Offset: 0x00052F90
		[__DynamicallyInvokable]
		public void CopyTo(Stream destination, int bufferSize)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (!this.CanRead && !this.CanWrite)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_StreamClosed"));
			}
			if (!destination.CanRead && !destination.CanWrite)
			{
				throw new ObjectDisposedException("destination", Environment.GetResourceString("ObjectDisposed_StreamClosed"));
			}
			if (!this.CanRead)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
			}
			if (!destination.CanWrite)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnwritableStream"));
			}
			this.InternalCopyTo(destination, bufferSize);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00054E44 File Offset: 0x00053044
		private void InternalCopyTo(Stream destination, int bufferSize)
		{
			byte[] array = new byte[bufferSize];
			int num;
			while ((num = this.Read(array, 0, array.Length)) != 0)
			{
				destination.Write(array, 0, num);
			}
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00054E72 File Offset: 0x00053072
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00054E81 File Offset: 0x00053081
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x00054E89 File Offset: 0x00053089
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06001983 RID: 6531
		[__DynamicallyInvokable]
		public abstract void Flush();

		// Token: 0x06001984 RID: 6532 RVA: 0x00054E8B File Offset: 0x0005308B
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task FlushAsync()
		{
			return this.FlushAsync(CancellationToken.None);
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00054E98 File Offset: 0x00053098
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task FlushAsync(CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(delegate(object state)
			{
				((Stream)state).Flush();
			}, this, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x00054ECB File Offset: 0x000530CB
		[Obsolete("CreateWaitHandle will be removed eventually.  Please use \"new ManualResetEvent(false)\" instead.")]
		protected virtual WaitHandle CreateWaitHandle()
		{
			return new ManualResetEvent(false);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00054ED3 File Offset: 0x000530D3
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.BeginReadInternal(buffer, offset, count, callback, state, false);
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x00054EE4 File Offset: 0x000530E4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		internal IAsyncResult BeginReadInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously)
		{
			if (!this.CanRead)
			{
				__Error.ReadNotSupported();
			}
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				return this.BlockingBeginRead(buffer, offset, count, callback, state);
			}
			SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
			Task task = null;
			if (serializeAsynchronously)
			{
				task = semaphoreSlim.WaitAsync();
			}
			else
			{
				semaphoreSlim.Wait();
			}
			Stream.ReadWriteTask readWriteTask = new Stream.ReadWriteTask(true, delegate
			{
				Stream.ReadWriteTask readWriteTask2 = Task.InternalCurrent as Stream.ReadWriteTask;
				int num = readWriteTask2._stream.Read(readWriteTask2._buffer, readWriteTask2._offset, readWriteTask2._count);
				readWriteTask2.ClearBeginState();
				return num;
			}, state, this, buffer, offset, count, callback);
			if (task != null)
			{
				this.RunReadWriteTaskWhenReady(task, readWriteTask);
			}
			else
			{
				this.RunReadWriteTask(readWriteTask);
			}
			return readWriteTask;
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00054F74 File Offset: 0x00053174
		[__DynamicallyInvokable]
		public virtual int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				return Stream.BlockingEndRead(asyncResult);
			}
			Stream.ReadWriteTask activeReadWriteTask = this._activeReadWriteTask;
			if (activeReadWriteTask == null)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple"));
			}
			if (activeReadWriteTask != asyncResult)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple"));
			}
			if (!activeReadWriteTask._isRead)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndReadCalledMultiple"));
			}
			int result;
			try
			{
				result = activeReadWriteTask.GetAwaiter().GetResult();
			}
			finally
			{
				this._activeReadWriteTask = null;
				this._asyncActiveSemaphore.Release();
			}
			return result;
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0005501C File Offset: 0x0005321C
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<int> ReadAsync(byte[] buffer, int offset, int count)
		{
			return this.ReadAsync(buffer, offset, count, CancellationToken.None);
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0005502C File Offset: 0x0005322C
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return this.BeginEndReadAsync(buffer, offset, count);
			}
			return Task.FromCancellation<int>(cancellationToken);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x00055048 File Offset: 0x00053248
		private Task<int> BeginEndReadAsync(byte[] buffer, int offset, int count)
		{
			return TaskFactory<int>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, new Stream.ReadWriteParameters
			{
				Buffer = buffer,
				Offset = offset,
				Count = count
			}, (Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state) => stream.BeginRead(args.Buffer, args.Offset, args.Count, callback, state), (Stream stream, IAsyncResult asyncResult) => stream.EndRead(asyncResult));
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x000550BA File Offset: 0x000532BA
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.BeginWriteInternal(buffer, offset, count, callback, state, false);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x000550CC File Offset: 0x000532CC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		internal IAsyncResult BeginWriteInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, bool serializeAsynchronously)
		{
			if (!this.CanWrite)
			{
				__Error.WriteNotSupported();
			}
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				return this.BlockingBeginWrite(buffer, offset, count, callback, state);
			}
			SemaphoreSlim semaphoreSlim = this.EnsureAsyncActiveSemaphoreInitialized();
			Task task = null;
			if (serializeAsynchronously)
			{
				task = semaphoreSlim.WaitAsync();
			}
			else
			{
				semaphoreSlim.Wait();
			}
			Stream.ReadWriteTask readWriteTask = new Stream.ReadWriteTask(false, delegate
			{
				Stream.ReadWriteTask readWriteTask2 = Task.InternalCurrent as Stream.ReadWriteTask;
				readWriteTask2._stream.Write(readWriteTask2._buffer, readWriteTask2._offset, readWriteTask2._count);
				readWriteTask2.ClearBeginState();
				return 0;
			}, state, this, buffer, offset, count, callback);
			if (task != null)
			{
				this.RunReadWriteTaskWhenReady(task, readWriteTask);
			}
			else
			{
				this.RunReadWriteTask(readWriteTask);
			}
			return readWriteTask;
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0005515C File Offset: 0x0005335C
		private void RunReadWriteTaskWhenReady(Task asyncWaiter, Stream.ReadWriteTask readWriteTask)
		{
			if (asyncWaiter.IsCompleted)
			{
				this.RunReadWriteTask(readWriteTask);
				return;
			}
			asyncWaiter.ContinueWith(delegate(Task t, object state)
			{
				Tuple<Stream, Stream.ReadWriteTask> tuple = (Tuple<Stream, Stream.ReadWriteTask>)state;
				tuple.Item1.RunReadWriteTask(tuple.Item2);
			}, Tuple.Create<Stream, Stream.ReadWriteTask>(this, readWriteTask), default(CancellationToken), TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x000551B9 File Offset: 0x000533B9
		private void RunReadWriteTask(Stream.ReadWriteTask readWriteTask)
		{
			this._activeReadWriteTask = readWriteTask;
			readWriteTask.m_taskScheduler = TaskScheduler.Default;
			readWriteTask.ScheduleAndStart(false);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x000551D4 File Offset: 0x000533D4
		[__DynamicallyInvokable]
		public virtual void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				Stream.BlockingEndWrite(asyncResult);
				return;
			}
			Stream.ReadWriteTask activeReadWriteTask = this._activeReadWriteTask;
			if (activeReadWriteTask == null)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple"));
			}
			if (activeReadWriteTask != asyncResult)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple"));
			}
			if (activeReadWriteTask._isRead)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndWriteCalledMultiple"));
			}
			try
			{
				activeReadWriteTask.GetAwaiter().GetResult();
			}
			finally
			{
				this._activeReadWriteTask = null;
				this._asyncActiveSemaphore.Release();
			}
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0005527C File Offset: 0x0005347C
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task WriteAsync(byte[] buffer, int offset, int count)
		{
			return this.WriteAsync(buffer, offset, count, CancellationToken.None);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0005528C File Offset: 0x0005348C
		[ComVisible(false)]
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public virtual Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return this.BeginEndWriteAsync(buffer, offset, count);
			}
			return Task.FromCancellation(cancellationToken);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x000552A8 File Offset: 0x000534A8
		private Task BeginEndWriteAsync(byte[] buffer, int offset, int count)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncTrim<Stream, Stream.ReadWriteParameters>(this, new Stream.ReadWriteParameters
			{
				Buffer = buffer,
				Offset = offset,
				Count = count
			}, (Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state) => stream.BeginWrite(args.Buffer, args.Offset, args.Count, callback, state), delegate(Stream stream, IAsyncResult asyncResult)
			{
				stream.EndWrite(asyncResult);
				return default(VoidTaskResult);
			});
		}

		// Token: 0x06001995 RID: 6549
		[__DynamicallyInvokable]
		public abstract long Seek(long offset, SeekOrigin origin);

		// Token: 0x06001996 RID: 6550
		[__DynamicallyInvokable]
		public abstract void SetLength(long value);

		// Token: 0x06001997 RID: 6551
		[__DynamicallyInvokable]
		public abstract int Read([In] [Out] byte[] buffer, int offset, int count);

		// Token: 0x06001998 RID: 6552 RVA: 0x0005531C File Offset: 0x0005351C
		[__DynamicallyInvokable]
		public virtual int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) == 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x06001999 RID: 6553
		[__DynamicallyInvokable]
		public abstract void Write(byte[] buffer, int offset, int count);

		// Token: 0x0600199A RID: 6554 RVA: 0x00055344 File Offset: 0x00053544
		[__DynamicallyInvokable]
		public virtual void WriteByte(byte value)
		{
			this.Write(new byte[] { value }, 0, 1);
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00055365 File Offset: 0x00053565
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static Stream Synchronized(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (stream is Stream.SyncStream)
			{
				return stream;
			}
			return new Stream.SyncStream(stream);
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00055385 File Offset: 0x00053585
		[Obsolete("Do not call or override this method.")]
		protected virtual void ObjectInvariant()
		{
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00055388 File Offset: 0x00053588
		internal IAsyncResult BlockingBeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			Stream.SynchronousAsyncResult synchronousAsyncResult;
			try
			{
				int num = this.Read(buffer, offset, count);
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(num, state);
			}
			catch (IOException ex)
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(ex, state, false);
			}
			if (callback != null)
			{
				callback(synchronousAsyncResult);
			}
			return synchronousAsyncResult;
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x000553D4 File Offset: 0x000535D4
		internal static int BlockingEndRead(IAsyncResult asyncResult)
		{
			return Stream.SynchronousAsyncResult.EndRead(asyncResult);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x000553DC File Offset: 0x000535DC
		internal IAsyncResult BlockingBeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			Stream.SynchronousAsyncResult synchronousAsyncResult;
			try
			{
				this.Write(buffer, offset, count);
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(state);
			}
			catch (IOException ex)
			{
				synchronousAsyncResult = new Stream.SynchronousAsyncResult(ex, state, true);
			}
			if (callback != null)
			{
				callback(synchronousAsyncResult);
			}
			return synchronousAsyncResult;
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00055428 File Offset: 0x00053628
		internal static void BlockingEndWrite(IAsyncResult asyncResult)
		{
			Stream.SynchronousAsyncResult.EndWrite(asyncResult);
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00055430 File Offset: 0x00053630
		[__DynamicallyInvokable]
		protected Stream()
		{
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00055438 File Offset: 0x00053638
		// Note: this type is marked as 'beforefieldinit'.
		static Stream()
		{
		}

		// Token: 0x040008F4 RID: 2292
		[__DynamicallyInvokable]
		public static readonly Stream Null = new Stream.NullStream();

		// Token: 0x040008F5 RID: 2293
		private const int _DefaultCopyBufferSize = 81920;

		// Token: 0x040008F6 RID: 2294
		[NonSerialized]
		private Stream.ReadWriteTask _activeReadWriteTask;

		// Token: 0x040008F7 RID: 2295
		[NonSerialized]
		private SemaphoreSlim _asyncActiveSemaphore;

		// Token: 0x02000B14 RID: 2836
		private struct ReadWriteParameters
		{
			// Token: 0x040032B4 RID: 12980
			internal byte[] Buffer;

			// Token: 0x040032B5 RID: 12981
			internal int Offset;

			// Token: 0x040032B6 RID: 12982
			internal int Count;
		}

		// Token: 0x02000B15 RID: 2837
		private sealed class ReadWriteTask : Task<int>, ITaskCompletionAction
		{
			// Token: 0x06006AA2 RID: 27298 RVA: 0x00170754 File Offset: 0x0016E954
			internal void ClearBeginState()
			{
				this._stream = null;
				this._buffer = null;
			}

			// Token: 0x06006AA3 RID: 27299 RVA: 0x00170764 File Offset: 0x0016E964
			[SecuritySafeCritical]
			[MethodImpl(MethodImplOptions.NoInlining)]
			public ReadWriteTask(bool isRead, Func<object, int> function, object state, Stream stream, byte[] buffer, int offset, int count, AsyncCallback callback)
				: base(function, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach)
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				this._isRead = isRead;
				this._stream = stream;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
				if (callback != null)
				{
					this._callback = callback;
					this._context = ExecutionContext.Capture(ref stackCrawlMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
					base.AddCompletionAction(this);
				}
			}

			// Token: 0x06006AA4 RID: 27300 RVA: 0x001707CC File Offset: 0x0016E9CC
			[SecurityCritical]
			private static void InvokeAsyncCallback(object completedTask)
			{
				Stream.ReadWriteTask readWriteTask = (Stream.ReadWriteTask)completedTask;
				AsyncCallback callback = readWriteTask._callback;
				readWriteTask._callback = null;
				callback(readWriteTask);
			}

			// Token: 0x06006AA5 RID: 27301 RVA: 0x001707F8 File Offset: 0x0016E9F8
			[SecuritySafeCritical]
			void ITaskCompletionAction.Invoke(Task completingTask)
			{
				ExecutionContext context = this._context;
				if (context == null)
				{
					AsyncCallback callback = this._callback;
					this._callback = null;
					callback(completingTask);
					return;
				}
				this._context = null;
				ContextCallback contextCallback = Stream.ReadWriteTask.s_invokeAsyncCallback;
				if (contextCallback == null)
				{
					contextCallback = (Stream.ReadWriteTask.s_invokeAsyncCallback = new ContextCallback(Stream.ReadWriteTask.InvokeAsyncCallback));
				}
				using (context)
				{
					ExecutionContext.Run(context, contextCallback, this, true);
				}
			}

			// Token: 0x040032B7 RID: 12983
			internal readonly bool _isRead;

			// Token: 0x040032B8 RID: 12984
			internal Stream _stream;

			// Token: 0x040032B9 RID: 12985
			internal byte[] _buffer;

			// Token: 0x040032BA RID: 12986
			internal int _offset;

			// Token: 0x040032BB RID: 12987
			internal int _count;

			// Token: 0x040032BC RID: 12988
			private AsyncCallback _callback;

			// Token: 0x040032BD RID: 12989
			private ExecutionContext _context;

			// Token: 0x040032BE RID: 12990
			[SecurityCritical]
			private static ContextCallback s_invokeAsyncCallback;
		}

		// Token: 0x02000B16 RID: 2838
		[Serializable]
		private sealed class NullStream : Stream
		{
			// Token: 0x06006AA6 RID: 27302 RVA: 0x00170870 File Offset: 0x0016EA70
			internal NullStream()
			{
			}

			// Token: 0x17001207 RID: 4615
			// (get) Token: 0x06006AA7 RID: 27303 RVA: 0x00170878 File Offset: 0x0016EA78
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001208 RID: 4616
			// (get) Token: 0x06006AA8 RID: 27304 RVA: 0x0017087B File Offset: 0x0016EA7B
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001209 RID: 4617
			// (get) Token: 0x06006AA9 RID: 27305 RVA: 0x0017087E File Offset: 0x0016EA7E
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700120A RID: 4618
			// (get) Token: 0x06006AAA RID: 27306 RVA: 0x00170881 File Offset: 0x0016EA81
			public override long Length
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x1700120B RID: 4619
			// (get) Token: 0x06006AAB RID: 27307 RVA: 0x00170885 File Offset: 0x0016EA85
			// (set) Token: 0x06006AAC RID: 27308 RVA: 0x00170889 File Offset: 0x0016EA89
			public override long Position
			{
				get
				{
					return 0L;
				}
				set
				{
				}
			}

			// Token: 0x06006AAD RID: 27309 RVA: 0x0017088B File Offset: 0x0016EA8B
			protected override void Dispose(bool disposing)
			{
			}

			// Token: 0x06006AAE RID: 27310 RVA: 0x0017088D File Offset: 0x0016EA8D
			public override void Flush()
			{
			}

			// Token: 0x06006AAF RID: 27311 RVA: 0x0017088F File Offset: 0x0016EA8F
			[ComVisible(false)]
			public override Task FlushAsync(CancellationToken cancellationToken)
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCancellation(cancellationToken);
			}

			// Token: 0x06006AB0 RID: 27312 RVA: 0x001708A6 File Offset: 0x0016EAA6
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (!this.CanRead)
				{
					__Error.ReadNotSupported();
				}
				return base.BlockingBeginRead(buffer, offset, count, callback, state);
			}

			// Token: 0x06006AB1 RID: 27313 RVA: 0x001708C2 File Offset: 0x0016EAC2
			public override int EndRead(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				return Stream.BlockingEndRead(asyncResult);
			}

			// Token: 0x06006AB2 RID: 27314 RVA: 0x001708D8 File Offset: 0x0016EAD8
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (!this.CanWrite)
				{
					__Error.WriteNotSupported();
				}
				return base.BlockingBeginWrite(buffer, offset, count, callback, state);
			}

			// Token: 0x06006AB3 RID: 27315 RVA: 0x001708F4 File Offset: 0x0016EAF4
			public override void EndWrite(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream.BlockingEndWrite(asyncResult);
			}

			// Token: 0x06006AB4 RID: 27316 RVA: 0x0017090A File Offset: 0x0016EB0A
			public override int Read([In] [Out] byte[] buffer, int offset, int count)
			{
				return 0;
			}

			// Token: 0x06006AB5 RID: 27317 RVA: 0x00170910 File Offset: 0x0016EB10
			[ComVisible(false)]
			public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				Task<int> task = Stream.NullStream.s_nullReadTask;
				if (task == null)
				{
					task = (Stream.NullStream.s_nullReadTask = new Task<int>(false, 0, (TaskCreationOptions)16384, CancellationToken.None));
				}
				return task;
			}

			// Token: 0x06006AB6 RID: 27318 RVA: 0x0017093F File Offset: 0x0016EB3F
			public override int ReadByte()
			{
				return -1;
			}

			// Token: 0x06006AB7 RID: 27319 RVA: 0x00170942 File Offset: 0x0016EB42
			public override void Write(byte[] buffer, int offset, int count)
			{
			}

			// Token: 0x06006AB8 RID: 27320 RVA: 0x00170944 File Offset: 0x0016EB44
			[ComVisible(false)]
			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				if (!cancellationToken.IsCancellationRequested)
				{
					return Task.CompletedTask;
				}
				return Task.FromCancellation(cancellationToken);
			}

			// Token: 0x06006AB9 RID: 27321 RVA: 0x0017095C File Offset: 0x0016EB5C
			public override void WriteByte(byte value)
			{
			}

			// Token: 0x06006ABA RID: 27322 RVA: 0x0017095E File Offset: 0x0016EB5E
			public override long Seek(long offset, SeekOrigin origin)
			{
				return 0L;
			}

			// Token: 0x06006ABB RID: 27323 RVA: 0x00170962 File Offset: 0x0016EB62
			public override void SetLength(long length)
			{
			}

			// Token: 0x040032BF RID: 12991
			private static Task<int> s_nullReadTask;
		}

		// Token: 0x02000B17 RID: 2839
		internal sealed class SynchronousAsyncResult : IAsyncResult
		{
			// Token: 0x06006ABC RID: 27324 RVA: 0x00170964 File Offset: 0x0016EB64
			internal SynchronousAsyncResult(int bytesRead, object asyncStateObject)
			{
				this._bytesRead = bytesRead;
				this._stateObject = asyncStateObject;
			}

			// Token: 0x06006ABD RID: 27325 RVA: 0x0017097A File Offset: 0x0016EB7A
			internal SynchronousAsyncResult(object asyncStateObject)
			{
				this._stateObject = asyncStateObject;
				this._isWrite = true;
			}

			// Token: 0x06006ABE RID: 27326 RVA: 0x00170990 File Offset: 0x0016EB90
			internal SynchronousAsyncResult(Exception ex, object asyncStateObject, bool isWrite)
			{
				this._exceptionInfo = ExceptionDispatchInfo.Capture(ex);
				this._stateObject = asyncStateObject;
				this._isWrite = isWrite;
			}

			// Token: 0x1700120C RID: 4620
			// (get) Token: 0x06006ABF RID: 27327 RVA: 0x001709B2 File Offset: 0x0016EBB2
			public bool IsCompleted
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700120D RID: 4621
			// (get) Token: 0x06006AC0 RID: 27328 RVA: 0x001709B5 File Offset: 0x0016EBB5
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return LazyInitializer.EnsureInitialized<ManualResetEvent>(ref this._waitHandle, () => new ManualResetEvent(true));
				}
			}

			// Token: 0x1700120E RID: 4622
			// (get) Token: 0x06006AC1 RID: 27329 RVA: 0x001709E1 File Offset: 0x0016EBE1
			public object AsyncState
			{
				get
				{
					return this._stateObject;
				}
			}

			// Token: 0x1700120F RID: 4623
			// (get) Token: 0x06006AC2 RID: 27330 RVA: 0x001709E9 File Offset: 0x0016EBE9
			public bool CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06006AC3 RID: 27331 RVA: 0x001709EC File Offset: 0x0016EBEC
			internal void ThrowIfError()
			{
				if (this._exceptionInfo != null)
				{
					this._exceptionInfo.Throw();
				}
			}

			// Token: 0x06006AC4 RID: 27332 RVA: 0x00170A04 File Offset: 0x0016EC04
			internal static int EndRead(IAsyncResult asyncResult)
			{
				Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
				if (synchronousAsyncResult == null || synchronousAsyncResult._isWrite)
				{
					__Error.WrongAsyncResult();
				}
				if (synchronousAsyncResult._endXxxCalled)
				{
					__Error.EndReadCalledTwice();
				}
				synchronousAsyncResult._endXxxCalled = true;
				synchronousAsyncResult.ThrowIfError();
				return synchronousAsyncResult._bytesRead;
			}

			// Token: 0x06006AC5 RID: 27333 RVA: 0x00170A48 File Offset: 0x0016EC48
			internal static void EndWrite(IAsyncResult asyncResult)
			{
				Stream.SynchronousAsyncResult synchronousAsyncResult = asyncResult as Stream.SynchronousAsyncResult;
				if (synchronousAsyncResult == null || !synchronousAsyncResult._isWrite)
				{
					__Error.WrongAsyncResult();
				}
				if (synchronousAsyncResult._endXxxCalled)
				{
					__Error.EndWriteCalledTwice();
				}
				synchronousAsyncResult._endXxxCalled = true;
				synchronousAsyncResult.ThrowIfError();
			}

			// Token: 0x040032C0 RID: 12992
			private readonly object _stateObject;

			// Token: 0x040032C1 RID: 12993
			private readonly bool _isWrite;

			// Token: 0x040032C2 RID: 12994
			private ManualResetEvent _waitHandle;

			// Token: 0x040032C3 RID: 12995
			private ExceptionDispatchInfo _exceptionInfo;

			// Token: 0x040032C4 RID: 12996
			private bool _endXxxCalled;

			// Token: 0x040032C5 RID: 12997
			private int _bytesRead;

			// Token: 0x02000D01 RID: 3329
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x060071F2 RID: 29170 RVA: 0x00188733 File Offset: 0x00186933
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x060071F3 RID: 29171 RVA: 0x0018873F File Offset: 0x0018693F
				public <>c()
				{
				}

				// Token: 0x060071F4 RID: 29172 RVA: 0x00188747 File Offset: 0x00186947
				internal ManualResetEvent <get_AsyncWaitHandle>b__12_0()
				{
					return new ManualResetEvent(true);
				}

				// Token: 0x04003932 RID: 14642
				public static readonly Stream.SynchronousAsyncResult.<>c <>9 = new Stream.SynchronousAsyncResult.<>c();

				// Token: 0x04003933 RID: 14643
				public static Func<ManualResetEvent> <>9__12_0;
			}
		}

		// Token: 0x02000B18 RID: 2840
		[Serializable]
		internal sealed class SyncStream : Stream, IDisposable
		{
			// Token: 0x06006AC6 RID: 27334 RVA: 0x00170A86 File Offset: 0x0016EC86
			internal SyncStream(Stream stream)
			{
				if (stream == null)
				{
					throw new ArgumentNullException("stream");
				}
				this._stream = stream;
			}

			// Token: 0x17001210 RID: 4624
			// (get) Token: 0x06006AC7 RID: 27335 RVA: 0x00170AA3 File Offset: 0x0016ECA3
			public override bool CanRead
			{
				get
				{
					return this._stream.CanRead;
				}
			}

			// Token: 0x17001211 RID: 4625
			// (get) Token: 0x06006AC8 RID: 27336 RVA: 0x00170AB0 File Offset: 0x0016ECB0
			public override bool CanWrite
			{
				get
				{
					return this._stream.CanWrite;
				}
			}

			// Token: 0x17001212 RID: 4626
			// (get) Token: 0x06006AC9 RID: 27337 RVA: 0x00170ABD File Offset: 0x0016ECBD
			public override bool CanSeek
			{
				get
				{
					return this._stream.CanSeek;
				}
			}

			// Token: 0x17001213 RID: 4627
			// (get) Token: 0x06006ACA RID: 27338 RVA: 0x00170ACA File Offset: 0x0016ECCA
			[ComVisible(false)]
			public override bool CanTimeout
			{
				get
				{
					return this._stream.CanTimeout;
				}
			}

			// Token: 0x17001214 RID: 4628
			// (get) Token: 0x06006ACB RID: 27339 RVA: 0x00170AD8 File Offset: 0x0016ECD8
			public override long Length
			{
				get
				{
					Stream stream = this._stream;
					long length;
					lock (stream)
					{
						length = this._stream.Length;
					}
					return length;
				}
			}

			// Token: 0x17001215 RID: 4629
			// (get) Token: 0x06006ACC RID: 27340 RVA: 0x00170B20 File Offset: 0x0016ED20
			// (set) Token: 0x06006ACD RID: 27341 RVA: 0x00170B68 File Offset: 0x0016ED68
			public override long Position
			{
				get
				{
					Stream stream = this._stream;
					long position;
					lock (stream)
					{
						position = this._stream.Position;
					}
					return position;
				}
				set
				{
					Stream stream = this._stream;
					lock (stream)
					{
						this._stream.Position = value;
					}
				}
			}

			// Token: 0x17001216 RID: 4630
			// (get) Token: 0x06006ACE RID: 27342 RVA: 0x00170BB0 File Offset: 0x0016EDB0
			// (set) Token: 0x06006ACF RID: 27343 RVA: 0x00170BBD File Offset: 0x0016EDBD
			[ComVisible(false)]
			public override int ReadTimeout
			{
				get
				{
					return this._stream.ReadTimeout;
				}
				set
				{
					this._stream.ReadTimeout = value;
				}
			}

			// Token: 0x17001217 RID: 4631
			// (get) Token: 0x06006AD0 RID: 27344 RVA: 0x00170BCB File Offset: 0x0016EDCB
			// (set) Token: 0x06006AD1 RID: 27345 RVA: 0x00170BD8 File Offset: 0x0016EDD8
			[ComVisible(false)]
			public override int WriteTimeout
			{
				get
				{
					return this._stream.WriteTimeout;
				}
				set
				{
					this._stream.WriteTimeout = value;
				}
			}

			// Token: 0x06006AD2 RID: 27346 RVA: 0x00170BE8 File Offset: 0x0016EDE8
			public override void Close()
			{
				Stream stream = this._stream;
				lock (stream)
				{
					try
					{
						this._stream.Close();
					}
					finally
					{
						base.Dispose(true);
					}
				}
			}

			// Token: 0x06006AD3 RID: 27347 RVA: 0x00170C44 File Offset: 0x0016EE44
			protected override void Dispose(bool disposing)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					try
					{
						if (disposing)
						{
							((IDisposable)this._stream).Dispose();
						}
					}
					finally
					{
						base.Dispose(disposing);
					}
				}
			}

			// Token: 0x06006AD4 RID: 27348 RVA: 0x00170CA0 File Offset: 0x0016EEA0
			public override void Flush()
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.Flush();
				}
			}

			// Token: 0x06006AD5 RID: 27349 RVA: 0x00170CE8 File Offset: 0x0016EEE8
			public override int Read([In] [Out] byte[] bytes, int offset, int count)
			{
				Stream stream = this._stream;
				int num;
				lock (stream)
				{
					num = this._stream.Read(bytes, offset, count);
				}
				return num;
			}

			// Token: 0x06006AD6 RID: 27350 RVA: 0x00170D34 File Offset: 0x0016EF34
			public override int ReadByte()
			{
				Stream stream = this._stream;
				int num;
				lock (stream)
				{
					num = this._stream.ReadByte();
				}
				return num;
			}

			// Token: 0x06006AD7 RID: 27351 RVA: 0x00170D7C File Offset: 0x0016EF7C
			private static bool OverridesBeginMethod(Stream stream, string methodName)
			{
				MethodInfo[] methods = stream.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public);
				foreach (MethodInfo methodInfo in methods)
				{
					if (methodInfo.DeclaringType == typeof(Stream) && methodInfo.Name == methodName)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06006AD8 RID: 27352 RVA: 0x00170DD4 File Offset: 0x0016EFD4
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (this._overridesBeginRead == null)
				{
					this._overridesBeginRead = new bool?(Stream.SyncStream.OverridesBeginMethod(this._stream, "BeginRead"));
				}
				Stream stream = this._stream;
				IAsyncResult asyncResult;
				lock (stream)
				{
					asyncResult = (this._overridesBeginRead.Value ? this._stream.BeginRead(buffer, offset, count, callback, state) : this._stream.BeginReadInternal(buffer, offset, count, callback, state, true));
				}
				return asyncResult;
			}

			// Token: 0x06006AD9 RID: 27353 RVA: 0x00170E6C File Offset: 0x0016F06C
			public override int EndRead(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream stream = this._stream;
				int num;
				lock (stream)
				{
					num = this._stream.EndRead(asyncResult);
				}
				return num;
			}

			// Token: 0x06006ADA RID: 27354 RVA: 0x00170EC4 File Offset: 0x0016F0C4
			public override long Seek(long offset, SeekOrigin origin)
			{
				Stream stream = this._stream;
				long num;
				lock (stream)
				{
					num = this._stream.Seek(offset, origin);
				}
				return num;
			}

			// Token: 0x06006ADB RID: 27355 RVA: 0x00170F10 File Offset: 0x0016F110
			public override void SetLength(long length)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.SetLength(length);
				}
			}

			// Token: 0x06006ADC RID: 27356 RVA: 0x00170F58 File Offset: 0x0016F158
			public override void Write(byte[] bytes, int offset, int count)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.Write(bytes, offset, count);
				}
			}

			// Token: 0x06006ADD RID: 27357 RVA: 0x00170FA0 File Offset: 0x0016F1A0
			public override void WriteByte(byte b)
			{
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.WriteByte(b);
				}
			}

			// Token: 0x06006ADE RID: 27358 RVA: 0x00170FE8 File Offset: 0x0016F1E8
			[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				if (this._overridesBeginWrite == null)
				{
					this._overridesBeginWrite = new bool?(Stream.SyncStream.OverridesBeginMethod(this._stream, "BeginWrite"));
				}
				Stream stream = this._stream;
				IAsyncResult asyncResult;
				lock (stream)
				{
					asyncResult = (this._overridesBeginWrite.Value ? this._stream.BeginWrite(buffer, offset, count, callback, state) : this._stream.BeginWriteInternal(buffer, offset, count, callback, state, true));
				}
				return asyncResult;
			}

			// Token: 0x06006ADF RID: 27359 RVA: 0x00171080 File Offset: 0x0016F280
			public override void EndWrite(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				Stream stream = this._stream;
				lock (stream)
				{
					this._stream.EndWrite(asyncResult);
				}
			}

			// Token: 0x040032C6 RID: 12998
			private Stream _stream;

			// Token: 0x040032C7 RID: 12999
			[NonSerialized]
			private bool? _overridesBeginRead;

			// Token: 0x040032C8 RID: 13000
			[NonSerialized]
			private bool? _overridesBeginWrite;
		}

		// Token: 0x02000B19 RID: 2841
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006AE0 RID: 27360 RVA: 0x001710D4 File Offset: 0x0016F2D4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006AE1 RID: 27361 RVA: 0x001710E0 File Offset: 0x0016F2E0
			public <>c()
			{
			}

			// Token: 0x06006AE2 RID: 27362 RVA: 0x001710E8 File Offset: 0x0016F2E8
			internal SemaphoreSlim <EnsureAsyncActiveSemaphoreInitialized>b__4_0()
			{
				return new SemaphoreSlim(1, 1);
			}

			// Token: 0x06006AE3 RID: 27363 RVA: 0x001710F1 File Offset: 0x0016F2F1
			internal void <FlushAsync>b__36_0(object state)
			{
				((Stream)state).Flush();
			}

			// Token: 0x06006AE4 RID: 27364 RVA: 0x00171100 File Offset: 0x0016F300
			internal int <BeginReadInternal>b__39_0(object <p0>)
			{
				Stream.ReadWriteTask readWriteTask = Task.InternalCurrent as Stream.ReadWriteTask;
				int num = readWriteTask._stream.Read(readWriteTask._buffer, readWriteTask._offset, readWriteTask._count);
				readWriteTask.ClearBeginState();
				return num;
			}

			// Token: 0x06006AE5 RID: 27365 RVA: 0x0017113D File Offset: 0x0016F33D
			internal IAsyncResult <BeginEndReadAsync>b__43_0(Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state)
			{
				return stream.BeginRead(args.Buffer, args.Offset, args.Count, callback, state);
			}

			// Token: 0x06006AE6 RID: 27366 RVA: 0x0017115A File Offset: 0x0016F35A
			internal int <BeginEndReadAsync>b__43_1(Stream stream, IAsyncResult asyncResult)
			{
				return stream.EndRead(asyncResult);
			}

			// Token: 0x06006AE7 RID: 27367 RVA: 0x00171164 File Offset: 0x0016F364
			internal int <BeginWriteInternal>b__46_0(object <p0>)
			{
				Stream.ReadWriteTask readWriteTask = Task.InternalCurrent as Stream.ReadWriteTask;
				readWriteTask._stream.Write(readWriteTask._buffer, readWriteTask._offset, readWriteTask._count);
				readWriteTask.ClearBeginState();
				return 0;
			}

			// Token: 0x06006AE8 RID: 27368 RVA: 0x001711A0 File Offset: 0x0016F3A0
			internal void <RunReadWriteTaskWhenReady>b__47_0(Task t, object state)
			{
				Tuple<Stream, Stream.ReadWriteTask> tuple = (Tuple<Stream, Stream.ReadWriteTask>)state;
				tuple.Item1.RunReadWriteTask(tuple.Item2);
			}

			// Token: 0x06006AE9 RID: 27369 RVA: 0x001711C5 File Offset: 0x0016F3C5
			internal IAsyncResult <BeginEndWriteAsync>b__53_0(Stream stream, Stream.ReadWriteParameters args, AsyncCallback callback, object state)
			{
				return stream.BeginWrite(args.Buffer, args.Offset, args.Count, callback, state);
			}

			// Token: 0x06006AEA RID: 27370 RVA: 0x001711E4 File Offset: 0x0016F3E4
			internal VoidTaskResult <BeginEndWriteAsync>b__53_1(Stream stream, IAsyncResult asyncResult)
			{
				stream.EndWrite(asyncResult);
				return default(VoidTaskResult);
			}

			// Token: 0x040032C9 RID: 13001
			public static readonly Stream.<>c <>9 = new Stream.<>c();

			// Token: 0x040032CA RID: 13002
			public static Func<SemaphoreSlim> <>9__4_0;

			// Token: 0x040032CB RID: 13003
			public static Action<object> <>9__36_0;

			// Token: 0x040032CC RID: 13004
			public static Func<object, int> <>9__39_0;

			// Token: 0x040032CD RID: 13005
			public static Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult> <>9__43_0;

			// Token: 0x040032CE RID: 13006
			public static Func<Stream, IAsyncResult, int> <>9__43_1;

			// Token: 0x040032CF RID: 13007
			public static Func<object, int> <>9__46_0;

			// Token: 0x040032D0 RID: 13008
			public static Action<Task, object> <>9__47_0;

			// Token: 0x040032D1 RID: 13009
			public static Func<Stream, Stream.ReadWriteParameters, AsyncCallback, object, IAsyncResult> <>9__53_0;

			// Token: 0x040032D2 RID: 13010
			public static Func<Stream, IAsyncResult, VoidTaskResult> <>9__53_1;
		}

		// Token: 0x02000B1A RID: 2842
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <CopyToAsyncInternal>d__27 : IAsyncStateMachine
		{
			// Token: 0x06006AEB RID: 27371 RVA: 0x00171204 File Offset: 0x0016F404
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				Stream stream = this;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter3;
					if (num != 0)
					{
						if (num != 1)
						{
							buffer = new byte[bufferSize];
							goto IL_A3;
						}
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
						configuredTaskAwaiter = configuredTaskAwaiter2;
						configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num2 = -1;
						goto IL_116;
					}
					else
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter4;
						configuredTaskAwaiter3 = configuredTaskAwaiter4;
						configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num2 = -1;
					}
					IL_9C:
					configuredTaskAwaiter3.GetResult();
					IL_A3:
					configuredTaskAwaiter = stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						num2 = 1;
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, Stream.<CopyToAsyncInternal>d__27>(ref configuredTaskAwaiter, ref this);
						return;
					}
					IL_116:
					int result = configuredTaskAwaiter.GetResult();
					int num3;
					if ((num3 = result) != 0)
					{
						configuredTaskAwaiter3 = destination.WriteAsync(buffer, 0, num3, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!configuredTaskAwaiter3.IsCompleted)
						{
							num2 = 0;
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter configuredTaskAwaiter4 = configuredTaskAwaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, Stream.<CopyToAsyncInternal>d__27>(ref configuredTaskAwaiter3, ref this);
							return;
						}
						goto IL_9C;
					}
				}
				catch (Exception ex)
				{
					num2 = -2;
					buffer = null;
					this.<>t__builder.SetException(ex);
					return;
				}
				num2 = -2;
				buffer = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06006AEC RID: 27372 RVA: 0x00171394 File Offset: 0x0016F594
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040032D3 RID: 13011
			public int <>1__state;

			// Token: 0x040032D4 RID: 13012
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040032D5 RID: 13013
			public int bufferSize;

			// Token: 0x040032D6 RID: 13014
			public Stream destination;

			// Token: 0x040032D7 RID: 13015
			public CancellationToken cancellationToken;

			// Token: 0x040032D8 RID: 13016
			public Stream <>4__this;

			// Token: 0x040032D9 RID: 13017
			private byte[] <buffer>5__2;

			// Token: 0x040032DA RID: 13018
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040032DB RID: 13019
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;
		}
	}
}
