using System;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000884 RID: 2180
	[Serializable]
	internal class EnvoyTerminatorSink : InternalSink, IMessageSink
	{
		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06005C84 RID: 23684 RVA: 0x00144644 File Offset: 0x00142844
		internal static IMessageSink MessageSink
		{
			get
			{
				if (EnvoyTerminatorSink.messageSink == null)
				{
					EnvoyTerminatorSink envoyTerminatorSink = new EnvoyTerminatorSink();
					object obj = EnvoyTerminatorSink.staticSyncObject;
					lock (obj)
					{
						if (EnvoyTerminatorSink.messageSink == null)
						{
							EnvoyTerminatorSink.messageSink = envoyTerminatorSink;
						}
					}
				}
				return EnvoyTerminatorSink.messageSink;
			}
		}

		// Token: 0x06005C85 RID: 23685 RVA: 0x001446A4 File Offset: 0x001428A4
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message = InternalSink.ValidateMessage(reqMsg);
			if (message != null)
			{
				return message;
			}
			return Thread.CurrentContext.GetClientContextChain().SyncProcessMessage(reqMsg);
		}

		// Token: 0x06005C86 RID: 23686 RVA: 0x001446D0 File Offset: 0x001428D0
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
				messageCtrl = Thread.CurrentContext.GetClientContextChain().AsyncProcessMessage(reqMsg, replySink);
			}
			return messageCtrl;
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06005C87 RID: 23687 RVA: 0x00144709 File Offset: 0x00142909
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x06005C88 RID: 23688 RVA: 0x0014470C File Offset: 0x0014290C
		public EnvoyTerminatorSink()
		{
		}

		// Token: 0x06005C89 RID: 23689 RVA: 0x00144714 File Offset: 0x00142914
		// Note: this type is marked as 'beforefieldinit'.
		static EnvoyTerminatorSink()
		{
		}

		// Token: 0x040029CF RID: 10703
		private static volatile EnvoyTerminatorSink messageSink;

		// Token: 0x040029D0 RID: 10704
		private static object staticSyncObject = new object();
	}
}
