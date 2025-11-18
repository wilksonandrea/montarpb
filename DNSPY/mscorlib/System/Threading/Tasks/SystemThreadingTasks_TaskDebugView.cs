using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000560 RID: 1376
	internal class SystemThreadingTasks_TaskDebugView
	{
		// Token: 0x0600414C RID: 16716 RVA: 0x000F3B30 File Offset: 0x000F1D30
		public SystemThreadingTasks_TaskDebugView(Task task)
		{
			this.m_task = task;
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x0600414D RID: 16717 RVA: 0x000F3B3F File Offset: 0x000F1D3F
		public object AsyncState
		{
			get
			{
				return this.m_task.AsyncState;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x0600414E RID: 16718 RVA: 0x000F3B4C File Offset: 0x000F1D4C
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_task.CreationOptions;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x0600414F RID: 16719 RVA: 0x000F3B59 File Offset: 0x000F1D59
		public Exception Exception
		{
			get
			{
				return this.m_task.Exception;
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06004150 RID: 16720 RVA: 0x000F3B66 File Offset: 0x000F1D66
		public int Id
		{
			get
			{
				return this.m_task.Id;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06004151 RID: 16721 RVA: 0x000F3B74 File Offset: 0x000F1D74
		public bool CancellationPending
		{
			get
			{
				return this.m_task.Status == TaskStatus.WaitingToRun && this.m_task.CancellationToken.IsCancellationRequested;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06004152 RID: 16722 RVA: 0x000F3BA4 File Offset: 0x000F1DA4
		public TaskStatus Status
		{
			get
			{
				return this.m_task.Status;
			}
		}

		// Token: 0x04001B22 RID: 6946
		private Task m_task;
	}
}
