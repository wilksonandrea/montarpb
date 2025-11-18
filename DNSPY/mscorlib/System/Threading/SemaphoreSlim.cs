using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Threading
{
	// Token: 0x02000542 RID: 1346
	[ComVisible(false)]
	[DebuggerDisplay("Current Count = {m_currentCount}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class SemaphoreSlim : IDisposable
	{
		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06003F07 RID: 16135 RVA: 0x000EA531 File Offset: 0x000E8731
		[__DynamicallyInvokable]
		public int CurrentCount
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_currentCount;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06003F08 RID: 16136 RVA: 0x000EA53C File Offset: 0x000E873C
		[__DynamicallyInvokable]
		public WaitHandle AvailableWaitHandle
		{
			[__DynamicallyInvokable]
			get
			{
				this.CheckDispose();
				if (this.m_waitHandle != null)
				{
					return this.m_waitHandle;
				}
				object lockObj = this.m_lockObj;
				lock (lockObj)
				{
					if (this.m_waitHandle == null)
					{
						this.m_waitHandle = new ManualResetEvent(this.m_currentCount != 0);
					}
				}
				return this.m_waitHandle;
			}
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x000EA5BC File Offset: 0x000E87BC
		[__DynamicallyInvokable]
		public SemaphoreSlim(int initialCount)
			: this(initialCount, int.MaxValue)
		{
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x000EA5CC File Offset: 0x000E87CC
		[__DynamicallyInvokable]
		public SemaphoreSlim(int initialCount, int maxCount)
		{
			if (initialCount < 0 || initialCount > maxCount)
			{
				throw new ArgumentOutOfRangeException("initialCount", initialCount, SemaphoreSlim.GetResourceString("SemaphoreSlim_ctor_InitialCountWrong"));
			}
			if (maxCount <= 0)
			{
				throw new ArgumentOutOfRangeException("maxCount", maxCount, SemaphoreSlim.GetResourceString("SemaphoreSlim_ctor_MaxCountWrong"));
			}
			this.m_maxCount = maxCount;
			this.m_lockObj = new object();
			this.m_currentCount = initialCount;
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x000EA63C File Offset: 0x000E883C
		[__DynamicallyInvokable]
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		// Token: 0x06003F0C RID: 16140 RVA: 0x000EA65A File Offset: 0x000E885A
		[__DynamicallyInvokable]
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		// Token: 0x06003F0D RID: 16141 RVA: 0x000EA668 File Offset: 0x000E8868
		[__DynamicallyInvokable]
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
			}
			return this.Wait((int)timeout.TotalMilliseconds, default(CancellationToken));
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x000EA6C0 File Offset: 0x000E88C0
		[__DynamicallyInvokable]
		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
			}
			return this.Wait((int)timeout.TotalMilliseconds, cancellationToken);
		}

		// Token: 0x06003F0F RID: 16143 RVA: 0x000EA710 File Offset: 0x000E8910
		[__DynamicallyInvokable]
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x000EA730 File Offset: 0x000E8930
		[__DynamicallyInvokable]
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.CheckDispose();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("totalMilliSeconds", millisecondsTimeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
			}
			cancellationToken.ThrowIfCancellationRequested();
			uint num = 0U;
			if (millisecondsTimeout != -1 && millisecondsTimeout > 0)
			{
				num = TimeoutHelper.GetTime();
			}
			bool flag = false;
			Task<bool> task = null;
			bool flag2 = false;
			CancellationTokenRegistration cancellationTokenRegistration = cancellationToken.InternalRegisterWithoutEC(SemaphoreSlim.s_cancellationTokenCanceledEventHandler, this);
			try
			{
				SpinWait spinWait = default(SpinWait);
				while (this.m_currentCount == 0 && !spinWait.NextSpinWillYield)
				{
					spinWait.SpinOnce();
				}
				try
				{
				}
				finally
				{
					Monitor.Enter(this.m_lockObj, ref flag2);
					if (flag2)
					{
						this.m_waitCount++;
					}
				}
				if (this.m_asyncHead != null)
				{
					task = this.WaitAsync(millisecondsTimeout, cancellationToken);
				}
				else
				{
					OperationCanceledException ex = null;
					if (this.m_currentCount == 0)
					{
						if (millisecondsTimeout == 0)
						{
							return false;
						}
						try
						{
							flag = this.WaitUntilCountOrTimeout(millisecondsTimeout, num, cancellationToken);
						}
						catch (OperationCanceledException ex2)
						{
							ex = ex2;
						}
					}
					if (this.m_currentCount > 0)
					{
						flag = true;
						this.m_currentCount--;
					}
					else if (ex != null)
					{
						throw ex;
					}
					if (this.m_waitHandle != null && this.m_currentCount == 0)
					{
						this.m_waitHandle.Reset();
					}
				}
			}
			finally
			{
				if (flag2)
				{
					this.m_waitCount--;
					Monitor.Exit(this.m_lockObj);
				}
				cancellationTokenRegistration.Dispose();
			}
			if (task == null)
			{
				return flag;
			}
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x000EA8D0 File Offset: 0x000E8AD0
		private bool WaitUntilCountOrTimeout(int millisecondsTimeout, uint startTime, CancellationToken cancellationToken)
		{
			int num = -1;
			while (this.m_currentCount == 0)
			{
				cancellationToken.ThrowIfCancellationRequested();
				if (millisecondsTimeout != -1)
				{
					num = TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout);
					if (num <= 0)
					{
						return false;
					}
				}
				if (!Monitor.Wait(this.m_lockObj, num))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x000EA918 File Offset: 0x000E8B18
		[__DynamicallyInvokable]
		public Task WaitAsync()
		{
			return this.WaitAsync(-1, default(CancellationToken));
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x000EA935 File Offset: 0x000E8B35
		[__DynamicallyInvokable]
		public Task WaitAsync(CancellationToken cancellationToken)
		{
			return this.WaitAsync(-1, cancellationToken);
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x000EA940 File Offset: 0x000E8B40
		[__DynamicallyInvokable]
		public Task<bool> WaitAsync(int millisecondsTimeout)
		{
			return this.WaitAsync(millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x000EA960 File Offset: 0x000E8B60
		[__DynamicallyInvokable]
		public Task<bool> WaitAsync(TimeSpan timeout)
		{
			return this.WaitAsync(timeout, default(CancellationToken));
		}

		// Token: 0x06003F16 RID: 16150 RVA: 0x000EA980 File Offset: 0x000E8B80
		[__DynamicallyInvokable]
		public Task<bool> WaitAsync(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
			}
			return this.WaitAsync((int)timeout.TotalMilliseconds, cancellationToken);
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x000EA9D0 File Offset: 0x000E8BD0
		[__DynamicallyInvokable]
		public Task<bool> WaitAsync(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.CheckDispose();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("totalMilliSeconds", millisecondsTimeout, SemaphoreSlim.GetResourceString("SemaphoreSlim_Wait_TimeoutWrong"));
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation<bool>(cancellationToken);
			}
			object lockObj = this.m_lockObj;
			Task<bool> task;
			lock (lockObj)
			{
				if (this.m_currentCount > 0)
				{
					this.m_currentCount--;
					if (this.m_waitHandle != null && this.m_currentCount == 0)
					{
						this.m_waitHandle.Reset();
					}
					task = SemaphoreSlim.s_trueTask;
				}
				else
				{
					SemaphoreSlim.TaskNode taskNode = this.CreateAndAddAsyncWaiter();
					task = ((millisecondsTimeout == -1 && !cancellationToken.CanBeCanceled) ? taskNode : this.WaitUntilCountOrTimeoutAsync(taskNode, millisecondsTimeout, cancellationToken));
				}
			}
			return task;
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x000EAAA8 File Offset: 0x000E8CA8
		private SemaphoreSlim.TaskNode CreateAndAddAsyncWaiter()
		{
			SemaphoreSlim.TaskNode taskNode = new SemaphoreSlim.TaskNode();
			if (this.m_asyncHead == null)
			{
				this.m_asyncHead = taskNode;
				this.m_asyncTail = taskNode;
			}
			else
			{
				this.m_asyncTail.Next = taskNode;
				taskNode.Prev = this.m_asyncTail;
				this.m_asyncTail = taskNode;
			}
			return taskNode;
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x000EAAF4 File Offset: 0x000E8CF4
		private bool RemoveAsyncWaiter(SemaphoreSlim.TaskNode task)
		{
			bool flag = this.m_asyncHead == task || task.Prev != null;
			if (task.Next != null)
			{
				task.Next.Prev = task.Prev;
			}
			if (task.Prev != null)
			{
				task.Prev.Next = task.Next;
			}
			if (this.m_asyncHead == task)
			{
				this.m_asyncHead = task.Next;
			}
			if (this.m_asyncTail == task)
			{
				this.m_asyncTail = task.Prev;
			}
			task.Next = (task.Prev = null);
			return flag;
		}

		// Token: 0x06003F1A RID: 16154 RVA: 0x000EAB84 File Offset: 0x000E8D84
		private async Task<bool> WaitUntilCountOrTimeoutAsync(SemaphoreSlim.TaskNode asyncWaiter, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			using (CancellationTokenSource cts = (cancellationToken.CanBeCanceled ? CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, default(CancellationToken)) : new CancellationTokenSource()))
			{
				Task<Task> task = Task.WhenAny(new Task[]
				{
					asyncWaiter,
					Task.Delay(millisecondsTimeout, cts.Token)
				});
				object obj = asyncWaiter;
				Task task2 = await task.ConfigureAwait(false);
				if (obj == task2)
				{
					obj = null;
					cts.Cancel();
					return true;
				}
			}
			CancellationTokenSource cts = null;
			object lockObj = this.m_lockObj;
			lock (lockObj)
			{
				if (this.RemoveAsyncWaiter(asyncWaiter))
				{
					cancellationToken.ThrowIfCancellationRequested();
					return false;
				}
			}
			return await asyncWaiter.ConfigureAwait(false);
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x000EABDF File Offset: 0x000E8DDF
		[__DynamicallyInvokable]
		public int Release()
		{
			return this.Release(1);
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x000EABE8 File Offset: 0x000E8DE8
		[__DynamicallyInvokable]
		public int Release(int releaseCount)
		{
			this.CheckDispose();
			if (releaseCount < 1)
			{
				throw new ArgumentOutOfRangeException("releaseCount", releaseCount, SemaphoreSlim.GetResourceString("SemaphoreSlim_Release_CountWrong"));
			}
			object lockObj = this.m_lockObj;
			int num2;
			lock (lockObj)
			{
				int num = this.m_currentCount;
				num2 = num;
				if (this.m_maxCount - num < releaseCount)
				{
					throw new SemaphoreFullException();
				}
				num += releaseCount;
				int waitCount = this.m_waitCount;
				if (num == 1 || waitCount == 1)
				{
					Monitor.Pulse(this.m_lockObj);
				}
				else if (waitCount > 1)
				{
					Monitor.PulseAll(this.m_lockObj);
				}
				if (this.m_asyncHead != null)
				{
					int num3 = num - waitCount;
					while (num3 > 0 && this.m_asyncHead != null)
					{
						num--;
						num3--;
						SemaphoreSlim.TaskNode asyncHead = this.m_asyncHead;
						this.RemoveAsyncWaiter(asyncHead);
						SemaphoreSlim.QueueWaiterTask(asyncHead);
					}
				}
				this.m_currentCount = num;
				if (this.m_waitHandle != null && num2 == 0 && num > 0)
				{
					this.m_waitHandle.Set();
				}
			}
			return num2;
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x000EAD00 File Offset: 0x000E8F00
		[SecuritySafeCritical]
		private static void QueueWaiterTask(SemaphoreSlim.TaskNode waiterTask)
		{
			ThreadPool.UnsafeQueueCustomWorkItem(waiterTask, false);
		}

		// Token: 0x06003F1E RID: 16158 RVA: 0x000EAD09 File Offset: 0x000E8F09
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x000EAD18 File Offset: 0x000E8F18
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_waitHandle != null)
				{
					this.m_waitHandle.Close();
					this.m_waitHandle = null;
				}
				this.m_lockObj = null;
				this.m_asyncHead = null;
				this.m_asyncTail = null;
			}
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x000EAD54 File Offset: 0x000E8F54
		private static void CancellationTokenCanceledEventHandler(object obj)
		{
			SemaphoreSlim semaphoreSlim = obj as SemaphoreSlim;
			object lockObj = semaphoreSlim.m_lockObj;
			lock (lockObj)
			{
				Monitor.PulseAll(semaphoreSlim.m_lockObj);
			}
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x000EADA0 File Offset: 0x000E8FA0
		private void CheckDispose()
		{
			if (this.m_lockObj == null)
			{
				throw new ObjectDisposedException(null, SemaphoreSlim.GetResourceString("SemaphoreSlim_Disposed"));
			}
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x000EADBB File Offset: 0x000E8FBB
		private static string GetResourceString(string str)
		{
			return Environment.GetResourceString(str);
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x000EADC4 File Offset: 0x000E8FC4
		// Note: this type is marked as 'beforefieldinit'.
		static SemaphoreSlim()
		{
		}

		// Token: 0x04001A81 RID: 6785
		private volatile int m_currentCount;

		// Token: 0x04001A82 RID: 6786
		private readonly int m_maxCount;

		// Token: 0x04001A83 RID: 6787
		private volatile int m_waitCount;

		// Token: 0x04001A84 RID: 6788
		private object m_lockObj;

		// Token: 0x04001A85 RID: 6789
		private volatile ManualResetEvent m_waitHandle;

		// Token: 0x04001A86 RID: 6790
		private SemaphoreSlim.TaskNode m_asyncHead;

		// Token: 0x04001A87 RID: 6791
		private SemaphoreSlim.TaskNode m_asyncTail;

		// Token: 0x04001A88 RID: 6792
		private static readonly Task<bool> s_trueTask = new Task<bool>(false, true, (TaskCreationOptions)16384, default(CancellationToken));

		// Token: 0x04001A89 RID: 6793
		private const int NO_MAXIMUM = 2147483647;

		// Token: 0x04001A8A RID: 6794
		private static Action<object> s_cancellationTokenCanceledEventHandler = new Action<object>(SemaphoreSlim.CancellationTokenCanceledEventHandler);

		// Token: 0x02000C02 RID: 3074
		private sealed class TaskNode : Task<bool>, IThreadPoolWorkItem
		{
			// Token: 0x06006F96 RID: 28566 RVA: 0x00180794 File Offset: 0x0017E994
			internal TaskNode()
			{
			}

			// Token: 0x06006F97 RID: 28567 RVA: 0x0018079C File Offset: 0x0017E99C
			[SecurityCritical]
			void IThreadPoolWorkItem.ExecuteWorkItem()
			{
				bool flag = base.TrySetResult(true);
			}

			// Token: 0x06006F98 RID: 28568 RVA: 0x001807B1 File Offset: 0x0017E9B1
			[SecurityCritical]
			void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
			{
			}

			// Token: 0x04003655 RID: 13909
			internal SemaphoreSlim.TaskNode Prev;

			// Token: 0x04003656 RID: 13910
			internal SemaphoreSlim.TaskNode Next;
		}

		// Token: 0x02000C03 RID: 3075
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WaitUntilCountOrTimeoutAsync>d__31 : IAsyncStateMachine
		{
			// Token: 0x06006F99 RID: 28569 RVA: 0x001807B4 File Offset: 0x0017E9B4
			void IAsyncStateMachine.MoveNext()
			{
				int num2;
				int num = num2;
				SemaphoreSlim semaphoreSlim = this;
				bool flag;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
							configuredTaskAwaiter = configuredTaskAwaiter2;
							configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							num = (num2 = -1);
							goto IL_1D3;
						}
						cts = (cancellationToken.CanBeCanceled ? CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, default(CancellationToken)) : new CancellationTokenSource());
					}
					try
					{
						ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter3;
						if (num != 0)
						{
							Task<Task> task = Task.WhenAny(new Task[]
							{
								asyncWaiter,
								Task.Delay(millisecondsTimeout, cts.Token)
							});
							obj = asyncWaiter;
							configuredTaskAwaiter3 = task.ConfigureAwait(false).GetAwaiter();
							if (!configuredTaskAwaiter3.IsCompleted)
							{
								num = (num2 = 0);
								ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter4 = configuredTaskAwaiter3;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter, SemaphoreSlim.<WaitUntilCountOrTimeoutAsync>d__31>(ref configuredTaskAwaiter3, ref this);
								return;
							}
						}
						else
						{
							ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter4;
							configuredTaskAwaiter3 = configuredTaskAwaiter4;
							configuredTaskAwaiter4 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
							num = (num2 = -1);
						}
						Task result = configuredTaskAwaiter3.GetResult();
						if (obj == result)
						{
							obj = null;
							cts.Cancel();
							flag = true;
							goto IL_1FA;
						}
					}
					finally
					{
						if (num < 0 && cts != null)
						{
							((IDisposable)cts).Dispose();
						}
					}
					cts = null;
					object lockObj = semaphoreSlim.m_lockObj;
					bool flag2 = false;
					try
					{
						Monitor.Enter(lockObj, ref flag2);
						if (semaphoreSlim.RemoveAsyncWaiter(asyncWaiter))
						{
							cancellationToken.ThrowIfCancellationRequested();
							flag = false;
							goto IL_1FA;
						}
					}
					finally
					{
						if (num < 0 && flag2)
						{
							Monitor.Exit(lockObj);
						}
					}
					configuredTaskAwaiter = asyncWaiter.ConfigureAwait(false).GetAwaiter();
					if (!configuredTaskAwaiter.IsCompleted)
					{
						num = (num2 = 1);
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2 = configuredTaskAwaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, SemaphoreSlim.<WaitUntilCountOrTimeoutAsync>d__31>(ref configuredTaskAwaiter, ref this);
						return;
					}
					IL_1D3:
					bool result2 = configuredTaskAwaiter.GetResult();
					flag = result2;
				}
				catch (Exception ex)
				{
					num2 = -2;
					this.<>t__builder.SetException(ex);
					return;
				}
				IL_1FA:
				num2 = -2;
				this.<>t__builder.SetResult(flag);
			}

			// Token: 0x06006F9A RID: 28570 RVA: 0x00180A1C File Offset: 0x0017EC1C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04003657 RID: 13911
			public int <>1__state;

			// Token: 0x04003658 RID: 13912
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04003659 RID: 13913
			public CancellationToken cancellationToken;

			// Token: 0x0400365A RID: 13914
			public SemaphoreSlim.TaskNode asyncWaiter;

			// Token: 0x0400365B RID: 13915
			public int millisecondsTimeout;

			// Token: 0x0400365C RID: 13916
			public SemaphoreSlim <>4__this;

			// Token: 0x0400365D RID: 13917
			private CancellationTokenSource <cts>5__2;

			// Token: 0x0400365E RID: 13918
			private object <>7__wrap2;

			// Token: 0x0400365F RID: 13919
			private ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04003660 RID: 13920
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}
	}
}
