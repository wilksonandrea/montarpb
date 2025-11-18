using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x0200053B RID: 1339
	[ComVisible(false)]
	[DebuggerDisplay("Initial Count={InitialCount}, Current Count={CurrentCount}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class CountdownEvent : IDisposable
	{
		// Token: 0x06003EC9 RID: 16073 RVA: 0x000E983E File Offset: 0x000E7A3E
		[__DynamicallyInvokable]
		public CountdownEvent(int initialCount)
		{
			if (initialCount < 0)
			{
				throw new ArgumentOutOfRangeException("initialCount");
			}
			this.m_initialCount = initialCount;
			this.m_currentCount = initialCount;
			this.m_event = new ManualResetEventSlim();
			if (initialCount == 0)
			{
				this.m_event.Set();
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06003ECA RID: 16074 RVA: 0x000E9880 File Offset: 0x000E7A80
		[__DynamicallyInvokable]
		public int CurrentCount
		{
			[__DynamicallyInvokable]
			get
			{
				int currentCount = this.m_currentCount;
				if (currentCount >= 0)
				{
					return currentCount;
				}
				return 0;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06003ECB RID: 16075 RVA: 0x000E989D File Offset: 0x000E7A9D
		[__DynamicallyInvokable]
		public int InitialCount
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_initialCount;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06003ECC RID: 16076 RVA: 0x000E98A5 File Offset: 0x000E7AA5
		[__DynamicallyInvokable]
		public bool IsSet
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_currentCount <= 0;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06003ECD RID: 16077 RVA: 0x000E98B5 File Offset: 0x000E7AB5
		[__DynamicallyInvokable]
		public WaitHandle WaitHandle
		{
			[__DynamicallyInvokable]
			get
			{
				this.ThrowIfDisposed();
				return this.m_event.WaitHandle;
			}
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x000E98C8 File Offset: 0x000E7AC8
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x000E98D7 File Offset: 0x000E7AD7
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.m_event.Dispose();
				this.m_disposed = true;
			}
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x000E98F0 File Offset: 0x000E7AF0
		[__DynamicallyInvokable]
		public bool Signal()
		{
			this.ThrowIfDisposed();
			if (this.m_currentCount <= 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Decrement_BelowZero"));
			}
			int num = Interlocked.Decrement(ref this.m_currentCount);
			if (num == 0)
			{
				this.m_event.Set();
				return true;
			}
			if (num < 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Decrement_BelowZero"));
			}
			return false;
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x000E9950 File Offset: 0x000E7B50
		[__DynamicallyInvokable]
		public bool Signal(int signalCount)
		{
			if (signalCount <= 0)
			{
				throw new ArgumentOutOfRangeException("signalCount");
			}
			this.ThrowIfDisposed();
			SpinWait spinWait = default(SpinWait);
			int currentCount;
			for (;;)
			{
				currentCount = this.m_currentCount;
				if (currentCount < signalCount)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_currentCount, currentCount - signalCount, currentCount) == currentCount)
				{
					goto IL_55;
				}
				spinWait.SpinOnce();
			}
			throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Decrement_BelowZero"));
			IL_55:
			if (currentCount == signalCount)
			{
				this.m_event.Set();
				return true;
			}
			return false;
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x000E99C4 File Offset: 0x000E7BC4
		[__DynamicallyInvokable]
		public void AddCount()
		{
			this.AddCount(1);
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x000E99CD File Offset: 0x000E7BCD
		[__DynamicallyInvokable]
		public bool TryAddCount()
		{
			return this.TryAddCount(1);
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x000E99D6 File Offset: 0x000E7BD6
		[__DynamicallyInvokable]
		public void AddCount(int signalCount)
		{
			if (!this.TryAddCount(signalCount))
			{
				throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Increment_AlreadyZero"));
			}
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x000E99F4 File Offset: 0x000E7BF4
		[__DynamicallyInvokable]
		public bool TryAddCount(int signalCount)
		{
			if (signalCount <= 0)
			{
				throw new ArgumentOutOfRangeException("signalCount");
			}
			this.ThrowIfDisposed();
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int currentCount = this.m_currentCount;
				if (currentCount <= 0)
				{
					break;
				}
				if (currentCount > 2147483647 - signalCount)
				{
					goto Block_3;
				}
				if (Interlocked.CompareExchange(ref this.m_currentCount, currentCount + signalCount, currentCount) == currentCount)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
			Block_3:
			throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Increment_AlreadyMax"));
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x000E9A63 File Offset: 0x000E7C63
		[__DynamicallyInvokable]
		public void Reset()
		{
			this.Reset(this.m_initialCount);
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x000E9A74 File Offset: 0x000E7C74
		[__DynamicallyInvokable]
		public void Reset(int count)
		{
			this.ThrowIfDisposed();
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.m_currentCount = count;
			this.m_initialCount = count;
			if (count == 0)
			{
				this.m_event.Set();
				return;
			}
			this.m_event.Reset();
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x000E9AC0 File Offset: 0x000E7CC0
		[__DynamicallyInvokable]
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x000E9ADE File Offset: 0x000E7CDE
		[__DynamicallyInvokable]
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x000E9AEC File Offset: 0x000E7CEC
		[__DynamicallyInvokable]
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, default(CancellationToken));
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x000E9B2C File Offset: 0x000E7D2C
		[__DynamicallyInvokable]
		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, cancellationToken);
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x000E9B64 File Offset: 0x000E7D64
		[__DynamicallyInvokable]
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x000E9B84 File Offset: 0x000E7D84
		[__DynamicallyInvokable]
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			this.ThrowIfDisposed();
			cancellationToken.ThrowIfCancellationRequested();
			bool flag = this.IsSet;
			if (!flag)
			{
				flag = this.m_event.Wait(millisecondsTimeout, cancellationToken);
			}
			return flag;
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x000E9BC6 File Offset: 0x000E7DC6
		private void ThrowIfDisposed()
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("CountdownEvent");
			}
		}

		// Token: 0x04001A6A RID: 6762
		private int m_initialCount;

		// Token: 0x04001A6B RID: 6763
		private volatile int m_currentCount;

		// Token: 0x04001A6C RID: 6764
		private ManualResetEventSlim m_event;

		// Token: 0x04001A6D RID: 6765
		private volatile bool m_disposed;
	}
}
