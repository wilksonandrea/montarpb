using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007C0 RID: 1984
	internal class ComRedirectionProxy : MarshalByRefObject, IMessageSink
	{
		// Token: 0x060055D6 RID: 21974 RVA: 0x00130B02 File Offset: 0x0012ED02
		internal ComRedirectionProxy(MarshalByRefObject comObject, Type serverType)
		{
			this._comObject = comObject;
			this._serverType = serverType;
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x00130B18 File Offset: 0x0012ED18
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			IMethodCallMessage methodCallMessage = (IMethodCallMessage)msg;
			IMethodReturnMessage methodReturnMessage = RemotingServices.ExecuteMessage(this._comObject, methodCallMessage);
			if (methodReturnMessage != null)
			{
				COMException ex = methodReturnMessage.Exception as COMException;
				if (ex != null && (ex._HResult == -2147023174 || ex._HResult == -2147023169))
				{
					this._comObject = (MarshalByRefObject)Activator.CreateInstance(this._serverType, true);
					methodReturnMessage = RemotingServices.ExecuteMessage(this._comObject, methodCallMessage);
				}
			}
			return methodReturnMessage;
		}

		// Token: 0x060055D8 RID: 21976 RVA: 0x00130B8C File Offset: 0x0012ED8C
		[SecurityCritical]
		public virtual IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			IMessage message = this.SyncProcessMessage(msg);
			if (replySink != null)
			{
				replySink.SyncProcessMessage(message);
			}
			return null;
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x060055D9 RID: 21977 RVA: 0x00130BAF File Offset: 0x0012EDAF
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x04002782 RID: 10114
		private MarshalByRefObject _comObject;

		// Token: 0x04002783 RID: 10115
		private Type _serverType;
	}
}
