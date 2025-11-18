using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x0200051C RID: 1308
	internal sealed class ThreadPoolWorkQueueThreadLocals
	{
		// Token: 0x06003DCD RID: 15821 RVA: 0x000E6FB2 File Offset: 0x000E51B2
		public ThreadPoolWorkQueueThreadLocals(ThreadPoolWorkQueue tpq)
		{
			this.workQueue = tpq;
			this.workStealingQueue = new ThreadPoolWorkQueue.WorkStealingQueue();
			ThreadPoolWorkQueue.allThreadQueues.Add(this.workStealingQueue);
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x000E6FF4 File Offset: 0x000E51F4
		[SecurityCritical]
		private void CleanUp()
		{
			if (this.workStealingQueue != null)
			{
				if (this.workQueue != null)
				{
					bool flag = false;
					while (!flag)
					{
						try
						{
						}
						finally
						{
							IThreadPoolWorkItem threadPoolWorkItem = null;
							if (this.workStealingQueue.LocalPop(out threadPoolWorkItem))
							{
								this.workQueue.Enqueue(threadPoolWorkItem, true);
							}
							else
							{
								flag = true;
							}
						}
					}
				}
				ThreadPoolWorkQueue.allThreadQueues.Remove(this.workStealingQueue);
			}
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x000E7060 File Offset: 0x000E5260
		[SecuritySafeCritical]
		~ThreadPoolWorkQueueThreadLocals()
		{
			if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
			{
				this.CleanUp();
			}
		}

		// Token: 0x04001A0C RID: 6668
		[ThreadStatic]
		[SecurityCritical]
		public static ThreadPoolWorkQueueThreadLocals threadLocals;

		// Token: 0x04001A0D RID: 6669
		public readonly ThreadPoolWorkQueue workQueue;

		// Token: 0x04001A0E RID: 6670
		public readonly ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue;

		// Token: 0x04001A0F RID: 6671
		public readonly Random random = new Random(Thread.CurrentThread.ManagedThreadId);
	}
}
