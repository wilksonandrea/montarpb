using System;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	// Token: 0x0200054E RID: 1358
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class TaskFactory<TResult>
	{
		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06003FD5 RID: 16341 RVA: 0x000ECE81 File Offset: 0x000EB081
		private TaskScheduler DefaultScheduler
		{
			get
			{
				if (this.m_defaultScheduler == null)
				{
					return TaskScheduler.Current;
				}
				return this.m_defaultScheduler;
			}
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x000ECE97 File Offset: 0x000EB097
		private TaskScheduler GetDefaultScheduler(Task currTask)
		{
			if (this.m_defaultScheduler != null)
			{
				return this.m_defaultScheduler;
			}
			if (currTask != null && (currTask.CreationOptions & TaskCreationOptions.HideScheduler) == TaskCreationOptions.None)
			{
				return currTask.ExecutingTaskScheduler;
			}
			return TaskScheduler.Default;
		}

		// Token: 0x06003FD7 RID: 16343 RVA: 0x000ECEC4 File Offset: 0x000EB0C4
		[__DynamicallyInvokable]
		public TaskFactory()
			: this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x000ECEE3 File Offset: 0x000EB0E3
		[__DynamicallyInvokable]
		public TaskFactory(CancellationToken cancellationToken)
			: this(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x000ECEF0 File Offset: 0x000EB0F0
		[__DynamicallyInvokable]
		public TaskFactory(TaskScheduler scheduler)
			: this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, scheduler)
		{
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x000ECF10 File Offset: 0x000EB110
		[__DynamicallyInvokable]
		public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions)
			: this(default(CancellationToken), creationOptions, continuationOptions, null)
		{
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x000ECF2F File Offset: 0x000EB12F
		[__DynamicallyInvokable]
		public TaskFactory(CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			TaskFactory.CheckCreationOptions(creationOptions);
			this.m_defaultCancellationToken = cancellationToken;
			this.m_defaultScheduler = scheduler;
			this.m_defaultCreationOptions = creationOptions;
			this.m_defaultContinuationOptions = continuationOptions;
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06003FDC RID: 16348 RVA: 0x000ECF60 File Offset: 0x000EB160
		[__DynamicallyInvokable]
		public CancellationToken CancellationToken
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultCancellationToken;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06003FDD RID: 16349 RVA: 0x000ECF68 File Offset: 0x000EB168
		[__DynamicallyInvokable]
		public TaskScheduler Scheduler
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultScheduler;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06003FDE RID: 16350 RVA: 0x000ECF70 File Offset: 0x000EB170
		[__DynamicallyInvokable]
		public TaskCreationOptions CreationOptions
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultCreationOptions;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06003FDF RID: 16351 RVA: 0x000ECF78 File Offset: 0x000EB178
		[__DynamicallyInvokable]
		public TaskContinuationOptions ContinuationOptions
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultContinuationOptions;
			}
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x000ECF80 File Offset: 0x000EB180
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<TResult> function)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x000ECFB4 File Offset: 0x000EB1B4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x000ECFE4 File Offset: 0x000EB1E4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<TResult> function, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x06003FE3 RID: 16355 RVA: 0x000ED014 File Offset: 0x000EB214
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x000ED038 File Offset: 0x000EB238
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<object, TResult> function, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x000ED06C File Offset: 0x000EB26C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x06003FE6 RID: 16358 RVA: 0x000ED09C File Offset: 0x000EB29C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x000ED0CC File Offset: 0x000EB2CC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, state, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x000ED0F4 File Offset: 0x000EB2F4
		private static void FromAsyncCoreLogic(IAsyncResult iar, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, Task<TResult> promise, bool requiresSynchronization)
		{
			Exception ex = null;
			OperationCanceledException ex2 = null;
			TResult tresult = default(TResult);
			try
			{
				if (endFunction != null)
				{
					tresult = endFunction(iar);
				}
				else
				{
					endAction(iar);
				}
			}
			catch (OperationCanceledException ex3)
			{
				ex2 = ex3;
			}
			catch (Exception ex4)
			{
				ex = ex4;
			}
			finally
			{
				if (ex2 != null)
				{
					promise.TrySetCanceled(ex2.CancellationToken, ex2);
				}
				else if (ex != null)
				{
					bool flag = promise.TrySetException(ex);
					if (flag && ex is ThreadAbortException)
					{
						promise.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
					}
				}
				else
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(promise.Id);
					}
					if (requiresSynchronization)
					{
						promise.TrySetResult(tresult);
					}
					else
					{
						promise.DangerousSetResult(tresult);
					}
				}
			}
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x000ED1DC File Offset: 0x000EB3DC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, this.m_defaultCreationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003FEA RID: 16362 RVA: 0x000ED204 File Offset: 0x000EB404
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003FEB RID: 16363 RVA: 0x000ED224 File Offset: 0x000EB424
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06003FEC RID: 16364 RVA: 0x000ED240 File Offset: 0x000EB440
		internal static Task<TResult> FromAsyncImpl(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TaskCreationOptions creationOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, false);
			Task<TResult> promise = new Task<TResult>(null, creationOptions);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync", 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			Task t = new Task(delegate
			{
				TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, true);
			}, null, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null, ref stackMark);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Verbose, t.Id, "TaskFactory.FromAsync Callback", 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(t);
			}
			if (asyncResult.IsCompleted)
			{
				try
				{
					t.InternalRunSynchronously(scheduler, false);
					goto IL_154;
				}
				catch (Exception ex)
				{
					promise.TrySetException(ex);
					goto IL_154;
				}
			}
			ThreadPool.RegisterWaitForSingleObject(asyncResult.AsyncWaitHandle, delegate
			{
				try
				{
					t.InternalRunSynchronously(scheduler, false);
				}
				catch (Exception ex2)
				{
					promise.TrySetException(ex2);
				}
			}, null, -1, true);
			IL_154:
			return promise;
		}

		// Token: 0x06003FED RID: 16365 RVA: 0x000ED3B8 File Offset: 0x000EB5B8
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, this.m_defaultCreationOptions);
		}

		// Token: 0x06003FEE RID: 16366 RVA: 0x000ED3C9 File Offset: 0x000EB5C9
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, creationOptions);
		}

		// Token: 0x06003FEF RID: 16367 RVA: 0x000ED3D8 File Offset: 0x000EB5D8
		internal static Task<TResult> FromAsyncImpl(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			try
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					IAsyncResult asyncResult = beginMethod(delegate(IAsyncResult iar)
					{
						if (!iar.CompletedSynchronously)
						{
							TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
						}
					}, state);
					if (asyncResult.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
					}
				}
				else
				{
					IAsyncResult asyncResult2 = beginMethod(delegate(IAsyncResult iar)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}, state);
				}
			}
			catch
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(promise.Id);
				}
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x000ED530 File Offset: 0x000EB730
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, this.m_defaultCreationOptions);
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x000ED543 File Offset: 0x000EB743
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, creationOptions);
		}

		// Token: 0x06003FF2 RID: 16370 RVA: 0x000ED554 File Offset: 0x000EB754
		internal static Task<TResult> FromAsyncImpl<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endFunction");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			try
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					IAsyncResult asyncResult = beginMethod(arg1, delegate(IAsyncResult iar)
					{
						if (!iar.CompletedSynchronously)
						{
							TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
						}
					}, state);
					if (asyncResult.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
					}
				}
				else
				{
					IAsyncResult asyncResult2 = beginMethod(arg1, delegate(IAsyncResult iar)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}, state);
				}
			}
			catch
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(promise.Id);
				}
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x000ED6B0 File Offset: 0x000EB8B0
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, this.m_defaultCreationOptions);
		}

		// Token: 0x06003FF4 RID: 16372 RVA: 0x000ED6C5 File Offset: 0x000EB8C5
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, creationOptions);
		}

		// Token: 0x06003FF5 RID: 16373 RVA: 0x000ED6D8 File Offset: 0x000EB8D8
		internal static Task<TResult> FromAsyncImpl<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			try
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					IAsyncResult asyncResult = beginMethod(arg1, arg2, delegate(IAsyncResult iar)
					{
						if (!iar.CompletedSynchronously)
						{
							TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
						}
					}, state);
					if (asyncResult.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
					}
				}
				else
				{
					IAsyncResult asyncResult2 = beginMethod(arg1, arg2, delegate(IAsyncResult iar)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}, state);
				}
			}
			catch
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(promise.Id);
				}
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		// Token: 0x06003FF6 RID: 16374 RVA: 0x000ED838 File Offset: 0x000EBA38
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
		}

		// Token: 0x06003FF7 RID: 16375 RVA: 0x000ED84F File Offset: 0x000EBA4F
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, creationOptions);
		}

		// Token: 0x06003FF8 RID: 16376 RVA: 0x000ED864 File Offset: 0x000EBA64
		internal static Task<TResult> FromAsyncImpl<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			try
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					IAsyncResult asyncResult = beginMethod(arg1, arg2, arg3, delegate(IAsyncResult iar)
					{
						if (!iar.CompletedSynchronously)
						{
							TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
						}
					}, state);
					if (asyncResult.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
					}
				}
				else
				{
					IAsyncResult asyncResult2 = beginMethod(arg1, arg2, arg3, delegate(IAsyncResult iar)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}, state);
				}
			}
			catch
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(promise.Id);
				}
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		// Token: 0x06003FF9 RID: 16377 RVA: 0x000ED9C8 File Offset: 0x000EBBC8
		internal static Task<TResult> FromAsyncTrim<TInstance, TArgs>(TInstance thisRef, TArgs args, Func<TInstance, TArgs, AsyncCallback, object, IAsyncResult> beginMethod, Func<TInstance, IAsyncResult, TResult> endMethod) where TInstance : class
		{
			TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> fromAsyncTrimPromise = new TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>(thisRef, endMethod);
			IAsyncResult asyncResult = beginMethod(thisRef, args, TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.s_completeFromAsyncResult, fromAsyncTrimPromise);
			if (asyncResult.CompletedSynchronously)
			{
				fromAsyncTrimPromise.Complete(thisRef, endMethod, asyncResult, false);
			}
			return fromAsyncTrimPromise;
		}

		// Token: 0x06003FFA RID: 16378 RVA: 0x000EDA00 File Offset: 0x000EBC00
		private static Task<TResult> CreateCanceledTask(TaskContinuationOptions continuationOptions, CancellationToken ct)
		{
			TaskCreationOptions taskCreationOptions;
			InternalTaskOptions internalTaskOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out taskCreationOptions, out internalTaskOptions);
			return new Task<TResult>(true, default(TResult), taskCreationOptions, ct);
		}

		// Token: 0x06003FFB RID: 16379 RVA: 0x000EDA28 File Offset: 0x000EBC28
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003FFC RID: 16380 RVA: 0x000EDA64 File Offset: 0x000EBC64
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003FFD RID: 16381 RVA: 0x000EDA98 File Offset: 0x000EBC98
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003FFE RID: 16382 RVA: 0x000EDACC File Offset: 0x000EBCCC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06003FFF RID: 16383 RVA: 0x000EDAF8 File Offset: 0x000EBCF8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06004000 RID: 16384 RVA: 0x000EDB34 File Offset: 0x000EBD34
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06004001 RID: 16385 RVA: 0x000EDB68 File Offset: 0x000EBD68
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x000EDB9C File Offset: 0x000EBD9C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06004003 RID: 16387 RVA: 0x000EDBC8 File Offset: 0x000EBDC8
		internal static Task<TResult> ContinueWhenAllImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, Action<Task<TAntecedentResult>[]> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<TAntecedentResult>[] array = TaskFactory.CheckMultiContinuationTasksAndCopy<TAntecedentResult>(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			Task<Task<TAntecedentResult>[]> task = TaskFactory.CommonCWAllLogic<TAntecedentResult>(array);
			if (continuationFunction != null)
			{
				return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAllFuncDelegate, continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
			}
			return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAllActionDelegate, continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
		}

		// Token: 0x06004004 RID: 16388 RVA: 0x000EDC48 File Offset: 0x000EBE48
		internal static Task<TResult> ContinueWhenAllImpl(Task[] tasks, Func<Task[], TResult> continuationFunction, Action<Task[]> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task[] array = TaskFactory.CheckMultiContinuationTasksAndCopy(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			Task<Task[]> task = TaskFactory.CommonCWAllLogic(array);
			if (continuationFunction != null)
			{
				return task.ContinueWith<TResult>(delegate(Task<Task[]> completedTasks, object state)
				{
					completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
					return ((Func<Task[], TResult>)state)(completedTasks.Result);
				}, continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
			}
			return task.ContinueWith<TResult>(delegate(Task<Task[]> completedTasks, object state)
			{
				completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
				((Action<Task[]>)state)(completedTasks.Result);
				return default(TResult);
			}, continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
		}

		// Token: 0x06004005 RID: 16389 RVA: 0x000EDCFC File Offset: 0x000EBEFC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06004006 RID: 16390 RVA: 0x000EDD38 File Offset: 0x000EBF38
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06004007 RID: 16391 RVA: 0x000EDD6C File Offset: 0x000EBF6C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x000EDDA0 File Offset: 0x000EBFA0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x000EDDCC File Offset: 0x000EBFCC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x0600400A RID: 16394 RVA: 0x000EDE08 File Offset: 0x000EC008
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x0600400B RID: 16395 RVA: 0x000EDE3C File Offset: 0x000EC03C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x0600400C RID: 16396 RVA: 0x000EDE70 File Offset: 0x000EC070
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x000EDE9C File Offset: 0x000EC09C
		internal static Task<TResult> ContinueWhenAnyImpl(Task[] tasks, Func<Task, TResult> continuationFunction, Action<Task> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), "tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<Task> task = TaskFactory.CommonCWAnyLogic(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			if (continuationFunction != null)
			{
				return task.ContinueWith<TResult>((Task<Task> completedTask, object state) => ((Func<Task, TResult>)state)(completedTask.Result), continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
			}
			return task.ContinueWith<TResult>(delegate(Task<Task> completedTask, object state)
			{
				((Action<Task>)state)(completedTask.Result);
				return default(TResult);
			}, continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x000EDF64 File Offset: 0x000EC164
		internal static Task<TResult> ContinueWhenAnyImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, Action<Task<TAntecedentResult>> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), "tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<Task> task = TaskFactory.CommonCWAnyLogic(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			if (continuationFunction != null)
			{
				return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAnyFuncDelegate, continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
			}
			return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAnyActionDelegate, continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
		}

		// Token: 0x04001AC3 RID: 6851
		private CancellationToken m_defaultCancellationToken;

		// Token: 0x04001AC4 RID: 6852
		private TaskScheduler m_defaultScheduler;

		// Token: 0x04001AC5 RID: 6853
		private TaskCreationOptions m_defaultCreationOptions;

		// Token: 0x04001AC6 RID: 6854
		private TaskContinuationOptions m_defaultContinuationOptions;

		// Token: 0x02000C05 RID: 3077
		private sealed class FromAsyncTrimPromise<TInstance> : Task<TResult> where TInstance : class
		{
			// Token: 0x06006F9E RID: 28574 RVA: 0x00180A4B File Offset: 0x0017EC4B
			internal FromAsyncTrimPromise(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod)
			{
				this.m_thisRef = thisRef;
				this.m_endMethod = endMethod;
			}

			// Token: 0x06006F9F RID: 28575 RVA: 0x00180A64 File Offset: 0x0017EC64
			internal static void CompleteFromAsyncResult(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> fromAsyncTrimPromise = asyncResult.AsyncState as TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>;
				if (fromAsyncTrimPromise == null)
				{
					throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndCalledMultiple"), "asyncResult");
				}
				TInstance thisRef = fromAsyncTrimPromise.m_thisRef;
				Func<TInstance, IAsyncResult, TResult> endMethod = fromAsyncTrimPromise.m_endMethod;
				fromAsyncTrimPromise.m_thisRef = default(TInstance);
				fromAsyncTrimPromise.m_endMethod = null;
				if (endMethod == null)
				{
					throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndCalledMultiple"), "asyncResult");
				}
				if (!asyncResult.CompletedSynchronously)
				{
					fromAsyncTrimPromise.Complete(thisRef, endMethod, asyncResult, true);
				}
			}

			// Token: 0x06006FA0 RID: 28576 RVA: 0x00180AF0 File Offset: 0x0017ECF0
			internal void Complete(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod, IAsyncResult asyncResult, bool requiresSynchronization)
			{
				try
				{
					TResult tresult = endMethod(thisRef, asyncResult);
					if (requiresSynchronization)
					{
						bool flag = base.TrySetResult(tresult);
					}
					else
					{
						base.DangerousSetResult(tresult);
					}
				}
				catch (OperationCanceledException ex)
				{
					bool flag = base.TrySetCanceled(ex.CancellationToken, ex);
				}
				catch (Exception ex2)
				{
					bool flag = base.TrySetException(ex2);
				}
			}

			// Token: 0x06006FA1 RID: 28577 RVA: 0x00180B5C File Offset: 0x0017ED5C
			// Note: this type is marked as 'beforefieldinit'.
			static FromAsyncTrimPromise()
			{
			}

			// Token: 0x04003662 RID: 13922
			internal static readonly AsyncCallback s_completeFromAsyncResult = new AsyncCallback(TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.CompleteFromAsyncResult);

			// Token: 0x04003663 RID: 13923
			private TInstance m_thisRef;

			// Token: 0x04003664 RID: 13924
			private Func<TInstance, IAsyncResult, TResult> m_endMethod;
		}

		// Token: 0x02000C06 RID: 3078
		[CompilerGenerated]
		private sealed class <>c__DisplayClass32_0
		{
			// Token: 0x06006FA2 RID: 28578 RVA: 0x00180B6F File Offset: 0x0017ED6F
			public <>c__DisplayClass32_0()
			{
			}

			// Token: 0x06006FA3 RID: 28579 RVA: 0x00180B77 File Offset: 0x0017ED77
			internal void <FromAsyncImpl>b__0(object <p0>)
			{
				TaskFactory<TResult>.FromAsyncCoreLogic(this.asyncResult, this.endFunction, this.endAction, this.promise, true);
			}

			// Token: 0x06006FA4 RID: 28580 RVA: 0x00180B98 File Offset: 0x0017ED98
			internal void <FromAsyncImpl>b__1(object <p0>, bool <p1>)
			{
				try
				{
					this.t.InternalRunSynchronously(this.scheduler, false);
				}
				catch (Exception ex)
				{
					this.promise.TrySetException(ex);
				}
			}

			// Token: 0x04003665 RID: 13925
			public IAsyncResult asyncResult;

			// Token: 0x04003666 RID: 13926
			public Func<IAsyncResult, TResult> endFunction;

			// Token: 0x04003667 RID: 13927
			public Action<IAsyncResult> endAction;

			// Token: 0x04003668 RID: 13928
			public Task<TResult> promise;

			// Token: 0x04003669 RID: 13929
			public Task t;

			// Token: 0x0400366A RID: 13930
			public TaskScheduler scheduler;
		}

		// Token: 0x02000C07 RID: 3079
		[CompilerGenerated]
		private sealed class <>c__DisplayClass35_0
		{
			// Token: 0x06006FA5 RID: 28581 RVA: 0x00180BDC File Offset: 0x0017EDDC
			public <>c__DisplayClass35_0()
			{
			}

			// Token: 0x06006FA6 RID: 28582 RVA: 0x00180BE4 File Offset: 0x0017EDE4
			internal void <FromAsyncImpl>b__0(IAsyncResult iar)
			{
				if (!iar.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(iar, this.endFunction, this.endAction, this.promise, true);
				}
			}

			// Token: 0x06006FA7 RID: 28583 RVA: 0x00180C07 File Offset: 0x0017EE07
			internal void <FromAsyncImpl>b__1(IAsyncResult iar)
			{
				TaskFactory<TResult>.FromAsyncCoreLogic(iar, this.endFunction, this.endAction, this.promise, true);
			}

			// Token: 0x0400366B RID: 13931
			public Func<IAsyncResult, TResult> endFunction;

			// Token: 0x0400366C RID: 13932
			public Action<IAsyncResult> endAction;

			// Token: 0x0400366D RID: 13933
			public Task<TResult> promise;
		}

		// Token: 0x02000C08 RID: 3080
		[CompilerGenerated]
		private sealed class <>c__DisplayClass38_0<TArg1>
		{
			// Token: 0x06006FA8 RID: 28584 RVA: 0x00180C22 File Offset: 0x0017EE22
			public <>c__DisplayClass38_0()
			{
			}

			// Token: 0x06006FA9 RID: 28585 RVA: 0x00180C2A File Offset: 0x0017EE2A
			internal void <FromAsyncImpl>b__0(IAsyncResult iar)
			{
				if (!iar.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(iar, this.endFunction, this.endAction, this.promise, true);
				}
			}

			// Token: 0x06006FAA RID: 28586 RVA: 0x00180C4D File Offset: 0x0017EE4D
			internal void <FromAsyncImpl>b__1(IAsyncResult iar)
			{
				TaskFactory<TResult>.FromAsyncCoreLogic(iar, this.endFunction, this.endAction, this.promise, true);
			}

			// Token: 0x0400366E RID: 13934
			public Func<IAsyncResult, TResult> endFunction;

			// Token: 0x0400366F RID: 13935
			public Action<IAsyncResult> endAction;

			// Token: 0x04003670 RID: 13936
			public Task<TResult> promise;
		}

		// Token: 0x02000C09 RID: 3081
		[CompilerGenerated]
		private sealed class <>c__DisplayClass41_0<TArg1, TArg2>
		{
			// Token: 0x06006FAB RID: 28587 RVA: 0x00180C68 File Offset: 0x0017EE68
			public <>c__DisplayClass41_0()
			{
			}

			// Token: 0x06006FAC RID: 28588 RVA: 0x00180C70 File Offset: 0x0017EE70
			internal void <FromAsyncImpl>b__0(IAsyncResult iar)
			{
				if (!iar.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(iar, this.endFunction, this.endAction, this.promise, true);
				}
			}

			// Token: 0x06006FAD RID: 28589 RVA: 0x00180C93 File Offset: 0x0017EE93
			internal void <FromAsyncImpl>b__1(IAsyncResult iar)
			{
				TaskFactory<TResult>.FromAsyncCoreLogic(iar, this.endFunction, this.endAction, this.promise, true);
			}

			// Token: 0x04003671 RID: 13937
			public Func<IAsyncResult, TResult> endFunction;

			// Token: 0x04003672 RID: 13938
			public Action<IAsyncResult> endAction;

			// Token: 0x04003673 RID: 13939
			public Task<TResult> promise;
		}

		// Token: 0x02000C0A RID: 3082
		[CompilerGenerated]
		private sealed class <>c__DisplayClass44_0<TArg1, TArg2, TArg3>
		{
			// Token: 0x06006FAE RID: 28590 RVA: 0x00180CAE File Offset: 0x0017EEAE
			public <>c__DisplayClass44_0()
			{
			}

			// Token: 0x06006FAF RID: 28591 RVA: 0x00180CB6 File Offset: 0x0017EEB6
			internal void <FromAsyncImpl>b__0(IAsyncResult iar)
			{
				if (!iar.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(iar, this.endFunction, this.endAction, this.promise, true);
				}
			}

			// Token: 0x06006FB0 RID: 28592 RVA: 0x00180CD9 File Offset: 0x0017EED9
			internal void <FromAsyncImpl>b__1(IAsyncResult iar)
			{
				TaskFactory<TResult>.FromAsyncCoreLogic(iar, this.endFunction, this.endAction, this.promise, true);
			}

			// Token: 0x04003674 RID: 13940
			public Func<IAsyncResult, TResult> endFunction;

			// Token: 0x04003675 RID: 13941
			public Action<IAsyncResult> endAction;

			// Token: 0x04003676 RID: 13942
			public Task<TResult> promise;
		}

		// Token: 0x02000C0B RID: 3083
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006FB1 RID: 28593 RVA: 0x00180CF4 File Offset: 0x0017EEF4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006FB2 RID: 28594 RVA: 0x00180D00 File Offset: 0x0017EF00
			public <>c()
			{
			}

			// Token: 0x06006FB3 RID: 28595 RVA: 0x00180D08 File Offset: 0x0017EF08
			internal TResult <ContinueWhenAllImpl>b__57_0(Task<Task[]> completedTasks, object state)
			{
				completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
				return ((Func<Task[], TResult>)state)(completedTasks.Result);
			}

			// Token: 0x06006FB4 RID: 28596 RVA: 0x00180D24 File Offset: 0x0017EF24
			internal TResult <ContinueWhenAllImpl>b__57_1(Task<Task[]> completedTasks, object state)
			{
				completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
				((Action<Task[]>)state)(completedTasks.Result);
				return default(TResult);
			}

			// Token: 0x06006FB5 RID: 28597 RVA: 0x00180D52 File Offset: 0x0017EF52
			internal TResult <ContinueWhenAnyImpl>b__66_0(Task<Task> completedTask, object state)
			{
				return ((Func<Task, TResult>)state)(completedTask.Result);
			}

			// Token: 0x06006FB6 RID: 28598 RVA: 0x00180D68 File Offset: 0x0017EF68
			internal TResult <ContinueWhenAnyImpl>b__66_1(Task<Task> completedTask, object state)
			{
				((Action<Task>)state)(completedTask.Result);
				return default(TResult);
			}

			// Token: 0x04003677 RID: 13943
			public static readonly TaskFactory<TResult>.<>c <>9 = new TaskFactory<TResult>.<>c();

			// Token: 0x04003678 RID: 13944
			public static Func<Task<Task[]>, object, TResult> <>9__57_0;

			// Token: 0x04003679 RID: 13945
			public static Func<Task<Task[]>, object, TResult> <>9__57_1;

			// Token: 0x0400367A RID: 13946
			public static Func<Task<Task>, object, TResult> <>9__66_0;

			// Token: 0x0400367B RID: 13947
			public static Func<Task<Task>, object, TResult> <>9__66_1;
		}
	}
}
