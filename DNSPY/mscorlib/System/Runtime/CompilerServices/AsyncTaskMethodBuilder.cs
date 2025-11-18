using System;
using System.Diagnostics;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008F1 RID: 2289
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct AsyncTaskMethodBuilder
	{
		// Token: 0x06005E1B RID: 24091 RVA: 0x0014A700 File Offset: 0x00148900
		[__DynamicallyInvokable]
		public static AsyncTaskMethodBuilder Create()
		{
			return default(AsyncTaskMethodBuilder);
		}

		// Token: 0x06005E1C RID: 24092 RVA: 0x0014A718 File Offset: 0x00148918
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

		// Token: 0x06005E1D RID: 24093 RVA: 0x0014A778 File Offset: 0x00148978
		[__DynamicallyInvokable]
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.m_builder.SetStateMachine(stateMachine);
		}

		// Token: 0x06005E1E RID: 24094 RVA: 0x0014A786 File Offset: 0x00148986
		[__DynamicallyInvokable]
		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this.m_builder.AwaitOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x06005E1F RID: 24095 RVA: 0x0014A795 File Offset: 0x00148995
		[__DynamicallyInvokable]
		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
		{
			this.m_builder.AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref awaiter, ref stateMachine);
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x06005E20 RID: 24096 RVA: 0x0014A7A4 File Offset: 0x001489A4
		[__DynamicallyInvokable]
		public Task Task
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_builder.Task;
			}
		}

		// Token: 0x06005E21 RID: 24097 RVA: 0x0014A7B1 File Offset: 0x001489B1
		[__DynamicallyInvokable]
		public void SetResult()
		{
			this.m_builder.SetResult(AsyncTaskMethodBuilder.s_cachedCompleted);
		}

		// Token: 0x06005E22 RID: 24098 RVA: 0x0014A7C3 File Offset: 0x001489C3
		[__DynamicallyInvokable]
		public void SetException(Exception exception)
		{
			this.m_builder.SetException(exception);
		}

		// Token: 0x06005E23 RID: 24099 RVA: 0x0014A7D1 File Offset: 0x001489D1
		internal void SetNotificationForWaitCompletion(bool enabled)
		{
			this.m_builder.SetNotificationForWaitCompletion(enabled);
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x06005E24 RID: 24100 RVA: 0x0014A7DF File Offset: 0x001489DF
		private object ObjectIdForDebugger
		{
			get
			{
				return this.Task;
			}
		}

		// Token: 0x06005E25 RID: 24101 RVA: 0x0014A7E7 File Offset: 0x001489E7
		// Note: this type is marked as 'beforefieldinit'.
		static AsyncTaskMethodBuilder()
		{
		}

		// Token: 0x04002A51 RID: 10833
		private static readonly Task<VoidTaskResult> s_cachedCompleted = AsyncTaskMethodBuilder<VoidTaskResult>.s_defaultResultTask;

		// Token: 0x04002A52 RID: 10834
		private AsyncTaskMethodBuilder<VoidTaskResult> m_builder;
	}
}
