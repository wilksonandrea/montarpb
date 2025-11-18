using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000514 RID: 1300
	internal class ThreadHelper
	{
		// Token: 0x06003D1F RID: 15647 RVA: 0x000E5F36 File Offset: 0x000E4136
		[SecuritySafeCritical]
		static ThreadHelper()
		{
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x000E5F49 File Offset: 0x000E4149
		internal ThreadHelper(Delegate start)
		{
			this._start = start;
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x000E5F58 File Offset: 0x000E4158
		internal void SetExecutionContextHelper(ExecutionContext ec)
		{
			this._executionContext = ec;
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x000E5F64 File Offset: 0x000E4164
		[SecurityCritical]
		private static void ThreadStart_Context(object state)
		{
			ThreadHelper threadHelper = (ThreadHelper)state;
			if (threadHelper._start is ThreadStart)
			{
				((ThreadStart)threadHelper._start)();
				return;
			}
			((ParameterizedThreadStart)threadHelper._start)(threadHelper._startArg);
		}

		// Token: 0x06003D23 RID: 15651 RVA: 0x000E5FAC File Offset: 0x000E41AC
		[SecurityCritical]
		internal void ThreadStart(object obj)
		{
			this._startArg = obj;
			if (this._executionContext != null)
			{
				ExecutionContext.Run(this._executionContext, ThreadHelper._ccb, this);
				return;
			}
			((ParameterizedThreadStart)this._start)(obj);
		}

		// Token: 0x06003D24 RID: 15652 RVA: 0x000E5FE0 File Offset: 0x000E41E0
		[SecurityCritical]
		internal void ThreadStart()
		{
			if (this._executionContext != null)
			{
				ExecutionContext.Run(this._executionContext, ThreadHelper._ccb, this);
				return;
			}
			((ThreadStart)this._start)();
		}

		// Token: 0x040019E4 RID: 6628
		private Delegate _start;

		// Token: 0x040019E5 RID: 6629
		private object _startArg;

		// Token: 0x040019E6 RID: 6630
		private ExecutionContext _executionContext;

		// Token: 0x040019E7 RID: 6631
		[SecurityCritical]
		internal static ContextCallback _ccb = new ContextCallback(ThreadHelper.ThreadStart_Context);
	}
}
