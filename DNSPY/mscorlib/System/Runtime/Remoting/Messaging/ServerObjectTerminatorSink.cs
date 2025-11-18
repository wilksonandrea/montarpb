using System;
using System.Runtime.Remoting.Contexts;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000889 RID: 2185
	[Serializable]
	internal class ServerObjectTerminatorSink : InternalSink, IMessageSink
	{
		// Token: 0x06005CA3 RID: 23715 RVA: 0x00144C7F File Offset: 0x00142E7F
		internal ServerObjectTerminatorSink(MarshalByRefObject srvObj)
		{
			this._stackBuilderSink = new StackBuilderSink(srvObj);
		}

		// Token: 0x06005CA4 RID: 23716 RVA: 0x00144C94 File Offset: 0x00142E94
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message != null)
			{
				return message;
			}
			ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
			ArrayWithSize serverSideDynamicSinks = serverIdentity.ServerSideDynamicSinks;
			if (serverSideDynamicSinks != null)
			{
				DynamicPropertyHolder.NotifyDynamicSinks(reqMsg, serverSideDynamicSinks, false, true, false);
			}
			IMessageSink messageSink = this._stackBuilderSink.ServerObject as IMessageSink;
			IMessage message2;
			if (messageSink != null)
			{
				message2 = messageSink.SyncProcessMessage(reqMsg);
			}
			else
			{
				message2 = this._stackBuilderSink.SyncProcessMessage(reqMsg);
			}
			if (serverSideDynamicSinks != null)
			{
				DynamicPropertyHolder.NotifyDynamicSinks(message2, serverSideDynamicSinks, false, false, false);
			}
			return message2;
		}

		// Token: 0x06005CA5 RID: 23717 RVA: 0x00144D04 File Offset: 0x00142F04
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			IMessageCtrl messageCtrl = null;
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message != null)
			{
				if (replySink != null)
				{
					replySink.SyncProcessMessage(message);
				}
			}
			else
			{
				IMessageSink messageSink = this._stackBuilderSink.ServerObject as IMessageSink;
				if (messageSink != null)
				{
					messageCtrl = messageSink.AsyncProcessMessage(reqMsg, replySink);
				}
				else
				{
					messageCtrl = this._stackBuilderSink.AsyncProcessMessage(reqMsg, replySink);
				}
			}
			return messageCtrl;
		}

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06005CA6 RID: 23718 RVA: 0x00144D58 File Offset: 0x00142F58
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x040029D9 RID: 10713
		internal StackBuilderSink _stackBuilderSink;
	}
}
