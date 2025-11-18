using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000885 RID: 2181
	internal class ClientContextTerminatorSink : InternalSink, IMessageSink
	{
		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06005C8A RID: 23690 RVA: 0x00144720 File Offset: 0x00142920
		internal static IMessageSink MessageSink
		{
			get
			{
				if (ClientContextTerminatorSink.messageSink == null)
				{
					ClientContextTerminatorSink clientContextTerminatorSink = new ClientContextTerminatorSink();
					object obj = ClientContextTerminatorSink.staticSyncObject;
					lock (obj)
					{
						if (ClientContextTerminatorSink.messageSink == null)
						{
							ClientContextTerminatorSink.messageSink = clientContextTerminatorSink;
						}
					}
				}
				return ClientContextTerminatorSink.messageSink;
			}
		}

		// Token: 0x06005C8B RID: 23691 RVA: 0x00144780 File Offset: 0x00142980
		[SecurityCritical]
		internal static object SyncProcessMessageCallback(object[] args)
		{
			IMessage message = (IMessage)args[0];
			IMessageSink messageSink = (IMessageSink)args[1];
			return messageSink.SyncProcessMessage(message);
		}

		// Token: 0x06005C8C RID: 23692 RVA: 0x001447A8 File Offset: 0x001429A8
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message != null)
			{
				return message;
			}
			Context currentContext = Thread.CurrentContext;
			bool flag = currentContext.NotifyDynamicSinks(reqMsg, true, true, false, true);
			IMessage message2;
			if (reqMsg is IConstructionCallMessage)
			{
				message = currentContext.NotifyActivatorProperties(reqMsg, false);
				if (message != null)
				{
					return message;
				}
				message2 = ((IConstructionCallMessage)reqMsg).Activator.Activate((IConstructionCallMessage)reqMsg);
				message = currentContext.NotifyActivatorProperties(message2, false);
				if (message != null)
				{
					return message;
				}
			}
			else
			{
				ChannelServices.NotifyProfiler(reqMsg, RemotingProfilerEvent.ClientSend);
				object[] array = new object[2];
				IMessageSink channelSink = this.GetChannelSink(reqMsg);
				array[0] = reqMsg;
				array[1] = channelSink;
				InternalCrossContextDelegate internalCrossContextDelegate = new InternalCrossContextDelegate(ClientContextTerminatorSink.SyncProcessMessageCallback);
				if (channelSink != CrossContextChannel.MessageSink)
				{
					message2 = (IMessage)Thread.CurrentThread.InternalCrossContextCallback(Context.DefaultContext, internalCrossContextDelegate, array);
				}
				else
				{
					message2 = (IMessage)internalCrossContextDelegate(array);
				}
				ChannelServices.NotifyProfiler(message2, RemotingProfilerEvent.ClientReceive);
			}
			if (flag)
			{
				currentContext.NotifyDynamicSinks(reqMsg, true, false, false, true);
			}
			return message2;
		}

		// Token: 0x06005C8D RID: 23693 RVA: 0x0014488C File Offset: 0x00142A8C
		[SecurityCritical]
		internal static object AsyncProcessMessageCallback(object[] args)
		{
			IMessage message = (IMessage)args[0];
			IMessageSink messageSink = (IMessageSink)args[1];
			IMessageSink messageSink2 = (IMessageSink)args[2];
			return messageSink2.AsyncProcessMessage(message, messageSink);
		}

		// Token: 0x06005C8E RID: 23694 RVA: 0x001448BC File Offset: 0x00142ABC
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			IMessageCtrl messageCtrl = null;
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
				if (RemotingServices.CORProfilerTrackRemotingAsync())
				{
					Guid guid;
					RemotingServices.CORProfilerRemotingClientSendingMessage(out guid, true);
					if (RemotingServices.CORProfilerTrackRemotingCookie())
					{
						reqMsg.Properties["CORProfilerCookie"] = guid;
					}
					if (replySink != null)
					{
						IMessageSink messageSink = new ClientAsyncReplyTerminatorSink(replySink);
						replySink = messageSink;
					}
				}
				Context currentContext = Thread.CurrentContext;
				currentContext.NotifyDynamicSinks(reqMsg, true, true, true, true);
				if (replySink != null)
				{
					replySink = new AsyncReplySink(replySink, currentContext);
				}
				object[] array = new object[3];
				InternalCrossContextDelegate internalCrossContextDelegate = new InternalCrossContextDelegate(ClientContextTerminatorSink.AsyncProcessMessageCallback);
				IMessageSink channelSink = this.GetChannelSink(reqMsg);
				array[0] = reqMsg;
				array[1] = replySink;
				array[2] = channelSink;
				if (channelSink != CrossContextChannel.MessageSink)
				{
					messageCtrl = (IMessageCtrl)Thread.CurrentThread.InternalCrossContextCallback(Context.DefaultContext, internalCrossContextDelegate, array);
				}
				else
				{
					messageCtrl = (IMessageCtrl)internalCrossContextDelegate(array);
				}
			}
			return messageCtrl;
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06005C8F RID: 23695 RVA: 0x001449A9 File Offset: 0x00142BA9
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x06005C90 RID: 23696 RVA: 0x001449AC File Offset: 0x00142BAC
		[SecurityCritical]
		private IMessageSink GetChannelSink(IMessage reqMsg)
		{
			Identity identity = InternalSink.GetIdentity(reqMsg);
			return identity.ChannelSink;
		}

		// Token: 0x06005C91 RID: 23697 RVA: 0x001449C6 File Offset: 0x00142BC6
		public ClientContextTerminatorSink()
		{
		}

		// Token: 0x06005C92 RID: 23698 RVA: 0x001449CE File Offset: 0x00142BCE
		// Note: this type is marked as 'beforefieldinit'.
		static ClientContextTerminatorSink()
		{
		}

		// Token: 0x040029D1 RID: 10705
		private static volatile ClientContextTerminatorSink messageSink;

		// Token: 0x040029D2 RID: 10706
		private static object staticSyncObject = new object();
	}
}
