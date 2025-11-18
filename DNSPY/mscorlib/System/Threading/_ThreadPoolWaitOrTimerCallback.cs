using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000524 RID: 1316
	internal class _ThreadPoolWaitOrTimerCallback
	{
		// Token: 0x06003DEF RID: 15855 RVA: 0x000E739B File Offset: 0x000E559B
		[SecuritySafeCritical]
		static _ThreadPoolWaitOrTimerCallback()
		{
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x000E73BF File Offset: 0x000E55BF
		[SecurityCritical]
		internal _ThreadPoolWaitOrTimerCallback(WaitOrTimerCallback waitOrTimerCallback, object state, bool compressStack, ref StackCrawlMark stackMark)
		{
			this._waitOrTimerCallback = waitOrTimerCallback;
			this._state = state;
			if (compressStack && !ExecutionContext.IsFlowSuppressed())
			{
				this._executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x000E73ED File Offset: 0x000E55ED
		[SecurityCritical]
		private static void WaitOrTimerCallback_Context_t(object state)
		{
			_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context(state, true);
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x000E73F6 File Offset: 0x000E55F6
		[SecurityCritical]
		private static void WaitOrTimerCallback_Context_f(object state)
		{
			_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context(state, false);
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x000E7400 File Offset: 0x000E5600
		private static void WaitOrTimerCallback_Context(object state, bool timedOut)
		{
			_ThreadPoolWaitOrTimerCallback threadPoolWaitOrTimerCallback = (_ThreadPoolWaitOrTimerCallback)state;
			threadPoolWaitOrTimerCallback._waitOrTimerCallback(threadPoolWaitOrTimerCallback._state, timedOut);
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x000E7428 File Offset: 0x000E5628
		[SecurityCritical]
		internal static void PerformWaitOrTimerCallback(object state, bool timedOut)
		{
			_ThreadPoolWaitOrTimerCallback threadPoolWaitOrTimerCallback = (_ThreadPoolWaitOrTimerCallback)state;
			if (threadPoolWaitOrTimerCallback._executionContext == null)
			{
				WaitOrTimerCallback waitOrTimerCallback = threadPoolWaitOrTimerCallback._waitOrTimerCallback;
				waitOrTimerCallback(threadPoolWaitOrTimerCallback._state, timedOut);
				return;
			}
			using (ExecutionContext executionContext = threadPoolWaitOrTimerCallback._executionContext.CreateCopy())
			{
				if (timedOut)
				{
					ExecutionContext.Run(executionContext, _ThreadPoolWaitOrTimerCallback._ccbt, threadPoolWaitOrTimerCallback, true);
				}
				else
				{
					ExecutionContext.Run(executionContext, _ThreadPoolWaitOrTimerCallback._ccbf, threadPoolWaitOrTimerCallback, true);
				}
			}
		}

		// Token: 0x04001A19 RID: 6681
		private WaitOrTimerCallback _waitOrTimerCallback;

		// Token: 0x04001A1A RID: 6682
		private ExecutionContext _executionContext;

		// Token: 0x04001A1B RID: 6683
		private object _state;

		// Token: 0x04001A1C RID: 6684
		[SecurityCritical]
		private static ContextCallback _ccbt = new ContextCallback(_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context_t);

		// Token: 0x04001A1D RID: 6685
		[SecurityCritical]
		private static ContextCallback _ccbf = new ContextCallback(_ThreadPoolWaitOrTimerCallback.WaitOrTimerCallback_Context_f);
	}
}
