using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x0200057A RID: 1402
	internal sealed class ThreadPoolTaskScheduler : TaskScheduler
	{
		// Token: 0x06004227 RID: 16935 RVA: 0x000F6334 File Offset: 0x000F4534
		internal ThreadPoolTaskScheduler()
		{
			int id = base.Id;
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x000F6350 File Offset: 0x000F4550
		private static void LongRunningThreadWork(object obj)
		{
			Task task = obj as Task;
			task.ExecuteEntry(false);
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x000F636C File Offset: 0x000F456C
		[SecurityCritical]
		protected internal override void QueueTask(Task task)
		{
			if ((task.Options & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
			{
				new Thread(ThreadPoolTaskScheduler.s_longRunningThreadWork)
				{
					IsBackground = true
				}.Start(task);
				return;
			}
			bool flag = (task.Options & TaskCreationOptions.PreferFairness) > TaskCreationOptions.None;
			ThreadPool.UnsafeQueueCustomWorkItem(task, flag);
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x000F63B0 File Offset: 0x000F45B0
		[SecurityCritical]
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			if (taskWasPreviouslyQueued && !ThreadPool.TryPopCustomWorkItem(task))
			{
				return false;
			}
			bool flag = false;
			try
			{
				flag = task.ExecuteEntry(false);
			}
			finally
			{
				if (taskWasPreviouslyQueued)
				{
					this.NotifyWorkItemProgress();
				}
			}
			return flag;
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x000F63F4 File Offset: 0x000F45F4
		[SecurityCritical]
		protected internal override bool TryDequeue(Task task)
		{
			return ThreadPool.TryPopCustomWorkItem(task);
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x000F63FC File Offset: 0x000F45FC
		[SecurityCritical]
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return this.FilterTasksFromWorkItems(ThreadPool.GetQueuedWorkItems());
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x000F6409 File Offset: 0x000F4609
		private IEnumerable<Task> FilterTasksFromWorkItems(IEnumerable<IThreadPoolWorkItem> tpwItems)
		{
			foreach (IThreadPoolWorkItem threadPoolWorkItem in tpwItems)
			{
				if (threadPoolWorkItem is Task)
				{
					yield return (Task)threadPoolWorkItem;
				}
			}
			IEnumerator<IThreadPoolWorkItem> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x000F6419 File Offset: 0x000F4619
		internal override void NotifyWorkItemProgress()
		{
			ThreadPool.NotifyWorkItemProgress();
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x0600422F RID: 16943 RVA: 0x000F6420 File Offset: 0x000F4620
		internal override bool RequiresAtomicStartTransition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x000F6423 File Offset: 0x000F4623
		// Note: this type is marked as 'beforefieldinit'.
		static ThreadPoolTaskScheduler()
		{
		}

		// Token: 0x04001B78 RID: 7032
		private static readonly ParameterizedThreadStart s_longRunningThreadWork = new ParameterizedThreadStart(ThreadPoolTaskScheduler.LongRunningThreadWork);

		// Token: 0x02000C26 RID: 3110
		[CompilerGenerated]
		private sealed class <FilterTasksFromWorkItems>d__7 : IEnumerable<Task>, IEnumerable, IEnumerator<Task>, IDisposable, IEnumerator
		{
			// Token: 0x0600700B RID: 28683 RVA: 0x00182477 File Offset: 0x00180677
			[DebuggerHidden]
			public <FilterTasksFromWorkItems>d__7(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600700C RID: 28684 RVA: 0x00182494 File Offset: 0x00180694
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x0600700D RID: 28685 RVA: 0x001824CC File Offset: 0x001806CC
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
					}
					else
					{
						this.<>1__state = -1;
						enumerator = tpwItems.GetEnumerator();
						this.<>1__state = -3;
					}
					while (enumerator.MoveNext())
					{
						IThreadPoolWorkItem threadPoolWorkItem = enumerator.Current;
						if (threadPoolWorkItem is Task)
						{
							this.<>2__current = (Task)threadPoolWorkItem;
							this.<>1__state = 1;
							return true;
						}
					}
					this.<>m__Finally1();
					enumerator = null;
					flag = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x0600700E RID: 28686 RVA: 0x00182578 File Offset: 0x00180778
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x1700132E RID: 4910
			// (get) Token: 0x0600700F RID: 28687 RVA: 0x00182594 File Offset: 0x00180794
			Task IEnumerator<Task>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06007010 RID: 28688 RVA: 0x0018259C File Offset: 0x0018079C
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700132F RID: 4911
			// (get) Token: 0x06007011 RID: 28689 RVA: 0x001825A3 File Offset: 0x001807A3
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06007012 RID: 28690 RVA: 0x001825AC File Offset: 0x001807AC
			[DebuggerHidden]
			IEnumerator<Task> IEnumerable<Task>.GetEnumerator()
			{
				ThreadPoolTaskScheduler.<FilterTasksFromWorkItems>d__7 <FilterTasksFromWorkItems>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<FilterTasksFromWorkItems>d__ = this;
				}
				else
				{
					<FilterTasksFromWorkItems>d__ = new ThreadPoolTaskScheduler.<FilterTasksFromWorkItems>d__7(0);
				}
				<FilterTasksFromWorkItems>d__.tpwItems = tpwItems;
				return <FilterTasksFromWorkItems>d__;
			}

			// Token: 0x06007013 RID: 28691 RVA: 0x001825EF File Offset: 0x001807EF
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Threading.Tasks.Task>.GetEnumerator();
			}

			// Token: 0x040036E0 RID: 14048
			private int <>1__state;

			// Token: 0x040036E1 RID: 14049
			private Task <>2__current;

			// Token: 0x040036E2 RID: 14050
			private int <>l__initialThreadId;

			// Token: 0x040036E3 RID: 14051
			private IEnumerable<IThreadPoolWorkItem> tpwItems;

			// Token: 0x040036E4 RID: 14052
			public IEnumerable<IThreadPoolWorkItem> <>3__tpwItems;

			// Token: 0x040036E5 RID: 14053
			private IEnumerator<IThreadPoolWorkItem> <>7__wrap1;
		}
	}
}
