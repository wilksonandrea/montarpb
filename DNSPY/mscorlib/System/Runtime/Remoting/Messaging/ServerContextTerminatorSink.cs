using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000887 RID: 2183
	[Serializable]
	internal class ServerContextTerminatorSink : InternalSink, IMessageSink
	{
		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06005C98 RID: 23704 RVA: 0x00144A88 File Offset: 0x00142C88
		internal static IMessageSink MessageSink
		{
			get
			{
				if (ServerContextTerminatorSink.messageSink == null)
				{
					ServerContextTerminatorSink serverContextTerminatorSink = new ServerContextTerminatorSink();
					object obj = ServerContextTerminatorSink.staticSyncObject;
					lock (obj)
					{
						if (ServerContextTerminatorSink.messageSink == null)
						{
							ServerContextTerminatorSink.messageSink = serverContextTerminatorSink;
						}
					}
				}
				return ServerContextTerminatorSink.messageSink;
			}
		}

		// Token: 0x06005C99 RID: 23705 RVA: 0x00144AE8 File Offset: 0x00142CE8
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message != null)
			{
				return message;
			}
			Context currentContext = Thread.CurrentContext;
			IMessage message2;
			if (reqMsg is IConstructionCallMessage)
			{
				message = currentContext.NotifyActivatorProperties(reqMsg, true);
				if (message != null)
				{
					return message;
				}
				message2 = ((IConstructionCallMessage)reqMsg).Activator.Activate((IConstructionCallMessage)reqMsg);
				message = currentContext.NotifyActivatorProperties(message2, true);
				if (message != null)
				{
					return message;
				}
			}
			else
			{
				MarshalByRefObject marshalByRefObject = null;
				try
				{
					message2 = this.GetObjectChain(reqMsg, out marshalByRefObject).SyncProcessMessage(reqMsg);
				}
				finally
				{
					IDisposable disposable;
					if (marshalByRefObject != null && (disposable = marshalByRefObject as IDisposable) != null)
					{
						disposable.Dispose();
					}
				}
			}
			return message2;
		}

		// Token: 0x06005C9A RID: 23706 RVA: 0x00144B80 File Offset: 0x00142D80
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			IMessageCtrl messageCtrl = null;
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message == null)
			{
				message = InternalSink.DisallowAsyncActivation(reqMsg);
			}
			if (message != null)
			{
				if (replySink != null)
				{
					replySink.SyncProcessMessage(message);
				}
			}
			else
			{
				MarshalByRefObject marshalByRefObject;
				IMessageSink objectChain = this.GetObjectChain(reqMsg, out marshalByRefObject);
				IDisposable disposable;
				if (marshalByRefObject != null && (disposable = marshalByRefObject as IDisposable) != null)
				{
					DisposeSink disposeSink = new DisposeSink(disposable, replySink);
					replySink = disposeSink;
				}
				messageCtrl = objectChain.AsyncProcessMessage(reqMsg, replySink);
			}
			return messageCtrl;
		}

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06005C9B RID: 23707 RVA: 0x00144BE0 File Offset: 0x00142DE0
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x06005C9C RID: 23708 RVA: 0x00144BE4 File Offset: 0x00142DE4
		[SecurityCritical]
		internal virtual IMessageSink GetObjectChain(IMessage reqMsg, out MarshalByRefObject obj)
		{
			ServerIdentity serverIdentity = InternalSink.GetServerIdentity(reqMsg);
			return serverIdentity.GetServerObjectChain(out obj);
		}

		// Token: 0x06005C9D RID: 23709 RVA: 0x00144BFF File Offset: 0x00142DFF
		public ServerContextTerminatorSink()
		{
		}

		// Token: 0x06005C9E RID: 23710 RVA: 0x00144C07 File Offset: 0x00142E07
		// Note: this type is marked as 'beforefieldinit'.
		static ServerContextTerminatorSink()
		{
		}

		// Token: 0x040029D5 RID: 10709
		private static volatile ServerContextTerminatorSink messageSink;

		// Token: 0x040029D6 RID: 10710
		private static object staticSyncObject = new object();
	}
}
