using System;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200088A RID: 2186
	internal class ClientAsyncReplyTerminatorSink : IMessageSink
	{
		// Token: 0x06005CA7 RID: 23719 RVA: 0x00144D5B File Offset: 0x00142F5B
		internal ClientAsyncReplyTerminatorSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x06005CA8 RID: 23720 RVA: 0x00144D6C File Offset: 0x00142F6C
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage replyMsg)
		{
			Guid guid = Guid.Empty;
			if (RemotingServices.CORProfilerTrackRemotingCookie())
			{
				object obj = replyMsg.Properties["CORProfilerCookie"];
				if (obj != null)
				{
					guid = (Guid)obj;
				}
			}
			RemotingServices.CORProfilerRemotingClientReceivingReply(guid, true);
			return this._nextSink.SyncProcessMessage(replyMsg);
		}

		// Token: 0x06005CA9 RID: 23721 RVA: 0x00144DB4 File Offset: 0x00142FB4
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage replyMsg, IMessageSink replySink)
		{
			return null;
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06005CAA RID: 23722 RVA: 0x00144DB7 File Offset: 0x00142FB7
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x040029DA RID: 10714
		internal IMessageSink _nextSink;
	}
}
