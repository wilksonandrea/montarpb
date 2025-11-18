using System;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000888 RID: 2184
	internal class DisposeSink : IMessageSink
	{
		// Token: 0x06005C9F RID: 23711 RVA: 0x00144C13 File Offset: 0x00142E13
		internal DisposeSink(IDisposable iDis, IMessageSink replySink)
		{
			this._iDis = iDis;
			this._replySink = replySink;
		}

		// Token: 0x06005CA0 RID: 23712 RVA: 0x00144C2C File Offset: 0x00142E2C
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage reqMsg)
		{
			IMessage message = null;
			try
			{
				if (this._replySink != null)
				{
					message = this._replySink.SyncProcessMessage(reqMsg);
				}
			}
			finally
			{
				this._iDis.Dispose();
			}
			return message;
		}

		// Token: 0x06005CA1 RID: 23713 RVA: 0x00144C70 File Offset: 0x00142E70
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06005CA2 RID: 23714 RVA: 0x00144C77 File Offset: 0x00142E77
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this._replySink;
			}
		}

		// Token: 0x040029D7 RID: 10711
		private IDisposable _iDis;

		// Token: 0x040029D8 RID: 10712
		private IMessageSink _replySink;
	}
}
