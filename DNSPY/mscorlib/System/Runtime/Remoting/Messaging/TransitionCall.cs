using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000871 RID: 2161
	[Serializable]
	internal class TransitionCall : IMessage, IInternalMessage, IMessageSink, ISerializable
	{
		// Token: 0x06005BD7 RID: 23511 RVA: 0x00142088 File Offset: 0x00140288
		[SecurityCritical]
		internal TransitionCall(IntPtr targetCtxID, CrossContextDelegate deleg)
		{
			this._sourceCtxID = Thread.CurrentContext.InternalContextID;
			this._targetCtxID = targetCtxID;
			this._delegate = deleg;
			this._targetDomainID = 0;
			this._eeData = IntPtr.Zero;
			this._srvID = new ServerIdentity(null, Thread.GetContextInternal(this._targetCtxID));
			this._ID = this._srvID;
			this._ID.RaceSetChannelSink(CrossContextChannel.MessageSink);
			this._srvID.RaceSetServerObjectChain(this);
		}

		// Token: 0x06005BD8 RID: 23512 RVA: 0x0014210C File Offset: 0x0014030C
		[SecurityCritical]
		internal TransitionCall(IntPtr targetCtxID, IntPtr eeData, int targetDomainID)
		{
			this._sourceCtxID = Thread.CurrentContext.InternalContextID;
			this._targetCtxID = targetCtxID;
			this._delegate = null;
			this._targetDomainID = targetDomainID;
			this._eeData = eeData;
			this._srvID = null;
			this._ID = new Identity("TransitionCallURI", null);
			CrossAppDomainData crossAppDomainData = new CrossAppDomainData(this._targetCtxID, this._targetDomainID, Identity.ProcessGuid);
			string text;
			IMessageSink messageSink = CrossAppDomainChannel.AppDomainChannel.CreateMessageSink(null, crossAppDomainData, out text);
			this._ID.RaceSetChannelSink(messageSink);
		}

		// Token: 0x06005BD9 RID: 23513 RVA: 0x00142198 File Offset: 0x00140398
		internal TransitionCall(SerializationInfo info, StreamingContext context)
		{
			if (info == null || context.State != StreamingContextStates.CrossAppDomain)
			{
				throw new ArgumentNullException("info");
			}
			this._props = (IDictionary)info.GetValue("props", typeof(IDictionary));
			this._delegate = (CrossContextDelegate)info.GetValue("delegate", typeof(CrossContextDelegate));
			this._sourceCtxID = (IntPtr)info.GetValue("sourceCtxID", typeof(IntPtr));
			this._targetCtxID = (IntPtr)info.GetValue("targetCtxID", typeof(IntPtr));
			this._eeData = (IntPtr)info.GetValue("eeData", typeof(IntPtr));
			this._targetDomainID = info.GetInt32("targetDomainID");
		}

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06005BDA RID: 23514 RVA: 0x00142278 File Offset: 0x00140478
		public IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._props == null)
				{
					lock (this)
					{
						if (this._props == null)
						{
							this._props = new Hashtable();
						}
					}
				}
				return this._props;
			}
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06005BDB RID: 23515 RVA: 0x001422D0 File Offset: 0x001404D0
		// (set) Token: 0x06005BDC RID: 23516 RVA: 0x00142354 File Offset: 0x00140554
		ServerIdentity IInternalMessage.ServerIdentityObject
		{
			[SecurityCritical]
			get
			{
				if (this._targetDomainID != 0 && this._srvID == null)
				{
					lock (this)
					{
						if (Thread.GetContextInternal(this._targetCtxID) == null)
						{
							Context defaultContext = Context.DefaultContext;
						}
						this._srvID = new ServerIdentity(null, Thread.GetContextInternal(this._targetCtxID));
						this._srvID.RaceSetServerObjectChain(this);
					}
				}
				return this._srvID;
			}
			[SecurityCritical]
			set
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06005BDD RID: 23517 RVA: 0x00142365 File Offset: 0x00140565
		// (set) Token: 0x06005BDE RID: 23518 RVA: 0x0014236D File Offset: 0x0014056D
		Identity IInternalMessage.IdentityObject
		{
			[SecurityCritical]
			get
			{
				return this._ID;
			}
			[SecurityCritical]
			set
			{
				throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
			}
		}

		// Token: 0x06005BDF RID: 23519 RVA: 0x0014237E File Offset: 0x0014057E
		[SecurityCritical]
		void IInternalMessage.SetURI(string uri)
		{
			throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
		}

		// Token: 0x06005BE0 RID: 23520 RVA: 0x0014238F File Offset: 0x0014058F
		[SecurityCritical]
		void IInternalMessage.SetCallContext(LogicalCallContext callContext)
		{
			throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
		}

		// Token: 0x06005BE1 RID: 23521 RVA: 0x001423A0 File Offset: 0x001405A0
		[SecurityCritical]
		bool IInternalMessage.HasProperties()
		{
			throw new RemotingException(Environment.GetResourceString("Remoting_Default"));
		}

		// Token: 0x06005BE2 RID: 23522 RVA: 0x001423B4 File Offset: 0x001405B4
		[SecurityCritical]
		public IMessage SyncProcessMessage(IMessage msg)
		{
			try
			{
				LogicalCallContext logicalCallContext = Message.PropagateCallContextFromMessageToThread(msg);
				if (this._delegate != null)
				{
					this._delegate();
				}
				else
				{
					CallBackHelper callBackHelper = new CallBackHelper(this._eeData, true, this._targetDomainID);
					CrossContextDelegate crossContextDelegate = new CrossContextDelegate(callBackHelper.Func);
					crossContextDelegate();
				}
				Message.PropagateCallContextFromThreadToMessage(msg, logicalCallContext);
			}
			catch (Exception ex)
			{
				ReturnMessage returnMessage = new ReturnMessage(ex, new ErrorMessage());
				returnMessage.SetLogicalCallContext((LogicalCallContext)msg.Properties[Message.CallContextKey]);
				return returnMessage;
			}
			return this;
		}

		// Token: 0x06005BE3 RID: 23523 RVA: 0x00142454 File Offset: 0x00140654
		[SecurityCritical]
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			IMessage message = this.SyncProcessMessage(msg);
			replySink.SyncProcessMessage(message);
			return null;
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06005BE4 RID: 23524 RVA: 0x00142472 File Offset: 0x00140672
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return null;
			}
		}

		// Token: 0x06005BE5 RID: 23525 RVA: 0x00142478 File Offset: 0x00140678
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null || context.State != StreamingContextStates.CrossAppDomain)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("props", this._props, typeof(IDictionary));
			info.AddValue("delegate", this._delegate, typeof(CrossContextDelegate));
			info.AddValue("sourceCtxID", this._sourceCtxID);
			info.AddValue("targetCtxID", this._targetCtxID);
			info.AddValue("targetDomainID", this._targetDomainID);
			info.AddValue("eeData", this._eeData);
		}

		// Token: 0x0400298A RID: 10634
		private IDictionary _props;

		// Token: 0x0400298B RID: 10635
		private IntPtr _sourceCtxID;

		// Token: 0x0400298C RID: 10636
		private IntPtr _targetCtxID;

		// Token: 0x0400298D RID: 10637
		private int _targetDomainID;

		// Token: 0x0400298E RID: 10638
		private ServerIdentity _srvID;

		// Token: 0x0400298F RID: 10639
		private Identity _ID;

		// Token: 0x04002990 RID: 10640
		private CrossContextDelegate _delegate;

		// Token: 0x04002991 RID: 10641
		private IntPtr _eeData;
	}
}
