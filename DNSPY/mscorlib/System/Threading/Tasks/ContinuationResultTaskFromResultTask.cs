using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200056D RID: 1389
	internal sealed class ContinuationResultTaskFromResultTask<TAntecedentResult, TResult> : Task<TResult>
	{
		// Token: 0x0600416F RID: 16751 RVA: 0x000F41F8 File Offset: 0x000F23F8
		public ContinuationResultTaskFromResultTask(Task<TAntecedentResult> antecedent, Delegate function, object state, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, ref StackCrawlMark stackMark)
			: base(function, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, internalOptions, null)
		{
			this.m_antecedent = antecedent;
			base.PossiblyCaptureContext(ref stackMark);
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x000F4234 File Offset: 0x000F2434
		internal override void InnerInvoke()
		{
			Task<TAntecedentResult> antecedent = this.m_antecedent;
			this.m_antecedent = null;
			antecedent.NotifyDebuggerOfWaitCompletionIfNecessary();
			Func<Task<TAntecedentResult>, TResult> func = this.m_action as Func<Task<TAntecedentResult>, TResult>;
			if (func != null)
			{
				this.m_result = func(antecedent);
				return;
			}
			Func<Task<TAntecedentResult>, object, TResult> func2 = this.m_action as Func<Task<TAntecedentResult>, object, TResult>;
			if (func2 != null)
			{
				this.m_result = func2(antecedent, this.m_stateObject);
				return;
			}
		}

		// Token: 0x04001B56 RID: 6998
		private Task<TAntecedentResult> m_antecedent;
	}
}
