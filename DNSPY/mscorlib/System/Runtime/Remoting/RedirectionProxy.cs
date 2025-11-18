using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007BF RID: 1983
	internal class RedirectionProxy : MarshalByRefObject, IMessageSink
	{
		// Token: 0x060055D1 RID: 21969 RVA: 0x00130A18 File Offset: 0x0012EC18
		[SecurityCritical]
		internal RedirectionProxy(MarshalByRefObject proxy, Type serverType)
		{
			this._proxy = proxy;
			this._realProxy = RemotingServices.GetRealProxy(this._proxy);
			this._serverType = serverType;
			this._objectMode = WellKnownObjectMode.Singleton;
		}

		// Token: 0x17000E22 RID: 3618
		// (set) Token: 0x060055D2 RID: 21970 RVA: 0x00130A46 File Offset: 0x0012EC46
		public WellKnownObjectMode ObjectMode
		{
			set
			{
				this._objectMode = value;
			}
		}

		// Token: 0x060055D3 RID: 21971 RVA: 0x00130A50 File Offset: 0x0012EC50
		[SecurityCritical]
		public virtual IMessage SyncProcessMessage(IMessage msg)
		{
			IMessage message = null;
			try
			{
				msg.Properties["__Uri"] = this._realProxy.IdentityObject.URI;
				if (this._objectMode == WellKnownObjectMode.Singleton)
				{
					message = this._realProxy.Invoke(msg);
				}
				else
				{
					MarshalByRefObject marshalByRefObject = (MarshalByRefObject)Activator.CreateInstance(this._serverType, true);
					RealProxy realProxy = RemotingServices.GetRealProxy(marshalByRefObject);
					message = realProxy.Invoke(msg);
				}
			}
			catch (Exception ex)
			{
				message = new ReturnMessage(ex, msg as IMethodCallMessage);
			}
			return message;
		}

		// Token: 0x060055D4 RID: 21972 RVA: 0x00130ADC File Offset: 0x0012ECDC
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

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x060055D5 RID: 21973 RVA: 0x00130AFF File Offset: 0x0012ECFF
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x0400277E RID: 10110
		private MarshalByRefObject _proxy;

		// Token: 0x0400277F RID: 10111
		[SecurityCritical]
		private RealProxy _realProxy;

		// Token: 0x04002780 RID: 10112
		private Type _serverType;

		// Token: 0x04002781 RID: 10113
		private WellKnownObjectMode _objectMode;
	}
}
