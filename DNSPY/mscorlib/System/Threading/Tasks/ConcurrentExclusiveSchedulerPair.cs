using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	// Token: 0x02000581 RID: 1409
	[DebuggerDisplay("Concurrent={ConcurrentTaskCountForDebugger}, Exclusive={ExclusiveTaskCountForDebugger}, Mode={ModeForDebugger}")]
	[DebuggerTypeProxy(typeof(ConcurrentExclusiveSchedulerPair.DebugView))]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class ConcurrentExclusiveSchedulerPair
	{
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x0600424C RID: 16972 RVA: 0x000F69F4 File Offset: 0x000F4BF4
		private static int DefaultMaxConcurrencyLevel
		{
			get
			{
				return Environment.ProcessorCount;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x0600424D RID: 16973 RVA: 0x000F69FB File Offset: 0x000F4BFB
		private object ValueLock
		{
			get
			{
				return this.m_threadProcessingMapping;
			}
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x000F6A03 File Offset: 0x000F4C03
		[__DynamicallyInvokable]
		public ConcurrentExclusiveSchedulerPair()
			: this(TaskScheduler.Default, ConcurrentExclusiveSchedulerPair.DefaultMaxConcurrencyLevel, -1)
		{
		}

		// Token: 0x0600424F RID: 16975 RVA: 0x000F6A16 File Offset: 0x000F4C16
		[__DynamicallyInvokable]
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler)
			: this(taskScheduler, ConcurrentExclusiveSchedulerPair.DefaultMaxConcurrencyLevel, -1)
		{
		}

		// Token: 0x06004250 RID: 16976 RVA: 0x000F6A25 File Offset: 0x000F4C25
		[__DynamicallyInvokable]
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler, int maxConcurrencyLevel)
			: this(taskScheduler, maxConcurrencyLevel, -1)
		{
		}

		// Token: 0x06004251 RID: 16977 RVA: 0x000F6A30 File Offset: 0x000F4C30
		[__DynamicallyInvokable]
		public ConcurrentExclusiveSchedulerPair(TaskScheduler taskScheduler, int maxConcurrencyLevel, int maxItemsPerTask)
		{
			if (taskScheduler == null)
			{
				throw new ArgumentNullException("taskScheduler");
			}
			if (maxConcurrencyLevel == 0 || maxConcurrencyLevel < -1)
			{
				throw new ArgumentOutOfRangeException("maxConcurrencyLevel");
			}
			if (maxItemsPerTask == 0 || maxItemsPerTask < -1)
			{
				throw new ArgumentOutOfRangeException("maxItemsPerTask");
			}
			this.m_underlyingTaskScheduler = taskScheduler;
			this.m_maxConcurrencyLevel = maxConcurrencyLevel;
			this.m_maxItemsPerTask = maxItemsPerTask;
			int maximumConcurrencyLevel = taskScheduler.MaximumConcurrencyLevel;
			if (maximumConcurrencyLevel > 0 && maximumConcurrencyLevel < this.m_maxConcurrencyLevel)
			{
				this.m_maxConcurrencyLevel = maximumConcurrencyLevel;
			}
			if (this.m_maxConcurrencyLevel == -1)
			{
				this.m_maxConcurrencyLevel = int.MaxValue;
			}
			if (this.m_maxItemsPerTask == -1)
			{
				this.m_maxItemsPerTask = int.MaxValue;
			}
			this.m_exclusiveTaskScheduler = new ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler(this, 1, ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask);
			this.m_concurrentTaskScheduler = new ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler(this, this.m_maxConcurrencyLevel, ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks);
		}

		// Token: 0x06004252 RID: 16978 RVA: 0x000F6AFC File Offset: 0x000F4CFC
		[__DynamicallyInvokable]
		public void Complete()
		{
			object valueLock = this.ValueLock;
			lock (valueLock)
			{
				if (!this.CompletionRequested)
				{
					this.RequestCompletion();
					this.CleanupStateIfCompletingAndQuiesced();
				}
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06004253 RID: 16979 RVA: 0x000F6B4C File Offset: 0x000F4D4C
		[__DynamicallyInvokable]
		public Task Completion
		{
			[__DynamicallyInvokable]
			get
			{
				return this.EnsureCompletionStateInitialized().Task;
			}
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x000F6B59 File Offset: 0x000F4D59
		private ConcurrentExclusiveSchedulerPair.CompletionState EnsureCompletionStateInitialized()
		{
			return LazyInitializer.EnsureInitialized<ConcurrentExclusiveSchedulerPair.CompletionState>(ref this.m_completionState, () => new ConcurrentExclusiveSchedulerPair.CompletionState());
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06004255 RID: 16981 RVA: 0x000F6B85 File Offset: 0x000F4D85
		private bool CompletionRequested
		{
			get
			{
				return this.m_completionState != null && Volatile.Read(ref this.m_completionState.m_completionRequested);
			}
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x000F6BA1 File Offset: 0x000F4DA1
		private void RequestCompletion()
		{
			this.EnsureCompletionStateInitialized().m_completionRequested = true;
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x000F6BAF File Offset: 0x000F4DAF
		private void CleanupStateIfCompletingAndQuiesced()
		{
			if (this.ReadyToComplete)
			{
				this.CompleteTaskAsync();
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06004258 RID: 16984 RVA: 0x000F6BC0 File Offset: 0x000F4DC0
		private bool ReadyToComplete
		{
			get
			{
				if (!this.CompletionRequested || this.m_processingCount != 0)
				{
					return false;
				}
				ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
				return (completionState.m_exceptions != null && completionState.m_exceptions.Count > 0) || (this.m_concurrentTaskScheduler.m_tasks.IsEmpty && this.m_exclusiveTaskScheduler.m_tasks.IsEmpty);
			}
		}

		// Token: 0x06004259 RID: 16985 RVA: 0x000F6C24 File Offset: 0x000F4E24
		private void CompleteTaskAsync()
		{
			ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
			if (!completionState.m_completionQueued)
			{
				completionState.m_completionQueued = true;
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					ConcurrentExclusiveSchedulerPair.CompletionState completionState2 = (ConcurrentExclusiveSchedulerPair.CompletionState)state;
					List<Exception> exceptions = completionState2.m_exceptions;
					if (exceptions == null || exceptions.Count <= 0)
					{
						completionState2.TrySetResult(default(VoidTaskResult));
					}
					else
					{
						completionState2.TrySetException(exceptions);
					}
				}, completionState);
			}
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x000F6C70 File Offset: 0x000F4E70
		private void FaultWithTask(Task faultedTask)
		{
			ConcurrentExclusiveSchedulerPair.CompletionState completionState = this.EnsureCompletionStateInitialized();
			if (completionState.m_exceptions == null)
			{
				completionState.m_exceptions = new List<Exception>();
			}
			completionState.m_exceptions.AddRange(faultedTask.Exception.InnerExceptions);
			this.RequestCompletion();
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x0600425B RID: 16987 RVA: 0x000F6CB3 File Offset: 0x000F4EB3
		[__DynamicallyInvokable]
		public TaskScheduler ConcurrentScheduler
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_concurrentTaskScheduler;
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x0600425C RID: 16988 RVA: 0x000F6CBB File Offset: 0x000F4EBB
		[__DynamicallyInvokable]
		public TaskScheduler ExclusiveScheduler
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_exclusiveTaskScheduler;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x0600425D RID: 16989 RVA: 0x000F6CC3 File Offset: 0x000F4EC3
		private int ConcurrentTaskCountForDebugger
		{
			get
			{
				return this.m_concurrentTaskScheduler.m_tasks.Count;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x0600425E RID: 16990 RVA: 0x000F6CD5 File Offset: 0x000F4ED5
		private int ExclusiveTaskCountForDebugger
		{
			get
			{
				return this.m_exclusiveTaskScheduler.m_tasks.Count;
			}
		}

		// Token: 0x0600425F RID: 16991 RVA: 0x000F6CE8 File Offset: 0x000F4EE8
		private void ProcessAsyncIfNecessary(bool fairly = false)
		{
			if (this.m_processingCount >= 0)
			{
				bool flag = !this.m_exclusiveTaskScheduler.m_tasks.IsEmpty;
				Task task = null;
				if (this.m_processingCount == 0 && flag)
				{
					this.m_processingCount = -1;
					try
					{
						task = new Task(delegate(object thisPair)
						{
							((ConcurrentExclusiveSchedulerPair)thisPair).ProcessExclusiveTasks();
						}, this, default(CancellationToken), ConcurrentExclusiveSchedulerPair.GetCreationOptionsForTask(fairly));
						task.Start(this.m_underlyingTaskScheduler);
						goto IL_149;
					}
					catch
					{
						this.m_processingCount = 0;
						this.FaultWithTask(task);
						goto IL_149;
					}
				}
				int count = this.m_concurrentTaskScheduler.m_tasks.Count;
				if (count > 0 && !flag && this.m_processingCount < this.m_maxConcurrencyLevel)
				{
					int num = 0;
					while (num < count && this.m_processingCount < this.m_maxConcurrencyLevel)
					{
						this.m_processingCount++;
						try
						{
							task = new Task(delegate(object thisPair)
							{
								((ConcurrentExclusiveSchedulerPair)thisPair).ProcessConcurrentTasks();
							}, this, default(CancellationToken), ConcurrentExclusiveSchedulerPair.GetCreationOptionsForTask(fairly));
							task.Start(this.m_underlyingTaskScheduler);
						}
						catch
						{
							this.m_processingCount--;
							this.FaultWithTask(task);
						}
						num++;
					}
				}
				IL_149:
				this.CleanupStateIfCompletingAndQuiesced();
			}
		}

		// Token: 0x06004260 RID: 16992 RVA: 0x000F6E60 File Offset: 0x000F5060
		private void ProcessExclusiveTasks()
		{
			try
			{
				this.m_threadProcessingMapping[Thread.CurrentThread.ManagedThreadId] = ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask;
				for (int i = 0; i < this.m_maxItemsPerTask; i++)
				{
					Task task;
					if (!this.m_exclusiveTaskScheduler.m_tasks.TryDequeue(out task))
					{
						break;
					}
					if (!task.IsFaulted)
					{
						this.m_exclusiveTaskScheduler.ExecuteTask(task);
					}
				}
			}
			finally
			{
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode;
				this.m_threadProcessingMapping.TryRemove(Thread.CurrentThread.ManagedThreadId, out processingMode);
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					this.m_processingCount = 0;
					this.ProcessAsyncIfNecessary(true);
				}
			}
		}

		// Token: 0x06004261 RID: 16993 RVA: 0x000F6F24 File Offset: 0x000F5124
		private void ProcessConcurrentTasks()
		{
			try
			{
				this.m_threadProcessingMapping[Thread.CurrentThread.ManagedThreadId] = ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks;
				for (int i = 0; i < this.m_maxItemsPerTask; i++)
				{
					Task task;
					if (!this.m_concurrentTaskScheduler.m_tasks.TryDequeue(out task))
					{
						break;
					}
					if (!task.IsFaulted)
					{
						this.m_concurrentTaskScheduler.ExecuteTask(task);
					}
					if (!this.m_exclusiveTaskScheduler.m_tasks.IsEmpty)
					{
						break;
					}
				}
			}
			finally
			{
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode;
				this.m_threadProcessingMapping.TryRemove(Thread.CurrentThread.ManagedThreadId, out processingMode);
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					if (this.m_processingCount > 0)
					{
						this.m_processingCount--;
					}
					this.ProcessAsyncIfNecessary(true);
				}
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06004262 RID: 16994 RVA: 0x000F7010 File Offset: 0x000F5210
		private ConcurrentExclusiveSchedulerPair.ProcessingMode ModeForDebugger
		{
			get
			{
				if (this.m_completionState != null && this.m_completionState.Task.IsCompleted)
				{
					return ConcurrentExclusiveSchedulerPair.ProcessingMode.Completed;
				}
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode = ConcurrentExclusiveSchedulerPair.ProcessingMode.NotCurrentlyProcessing;
				if (this.m_processingCount == -1)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask;
				}
				if (this.m_processingCount >= 1)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingConcurrentTasks;
				}
				if (this.CompletionRequested)
				{
					processingMode |= ConcurrentExclusiveSchedulerPair.ProcessingMode.Completing;
				}
				return processingMode;
			}
		}

		// Token: 0x06004263 RID: 16995 RVA: 0x000F7062 File Offset: 0x000F5262
		[Conditional("DEBUG")]
		internal static void ContractAssertMonitorStatus(object syncObj, bool held)
		{
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x000F7064 File Offset: 0x000F5264
		internal static TaskCreationOptions GetCreationOptionsForTask(bool isReplacementReplica = false)
		{
			TaskCreationOptions taskCreationOptions = TaskCreationOptions.DenyChildAttach;
			if (isReplacementReplica)
			{
				taskCreationOptions |= TaskCreationOptions.PreferFairness;
			}
			return taskCreationOptions;
		}

		// Token: 0x04001B91 RID: 7057
		private readonly ConcurrentDictionary<int, ConcurrentExclusiveSchedulerPair.ProcessingMode> m_threadProcessingMapping = new ConcurrentDictionary<int, ConcurrentExclusiveSchedulerPair.ProcessingMode>();

		// Token: 0x04001B92 RID: 7058
		private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_concurrentTaskScheduler;

		// Token: 0x04001B93 RID: 7059
		private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_exclusiveTaskScheduler;

		// Token: 0x04001B94 RID: 7060
		private readonly TaskScheduler m_underlyingTaskScheduler;

		// Token: 0x04001B95 RID: 7061
		private readonly int m_maxConcurrencyLevel;

		// Token: 0x04001B96 RID: 7062
		private readonly int m_maxItemsPerTask;

		// Token: 0x04001B97 RID: 7063
		private int m_processingCount;

		// Token: 0x04001B98 RID: 7064
		private ConcurrentExclusiveSchedulerPair.CompletionState m_completionState;

		// Token: 0x04001B99 RID: 7065
		private const int UNLIMITED_PROCESSING = -1;

		// Token: 0x04001B9A RID: 7066
		private const int EXCLUSIVE_PROCESSING_SENTINEL = -1;

		// Token: 0x04001B9B RID: 7067
		private const int DEFAULT_MAXITEMSPERTASK = -1;

		// Token: 0x02000C28 RID: 3112
		private sealed class CompletionState : TaskCompletionSource<VoidTaskResult>
		{
			// Token: 0x06007014 RID: 28692 RVA: 0x001825F7 File Offset: 0x001807F7
			public CompletionState()
			{
			}

			// Token: 0x040036E9 RID: 14057
			internal bool m_completionRequested;

			// Token: 0x040036EA RID: 14058
			internal bool m_completionQueued;

			// Token: 0x040036EB RID: 14059
			internal List<Exception> m_exceptions;
		}

		// Token: 0x02000C29 RID: 3113
		[DebuggerDisplay("Count={CountForDebugger}, MaxConcurrencyLevel={m_maxConcurrencyLevel}, Id={Id}")]
		[DebuggerTypeProxy(typeof(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.DebugView))]
		private sealed class ConcurrentExclusiveTaskScheduler : TaskScheduler
		{
			// Token: 0x06007015 RID: 28693 RVA: 0x00182600 File Offset: 0x00180800
			internal ConcurrentExclusiveTaskScheduler(ConcurrentExclusiveSchedulerPair pair, int maxConcurrencyLevel, ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode)
			{
				this.m_pair = pair;
				this.m_maxConcurrencyLevel = maxConcurrencyLevel;
				this.m_processingMode = processingMode;
				IProducerConsumerQueue<Task> producerConsumerQueue2;
				if (processingMode != ConcurrentExclusiveSchedulerPair.ProcessingMode.ProcessingExclusiveTask)
				{
					IProducerConsumerQueue<Task> producerConsumerQueue = new MultiProducerMultiConsumerQueue<Task>();
					producerConsumerQueue2 = producerConsumerQueue;
				}
				else
				{
					IProducerConsumerQueue<Task> producerConsumerQueue = new SingleProducerSingleConsumerQueue<Task>();
					producerConsumerQueue2 = producerConsumerQueue;
				}
				this.m_tasks = producerConsumerQueue2;
			}

			// Token: 0x17001330 RID: 4912
			// (get) Token: 0x06007016 RID: 28694 RVA: 0x00182642 File Offset: 0x00180842
			public override int MaximumConcurrencyLevel
			{
				get
				{
					return this.m_maxConcurrencyLevel;
				}
			}

			// Token: 0x06007017 RID: 28695 RVA: 0x0018264C File Offset: 0x0018084C
			[SecurityCritical]
			protected internal override void QueueTask(Task task)
			{
				object valueLock = this.m_pair.ValueLock;
				lock (valueLock)
				{
					if (this.m_pair.CompletionRequested)
					{
						throw new InvalidOperationException(base.GetType().Name);
					}
					this.m_tasks.Enqueue(task);
					this.m_pair.ProcessAsyncIfNecessary(false);
				}
			}

			// Token: 0x06007018 RID: 28696 RVA: 0x001826C4 File Offset: 0x001808C4
			[SecuritySafeCritical]
			internal void ExecuteTask(Task task)
			{
				base.TryExecuteTask(task);
			}

			// Token: 0x06007019 RID: 28697 RVA: 0x001826D0 File Offset: 0x001808D0
			[SecurityCritical]
			protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
			{
				if (!taskWasPreviouslyQueued && this.m_pair.CompletionRequested)
				{
					return false;
				}
				bool flag = this.m_pair.m_underlyingTaskScheduler == TaskScheduler.Default;
				if (flag && taskWasPreviouslyQueued && !Thread.CurrentThread.IsThreadPoolThread)
				{
					return false;
				}
				ConcurrentExclusiveSchedulerPair.ProcessingMode processingMode;
				if (!this.m_pair.m_threadProcessingMapping.TryGetValue(Thread.CurrentThread.ManagedThreadId, out processingMode) || processingMode != this.m_processingMode)
				{
					return false;
				}
				if (!flag || taskWasPreviouslyQueued)
				{
					return this.TryExecuteTaskInlineOnTargetScheduler(task);
				}
				return base.TryExecuteTask(task);
			}

			// Token: 0x0600701A RID: 28698 RVA: 0x00182754 File Offset: 0x00180954
			private bool TryExecuteTaskInlineOnTargetScheduler(Task task)
			{
				Task<bool> task2 = new Task<bool>(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.s_tryExecuteTaskShim, Tuple.Create<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task>(this, task));
				bool result;
				try
				{
					task2.RunSynchronously(this.m_pair.m_underlyingTaskScheduler);
					result = task2.Result;
				}
				catch
				{
					AggregateException exception = task2.Exception;
					throw;
				}
				finally
				{
					task2.Dispose();
				}
				return result;
			}

			// Token: 0x0600701B RID: 28699 RVA: 0x001827BC File Offset: 0x001809BC
			[SecuritySafeCritical]
			private static bool TryExecuteTaskShim(object state)
			{
				Tuple<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task> tuple = (Tuple<ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler, Task>)state;
				return tuple.Item1.TryExecuteTask(tuple.Item2);
			}

			// Token: 0x0600701C RID: 28700 RVA: 0x001827E1 File Offset: 0x001809E1
			[SecurityCritical]
			protected override IEnumerable<Task> GetScheduledTasks()
			{
				return this.m_tasks;
			}

			// Token: 0x17001331 RID: 4913
			// (get) Token: 0x0600701D RID: 28701 RVA: 0x001827E9 File Offset: 0x001809E9
			private int CountForDebugger
			{
				get
				{
					return this.m_tasks.Count;
				}
			}

			// Token: 0x0600701E RID: 28702 RVA: 0x001827F6 File Offset: 0x001809F6
			// Note: this type is marked as 'beforefieldinit'.
			static ConcurrentExclusiveTaskScheduler()
			{
			}

			// Token: 0x040036EC RID: 14060
			private static readonly Func<object, bool> s_tryExecuteTaskShim = new Func<object, bool>(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler.TryExecuteTaskShim);

			// Token: 0x040036ED RID: 14061
			private readonly ConcurrentExclusiveSchedulerPair m_pair;

			// Token: 0x040036EE RID: 14062
			private readonly int m_maxConcurrencyLevel;

			// Token: 0x040036EF RID: 14063
			private readonly ConcurrentExclusiveSchedulerPair.ProcessingMode m_processingMode;

			// Token: 0x040036F0 RID: 14064
			internal readonly IProducerConsumerQueue<Task> m_tasks;

			// Token: 0x02000D11 RID: 3345
			private sealed class DebugView
			{
				// Token: 0x0600722A RID: 29226 RVA: 0x001894ED File Offset: 0x001876ED
				public DebugView(ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler scheduler)
				{
					this.m_taskScheduler = scheduler;
				}

				// Token: 0x17001390 RID: 5008
				// (get) Token: 0x0600722B RID: 29227 RVA: 0x001894FC File Offset: 0x001876FC
				public int MaximumConcurrencyLevel
				{
					get
					{
						return this.m_taskScheduler.m_maxConcurrencyLevel;
					}
				}

				// Token: 0x17001391 RID: 5009
				// (get) Token: 0x0600722C RID: 29228 RVA: 0x00189509 File Offset: 0x00187709
				public IEnumerable<Task> ScheduledTasks
				{
					get
					{
						return this.m_taskScheduler.m_tasks;
					}
				}

				// Token: 0x17001392 RID: 5010
				// (get) Token: 0x0600722D RID: 29229 RVA: 0x00189516 File Offset: 0x00187716
				public ConcurrentExclusiveSchedulerPair SchedulerPair
				{
					get
					{
						return this.m_taskScheduler.m_pair;
					}
				}

				// Token: 0x04003967 RID: 14695
				private readonly ConcurrentExclusiveSchedulerPair.ConcurrentExclusiveTaskScheduler m_taskScheduler;
			}
		}

		// Token: 0x02000C2A RID: 3114
		private sealed class DebugView
		{
			// Token: 0x0600701F RID: 28703 RVA: 0x00182809 File Offset: 0x00180A09
			public DebugView(ConcurrentExclusiveSchedulerPair pair)
			{
				this.m_pair = pair;
			}

			// Token: 0x17001332 RID: 4914
			// (get) Token: 0x06007020 RID: 28704 RVA: 0x00182818 File Offset: 0x00180A18
			public ConcurrentExclusiveSchedulerPair.ProcessingMode Mode
			{
				get
				{
					return this.m_pair.ModeForDebugger;
				}
			}

			// Token: 0x17001333 RID: 4915
			// (get) Token: 0x06007021 RID: 28705 RVA: 0x00182825 File Offset: 0x00180A25
			public IEnumerable<Task> ScheduledExclusive
			{
				get
				{
					return this.m_pair.m_exclusiveTaskScheduler.m_tasks;
				}
			}

			// Token: 0x17001334 RID: 4916
			// (get) Token: 0x06007022 RID: 28706 RVA: 0x00182837 File Offset: 0x00180A37
			public IEnumerable<Task> ScheduledConcurrent
			{
				get
				{
					return this.m_pair.m_concurrentTaskScheduler.m_tasks;
				}
			}

			// Token: 0x17001335 RID: 4917
			// (get) Token: 0x06007023 RID: 28707 RVA: 0x00182849 File Offset: 0x00180A49
			public int CurrentlyExecutingTaskCount
			{
				get
				{
					if (this.m_pair.m_processingCount != -1)
					{
						return this.m_pair.m_processingCount;
					}
					return 1;
				}
			}

			// Token: 0x17001336 RID: 4918
			// (get) Token: 0x06007024 RID: 28708 RVA: 0x00182866 File Offset: 0x00180A66
			public TaskScheduler TargetScheduler
			{
				get
				{
					return this.m_pair.m_underlyingTaskScheduler;
				}
			}

			// Token: 0x040036F1 RID: 14065
			private readonly ConcurrentExclusiveSchedulerPair m_pair;
		}

		// Token: 0x02000C2B RID: 3115
		[Flags]
		private enum ProcessingMode : byte
		{
			// Token: 0x040036F3 RID: 14067
			NotCurrentlyProcessing = 0,
			// Token: 0x040036F4 RID: 14068
			ProcessingExclusiveTask = 1,
			// Token: 0x040036F5 RID: 14069
			ProcessingConcurrentTasks = 2,
			// Token: 0x040036F6 RID: 14070
			Completing = 4,
			// Token: 0x040036F7 RID: 14071
			Completed = 8
		}

		// Token: 0x02000C2C RID: 3116
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06007025 RID: 28709 RVA: 0x00182873 File Offset: 0x00180A73
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06007026 RID: 28710 RVA: 0x0018287F File Offset: 0x00180A7F
			public <>c()
			{
			}

			// Token: 0x06007027 RID: 28711 RVA: 0x00182887 File Offset: 0x00180A87
			internal ConcurrentExclusiveSchedulerPair.CompletionState <EnsureCompletionStateInitialized>b__22_0()
			{
				return new ConcurrentExclusiveSchedulerPair.CompletionState();
			}

			// Token: 0x06007028 RID: 28712 RVA: 0x00182890 File Offset: 0x00180A90
			internal void <CompleteTaskAsync>b__29_0(object state)
			{
				ConcurrentExclusiveSchedulerPair.CompletionState completionState = (ConcurrentExclusiveSchedulerPair.CompletionState)state;
				List<Exception> exceptions = completionState.m_exceptions;
				if (exceptions == null || exceptions.Count <= 0)
				{
					completionState.TrySetResult(default(VoidTaskResult));
				}
				else
				{
					completionState.TrySetException(exceptions);
				}
			}

			// Token: 0x06007029 RID: 28713 RVA: 0x001828D0 File Offset: 0x00180AD0
			internal void <ProcessAsyncIfNecessary>b__39_0(object thisPair)
			{
				((ConcurrentExclusiveSchedulerPair)thisPair).ProcessExclusiveTasks();
			}

			// Token: 0x0600702A RID: 28714 RVA: 0x001828DD File Offset: 0x00180ADD
			internal void <ProcessAsyncIfNecessary>b__39_1(object thisPair)
			{
				((ConcurrentExclusiveSchedulerPair)thisPair).ProcessConcurrentTasks();
			}

			// Token: 0x040036F8 RID: 14072
			public static readonly ConcurrentExclusiveSchedulerPair.<>c <>9 = new ConcurrentExclusiveSchedulerPair.<>c();

			// Token: 0x040036F9 RID: 14073
			public static Func<ConcurrentExclusiveSchedulerPair.CompletionState> <>9__22_0;

			// Token: 0x040036FA RID: 14074
			public static WaitCallback <>9__29_0;

			// Token: 0x040036FB RID: 14075
			public static Action<object> <>9__39_0;

			// Token: 0x040036FC RID: 14076
			public static Action<object> <>9__39_1;
		}
	}
}
