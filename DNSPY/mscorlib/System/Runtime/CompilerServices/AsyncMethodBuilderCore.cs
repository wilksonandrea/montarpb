using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008F4 RID: 2292
	internal struct AsyncMethodBuilderCore
	{
		// Token: 0x06005E36 RID: 24118 RVA: 0x0014ADF8 File Offset: 0x00148FF8
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			if (stateMachine == null)
			{
				throw new ArgumentNullException("stateMachine");
			}
			if (this.m_stateMachine != null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("AsyncMethodBuilder_InstanceNotInitialized"));
			}
			this.m_stateMachine = stateMachine;
		}

		// Token: 0x06005E37 RID: 24119 RVA: 0x0014AE28 File Offset: 0x00149028
		[SecuritySafeCritical]
		internal Action GetCompletionAction(Task taskForTracing, ref AsyncMethodBuilderCore.MoveNextRunner runnerToInitialize)
		{
			Debugger.NotifyOfCrossThreadDependency();
			ExecutionContext executionContext = ExecutionContext.FastCapture();
			Action action;
			AsyncMethodBuilderCore.MoveNextRunner moveNextRunner;
			if (executionContext != null && executionContext.IsPreAllocatedDefault)
			{
				action = this.m_defaultContextAction;
				if (action != null)
				{
					return action;
				}
				moveNextRunner = new AsyncMethodBuilderCore.MoveNextRunner(executionContext, this.m_stateMachine);
				action = new Action(moveNextRunner.Run);
				if (taskForTracing != null)
				{
					action = (this.m_defaultContextAction = this.OutputAsyncCausalityEvents(taskForTracing, action));
				}
				else
				{
					this.m_defaultContextAction = action;
				}
			}
			else
			{
				moveNextRunner = new AsyncMethodBuilderCore.MoveNextRunner(executionContext, this.m_stateMachine);
				action = new Action(moveNextRunner.Run);
				if (taskForTracing != null)
				{
					action = this.OutputAsyncCausalityEvents(taskForTracing, action);
				}
			}
			if (this.m_stateMachine == null)
			{
				runnerToInitialize = moveNextRunner;
			}
			return action;
		}

		// Token: 0x06005E38 RID: 24120 RVA: 0x0014AEC4 File Offset: 0x001490C4
		private Action OutputAsyncCausalityEvents(Task innerTask, Action continuation)
		{
			return AsyncMethodBuilderCore.CreateContinuationWrapper(continuation, delegate
			{
				AsyncCausalityTracer.TraceSynchronousWorkStart(CausalityTraceLevel.Required, innerTask.Id, CausalitySynchronousWork.Execution);
				continuation();
				AsyncCausalityTracer.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.Execution);
			}, innerTask);
		}

		// Token: 0x06005E39 RID: 24121 RVA: 0x0014AF04 File Offset: 0x00149104
		internal void PostBoxInitialization(IAsyncStateMachine stateMachine, AsyncMethodBuilderCore.MoveNextRunner runner, Task builtTask)
		{
			if (builtTask != null)
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, builtTask.Id, "Async: " + stateMachine.GetType().Name, 0UL);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.AddToActiveTasks(builtTask);
				}
			}
			this.m_stateMachine = stateMachine;
			this.m_stateMachine.SetStateMachine(this.m_stateMachine);
			runner.m_stateMachine = this.m_stateMachine;
		}

		// Token: 0x06005E3A RID: 24122 RVA: 0x0014AF70 File Offset: 0x00149170
		internal static void ThrowAsync(Exception exception, SynchronizationContext targetContext)
		{
			ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(exception);
			if (targetContext != null)
			{
				try
				{
					targetContext.Post(delegate(object state)
					{
						((ExceptionDispatchInfo)state).Throw();
					}, exceptionDispatchInfo);
					return;
				}
				catch (Exception ex)
				{
					exceptionDispatchInfo = ExceptionDispatchInfo.Capture(new AggregateException(new Exception[] { exception, ex }));
				}
			}
			if (!WindowsRuntimeMarshal.ReportUnhandledError(exceptionDispatchInfo.SourceException))
			{
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					((ExceptionDispatchInfo)state).Throw();
				}, exceptionDispatchInfo);
			}
		}

		// Token: 0x06005E3B RID: 24123 RVA: 0x0014B010 File Offset: 0x00149210
		internal static Action CreateContinuationWrapper(Action continuation, Action invokeAction, Task innerTask = null)
		{
			return new Action(new AsyncMethodBuilderCore.ContinuationWrapper(continuation, invokeAction, innerTask).Invoke);
		}

		// Token: 0x06005E3C RID: 24124 RVA: 0x0014B028 File Offset: 0x00149228
		internal static Action TryGetStateMachineForDebugger(Action action)
		{
			object target = action.Target;
			AsyncMethodBuilderCore.MoveNextRunner moveNextRunner = target as AsyncMethodBuilderCore.MoveNextRunner;
			if (moveNextRunner != null)
			{
				return new Action(moveNextRunner.m_stateMachine.MoveNext);
			}
			AsyncMethodBuilderCore.ContinuationWrapper continuationWrapper = target as AsyncMethodBuilderCore.ContinuationWrapper;
			if (continuationWrapper != null)
			{
				return AsyncMethodBuilderCore.TryGetStateMachineForDebugger(continuationWrapper.m_continuation);
			}
			return action;
		}

		// Token: 0x06005E3D RID: 24125 RVA: 0x0014B070 File Offset: 0x00149270
		internal static Task TryGetContinuationTask(Action action)
		{
			if (action != null)
			{
				AsyncMethodBuilderCore.ContinuationWrapper continuationWrapper = action.Target as AsyncMethodBuilderCore.ContinuationWrapper;
				if (continuationWrapper != null)
				{
					return continuationWrapper.m_innerTask;
				}
			}
			return null;
		}

		// Token: 0x04002A5B RID: 10843
		internal IAsyncStateMachine m_stateMachine;

		// Token: 0x04002A5C RID: 10844
		internal Action m_defaultContextAction;

		// Token: 0x02000C91 RID: 3217
		internal sealed class MoveNextRunner
		{
			// Token: 0x060070F8 RID: 28920 RVA: 0x00184D0E File Offset: 0x00182F0E
			[SecurityCritical]
			internal MoveNextRunner(ExecutionContext context, IAsyncStateMachine stateMachine)
			{
				this.m_context = context;
				this.m_stateMachine = stateMachine;
			}

			// Token: 0x060070F9 RID: 28921 RVA: 0x00184D24 File Offset: 0x00182F24
			[SecuritySafeCritical]
			internal void Run()
			{
				if (this.m_context != null)
				{
					try
					{
						ContextCallback contextCallback = AsyncMethodBuilderCore.MoveNextRunner.s_invokeMoveNext;
						if (contextCallback == null)
						{
							contextCallback = (AsyncMethodBuilderCore.MoveNextRunner.s_invokeMoveNext = new ContextCallback(AsyncMethodBuilderCore.MoveNextRunner.InvokeMoveNext));
						}
						ExecutionContext.Run(this.m_context, contextCallback, this.m_stateMachine, true);
						return;
					}
					finally
					{
						this.m_context.Dispose();
					}
				}
				this.m_stateMachine.MoveNext();
			}

			// Token: 0x060070FA RID: 28922 RVA: 0x00184D94 File Offset: 0x00182F94
			[SecurityCritical]
			private static void InvokeMoveNext(object stateMachine)
			{
				((IAsyncStateMachine)stateMachine).MoveNext();
			}

			// Token: 0x04003848 RID: 14408
			private readonly ExecutionContext m_context;

			// Token: 0x04003849 RID: 14409
			internal IAsyncStateMachine m_stateMachine;

			// Token: 0x0400384A RID: 14410
			[SecurityCritical]
			private static ContextCallback s_invokeMoveNext;
		}

		// Token: 0x02000C92 RID: 3218
		private class ContinuationWrapper
		{
			// Token: 0x060070FB RID: 28923 RVA: 0x00184DA1 File Offset: 0x00182FA1
			internal ContinuationWrapper(Action continuation, Action invokeAction, Task innerTask)
			{
				if (innerTask == null)
				{
					innerTask = AsyncMethodBuilderCore.TryGetContinuationTask(continuation);
				}
				this.m_continuation = continuation;
				this.m_innerTask = innerTask;
				this.m_invokeAction = invokeAction;
			}

			// Token: 0x060070FC RID: 28924 RVA: 0x00184DC9 File Offset: 0x00182FC9
			internal void Invoke()
			{
				this.m_invokeAction();
			}

			// Token: 0x0400384B RID: 14411
			internal readonly Action m_continuation;

			// Token: 0x0400384C RID: 14412
			private readonly Action m_invokeAction;

			// Token: 0x0400384D RID: 14413
			internal readonly Task m_innerTask;
		}

		// Token: 0x02000C93 RID: 3219
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x060070FD RID: 28925 RVA: 0x00184DD6 File Offset: 0x00182FD6
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x060070FE RID: 28926 RVA: 0x00184DDE File Offset: 0x00182FDE
			internal void <OutputAsyncCausalityEvents>b__0()
			{
				AsyncCausalityTracer.TraceSynchronousWorkStart(CausalityTraceLevel.Required, this.innerTask.Id, CausalitySynchronousWork.Execution);
				this.continuation();
				AsyncCausalityTracer.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.Execution);
			}

			// Token: 0x0400384E RID: 14414
			public Task innerTask;

			// Token: 0x0400384F RID: 14415
			public Action continuation;
		}

		// Token: 0x02000C94 RID: 3220
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060070FF RID: 28927 RVA: 0x00184E04 File Offset: 0x00183004
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06007100 RID: 28928 RVA: 0x00184E10 File Offset: 0x00183010
			public <>c()
			{
			}

			// Token: 0x06007101 RID: 28929 RVA: 0x00184E18 File Offset: 0x00183018
			internal void <ThrowAsync>b__6_0(object state)
			{
				((ExceptionDispatchInfo)state).Throw();
			}

			// Token: 0x06007102 RID: 28930 RVA: 0x00184E25 File Offset: 0x00183025
			internal void <ThrowAsync>b__6_1(object state)
			{
				((ExceptionDispatchInfo)state).Throw();
			}

			// Token: 0x04003850 RID: 14416
			public static readonly AsyncMethodBuilderCore.<>c <>9 = new AsyncMethodBuilderCore.<>c();

			// Token: 0x04003851 RID: 14417
			public static SendOrPostCallback <>9__6_0;

			// Token: 0x04003852 RID: 14418
			public static WaitCallback <>9__6_1;
		}
	}
}
