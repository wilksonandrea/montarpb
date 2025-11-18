using System;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200081D RID: 2077
	internal class WorkItem
	{
		// Token: 0x06005907 RID: 22791 RVA: 0x001397AA File Offset: 0x001379AA
		[SecuritySafeCritical]
		static WorkItem()
		{
		}

		// Token: 0x06005908 RID: 22792 RVA: 0x001397C0 File Offset: 0x001379C0
		[SecurityCritical]
		internal WorkItem(IMessage reqMsg, IMessageSink nextSink, IMessageSink replySink)
		{
			this._reqMsg = reqMsg;
			this._replyMsg = null;
			this._nextSink = nextSink;
			this._replySink = replySink;
			this._ctx = Thread.CurrentContext;
			this._callCtx = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
		}

		// Token: 0x06005909 RID: 22793 RVA: 0x0013980F File Offset: 0x00137A0F
		internal virtual void SetWaiting()
		{
			this._flags |= 1;
		}

		// Token: 0x0600590A RID: 22794 RVA: 0x0013981F File Offset: 0x00137A1F
		internal virtual bool IsWaiting()
		{
			return (this._flags & 1) == 1;
		}

		// Token: 0x0600590B RID: 22795 RVA: 0x0013982C File Offset: 0x00137A2C
		internal virtual void SetSignaled()
		{
			this._flags |= 2;
		}

		// Token: 0x0600590C RID: 22796 RVA: 0x0013983C File Offset: 0x00137A3C
		internal virtual bool IsSignaled()
		{
			return (this._flags & 2) == 2;
		}

		// Token: 0x0600590D RID: 22797 RVA: 0x00139849 File Offset: 0x00137A49
		internal virtual void SetAsync()
		{
			this._flags |= 4;
		}

		// Token: 0x0600590E RID: 22798 RVA: 0x00139859 File Offset: 0x00137A59
		internal virtual bool IsAsync()
		{
			return (this._flags & 4) == 4;
		}

		// Token: 0x0600590F RID: 22799 RVA: 0x00139866 File Offset: 0x00137A66
		internal virtual void SetDummy()
		{
			this._flags |= 8;
		}

		// Token: 0x06005910 RID: 22800 RVA: 0x00139876 File Offset: 0x00137A76
		internal virtual bool IsDummy()
		{
			return (this._flags & 8) == 8;
		}

		// Token: 0x06005911 RID: 22801 RVA: 0x00139884 File Offset: 0x00137A84
		[SecurityCritical]
		internal static object ExecuteCallback(object[] args)
		{
			WorkItem workItem = (WorkItem)args[0];
			if (workItem.IsAsync())
			{
				workItem._nextSink.AsyncProcessMessage(workItem._reqMsg, workItem._replySink);
			}
			else if (workItem._nextSink != null)
			{
				workItem._replyMsg = workItem._nextSink.SyncProcessMessage(workItem._reqMsg);
			}
			return null;
		}

		// Token: 0x06005912 RID: 22802 RVA: 0x001398DC File Offset: 0x00137ADC
		[SecurityCritical]
		internal virtual void Execute()
		{
			Thread.CurrentThread.InternalCrossContextCallback(this._ctx, WorkItem._xctxDel, new object[] { this });
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06005913 RID: 22803 RVA: 0x001398FE File Offset: 0x00137AFE
		internal virtual IMessage ReplyMessage
		{
			get
			{
				return this._replyMsg;
			}
		}

		// Token: 0x04002894 RID: 10388
		private const int FLG_WAITING = 1;

		// Token: 0x04002895 RID: 10389
		private const int FLG_SIGNALED = 2;

		// Token: 0x04002896 RID: 10390
		private const int FLG_ASYNC = 4;

		// Token: 0x04002897 RID: 10391
		private const int FLG_DUMMY = 8;

		// Token: 0x04002898 RID: 10392
		internal int _flags;

		// Token: 0x04002899 RID: 10393
		internal IMessage _reqMsg;

		// Token: 0x0400289A RID: 10394
		internal IMessageSink _nextSink;

		// Token: 0x0400289B RID: 10395
		internal IMessageSink _replySink;

		// Token: 0x0400289C RID: 10396
		internal IMessage _replyMsg;

		// Token: 0x0400289D RID: 10397
		internal Context _ctx;

		// Token: 0x0400289E RID: 10398
		[SecurityCritical]
		internal LogicalCallContext _callCtx;

		// Token: 0x0400289F RID: 10399
		internal static InternalCrossContextDelegate _xctxDel = new InternalCrossContextDelegate(WorkItem.ExecuteCallback);
	}
}
