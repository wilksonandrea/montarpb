using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000562 RID: 1378
	internal class ParallelForReplicaTask : Task
	{
		// Token: 0x06004156 RID: 16726 RVA: 0x000F3C30 File Offset: 0x000F1E30
		internal ParallelForReplicaTask(Action<object> taskReplicaDelegate, object stateObject, Task parentTask, TaskScheduler taskScheduler, TaskCreationOptions creationOptionsForReplica, InternalTaskOptions internalOptionsForReplica)
			: base(taskReplicaDelegate, stateObject, parentTask, default(CancellationToken), creationOptionsForReplica, internalOptionsForReplica, taskScheduler)
		{
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06004157 RID: 16727 RVA: 0x000F3C55 File Offset: 0x000F1E55
		// (set) Token: 0x06004158 RID: 16728 RVA: 0x000F3C5D File Offset: 0x000F1E5D
		internal override object SavedStateForNextReplica
		{
			get
			{
				return this.m_stateForNextReplica;
			}
			set
			{
				this.m_stateForNextReplica = value;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06004159 RID: 16729 RVA: 0x000F3C66 File Offset: 0x000F1E66
		// (set) Token: 0x0600415A RID: 16730 RVA: 0x000F3C6E File Offset: 0x000F1E6E
		internal override object SavedStateFromPreviousReplica
		{
			get
			{
				return this.m_stateFromPreviousReplica;
			}
			set
			{
				this.m_stateFromPreviousReplica = value;
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x0600415B RID: 16731 RVA: 0x000F3C77 File Offset: 0x000F1E77
		// (set) Token: 0x0600415C RID: 16732 RVA: 0x000F3C7F File Offset: 0x000F1E7F
		internal override Task HandedOverChildReplica
		{
			get
			{
				return this.m_handedOverChildReplica;
			}
			set
			{
				this.m_handedOverChildReplica = value;
			}
		}

		// Token: 0x04001B24 RID: 6948
		internal object m_stateForNextReplica;

		// Token: 0x04001B25 RID: 6949
		internal object m_stateFromPreviousReplica;

		// Token: 0x04001B26 RID: 6950
		internal Task m_handedOverChildReplica;
	}
}
