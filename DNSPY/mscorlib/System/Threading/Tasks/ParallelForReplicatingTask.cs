using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000561 RID: 1377
	internal class ParallelForReplicatingTask : Task
	{
		// Token: 0x06004153 RID: 16723 RVA: 0x000F3BB4 File Offset: 0x000F1DB4
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal ParallelForReplicatingTask(ParallelOptions parallelOptions, Action action, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions)
			: base(action, null, Task.InternalCurrent, default(CancellationToken), creationOptions, internalOptions | InternalTaskOptions.SelfReplicating, null)
		{
			this.m_replicationDownCount = parallelOptions.EffectiveMaxConcurrencyLevel;
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			base.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x000F3BF7 File Offset: 0x000F1DF7
		internal override bool ShouldReplicate()
		{
			if (this.m_replicationDownCount == -1)
			{
				return true;
			}
			if (this.m_replicationDownCount > 0)
			{
				this.m_replicationDownCount--;
				return true;
			}
			return false;
		}

		// Token: 0x06004155 RID: 16725 RVA: 0x000F3C1E File Offset: 0x000F1E1E
		internal override Task CreateReplicaTask(Action<object> taskReplicaDelegate, object stateObject, Task parentTask, TaskScheduler taskScheduler, TaskCreationOptions creationOptionsForReplica, InternalTaskOptions internalOptionsForReplica)
		{
			return new ParallelForReplicaTask(taskReplicaDelegate, stateObject, parentTask, taskScheduler, creationOptionsForReplica, internalOptionsForReplica);
		}

		// Token: 0x04001B23 RID: 6947
		private int m_replicationDownCount;
	}
}
