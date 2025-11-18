using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000588 RID: 1416
	internal sealed class BeginEndAwaitableAdapter : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x0600429F RID: 17055 RVA: 0x000F81DA File Offset: 0x000F63DA
		public BeginEndAwaitableAdapter GetAwaiter()
		{
			return this;
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x060042A0 RID: 17056 RVA: 0x000F81DD File Offset: 0x000F63DD
		public bool IsCompleted
		{
			get
			{
				return this._continuation == BeginEndAwaitableAdapter.CALLBACK_RAN;
			}
		}

		// Token: 0x060042A1 RID: 17057 RVA: 0x000F81EF File Offset: 0x000F63EF
		[SecurityCritical]
		public void UnsafeOnCompleted(Action continuation)
		{
			this.OnCompleted(continuation);
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x000F81F8 File Offset: 0x000F63F8
		public void OnCompleted(Action continuation)
		{
			if (this._continuation == BeginEndAwaitableAdapter.CALLBACK_RAN || Interlocked.CompareExchange<Action>(ref this._continuation, continuation, null) == BeginEndAwaitableAdapter.CALLBACK_RAN)
			{
				Task.Run(continuation);
			}
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x000F822C File Offset: 0x000F642C
		public IAsyncResult GetResult()
		{
			IAsyncResult asyncResult = this._asyncResult;
			this._asyncResult = null;
			this._continuation = null;
			return asyncResult;
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x000F824F File Offset: 0x000F644F
		public BeginEndAwaitableAdapter()
		{
		}

		// Token: 0x060042A5 RID: 17061 RVA: 0x000F8257 File Offset: 0x000F6457
		// Note: this type is marked as 'beforefieldinit'.
		static BeginEndAwaitableAdapter()
		{
		}

		// Token: 0x04001BB9 RID: 7097
		private static readonly Action CALLBACK_RAN = delegate
		{
		};

		// Token: 0x04001BBA RID: 7098
		private IAsyncResult _asyncResult;

		// Token: 0x04001BBB RID: 7099
		private Action _continuation;

		// Token: 0x04001BBC RID: 7100
		public static readonly AsyncCallback Callback = delegate(IAsyncResult asyncResult)
		{
			BeginEndAwaitableAdapter beginEndAwaitableAdapter = (BeginEndAwaitableAdapter)asyncResult.AsyncState;
			beginEndAwaitableAdapter._asyncResult = asyncResult;
			Action action = Interlocked.Exchange<Action>(ref beginEndAwaitableAdapter._continuation, BeginEndAwaitableAdapter.CALLBACK_RAN);
			if (action != null)
			{
				action();
			}
		};

		// Token: 0x02000C35 RID: 3125
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06007036 RID: 28726 RVA: 0x00182A8C File Offset: 0x00180C8C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06007037 RID: 28727 RVA: 0x00182A98 File Offset: 0x00180C98
			public <>c()
			{
			}

			// Token: 0x06007038 RID: 28728 RVA: 0x00182AA0 File Offset: 0x00180CA0
			internal void <.cctor>b__11_0()
			{
			}

			// Token: 0x06007039 RID: 28729 RVA: 0x00182AA4 File Offset: 0x00180CA4
			internal void <.cctor>b__11_1(IAsyncResult asyncResult)
			{
				BeginEndAwaitableAdapter beginEndAwaitableAdapter = (BeginEndAwaitableAdapter)asyncResult.AsyncState;
				beginEndAwaitableAdapter._asyncResult = asyncResult;
				Action action = Interlocked.Exchange<Action>(ref beginEndAwaitableAdapter._continuation, BeginEndAwaitableAdapter.CALLBACK_RAN);
				if (action != null)
				{
					action();
				}
			}

			// Token: 0x04003728 RID: 14120
			public static readonly BeginEndAwaitableAdapter.<>c <>9 = new BeginEndAwaitableAdapter.<>c();
		}
	}
}
