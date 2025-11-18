using System;
using System.Runtime.Serialization;

namespace System.Threading.Tasks
{
	// Token: 0x02000573 RID: 1395
	[__DynamicallyInvokable]
	[Serializable]
	public class TaskCanceledException : OperationCanceledException
	{
		// Token: 0x0600418F RID: 16783 RVA: 0x000F4A84 File Offset: 0x000F2C84
		[__DynamicallyInvokable]
		public TaskCanceledException()
			: base(Environment.GetResourceString("TaskCanceledException_ctor_DefaultMessage"))
		{
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x000F4A96 File Offset: 0x000F2C96
		[__DynamicallyInvokable]
		public TaskCanceledException(string message)
			: base(message)
		{
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x000F4A9F File Offset: 0x000F2C9F
		[__DynamicallyInvokable]
		public TaskCanceledException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x000F4AAC File Offset: 0x000F2CAC
		[__DynamicallyInvokable]
		public TaskCanceledException(Task task)
			: base(Environment.GetResourceString("TaskCanceledException_ctor_DefaultMessage"), (task != null) ? task.CancellationToken : default(CancellationToken))
		{
			this.m_canceledTask = task;
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x000F4AE4 File Offset: 0x000F2CE4
		protected TaskCanceledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06004194 RID: 16788 RVA: 0x000F4AEE File Offset: 0x000F2CEE
		[__DynamicallyInvokable]
		public Task Task
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_canceledTask;
			}
		}

		// Token: 0x04001B62 RID: 7010
		[NonSerialized]
		private Task m_canceledTask;
	}
}
