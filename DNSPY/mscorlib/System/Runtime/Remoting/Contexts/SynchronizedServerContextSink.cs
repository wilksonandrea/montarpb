using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200081C RID: 2076
	internal class SynchronizedServerContextSink : InternalSink, IMessageSink
	{
		// Token: 0x06005902 RID: 22786 RVA: 0x001396F8 File Offset: 0x001378F8
		[SecurityCritical]
		internal SynchronizedServerContextSink(SynchronizationAttribute prop, IMessageSink nextSink)
		{
			this._property = prop;
			this._nextSink = nextSink;
		}

		// Token: 0x06005903 RID: 22787 RVA: 0x00139710 File Offset: 0x00137910
		[SecuritySafeCritical]
		~SynchronizedServerContextSink()
		{
			this._property.Dispose();
		}

		// Token: 0x06005904 RID: 22788 RVA: 0x00139744 File Offset: 0x00137944
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			WorkItem workItem = new WorkItem(reqMsg, this._nextSink, null);
			this._property.HandleWorkRequest(workItem);
			return workItem.ReplyMessage;
		}

		// Token: 0x06005905 RID: 22789 RVA: 0x00139774 File Offset: 0x00137974
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			WorkItem workItem = new WorkItem(reqMsg, this._nextSink, replySink);
			workItem.SetAsync();
			this._property.HandleWorkRequest(workItem);
			return null;
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06005906 RID: 22790 RVA: 0x001397A2 File Offset: 0x001379A2
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x04002892 RID: 10386
		internal IMessageSink _nextSink;

		// Token: 0x04002893 RID: 10387
		[SecurityCritical]
		internal SynchronizationAttribute _property;
	}
}
