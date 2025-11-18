using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x02000537 RID: 1335
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(SpinLock.SystemThreading_SpinLockDebugView))]
	[DebuggerDisplay("IsHeld = {IsHeld}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct SpinLock
	{
		// Token: 0x06003EAF RID: 16047 RVA: 0x000E90BD File Offset: 0x000E72BD
		[__DynamicallyInvokable]
		public SpinLock(bool enableThreadOwnerTracking)
		{
			this.m_owner = 0;
			if (!enableThreadOwnerTracking)
			{
				this.m_owner |= int.MinValue;
			}
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x000E90E4 File Offset: 0x000E72E4
		[__DynamicallyInvokable]
		public void Enter(ref bool lockTaken)
		{
			Thread.BeginCriticalRegion();
			int owner = this.m_owner;
			if (lockTaken || (owner & -2147483647) != -2147483648 || Interlocked.CompareExchange(ref this.m_owner, owner | 1, owner, ref lockTaken) != owner)
			{
				this.ContinueTryEnter(-1, ref lockTaken);
			}
		}

		// Token: 0x06003EB1 RID: 16049 RVA: 0x000E912C File Offset: 0x000E732C
		[__DynamicallyInvokable]
		public void TryEnter(ref bool lockTaken)
		{
			this.TryEnter(0, ref lockTaken);
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x000E9138 File Offset: 0x000E7338
		[__DynamicallyInvokable]
		public void TryEnter(TimeSpan timeout, ref bool lockTaken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, Environment.GetResourceString("SpinLock_TryEnter_ArgumentOutOfRange"));
			}
			this.TryEnter((int)timeout.TotalMilliseconds, ref lockTaken);
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x000E9188 File Offset: 0x000E7388
		[__DynamicallyInvokable]
		public void TryEnter(int millisecondsTimeout, ref bool lockTaken)
		{
			Thread.BeginCriticalRegion();
			int owner = this.m_owner;
			if (((millisecondsTimeout < -1) | lockTaken) || (owner & -2147483647) != -2147483648 || Interlocked.CompareExchange(ref this.m_owner, owner | 1, owner, ref lockTaken) != owner)
			{
				this.ContinueTryEnter(millisecondsTimeout, ref lockTaken);
			}
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x000E91D8 File Offset: 0x000E73D8
		private void ContinueTryEnter(int millisecondsTimeout, ref bool lockTaken)
		{
			Thread.EndCriticalRegion();
			if (lockTaken)
			{
				lockTaken = false;
				throw new ArgumentException(Environment.GetResourceString("SpinLock_TryReliableEnter_ArgumentException"));
			}
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, Environment.GetResourceString("SpinLock_TryEnter_ArgumentOutOfRange"));
			}
			uint num = 0U;
			if (millisecondsTimeout != -1 && millisecondsTimeout != 0)
			{
				num = TimeoutHelper.GetTime();
			}
			if (CdsSyncEtwBCLProvider.Log.IsEnabled())
			{
				CdsSyncEtwBCLProvider.Log.SpinLock_FastPathFailed(this.m_owner);
			}
			if (this.IsThreadOwnerTrackingEnabled)
			{
				this.ContinueTryEnterWithThreadTracking(millisecondsTimeout, num, ref lockTaken);
				return;
			}
			int num2 = int.MaxValue;
			int num3 = this.m_owner;
			if ((num3 & 1) == 0)
			{
				Thread.BeginCriticalRegion();
				if (Interlocked.CompareExchange(ref this.m_owner, num3 | 1, num3, ref lockTaken) == num3)
				{
					return;
				}
				Thread.EndCriticalRegion();
			}
			else if ((num3 & 2147483646) != SpinLock.MAXIMUM_WAITERS)
			{
				num2 = (Interlocked.Add(ref this.m_owner, 2) & 2147483646) >> 1;
			}
			if (millisecondsTimeout == 0 || (millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(num, millisecondsTimeout) <= 0))
			{
				this.DecrementWaiters();
				return;
			}
			int processorCount = PlatformHelper.ProcessorCount;
			if (num2 < processorCount)
			{
				int num4 = 1;
				for (int i = 1; i <= num2 * 100; i++)
				{
					Thread.SpinWait((num2 + i) * 100 * num4);
					if (num4 < processorCount)
					{
						num4++;
					}
					num3 = this.m_owner;
					if ((num3 & 1) == 0)
					{
						Thread.BeginCriticalRegion();
						int num5 = (((num3 & 2147483646) == 0) ? (num3 | 1) : ((num3 - 2) | 1));
						if (Interlocked.CompareExchange(ref this.m_owner, num5, num3, ref lockTaken) == num3)
						{
							return;
						}
						Thread.EndCriticalRegion();
					}
				}
			}
			if (millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(num, millisecondsTimeout) <= 0)
			{
				this.DecrementWaiters();
				return;
			}
			int num6 = 0;
			for (;;)
			{
				num3 = this.m_owner;
				if ((num3 & 1) == 0)
				{
					Thread.BeginCriticalRegion();
					int num7 = (((num3 & 2147483646) == 0) ? (num3 | 1) : ((num3 - 2) | 1));
					if (Interlocked.CompareExchange(ref this.m_owner, num7, num3, ref lockTaken) == num3)
					{
						break;
					}
					Thread.EndCriticalRegion();
				}
				if (num6 % 40 == 0)
				{
					Thread.Sleep(1);
				}
				else if (num6 % 10 == 0)
				{
					Thread.Sleep(0);
				}
				else
				{
					Thread.Yield();
				}
				if (num6 % 10 == 0 && millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(num, millisecondsTimeout) <= 0)
				{
					goto Block_26;
				}
				num6++;
			}
			return;
			Block_26:
			this.DecrementWaiters();
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x000E93EC File Offset: 0x000E75EC
		private void DecrementWaiters()
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int owner = this.m_owner;
				if ((owner & 2147483646) == 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_owner, owner - 2, owner) == owner)
				{
					return;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x000E9430 File Offset: 0x000E7630
		private void ContinueTryEnterWithThreadTracking(int millisecondsTimeout, uint startTime, ref bool lockTaken)
		{
			int num = 0;
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			if (this.m_owner == managedThreadId)
			{
				throw new LockRecursionException(Environment.GetResourceString("SpinLock_TryEnter_LockRecursionException"));
			}
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				spinWait.SpinOnce();
				if (this.m_owner == num)
				{
					Thread.BeginCriticalRegion();
					if (Interlocked.CompareExchange(ref this.m_owner, managedThreadId, num, ref lockTaken) == num)
					{
						break;
					}
					Thread.EndCriticalRegion();
				}
				if (millisecondsTimeout == 0 || (millisecondsTimeout != -1 && spinWait.NextSpinWillYield && TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) <= 0))
				{
					return;
				}
			}
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x000E94B5 File Offset: 0x000E76B5
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public void Exit()
		{
			if ((this.m_owner & -2147483648) == 0)
			{
				this.ExitSlowPath(true);
			}
			else
			{
				Interlocked.Decrement(ref this.m_owner);
			}
			Thread.EndCriticalRegion();
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x000E94E4 File Offset: 0x000E76E4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public void Exit(bool useMemoryBarrier)
		{
			if ((this.m_owner & -2147483648) != 0 && !useMemoryBarrier)
			{
				int owner = this.m_owner;
				this.m_owner = owner & -2;
			}
			else
			{
				this.ExitSlowPath(useMemoryBarrier);
			}
			Thread.EndCriticalRegion();
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x000E9528 File Offset: 0x000E7728
		private void ExitSlowPath(bool useMemoryBarrier)
		{
			bool flag = (this.m_owner & int.MinValue) == 0;
			if (flag && !this.IsHeldByCurrentThread)
			{
				throw new SynchronizationLockException(Environment.GetResourceString("SpinLock_Exit_SynchronizationLockException"));
			}
			if (useMemoryBarrier)
			{
				if (flag)
				{
					Interlocked.Exchange(ref this.m_owner, 0);
					return;
				}
				Interlocked.Decrement(ref this.m_owner);
				return;
			}
			else
			{
				if (flag)
				{
					this.m_owner = 0;
					return;
				}
				int owner = this.m_owner;
				this.m_owner = owner & -2;
				return;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06003EBA RID: 16058 RVA: 0x000E95A5 File Offset: 0x000E77A5
		[__DynamicallyInvokable]
		public bool IsHeld
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				if (this.IsThreadOwnerTrackingEnabled)
				{
					return this.m_owner != 0;
				}
				return (this.m_owner & 1) != 0;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06003EBB RID: 16059 RVA: 0x000E95C8 File Offset: 0x000E77C8
		[__DynamicallyInvokable]
		public bool IsHeldByCurrentThread
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				if (!this.IsThreadOwnerTrackingEnabled)
				{
					throw new InvalidOperationException(Environment.GetResourceString("SpinLock_IsHeldByCurrentThread"));
				}
				return (this.m_owner & int.MaxValue) == Thread.CurrentThread.ManagedThreadId;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06003EBC RID: 16060 RVA: 0x000E95FC File Offset: 0x000E77FC
		[__DynamicallyInvokable]
		public bool IsThreadOwnerTrackingEnabled
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				return (this.m_owner & int.MinValue) == 0;
			}
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x000E960F File Offset: 0x000E780F
		// Note: this type is marked as 'beforefieldinit'.
		static SpinLock()
		{
		}

		// Token: 0x04001A58 RID: 6744
		private volatile int m_owner;

		// Token: 0x04001A59 RID: 6745
		private const int SPINNING_FACTOR = 100;

		// Token: 0x04001A5A RID: 6746
		private const int SLEEP_ONE_FREQUENCY = 40;

		// Token: 0x04001A5B RID: 6747
		private const int SLEEP_ZERO_FREQUENCY = 10;

		// Token: 0x04001A5C RID: 6748
		private const int TIMEOUT_CHECK_FREQUENCY = 10;

		// Token: 0x04001A5D RID: 6749
		private const int LOCK_ID_DISABLE_MASK = -2147483648;

		// Token: 0x04001A5E RID: 6750
		private const int LOCK_ANONYMOUS_OWNED = 1;

		// Token: 0x04001A5F RID: 6751
		private const int WAITERS_MASK = 2147483646;

		// Token: 0x04001A60 RID: 6752
		private const int ID_DISABLED_AND_ANONYMOUS_OWNED = -2147483647;

		// Token: 0x04001A61 RID: 6753
		private const int LOCK_UNOWNED = 0;

		// Token: 0x04001A62 RID: 6754
		private static int MAXIMUM_WAITERS = 2147483646;

		// Token: 0x02000BFD RID: 3069
		internal class SystemThreading_SpinLockDebugView
		{
			// Token: 0x06006F8C RID: 28556 RVA: 0x0018050D File Offset: 0x0017E70D
			public SystemThreading_SpinLockDebugView(SpinLock spinLock)
			{
				this.m_spinLock = spinLock;
			}

			// Token: 0x17001325 RID: 4901
			// (get) Token: 0x06006F8D RID: 28557 RVA: 0x0018051C File Offset: 0x0017E71C
			public bool? IsHeldByCurrentThread
			{
				get
				{
					bool? flag;
					try
					{
						flag = new bool?(this.m_spinLock.IsHeldByCurrentThread);
					}
					catch (InvalidOperationException)
					{
						flag = null;
					}
					return flag;
				}
			}

			// Token: 0x17001326 RID: 4902
			// (get) Token: 0x06006F8E RID: 28558 RVA: 0x0018055C File Offset: 0x0017E75C
			public int? OwnerThreadID
			{
				get
				{
					if (this.m_spinLock.IsThreadOwnerTrackingEnabled)
					{
						return new int?(this.m_spinLock.m_owner);
					}
					return null;
				}
			}

			// Token: 0x17001327 RID: 4903
			// (get) Token: 0x06006F8F RID: 28559 RVA: 0x00180592 File Offset: 0x0017E792
			public bool IsHeld
			{
				get
				{
					return this.m_spinLock.IsHeld;
				}
			}

			// Token: 0x0400364B RID: 13899
			private SpinLock m_spinLock;
		}
	}
}
