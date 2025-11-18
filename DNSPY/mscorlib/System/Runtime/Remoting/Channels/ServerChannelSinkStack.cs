using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000834 RID: 2100
	[SecurityCritical]
	[ComVisible(true)]
	public class ServerChannelSinkStack : IServerChannelSinkStack, IServerResponseChannelSinkStack
	{
		// Token: 0x060059B9 RID: 22969 RVA: 0x0013C028 File Offset: 0x0013A228
		[SecurityCritical]
		public void Push(IServerChannelSink sink, object state)
		{
			this._stack = new ServerChannelSinkStack.SinkStack
			{
				PrevStack = this._stack,
				Sink = sink,
				State = state
			};
		}

		// Token: 0x060059BA RID: 22970 RVA: 0x0013C05C File Offset: 0x0013A25C
		[SecurityCritical]
		public object Pop(IServerChannelSink sink)
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

		// Token: 0x060059BB RID: 22971 RVA: 0x0013C0E4 File Offset: 0x0013A2E4
		[SecurityCritical]
		public void Store(IServerChannelSink sink, object state)
		{
			if (this._stack == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_StoreOnEmptySinkStack"));
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
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_StoreOnSinkStackWithoutPush"));
			}
			this._rememberedStack = new ServerChannelSinkStack.SinkStack
			{
				PrevStack = this._rememberedStack,
				Sink = sink,
				State = state
			};
			this.Pop(sink);
		}

		// Token: 0x060059BC RID: 22972 RVA: 0x0013C17C File Offset: 0x0013A37C
		[SecurityCritical]
		public void StoreAndDispatch(IServerChannelSink sink, object state)
		{
			this.Store(sink, state);
			this.FlipRememberedStack();
			CrossContextChannel.DoAsyncDispatch(this._asyncMsg, null);
		}

		// Token: 0x060059BD RID: 22973 RVA: 0x0013C19C File Offset: 0x0013A39C
		private void FlipRememberedStack()
		{
			if (this._stack != null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallFRSWhenStackEmtpy"));
			}
			while (this._rememberedStack != null)
			{
				this._stack = new ServerChannelSinkStack.SinkStack
				{
					PrevStack = this._stack,
					Sink = this._rememberedStack.Sink,
					State = this._rememberedStack.State
				};
				this._rememberedStack = this._rememberedStack.PrevStack;
			}
		}

		// Token: 0x060059BE RID: 22974 RVA: 0x0013C218 File Offset: 0x0013A418
		[SecurityCritical]
		public void AsyncProcessResponse(IMessage msg, ITransportHeaders headers, Stream stream)
		{
			if (this._stack == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallAPRWhenStackEmpty"));
			}
			IServerChannelSink sink = this._stack.Sink;
			object state = this._stack.State;
			this._stack = this._stack.PrevStack;
			sink.AsyncProcessResponse(this, state, msg, headers, stream);
		}

		// Token: 0x060059BF RID: 22975 RVA: 0x0013C274 File Offset: 0x0013A474
		[SecurityCritical]
		public Stream GetResponseStream(IMessage msg, ITransportHeaders headers)
		{
			if (this._stack == null)
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CantCallGetResponseStreamWhenStackEmpty"));
			}
			IServerChannelSink sink = this._stack.Sink;
			object state = this._stack.State;
			this._stack = this._stack.PrevStack;
			Stream responseStream = sink.GetResponseStream(this, state, msg, headers);
			this.Push(sink, state);
			return responseStream;
		}

		// Token: 0x17000EDE RID: 3806
		// (set) Token: 0x060059C0 RID: 22976 RVA: 0x0013C2D6 File Offset: 0x0013A4D6
		internal object ServerObject
		{
			set
			{
				this._serverObject = value;
			}
		}

		// Token: 0x060059C1 RID: 22977 RVA: 0x0013C2E0 File Offset: 0x0013A4E0
		[SecurityCritical]
		public void ServerCallback(IAsyncResult ar)
		{
			if (this._asyncEnd != null)
			{
				RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this._asyncEnd);
				MethodInfo methodInfo = (MethodInfo)this._msg.MethodBase;
				RemotingMethodCachedData reflectionCachedData2 = InternalRemotingServices.GetReflectionCachedData(methodInfo);
				ParameterInfo[] parameters = reflectionCachedData.Parameters;
				object[] array = new object[parameters.Length];
				array[parameters.Length - 1] = ar;
				object[] args = this._msg.Args;
				AsyncMessageHelper.GetOutArgs(reflectionCachedData2.Parameters, args, array);
				StackBuilderSink stackBuilderSink = new StackBuilderSink(this._serverObject);
				object[] array2;
				object obj = stackBuilderSink.PrivateProcessMessage(this._asyncEnd.MethodHandle, Message.CoerceArgs(this._asyncEnd, array, parameters), this._serverObject, out array2);
				if (array2 != null)
				{
					array2 = ArgMapper.ExpandAsyncEndArgsToSyncArgs(reflectionCachedData2, array2);
				}
				stackBuilderSink.CopyNonByrefOutArgsFromOriginalArgs(reflectionCachedData2, args, ref array2);
				IMessage message = new ReturnMessage(obj, array2, this._msg.ArgCount, Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext, this._msg);
				this.AsyncProcessResponse(message, null, null);
			}
		}

		// Token: 0x060059C2 RID: 22978 RVA: 0x0013C3DE File Offset: 0x0013A5DE
		public ServerChannelSinkStack()
		{
		}

		// Token: 0x040028DD RID: 10461
		private ServerChannelSinkStack.SinkStack _stack;

		// Token: 0x040028DE RID: 10462
		private ServerChannelSinkStack.SinkStack _rememberedStack;

		// Token: 0x040028DF RID: 10463
		private IMessage _asyncMsg;

		// Token: 0x040028E0 RID: 10464
		private MethodInfo _asyncEnd;

		// Token: 0x040028E1 RID: 10465
		private object _serverObject;

		// Token: 0x040028E2 RID: 10466
		private IMethodCallMessage _msg;

		// Token: 0x02000C7A RID: 3194
		private class SinkStack
		{
			// Token: 0x060070BF RID: 28863 RVA: 0x00184783 File Offset: 0x00182983
			public SinkStack()
			{
			}

			// Token: 0x0400380D RID: 14349
			public ServerChannelSinkStack.SinkStack PrevStack;

			// Token: 0x0400380E RID: 14350
			public IServerChannelSink Sink;

			// Token: 0x0400380F RID: 14351
			public object State;
		}
	}
}
