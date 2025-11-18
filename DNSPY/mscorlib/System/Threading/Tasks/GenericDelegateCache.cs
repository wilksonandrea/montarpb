using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200054F RID: 1359
	internal static class GenericDelegateCache<TAntecedentResult, TResult>
	{
		// Token: 0x0600400F RID: 16399 RVA: 0x000EDFF8 File Offset: 0x000EC1F8
		// Note: this type is marked as 'beforefieldinit'.
		static GenericDelegateCache()
		{
		}

		// Token: 0x04001AC7 RID: 6855
		internal static Func<Task<Task>, object, TResult> CWAnyFuncDelegate = delegate(Task<Task> wrappedWinner, object state)
		{
			Func<Task<TAntecedentResult>, TResult> func = (Func<Task<TAntecedentResult>, TResult>)state;
			Task<TAntecedentResult> task = (Task<TAntecedentResult>)wrappedWinner.Result;
			return func(task);
		};

		// Token: 0x04001AC8 RID: 6856
		internal static Func<Task<Task>, object, TResult> CWAnyActionDelegate = delegate(Task<Task> wrappedWinner, object state)
		{
			Action<Task<TAntecedentResult>> action = (Action<Task<TAntecedentResult>>)state;
			Task<TAntecedentResult> task = (Task<TAntecedentResult>)wrappedWinner.Result;
			action(task);
			return default(TResult);
		};

		// Token: 0x04001AC9 RID: 6857
		internal static Func<Task<Task<TAntecedentResult>[]>, object, TResult> CWAllFuncDelegate = delegate(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
		{
			wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
			Func<Task<TAntecedentResult>[], TResult> func = (Func<Task<TAntecedentResult>[], TResult>)state;
			return func(wrappedAntecedents.Result);
		};

		// Token: 0x04001ACA RID: 6858
		internal static Func<Task<Task<TAntecedentResult>[]>, object, TResult> CWAllActionDelegate = delegate(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
		{
			wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
			Action<Task<TAntecedentResult>[]> action = (Action<Task<TAntecedentResult>[]>)state;
			action(wrappedAntecedents.Result);
			return default(TResult);
		};

		// Token: 0x02000C0C RID: 3084
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06006FB7 RID: 28599 RVA: 0x00180D8F File Offset: 0x0017EF8F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06006FB8 RID: 28600 RVA: 0x00180D9B File Offset: 0x0017EF9B
			public <>c()
			{
			}

			// Token: 0x06006FB9 RID: 28601 RVA: 0x00180DA4 File Offset: 0x0017EFA4
			internal TResult <.cctor>b__4_0(Task<Task> wrappedWinner, object state)
			{
				Func<Task<TAntecedentResult>, TResult> func = (Func<Task<TAntecedentResult>, TResult>)state;
				Task<TAntecedentResult> task = (Task<TAntecedentResult>)wrappedWinner.Result;
				return func(task);
			}

			// Token: 0x06006FBA RID: 28602 RVA: 0x00180DCC File Offset: 0x0017EFCC
			internal TResult <.cctor>b__4_1(Task<Task> wrappedWinner, object state)
			{
				Action<Task<TAntecedentResult>> action = (Action<Task<TAntecedentResult>>)state;
				Task<TAntecedentResult> task = (Task<TAntecedentResult>)wrappedWinner.Result;
				action(task);
				return default(TResult);
			}

			// Token: 0x06006FBB RID: 28603 RVA: 0x00180DFC File Offset: 0x0017EFFC
			internal TResult <.cctor>b__4_2(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
			{
				wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
				Func<Task<TAntecedentResult>[], TResult> func = (Func<Task<TAntecedentResult>[], TResult>)state;
				return func(wrappedAntecedents.Result);
			}

			// Token: 0x06006FBC RID: 28604 RVA: 0x00180E24 File Offset: 0x0017F024
			internal TResult <.cctor>b__4_3(Task<Task<TAntecedentResult>[]> wrappedAntecedents, object state)
			{
				wrappedAntecedents.NotifyDebuggerOfWaitCompletionIfNecessary();
				Action<Task<TAntecedentResult>[]> action = (Action<Task<TAntecedentResult>[]>)state;
				action(wrappedAntecedents.Result);
				return default(TResult);
			}

			// Token: 0x0400367C RID: 13948
			public static readonly GenericDelegateCache<TAntecedentResult, TResult>.<>c <>9 = new GenericDelegateCache<TAntecedentResult, TResult>.<>c();
		}
	}
}
