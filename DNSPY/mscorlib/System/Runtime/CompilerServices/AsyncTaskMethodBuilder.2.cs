using System;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008F2 RID: 2290
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct AsyncTaskMethodBuilder<TResult>
	{
		// Token: 0x06005E26 RID: 24102 RVA: 0x0014A7F4 File Offset: 0x001489F4
		[__DynamicallyInvokable]
		public static AsyncTaskMethodBuilder<TResult> Create()
		{
			return default(AsyncTaskMethodBuilder<TResult>);
		}

		// Token: 0x06005E27 RID: 24103 RVA: 0x0014A80C File Offset: 0x00148A0C
		[SecuritySafeCritical]
		[DebuggerStepThrough]
		[__DynamicallyInvokable]
		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			if (stateMachine == null)
			{
				throw new ArgumentNullException("stateMachine");
			}
			ExecutionContextSwitcher executionContextSwitcher = default(ExecutionContextSwitcher);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				ExecutionContext.EstablishCopyOnWriteScope(ref executionContextSwitcher);
				stateMachine.MoveNext();
			}
			finally
			{
				executionContextSwitcher.Undo();
			}
		}

		// Token: 0x06005E28 RID: 24104 RVA: 0x0014A86C File Offset: 0x00148A6C
		[__DynamicallyInvokable]
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.m_coreState.SetStateMachine(stateMachine);
		}

		// Token: 0x06005E29 RID: 24105 RVA: 0x0014A87C File Offset: 0x00148A7C
		[__DynamicallyInvokable]
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			try
			{
				AsyncMethodBuilderCore.MoveNextRunner moveNextRunner = null;
				Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? this.Task : null, ref moveNextRunner);
				if (this.m_coreState.m_stateMachine == null)
				{
					Task<TResult> task = this.Task;
					this.m_coreState.PostBoxInitialization(stateMachine, moveNextRunner, task);
				}
				awaiter.OnCompleted(completionAction);
			}
			catch (Exception ex)
			{
				AsyncMethodBuilderCore.ThrowAsync(ex, null);
			}
		}

		// Token: 0x06005E2A RID: 24106 RVA: 0x0014A900 File Offset: 0x00148B00
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			try
			{
				AsyncMethodBuilderCore.MoveNextRunner moveNextRunner = null;
				Action completionAction = this.m_coreState.GetCompletionAction(AsyncCausalityTracer.LoggingOn ? this.Task : null, ref moveNextRunner);
				if (this.m_coreState.m_stateMachine == null)
				{
					Task<TResult> task = this.Task;
					this.m_coreState.PostBoxInitialization(stateMachine, moveNextRunner, task);
				}
				awaiter.UnsafeOnCompleted(completionAction);
			}
			catch (Exception ex)
			{
				AsyncMethodBuilderCore.ThrowAsync(ex, null);
			}
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06005E2B RID: 24107 RVA: 0x0014A984 File Offset: 0x00148B84
		[__DynamicallyInvokable]
		public Task<TResult> Task
		{
			[__DynamicallyInvokable]
			get
			{
				Task<TResult> task = this.m_task;
				if (task == null)
				{
					task = (this.m_task = new Task<TResult>());
				}
				return task;
			}
		}

		// Token: 0x06005E2C RID: 24108 RVA: 0x0014A9AC File Offset: 0x00148BAC
		[__DynamicallyInvokable]
		public void SetResult(TResult result)
		{
			Task<TResult> task = this.m_task;
			if (task == null)
			{
				this.m_task = this.GetTaskForResult(result);
				return;
			}
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, task.Id, AsyncCausalityStatus.Completed);
			}
			if (System.Threading.Tasks.Task.s_asyncDebuggingEnabled)
			{
				System.Threading.Tasks.Task.RemoveFromActiveTasks(task.Id);
			}
			if (!task.TrySetResult(result))
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
			}
		}

		// Token: 0x06005E2D RID: 24109 RVA: 0x0014AA10 File Offset: 0x00148C10
		internal void SetResult(Task<TResult> completedTask)
		{
			if (this.m_task == null)
			{
				this.m_task = completedTask;
				return;
			}
			this.SetResult(default(TResult));
		}

		// Token: 0x06005E2E RID: 24110 RVA: 0x0014AA40 File Offset: 0x00148C40
		[__DynamicallyInvokable]
		public void SetException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			Task<TResult> task = this.m_task;
			if (task == null)
			{
				task = this.Task;
			}
			OperationCanceledException ex = exception as OperationCanceledException;
			if (!((ex != null) ? task.TrySetCanceled(ex.CancellationToken, ex) : task.TrySetException(exception)))
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
			}
		}

		// Token: 0x06005E2F RID: 24111 RVA: 0x0014AAA0 File Offset: 0x00148CA0
		internal void SetNotificationForWaitCompletion(bool enabled)
		{
			this.Task.SetNotificationForWaitCompletion(enabled);
		}

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06005E30 RID: 24112 RVA: 0x0014AAAE File Offset: 0x00148CAE
		private object ObjectIdForDebugger
		{
			get
			{
				return this.Task;
			}
		}

		// Token: 0x06005E31 RID: 24113 RVA: 0x0014AAB8 File Offset: 0x00148CB8
		[SecuritySafeCritical]
		private Task<TResult> GetTaskForResult(TResult result)
		{
			if (default(TResult) != null)
			{
				if (typeof(TResult) == typeof(bool))
				{
					Task<bool> task = (((bool)((object)result)) ? AsyncTaskCache.TrueTask : AsyncTaskCache.FalseTask);
					return JitHelpers.UnsafeCast<Task<TResult>>(task);
				}
				if (typeof(TResult) == typeof(int))
				{
					int num = (int)((object)result);
					if (num < 9 && num >= -1)
					{
						Task<int> task2 = AsyncTaskCache.Int32Tasks[num - -1];
						return JitHelpers.UnsafeCast<Task<TResult>>(task2);
					}
				}
				else if ((typeof(TResult) == typeof(uint) && (uint)((object)result) == 0U) || (typeof(TResult) == typeof(byte) && (byte)((object)result) == 0) || (typeof(TResult) == typeof(sbyte) && (sbyte)((object)result) == 0) || (typeof(TResult) == typeof(char) && (char)((object)result) == '\0') || (typeof(TResult) == typeof(decimal) && 0m == (decimal)((object)result)) || (typeof(TResult) == typeof(long) && (long)((object)result) == 0L) || (typeof(TResult) == typeof(ulong) && (ulong)((object)result) == 0UL) || (typeof(TResult) == typeof(short) && (short)((object)result) == 0) || (typeof(TResult) == typeof(ushort) && (ushort)((object)result) == 0) || (typeof(TResult) == typeof(IntPtr) && (IntPtr)0 == (IntPtr)((object)result)) || (typeof(TResult) == typeof(UIntPtr) && (UIntPtr)0 == (UIntPtr)((object)result)))
				{
					return AsyncTaskMethodBuilder<TResult>.s_defaultResultTask;
				}
			}
			else if (result == null)
			{
				return AsyncTaskMethodBuilder<TResult>.s_defaultResultTask;
			}
			return new Task<TResult>(result);
		}

		// Token: 0x06005E32 RID: 24114 RVA: 0x0014AD64 File Offset: 0x00148F64
		// Note: this type is marked as 'beforefieldinit'.
		static AsyncTaskMethodBuilder()
		{
		}

		// Token: 0x04002A53 RID: 10835
		internal static readonly Task<TResult> s_defaultResultTask = AsyncTaskCache.CreateCacheableTask<TResult>(default(TResult));

		// Token: 0x04002A54 RID: 10836
		private AsyncMethodBuilderCore m_coreState;

		// Token: 0x04002A55 RID: 10837
		private Task<TResult> m_task;
	}
}
