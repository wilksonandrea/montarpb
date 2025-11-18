using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000831 RID: 2097
	[SecurityCritical]
	[ComVisible(true)]
	public class ClientChannelSinkStack : IClientChannelSinkStack, IClientResponseChannelSinkStack
	{
		// Token: 0x060059AB RID: 22955 RVA: 0x0013BECD File Offset: 0x0013A0CD
		public ClientChannelSinkStack()
		{
		}

		// Token: 0x060059AC RID: 22956 RVA: 0x0013BED5 File Offset: 0x0013A0D5
		public ClientChannelSinkStack(IMessageSink replySink)
		{
			this._replySink = replySink;
		}

		// Token: 0x060059AD RID: 22957 RVA: 0x0013BEE4 File Offset: 0x0013A0E4
		[SecurityCritical]
		public void Push(IClientChannelSink sink, object state)
		{
			this._stack = new ClientChannelSinkStack.SinkStack
			{
				PrevStack = this._stack,
				Sink = sink,
				State = state
			};
		}

		// Token: 0x060059AE RID: 22958 RVA: 0x0013BF18 File Offset: 0x0013A118
		[SecurityCritical]
		public object Pop(IClientChannelSink sink)
		{
			if (this._stack == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_PopOnEmptySinkStack"));
			}
			while (this._stack.Sink != sink)
			{
				this._stack = this._stack.PrevStack;
				if (this._stack == null)
				{
					break;
				}
			}
			if (this._stack.Sink == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_PopFromSinkStackWithoutPush"));
			}
			object state = this._stack.State;
			this._stack = this._stack.PrevStack;
			return state;
		}

		// Token: 0x060059AF RID: 22959 RVA: 0x0013BFA0 File Offset: 0x0013A1A0
		[SecurityCritical]
		public void AsyncProcessResponse(ITransportHeaders headers, Stream stream)
		{
			if (this._replySink != null)
			{
				if (this._stack == null)
				{
					throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallAPRWhenStackEmpty"));
				}
				IClientChannelSink sink = this._stack.Sink;
				object state = this._stack.State;
				this._stack = this._stack.PrevStack;
				sink.AsyncProcessResponse(this, state, headers, stream);
			}
		}

		// Token: 0x060059B0 RID: 22960 RVA: 0x0013C000 File Offset: 0x0013A200
		[SecurityCritical]
		public void DispatchReplyMessage(IMessage msg)
		{
			if (this._replySink != null)
			{
				this._replySink.SyncProcessMessage(msg);
			}
		}

		// Token: 0x060059B1 RID: 22961 RVA: 0x0013C017 File Offset: 0x0013A217
		[SecurityCritical]
		public void DispatchException(Exception e)
		{
			this.DispatchReplyMessage(new ReturnMessage(e, null));
		}

		// Token: 0x040028DB RID: 10459
		private ClientChannelSinkStack.SinkStack _stack;

		// Token: 0x040028DC RID: 10460
		private IMessageSink _replySink;

		// Token: 0x02000C79 RID: 3193
		private class SinkStack
		{
			// Token: 0x060070BE RID: 28862 RVA: 0x0018477B File Offset: 0x0018297B
			public SinkStack()
			{
			}

			// Token: 0x0400380A RID: 14346
			public ClientChannelSinkStack.SinkStack PrevStack;

			// Token: 0x0400380B RID: 14347
			public IClientChannelSink Sink;

			// Token: 0x0400380C RID: 14348
			public object State;
		}
	}
}
