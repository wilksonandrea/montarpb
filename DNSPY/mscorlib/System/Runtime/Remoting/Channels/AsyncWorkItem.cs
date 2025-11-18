using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x02000837 RID: 2103
	internal class AsyncWorkItem : IMessageSink
	{
		// Token: 0x060059D0 RID: 22992 RVA: 0x0013C885 File Offset: 0x0013AA85
		[SecurityCritical]
		internal AsyncWorkItem(IMessageSink replySink, Context oldCtx)
			: this(null, replySink, oldCtx, null)
		{
		}

		// Token: 0x060059D1 RID: 22993 RVA: 0x0013C891 File Offset: 0x0013AA91
		[SecurityCritical]
		internal AsyncWorkItem(IMessage reqMsg, IMessageSink replySink, Context oldCtx, ServerIdentity srvID)
		{
			this._reqMsg = reqMsg;
			this._replySink = replySink;
			this._oldCtx = oldCtx;
			this._callCtx = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
			this._srvID = srvID;
		}

		// Token: 0x060059D2 RID: 22994 RVA: 0x0013C8CC File Offset: 0x0013AACC
		[SecurityCritical]
		internal static object SyncProcessMessageCallback(object[] args)
		{
			IMessageSink messageSink = (IMessageSink)args[0];
			IMessage message = (IMessage)args[1];
			return messageSink.SyncProcessMessage(message);
		}

		// Token: 0x060059D3 RID: 22995 RVA: 0x0013C8F4 File Offset: 0x0013AAF4
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			IMessage message = null;
			if (this._replySink != null)
			{
				Thread.CurrentContext.NotifyDynamicSinks(msg, false, false, true, true);
				object[] array = new object[] { this._replySink, msg };
				InternalCrossContextDelegate internalCrossContextDelegate = new InternalCrossContextDelegate(AsyncWorkItem.SyncProcessMessageCallback);
				message = (IMessage)Thread.CurrentThread.InternalCrossContextCallback(this._oldCtx, internalCrossContextDelegate, array);
			}
			return message;
		}

		// Token: 0x060059D4 RID: 22996 RVA: 0x0013C955 File Offset: 0x0013AB55
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x060059D5 RID: 22997 RVA: 0x0013C966 File Offset: 0x0013AB66
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x060059D6 RID: 22998 RVA: 0x0013C970 File Offset: 0x0013AB70
		[SecurityCritical]
		internal static object FinishAsyncWorkCallback(object[] args)
		{
			AsyncWorkItem asyncWorkItem = (AsyncWorkItem)args[0];
			Context serverContext = asyncWorkItem._srvID.ServerContext;
			LogicalCallContext logicalCallContext = CallContext.SetLogicalCallContext(asyncWorkItem._callCtx);
			serverContext.NotifyDynamicSinks(asyncWorkItem._reqMsg, false, true, true, true);
			IMessageCtrl messageCtrl = serverContext.GetServerContextChain().AsyncProcessMessage(asyncWorkItem._reqMsg, asyncWorkItem);
			CallContext.SetLogicalCallContext(logicalCallContext);
			return null;
		}

		// Token: 0x060059D7 RID: 22999 RVA: 0x0013C9CC File Offset: 0x0013ABCC
		[SecurityCritical]
		internal virtual void FinishAsyncWork(object stateIgnored)
		{
			InternalCrossContextDelegate internalCrossContextDelegate = new InternalCrossContextDelegate(AsyncWorkItem.FinishAsyncWorkCallback);
			object[] array = new object[] { this };
			Thread.CurrentThread.InternalCrossContextCallback(this._srvID.ServerContext, internalCrossContextDelegate, array);
		}

		// Token: 0x040028E8 RID: 10472
		private IMessageSink _replySink;

		// Token: 0x040028E9 RID: 10473
		private ServerIdentity _srvID;

		// Token: 0x040028EA RID: 10474
		private Context _oldCtx;

		// Token: 0x040028EB RID: 10475
		[SecurityCritical]
		private LogicalCallContext _callCtx;

		// Token: 0x040028EC RID: 10476
		private IMessage _reqMsg;
	}
}
