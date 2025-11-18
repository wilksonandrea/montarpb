using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000523 RID: 1315
	internal sealed class QueueUserWorkItemCallback : IThreadPoolWorkItem
	{
		// Token: 0x06003DE9 RID: 15849 RVA: 0x000E72D0 File Offset: 0x000E54D0
		[SecuritySafeCritical]
		static QueueUserWorkItemCallback()
		{
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x000E72E3 File Offset: 0x000E54E3
		[SecurityCritical]
		internal QueueUserWorkItemCallback(WaitCallback waitCallback, object stateObj, bool compressStack, ref StackCrawlMark stackMark)
		{
			this.callback = waitCallback;
			this.state = stateObj;
			if (compressStack && !ExecutionContext.IsFlowSuppressed())
			{
				this.context = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x000E7311 File Offset: 0x000E5511
		internal QueueUserWorkItemCallback(WaitCallback waitCallback, object stateObj, ExecutionContext ec)
		{
			this.callback = waitCallback;
			this.state = stateObj;
			this.context = ec;
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x000E7330 File Offset: 0x000E5530
		[SecurityCritical]
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			if (this.context == null)
			{
				WaitCallback waitCallback = this.callback;
				this.callback = null;
				waitCallback(this.state);
				return;
			}
			ExecutionContext.Run(this.context, QueueUserWorkItemCallback.ccb, this, true);
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x000E7372 File Offset: 0x000E5572
		[SecurityCritical]
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x000E7374 File Offset: 0x000E5574
		[SecurityCritical]
		private static void WaitCallback_Context(object state)
		{
			QueueUserWorkItemCallback queueUserWorkItemCallback = (QueueUserWorkItemCallback)state;
			WaitCallback waitCallback = queueUserWorkItemCallback.callback;
			waitCallback(queueUserWorkItemCallback.state);
		}

		// Token: 0x04001A15 RID: 6677
		private WaitCallback callback;

		// Token: 0x04001A16 RID: 6678
		private ExecutionContext context;

		// Token: 0x04001A17 RID: 6679
		private object state;

		// Token: 0x04001A18 RID: 6680
		[SecurityCritical]
		internal static ContextCallback ccb = new ContextCallback(QueueUserWorkItemCallback.WaitCallback_Context);
	}
}
