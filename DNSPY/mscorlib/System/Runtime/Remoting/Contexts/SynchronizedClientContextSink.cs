using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200081E RID: 2078
	internal class SynchronizedClientContextSink : InternalSink, IMessageSink
	{
		// Token: 0x06005914 RID: 22804 RVA: 0x00139906 File Offset: 0x00137B06
		[SecurityCritical]
		internal SynchronizedClientContextSink(SynchronizationAttribute prop, IMessageSink nextSink)
		{
			this._property = prop;
			this._nextSink = nextSink;
		}

		// Token: 0x06005915 RID: 22805 RVA: 0x0013991C File Offset: 0x00137B1C
		[SecuritySafeCritical]
		~SynchronizedClientContextSink()
		{
			this._property.Dispose();
		}

		// Token: 0x06005916 RID: 22806 RVA: 0x00139950 File Offset: 0x00137B50
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message;
			if (this._property.IsReEntrant)
			{
				this._property.HandleThreadExit();
				message = this._nextSink.SyncProcessMessage(reqMsg);
				this._property.HandleThreadReEntry();
			}
			else
			{
				LogicalCallContext logicalCallContext = (LogicalCallContext)reqMsg.Properties[Message.CallContextKey];
				string text = logicalCallContext.RemotingData.LogicalCallID;
				bool flag = false;
				if (text == null)
				{
					text = Identity.GetNewLogicalCallID();
					logicalCallContext.RemotingData.LogicalCallID = text;
					flag = true;
				}
				bool flag2 = false;
				if (this._property.SyncCallOutLCID == null)
				{
					this._property.SyncCallOutLCID = text;
					flag2 = true;
				}
				message = this._nextSink.SyncProcessMessage(reqMsg);
				if (flag2)
				{
					this._property.SyncCallOutLCID = null;
					if (flag)
					{
						LogicalCallContext logicalCallContext2 = (LogicalCallContext)message.Properties[Message.CallContextKey];
						logicalCallContext2.RemotingData.LogicalCallID = null;
					}
				}
			}
			return message;
		}

		// Token: 0x06005917 RID: 22807 RVA: 0x00139A34 File Offset: 0x00137C34
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			if (!this._property.IsReEntrant)
			{
				LogicalCallContext logicalCallContext = (LogicalCallContext)reqMsg.Properties[Message.CallContextKey];
				string newLogicalCallID = Identity.GetNewLogicalCallID();
				logicalCallContext.RemotingData.LogicalCallID = newLogicalCallID;
				this._property.AsyncCallOutLCIDList.Add(newLogicalCallID);
			}
			SynchronizedClientContextSink.AsyncReplySink asyncReplySink = new SynchronizedClientContextSink.AsyncReplySink(replySink, this._property);
			return this._nextSink.AsyncProcessMessage(reqMsg, asyncReplySink);
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06005918 RID: 22808 RVA: 0x00139AA6 File Offset: 0x00137CA6
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x040028A0 RID: 10400
		internal IMessageSink _nextSink;

		// Token: 0x040028A1 RID: 10401
		[SecurityCritical]
		internal SynchronizationAttribute _property;

		// Token: 0x02000C74 RID: 3188
		internal class AsyncReplySink : IMessageSink
		{
			// Token: 0x060070B4 RID: 28852 RVA: 0x001846B9 File Offset: 0x001828B9
			[SecurityCritical]
			internal AsyncReplySink(IMessageSink nextSink, SynchronizationAttribute prop)
			{
				this._nextSink = nextSink;
				this._property = prop;
			}

			// Token: 0x060070B5 RID: 28853 RVA: 0x001846D0 File Offset: 0x001828D0
			[SecurityCritical]
			public virtual IMessage SyncProcessMessage(IMessage reqMsg)
			{
				WorkItem workItem = new WorkItem(reqMsg, this._nextSink, null);
				this._property.HandleWorkRequest(workItem);
				if (!this._property.IsReEntrant)
				{
					this._property.AsyncCallOutLCIDList.Remove(((LogicalCallContext)reqMsg.Properties[Message.CallContextKey]).RemotingData.LogicalCallID);
				}
				return workItem.ReplyMessage;
			}

			// Token: 0x060070B6 RID: 28854 RVA: 0x00184739 File Offset: 0x00182939
			[SecurityCritical]
			public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
			{
				throw new NotSupportedException();
			}

			// Token: 0x17001352 RID: 4946
			// (get) Token: 0x060070B7 RID: 28855 RVA: 0x00184740 File Offset: 0x00182940
			public IMessageSink NextSink
			{
				[SecurityCritical]
				get
				{
					return this._nextSink;
				}
			}

			// Token: 0x040037FF RID: 14335
			internal IMessageSink _nextSink;

			// Token: 0x04003800 RID: 14336
			[SecurityCritical]
			internal SynchronizationAttribute _property;
		}
	}
}
