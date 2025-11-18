using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000547 RID: 1351
	internal class CancellationCallbackInfo
	{
		// Token: 0x06003F6D RID: 16237 RVA: 0x000EBEAC File Offset: 0x000EA0AC
		internal CancellationCallbackInfo(Action<object> callback, object stateForCallback, SynchronizationContext targetSyncContext, ExecutionContext targetExecutionContext, CancellationTokenSource cancellationTokenSource)
		{
			this.Callback = callback;
			this.StateForCallback = stateForCallback;
			this.TargetSyncContext = targetSyncContext;
			this.TargetExecutionContext = targetExecutionContext;
			this.CancellationTokenSource = cancellationTokenSource;
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x000EBEDC File Offset: 0x000EA0DC
		[SecuritySafeCritical]
		internal void ExecuteCallback()
		{
			if (this.TargetExecutionContext != null)
			{
				ContextCallback contextCallback = CancellationCallbackInfo.s_executionContextCallback;
				if (contextCallback == null)
				{
					contextCallback = (CancellationCallbackInfo.s_executionContextCallback = new ContextCallback(CancellationCallbackInfo.ExecutionContextCallback));
				}
				ExecutionContext.Run(this.TargetExecutionContext, contextCallback, this);
				return;
			}
			CancellationCallbackInfo.ExecutionContextCallback(this);
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x000EBF24 File Offset: 0x000EA124
		[SecurityCritical]
		private static void ExecutionContextCallback(object obj)
		{
			CancellationCallbackInfo cancellationCallbackInfo = obj as CancellationCallbackInfo;
			cancellationCallbackInfo.Callback(cancellationCallbackInfo.StateForCallback);
		}

		// Token: 0x04001AAF RID: 6831
		internal readonly Action<object> Callback;

		// Token: 0x04001AB0 RID: 6832
		internal readonly object StateForCallback;

		// Token: 0x04001AB1 RID: 6833
		internal readonly SynchronizationContext TargetSyncContext;

		// Token: 0x04001AB2 RID: 6834
		internal readonly ExecutionContext TargetExecutionContext;

		// Token: 0x04001AB3 RID: 6835
		internal readonly CancellationTokenSource CancellationTokenSource;

		// Token: 0x04001AB4 RID: 6836
		[SecurityCritical]
		private static ContextCallback s_executionContextCallback;
	}
}
