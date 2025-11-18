using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	// Token: 0x02000576 RID: 1398
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class TaskFactory
	{
		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x060041AA RID: 16810 RVA: 0x000F4FA5 File Offset: 0x000F31A5
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

		// Token: 0x060041AB RID: 16811 RVA: 0x000F4FBB File Offset: 0x000F31BB
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

		// Token: 0x060041AC RID: 16812 RVA: 0x000F4FE8 File Offset: 0x000F31E8
		[__DynamicallyInvokable]
		public TaskFactory()
			: this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x000F5007 File Offset: 0x000F3207
		[__DynamicallyInvokable]
		public TaskFactory(CancellationToken cancellationToken)
			: this(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x000F5014 File Offset: 0x000F3214
		[__DynamicallyInvokable]
		public TaskFactory(TaskScheduler scheduler)
			: this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, scheduler)
		{
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x000F5034 File Offset: 0x000F3234
		[__DynamicallyInvokable]
		public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions)
			: this(default(CancellationToken), creationOptions, continuationOptions, null)
		{
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x000F5053 File Offset: 0x000F3253
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

		// Token: 0x060041B1 RID: 16817 RVA: 0x000F5084 File Offset: 0x000F3284
		internal static void CheckCreationOptions(TaskCreationOptions creationOptions)
		{
			if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060041B2 RID: 16818 RVA: 0x000F5097 File Offset: 0x000F3297
		[__DynamicallyInvokable]
		public CancellationToken CancellationToken
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultCancellationToken;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060041B3 RID: 16819 RVA: 0x000F509F File Offset: 0x000F329F
		[__DynamicallyInvokable]
		public TaskScheduler Scheduler
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultScheduler;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060041B4 RID: 16820 RVA: 0x000F50A7 File Offset: 0x000F32A7
		[__DynamicallyInvokable]
		public TaskCreationOptions CreationOptions
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultCreationOptions;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060041B5 RID: 16821 RVA: 0x000F50AF File Offset: 0x000F32AF
		[__DynamicallyInvokable]
		public TaskContinuationOptions ContinuationOptions
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultContinuationOptions;
			}
		}

		// Token: 0x060041B6 RID: 16822 RVA: 0x000F50B8 File Offset: 0x000F32B8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action action)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, null, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x000F50EC File Offset: 0x000F32EC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action action, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, null, cancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x000F511C File Offset: 0x000F331C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action action, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, null, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), creationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		// Token: 0x060041B9 RID: 16825 RVA: 0x000F514C File Offset: 0x000F334C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), action, null, cancellationToken, scheduler, creationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x000F5170 File Offset: 0x000F3370
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal Task StartNew(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), action, null, cancellationToken, scheduler, creationOptions, internalOptions, ref stackCrawlMark);
		}

		// Token: 0x060041BB RID: 16827 RVA: 0x000F5194 File Offset: 0x000F3394
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action<object> action, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, state, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		// Token: 0x060041BC RID: 16828 RVA: 0x000F51C8 File Offset: 0x000F33C8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action<object> action, object state, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, state, cancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		// Token: 0x060041BD RID: 16829 RVA: 0x000F51F8 File Offset: 0x000F33F8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action<object> action, object state, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, state, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), creationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x000F5228 File Offset: 0x000F3428
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task StartNew(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), action, state, cancellationToken, scheduler, creationOptions, InternalTaskOptions.None, ref stackCrawlMark);
		}

		// Token: 0x060041BF RID: 16831 RVA: 0x000F5250 File Offset: 0x000F3450
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<TResult> function)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x000F5284 File Offset: 0x000F3484
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<TResult> function, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x060041C1 RID: 16833 RVA: 0x000F52B4 File Offset: 0x000F34B4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<TResult> function, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x060041C2 RID: 16834 RVA: 0x000F52E4 File Offset: 0x000F34E4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackCrawlMark);
		}

		// Token: 0x060041C3 RID: 16835 RVA: 0x000F5308 File Offset: 0x000F3508
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x000F533C File Offset: 0x000F353C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x000F536C File Offset: 0x000F356C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x000F539C File Offset: 0x000F359C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, state, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackCrawlMark);
		}

		// Token: 0x060041C7 RID: 16839 RVA: 0x000F53C4 File Offset: 0x000F35C4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.FromAsync(asyncResult, endMethod, this.m_defaultCreationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041C8 RID: 16840 RVA: 0x000F53EC File Offset: 0x000F35EC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.FromAsync(asyncResult, endMethod, creationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041C9 RID: 16841 RVA: 0x000F540C File Offset: 0x000F360C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.FromAsync(asyncResult, endMethod, creationOptions, scheduler, ref stackCrawlMark);
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x000F5428 File Offset: 0x000F3628
		private Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl(asyncResult, null, endMethod, creationOptions, scheduler, ref stackMark);
		}

		// Token: 0x060041CB RID: 16843 RVA: 0x000F5437 File Offset: 0x000F3637
		[__DynamicallyInvokable]
		public Task FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, object state)
		{
			return this.FromAsync(beginMethod, endMethod, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x000F5448 File Offset: 0x000F3648
		[__DynamicallyInvokable]
		public Task FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl(beginMethod, null, endMethod, state, creationOptions);
		}

		// Token: 0x060041CD RID: 16845 RVA: 0x000F5455 File Offset: 0x000F3655
		[__DynamicallyInvokable]
		public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, object state)
		{
			return this.FromAsync<TArg1>(beginMethod, endMethod, arg1, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060041CE RID: 16846 RVA: 0x000F5468 File Offset: 0x000F3668
		[__DynamicallyInvokable]
		public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1>(beginMethod, null, endMethod, arg1, state, creationOptions);
		}

		// Token: 0x060041CF RID: 16847 RVA: 0x000F5477 File Offset: 0x000F3677
		[__DynamicallyInvokable]
		public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
		{
			return this.FromAsync<TArg1, TArg2>(beginMethod, endMethod, arg1, arg2, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060041D0 RID: 16848 RVA: 0x000F548C File Offset: 0x000F368C
		[__DynamicallyInvokable]
		public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, null, endMethod, arg1, arg2, state, creationOptions);
		}

		// Token: 0x060041D1 RID: 16849 RVA: 0x000F549D File Offset: 0x000F369D
		[__DynamicallyInvokable]
		public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
		{
			return this.FromAsync<TArg1, TArg2, TArg3>(beginMethod, endMethod, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060041D2 RID: 16850 RVA: 0x000F54B4 File Offset: 0x000F36B4
		[__DynamicallyInvokable]
		public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, null, endMethod, arg1, arg2, arg3, state, creationOptions);
		}

		// Token: 0x060041D3 RID: 16851 RVA: 0x000F54C8 File Offset: 0x000F36C8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, this.m_defaultCreationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041D4 RID: 16852 RVA: 0x000F54F0 File Offset: 0x000F36F0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x000F5510 File Offset: 0x000F3710
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, scheduler, ref stackCrawlMark);
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x000F552C File Offset: 0x000F372C
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TResult>(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x000F553D File Offset: 0x000F373D
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TResult>(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, creationOptions);
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x000F554A File Offset: 0x000F374A
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TResult>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060041D9 RID: 16857 RVA: 0x000F555D File Offset: 0x000F375D
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TResult>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, creationOptions);
		}

		// Token: 0x060041DA RID: 16858 RVA: 0x000F556C File Offset: 0x000F376C
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TArg2, TResult>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060041DB RID: 16859 RVA: 0x000F5581 File Offset: 0x000F3781
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TArg2, TResult>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, creationOptions);
		}

		// Token: 0x060041DC RID: 16860 RVA: 0x000F5592 File Offset: 0x000F3792
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
		}

		// Token: 0x060041DD RID: 16861 RVA: 0x000F55A9 File Offset: 0x000F37A9
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, creationOptions);
		}

		// Token: 0x060041DE RID: 16862 RVA: 0x000F55BC File Offset: 0x000F37BC
		internal static void CheckFromAsyncOptions(TaskCreationOptions creationOptions, bool hasBeginMethod)
		{
			if (hasBeginMethod)
			{
				if ((creationOptions & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
				{
					throw new ArgumentOutOfRangeException("creationOptions", Environment.GetResourceString("Task_FromAsync_LongRunning"));
				}
				if ((creationOptions & TaskCreationOptions.PreferFairness) != TaskCreationOptions.None)
				{
					throw new ArgumentOutOfRangeException("creationOptions", Environment.GetResourceString("Task_FromAsync_PreferFairness"));
				}
			}
			if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
		}

		// Token: 0x060041DF RID: 16863 RVA: 0x000F5614 File Offset: 0x000F3814
		internal static Task<Task[]> CommonCWAllLogic(Task[] tasksCopy)
		{
			TaskFactory.CompleteOnCountdownPromise completeOnCountdownPromise = new TaskFactory.CompleteOnCountdownPromise(tasksCopy);
			for (int i = 0; i < tasksCopy.Length; i++)
			{
				if (tasksCopy[i].IsCompleted)
				{
					completeOnCountdownPromise.Invoke(tasksCopy[i]);
				}
				else
				{
					tasksCopy[i].AddCompletionAction(completeOnCountdownPromise);
				}
			}
			return completeOnCountdownPromise;
		}

		// Token: 0x060041E0 RID: 16864 RVA: 0x000F5658 File Offset: 0x000F3858
		internal static Task<Task<T>[]> CommonCWAllLogic<T>(Task<T>[] tasksCopy)
		{
			TaskFactory.CompleteOnCountdownPromise<T> completeOnCountdownPromise = new TaskFactory.CompleteOnCountdownPromise<T>(tasksCopy);
			for (int i = 0; i < tasksCopy.Length; i++)
			{
				if (tasksCopy[i].IsCompleted)
				{
					completeOnCountdownPromise.Invoke(tasksCopy[i]);
				}
				else
				{
					tasksCopy[i].AddCompletionAction(completeOnCountdownPromise);
				}
			}
			return completeOnCountdownPromise;
		}

		// Token: 0x060041E1 RID: 16865 RVA: 0x000F569C File Offset: 0x000F389C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041E2 RID: 16866 RVA: 0x000F56D8 File Offset: 0x000F38D8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041E3 RID: 16867 RVA: 0x000F570C File Offset: 0x000F390C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041E4 RID: 16868 RVA: 0x000F5740 File Offset: 0x000F3940
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x000F576C File Offset: 0x000F396C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041E6 RID: 16870 RVA: 0x000F57A8 File Offset: 0x000F39A8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x000F57DC File Offset: 0x000F39DC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x000F5810 File Offset: 0x000F3A10
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x000F583C File Offset: 0x000F3A3C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x000F5878 File Offset: 0x000F3A78
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041EB RID: 16875 RVA: 0x000F58AC File Offset: 0x000F3AAC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x000F58E0 File Offset: 0x000F3AE0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x000F590C File Offset: 0x000F3B0C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x000F5948 File Offset: 0x000F3B48
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x000F597C File Offset: 0x000F3B7C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x000F59B0 File Offset: 0x000F3BB0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x000F59DC File Offset: 0x000F3BDC
		internal static Task<Task> CommonCWAnyLogic(IList<Task> tasks)
		{
			TaskFactory.CompleteOnInvokePromise completeOnInvokePromise = new TaskFactory.CompleteOnInvokePromise(tasks);
			bool flag = false;
			int count = tasks.Count;
			for (int i = 0; i < count; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
				}
				if (!flag)
				{
					if (completeOnInvokePromise.IsCompleted)
					{
						flag = true;
					}
					else if (task.IsCompleted)
					{
						completeOnInvokePromise.Invoke(task);
						flag = true;
					}
					else
					{
						task.AddCompletionAction(completeOnInvokePromise);
						if (completeOnInvokePromise.IsCompleted)
						{
							task.RemoveContinuation(completeOnInvokePromise);
						}
					}
				}
			}
			return completeOnInvokePromise;
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x000F5A64 File Offset: 0x000F3C64
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x000F5AA0 File Offset: 0x000F3CA0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x000F5AD4 File Offset: 0x000F3CD4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x000F5B08 File Offset: 0x000F3D08
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x000F5B34 File Offset: 0x000F3D34
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x000F5B70 File Offset: 0x000F3D70
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x000F5BA4 File Offset: 0x000F3DA4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041F9 RID: 16889 RVA: 0x000F5BD8 File Offset: 0x000F3DD8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x060041FA RID: 16890 RVA: 0x000F5C04 File Offset: 0x000F3E04
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x000F5C40 File Offset: 0x000F3E40
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x000F5C74 File Offset: 0x000F3E74
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x000F5CA8 File Offset: 0x000F3EA8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x000F5CD4 File Offset: 0x000F3ED4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, null, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x000F5D10 File Offset: 0x000F3F10
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, null, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x000F5D44 File Offset: 0x000F3F44
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, null, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06004201 RID: 16897 RVA: 0x000F5D78 File Offset: 0x000F3F78
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, null, continuationAction, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06004202 RID: 16898 RVA: 0x000F5DA4 File Offset: 0x000F3FA4
		internal static Task[] CheckMultiContinuationTasksAndCopy(Task[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), "tasks");
			}
			Task[] array = new Task[tasks.Length];
			for (int i = 0; i < tasks.Length; i++)
			{
				array[i] = tasks[i];
				if (array[i] == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
				}
			}
			return array;
		}

		// Token: 0x06004203 RID: 16899 RVA: 0x000F5E10 File Offset: 0x000F4010
		internal static Task<TResult>[] CheckMultiContinuationTasksAndCopy<TResult>(Task<TResult>[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), "tasks");
			}
			Task<TResult>[] array = new Task<TResult>[tasks.Length];
			for (int i = 0; i < tasks.Length; i++)
			{
				array[i] = tasks[i];
				if (array[i] == null)
				{
					throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_NullTask"), "tasks");
				}
			}
			return array;
		}

		// Token: 0x06004204 RID: 16900 RVA: 0x000F5E7C File Offset: 0x000F407C
		internal static void CheckMultiTaskContinuationOptions(TaskContinuationOptions continuationOptions)
		{
			if ((continuationOptions & (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously)) == (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously))
			{
				throw new ArgumentOutOfRangeException("continuationOptions", Environment.GetResourceString("Task_ContinueWith_ESandLR"));
			}
			if ((continuationOptions & ~(TaskContinuationOptions.PreferFairness | TaskContinuationOptions.LongRunning | TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.HideScheduler | TaskContinuationOptions.LazyCancellation | TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously)) != TaskContinuationOptions.None)
			{
				throw new ArgumentOutOfRangeException("continuationOptions");
			}
			if ((continuationOptions & (TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled)) != TaskContinuationOptions.None)
			{
				throw new ArgumentOutOfRangeException("continuationOptions", Environment.GetResourceString("Task_MultiTaskContinuation_FireOptions"));
			}
		}

		// Token: 0x04001B6A RID: 7018
		private CancellationToken m_defaultCancellationToken;

		// Token: 0x04001B6B RID: 7019
		private TaskScheduler m_defaultScheduler;

		// Token: 0x04001B6C RID: 7020
		private TaskCreationOptions m_defaultCreationOptions;

		// Token: 0x04001B6D RID: 7021
		private TaskContinuationOptions m_defaultContinuationOptions;

		// Token: 0x02000C22 RID: 3106
		private sealed class CompleteOnCountdownPromise : Task<Task[]>, ITaskCompletionAction
		{
			// Token: 0x06007000 RID: 28672 RVA: 0x001821E1 File Offset: 0x001803E1
			internal CompleteOnCountdownPromise(Task[] tasksCopy)
			{
				this._tasks = tasksCopy;
				this._count = tasksCopy.Length;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "TaskFactory.ContinueWhenAll", 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
			}

			// Token: 0x06007001 RID: 28673 RVA: 0x00182224 File Offset: 0x00180424
			public void Invoke(Task completingTask)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, base.Id, CausalityRelation.Join);
				}
				if (completingTask.IsWaitNotificationEnabled)
				{
					base.SetNotificationForWaitCompletion(true);
				}
				if (Interlocked.Decrement(ref this._count) == 0)
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(base.Id);
					}
					base.TrySetResult(this._tasks);
				}
			}

			// Token: 0x1700132A RID: 4906
			// (get) Token: 0x06007002 RID: 28674 RVA: 0x00182294 File Offset: 0x00180494
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					return base.ShouldNotifyDebuggerOfWaitCompletion && Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(this._tasks);
				}
			}

			// Token: 0x040036D9 RID: 14041
			private readonly Task[] _tasks;

			// Token: 0x040036DA RID: 14042
			private int _count;
		}

		// Token: 0x02000C23 RID: 3107
		private sealed class CompleteOnCountdownPromise<T> : Task<Task<T>[]>, ITaskCompletionAction
		{
			// Token: 0x06007003 RID: 28675 RVA: 0x001822AB File Offset: 0x001804AB
			internal CompleteOnCountdownPromise(Task<T>[] tasksCopy)
			{
				this._tasks = tasksCopy;
				this._count = tasksCopy.Length;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "TaskFactory.ContinueWhenAll<>", 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
			}

			// Token: 0x06007004 RID: 28676 RVA: 0x001822EC File Offset: 0x001804EC
			public void Invoke(Task completingTask)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, base.Id, CausalityRelation.Join);
				}
				if (completingTask.IsWaitNotificationEnabled)
				{
					base.SetNotificationForWaitCompletion(true);
				}
				if (Interlocked.Decrement(ref this._count) == 0)
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(base.Id);
					}
					base.TrySetResult(this._tasks);
				}
			}

			// Token: 0x1700132B RID: 4907
			// (get) Token: 0x06007005 RID: 28677 RVA: 0x0018235C File Offset: 0x0018055C
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					if (base.ShouldNotifyDebuggerOfWaitCompletion)
					{
						Task[] tasks = this._tasks;
						return Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(tasks);
					}
					return false;
				}
			}

			// Token: 0x040036DB RID: 14043
			private readonly Task<T>[] _tasks;

			// Token: 0x040036DC RID: 14044
			private int _count;
		}

		// Token: 0x02000C24 RID: 3108
		internal sealed class CompleteOnInvokePromise : Task<Task>, ITaskCompletionAction
		{
			// Token: 0x06007006 RID: 28678 RVA: 0x00182380 File Offset: 0x00180580
			public CompleteOnInvokePromise(IList<Task> tasks)
			{
				this._tasks = tasks;
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, base.Id, "TaskFactory.ContinueWhenAny", 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(this);
				}
			}

			// Token: 0x06007007 RID: 28679 RVA: 0x001823B8 File Offset: 0x001805B8
			public void Invoke(Task completingTask)
			{
				if (Interlocked.CompareExchange(ref this.m_firstTaskAlreadyCompleted, 1, 0) == 0)
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationRelation(CausalityTraceLevel.Important, base.Id, CausalityRelation.Choice);
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, base.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(base.Id);
					}
					bool flag = base.TrySetResult(completingTask);
					IList<Task> tasks = this._tasks;
					int count = tasks.Count;
					for (int i = 0; i < count; i++)
					{
						Task task = tasks[i];
						if (task != null && !task.IsCompleted)
						{
							task.RemoveContinuation(this);
						}
					}
					this._tasks = null;
				}
			}

			// Token: 0x040036DD RID: 14045
			private IList<Task> _tasks;

			// Token: 0x040036DE RID: 14046
			private int m_firstTaskAlreadyCompleted;
		}
	}
}
