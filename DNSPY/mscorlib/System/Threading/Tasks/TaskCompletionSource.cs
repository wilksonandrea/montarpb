using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	// Token: 0x0200057B RID: 1403
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class TaskCompletionSource<TResult>
	{
		// Token: 0x06004231 RID: 16945 RVA: 0x000F6436 File Offset: 0x000F4636
		[__DynamicallyInvokable]
		public TaskCompletionSource()
		{
			this.m_task = new Task<TResult>();
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x000F6449 File Offset: 0x000F4649
		[__DynamicallyInvokable]
		public TaskCompletionSource(TaskCreationOptions creationOptions)
			: this(null, creationOptions)
		{
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x000F6453 File Offset: 0x000F4653
		[__DynamicallyInvokable]
		public TaskCompletionSource(object state)
			: this(state, TaskCreationOptions.None)
		{
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x000F645D File Offset: 0x000F465D
		[__DynamicallyInvokable]
		public TaskCompletionSource(object state, TaskCreationOptions creationOptions)
		{
			this.m_task = new Task<TResult>(state, creationOptions);
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06004235 RID: 16949 RVA: 0x000F6472 File Offset: 0x000F4672
		[__DynamicallyInvokable]
		public Task<TResult> Task
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_task;
			}
		}

		// Token: 0x06004236 RID: 16950 RVA: 0x000F647C File Offset: 0x000F467C
		private void SpinUntilCompleted()
		{
			SpinWait spinWait = default(SpinWait);
			while (!this.m_task.IsCompleted)
			{
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06004237 RID: 16951 RVA: 0x000F64A8 File Offset: 0x000F46A8
		[__DynamicallyInvokable]
		public bool TrySetException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			bool flag = this.m_task.TrySetException(exception);
			if (!flag && !this.m_task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x000F64E8 File Offset: 0x000F46E8
		[__DynamicallyInvokable]
		public bool TrySetException(IEnumerable<Exception> exceptions)
		{
			if (exceptions == null)
			{
				throw new ArgumentNullException("exceptions");
			}
			List<Exception> list = new List<Exception>();
			foreach (Exception ex in exceptions)
			{
				if (ex == null)
				{
					throw new ArgumentException(Environment.GetResourceString("TaskCompletionSourceT_TrySetException_NullException"), "exceptions");
				}
				list.Add(ex);
			}
			if (list.Count == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("TaskCompletionSourceT_TrySetException_NoExceptions"), "exceptions");
			}
			bool flag = this.m_task.TrySetException(list);
			if (!flag && !this.m_task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x06004239 RID: 16953 RVA: 0x000F65A0 File Offset: 0x000F47A0
		internal bool TrySetException(IEnumerable<ExceptionDispatchInfo> exceptions)
		{
			bool flag = this.m_task.TrySetException(exceptions);
			if (!flag && !this.m_task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x0600423A RID: 16954 RVA: 0x000F65D1 File Offset: 0x000F47D1
		[__DynamicallyInvokable]
		public void SetException(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			if (!this.TrySetException(exception))
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
			}
		}

		// Token: 0x0600423B RID: 16955 RVA: 0x000F65FA File Offset: 0x000F47FA
		[__DynamicallyInvokable]
		public void SetException(IEnumerable<Exception> exceptions)
		{
			if (!this.TrySetException(exceptions))
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
			}
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x000F6618 File Offset: 0x000F4818
		[__DynamicallyInvokable]
		public bool TrySetResult(TResult result)
		{
			bool flag = this.m_task.TrySetResult(result);
			if (!flag && !this.m_task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x000F6649 File Offset: 0x000F4849
		[__DynamicallyInvokable]
		public void SetResult(TResult result)
		{
			if (!this.TrySetResult(result))
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
			}
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x000F6664 File Offset: 0x000F4864
		[__DynamicallyInvokable]
		public bool TrySetCanceled()
		{
			return this.TrySetCanceled(default(CancellationToken));
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x000F6680 File Offset: 0x000F4880
		[__DynamicallyInvokable]
		public bool TrySetCanceled(CancellationToken cancellationToken)
		{
			bool flag = this.m_task.TrySetCanceled(cancellationToken);
			if (!flag && !this.m_task.IsCompleted)
			{
				this.SpinUntilCompleted();
			}
			return flag;
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x000F66B1 File Offset: 0x000F48B1
		[__DynamicallyInvokable]
		public void SetCanceled()
		{
			if (!this.TrySetCanceled())
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskT_TransitionToFinal_AlreadyCompleted"));
			}
		}

		// Token: 0x04001B79 RID: 7033
		private readonly Task<TResult> m_task;
	}
}
