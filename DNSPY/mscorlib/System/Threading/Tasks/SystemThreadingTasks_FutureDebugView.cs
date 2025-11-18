using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200054D RID: 1357
	internal class SystemThreadingTasks_FutureDebugView<TResult>
	{
		// Token: 0x06003FCD RID: 16333 RVA: 0x000ECDCF File Offset: 0x000EAFCF
		public SystemThreadingTasks_FutureDebugView(Task<TResult> task)
		{
			this.m_task = task;
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06003FCE RID: 16334 RVA: 0x000ECDE0 File Offset: 0x000EAFE0
		public TResult Result
		{
			get
			{
				if (this.m_task.Status != TaskStatus.RanToCompletion)
				{
					return default(TResult);
				}
				return this.m_task.Result;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06003FCF RID: 16335 RVA: 0x000ECE10 File Offset: 0x000EB010
		public object AsyncState
		{
			get
			{
				return this.m_task.AsyncState;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06003FD0 RID: 16336 RVA: 0x000ECE1D File Offset: 0x000EB01D
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_task.CreationOptions;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06003FD1 RID: 16337 RVA: 0x000ECE2A File Offset: 0x000EB02A
		public Exception Exception
		{
			get
			{
				return this.m_task.Exception;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06003FD2 RID: 16338 RVA: 0x000ECE37 File Offset: 0x000EB037
		public int Id
		{
			get
			{
				return this.m_task.Id;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06003FD3 RID: 16339 RVA: 0x000ECE44 File Offset: 0x000EB044
		public bool CancellationPending
		{
			get
			{
				return this.m_task.Status == TaskStatus.WaitingToRun && this.m_task.CancellationToken.IsCancellationRequested;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06003FD4 RID: 16340 RVA: 0x000ECE74 File Offset: 0x000EB074
		public TaskStatus Status
		{
			get
			{
				return this.m_task.Status;
			}
		}

		// Token: 0x04001AC2 RID: 6850
		private Task<TResult> m_task;
	}
}
