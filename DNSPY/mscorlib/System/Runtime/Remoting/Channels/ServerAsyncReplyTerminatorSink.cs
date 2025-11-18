using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200082E RID: 2094
	internal class ServerAsyncReplyTerminatorSink : IMessageSink
	{
		// Token: 0x060059A2 RID: 22946 RVA: 0x0013BE74 File Offset: 0x0013A074
		internal ServerAsyncReplyTerminatorSink(IMessageSink nextSink)
		{
			this._nextSink = nextSink;
		}

		// Token: 0x060059A3 RID: 22947 RVA: 0x0013BE84 File Offset: 0x0013A084
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage replyMsg)
		{
			Guid guid;
			RemotingServices.CORProfilerRemotingServerSendingReply(out guid, true);
			if (RemotingServices.CORProfilerTrackRemotingCookie())
			{
				replyMsg.Properties["CORProfilerCookie"] = guid;
			}
			return this._nextSink.SyncProcessMessage(replyMsg);
		}

		// Token: 0x060059A4 RID: 22948 RVA: 0x0013BEC2 File Offset: 0x0013A0C2
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage replyMsg, IMessageSink replySink)
		{
			return null;
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x060059A5 RID: 22949 RVA: 0x0013BEC5 File Offset: 0x0013A0C5
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._nextSink;
			}
		}

		// Token: 0x040028DA RID: 10458
		internal IMessageSink _nextSink;
	}
}
