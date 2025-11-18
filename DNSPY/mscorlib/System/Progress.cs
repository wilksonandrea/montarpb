using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System
{
	// Token: 0x02000124 RID: 292
	[__DynamicallyInvokable]
	public class Progress<T> : IProgress<T>
	{
		// Token: 0x060010F0 RID: 4336 RVA: 0x00032F33 File Offset: 0x00031133
		[__DynamicallyInvokable]
		public Progress()
		{
			this.m_synchronizationContext = SynchronizationContext.CurrentNoFlow ?? ProgressStatics.DefaultContext;
			this.m_invokeHandlers = new SendOrPostCallback(this.InvokeHandlers);
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00032F61 File Offset: 0x00031161
		[__DynamicallyInvokable]
		public Progress(Action<T> handler)
			: this()
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.m_handler = handler;
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060010F2 RID: 4338 RVA: 0x00032F80 File Offset: 0x00031180
		// (remove) Token: 0x060010F3 RID: 4339 RVA: 0x00032FB8 File Offset: 0x000311B8
		[__DynamicallyInvokable]
		public event EventHandler<T> ProgressChanged
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			add
			{
				EventHandler<T> eventHandler = this.ProgressChanged;
				EventHandler<T> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<T> eventHandler3 = (EventHandler<T>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<T>>(ref this.ProgressChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			[__DynamicallyInvokable]
			remove
			{
				EventHandler<T> eventHandler = this.ProgressChanged;
				EventHandler<T> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<T> eventHandler3 = (EventHandler<T>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<T>>(ref this.ProgressChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00032FF0 File Offset: 0x000311F0
		[__DynamicallyInvokable]
		protected virtual void OnReport(T value)
		{
			Action<T> handler = this.m_handler;
			EventHandler<T> progressChanged = this.ProgressChanged;
			if (handler != null || progressChanged != null)
			{
				this.m_synchronizationContext.Post(this.m_invokeHandlers, value);
			}
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00033028 File Offset: 0x00031228
		[__DynamicallyInvokable]
		void IProgress<T>.Report(T value)
		{
			this.OnReport(value);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00033034 File Offset: 0x00031234
		private void InvokeHandlers(object state)
		{
			T t = (T)((object)state);
			Action<T> handler = this.m_handler;
			EventHandler<T> progressChanged = this.ProgressChanged;
			if (handler != null)
			{
				handler(t);
			}
			if (progressChanged != null)
			{
				progressChanged(this, t);
			}
		}

		// Token: 0x040005EB RID: 1515
		private readonly SynchronizationContext m_synchronizationContext;

		// Token: 0x040005EC RID: 1516
		private readonly Action<T> m_handler;

		// Token: 0x040005ED RID: 1517
		private readonly SendOrPostCallback m_invokeHandlers;

		// Token: 0x040005EE RID: 1518
		[CompilerGenerated]
		private EventHandler<T> ProgressChanged;
	}
}
