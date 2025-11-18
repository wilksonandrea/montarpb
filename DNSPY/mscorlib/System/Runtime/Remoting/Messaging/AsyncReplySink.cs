using System;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000886 RID: 2182
	internal class AsyncReplySink : IMessageSink
	{
		// Token: 0x06005C93 RID: 23699 RVA: 0x001449DA File Offset: 0x00142BDA
		internal AsyncReplySink(IMessageSink replySink, Context cliCtx)
		{
			this._replySink = replySink;
			this._cliCtx = cliCtx;
		}

		// Token: 0x06005C94 RID: 23700 RVA: 0x001449F0 File Offset: 0x00142BF0
		[SecurityCritical]
		internal static object SyncProcessMessageCallback(object[] args)
		{
			IMessage message = (IMessage)args[0];
			IMessageSink messageSink = (IMessageSink)args[1];
			Thread.CurrentContext.NotifyDynamicSinks(message, true, false, true, true);
			return messageSink.SyncProcessMessage(message);
		}

		// Token: 0x06005C95 RID: 23701 RVA: 0x00144A28 File Offset: 0x00142C28
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message = null;
			if (this._replySink != null)
			{
				object[] array = new object[] { reqMsg, this._replySink };
				InternalCrossContextDelegate internalCrossContextDelegate = new InternalCrossContextDelegate(AsyncReplySink.SyncProcessMessageCallback);
				message = (IMessage)Thread.CurrentThread.InternalCrossContextCallback(this._cliCtx, internalCrossContextDelegate, array);
			}
			return message;
		}

		// Token: 0x06005C96 RID: 23702 RVA: 0x00144A79 File Offset: 0x00142C79
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06005C97 RID: 23703 RVA: 0x00144A80 File Offset: 0x00142C80
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x040029D3 RID: 10707
		private IMessageSink _replySink;

		// Token: 0x040029D4 RID: 10708
		private Context _cliCtx;
	}
}
