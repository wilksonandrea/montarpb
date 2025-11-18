using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200086C RID: 2156
	[SecurityCritical]
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class ConstructionCall : MethodCall, IConstructionCallMessage, IMethodCallMessage, IMethodMessage, IMessage
	{
		// Token: 0x06005B9A RID: 23450 RVA: 0x0014118B File Offset: 0x0013F38B
		public ConstructionCall(Header[] headers)
			: base(headers)
		{
		}

		// Token: 0x06005B9B RID: 23451 RVA: 0x00141194 File Offset: 0x0013F394
		public ConstructionCall(IMessage m)
			: base(m)
		{
		}

		// Token: 0x06005B9C RID: 23452 RVA: 0x0014119D File Offset: 0x0013F39D
		internal ConstructionCall(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06005B9D RID: 23453 RVA: 0x001411A8 File Offset: 0x0013F3A8
		[SecurityCritical]
		internal override bool FillSpecialHeader(string key, object value)
		{
			if (key != null)
			{
				if (key.Equals("__ActivationType"))
				{
					this._activationType = null;
				}
				else if (key.Equals("__ContextProperties"))
				{
					this._contextProperties = (IList)value;
				}
				else if (key.Equals("__CallSiteActivationAttributes"))
				{
					this._callSiteActivationAttributes = (object[])value;
				}
				else if (key.Equals("__Activator"))
				{
					this._activator = (IActivator)value;
				}
				else
				{
					if (!key.Equals("__ActivationTypeName"))
					{
						return base.FillSpecialHeader(key, value);
					}
					this._activationTypeName = (string)value;
				}
			}
			return true;
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06005B9E RID: 23454 RVA: 0x00141247 File Offset: 0x0013F447
		public object[] CallSiteActivationAttributes
		{
			[SecurityCritical]
			get
			{
				return this._callSiteActivationAttributes;
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06005B9F RID: 23455 RVA: 0x0014124F File Offset: 0x0013F44F
		public Type ActivationType
		{
			[SecurityCritical]
			get
			{
				if (this._activationType == null && this._activationTypeName != null)
				{
					this._activationType = RemotingServices.InternalGetTypeFromQualifiedTypeName(this._activationTypeName, false);
				}
				return this._activationType;
			}
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06005BA0 RID: 23456 RVA: 0x0014127F File Offset: 0x0013F47F
		public string ActivationTypeName
		{
			[SecurityCritical]
			get
			{
				return this._activationTypeName;
			}
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06005BA1 RID: 23457 RVA: 0x00141287 File Offset: 0x0013F487
		public IList ContextProperties
		{
			[SecurityCritical]
			get
			{
				if (this._contextProperties == null)
				{
					this._contextProperties = new ArrayList();
				}
				return this._contextProperties;
			}
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06005BA2 RID: 23458 RVA: 0x001412A4 File Offset: 0x0013F4A4
		public override IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				IDictionary externalProperties;
				lock (this)
				{
					if (this.InternalProperties == null)
					{
						this.InternalProperties = new Hashtable();
					}
					if (this.ExternalProperties == null)
					{
						this.ExternalProperties = new CCMDictionary(this, this.InternalProperties);
					}
					externalProperties = this.ExternalProperties;
				}
				return externalProperties;
			}
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06005BA3 RID: 23459 RVA: 0x00141310 File Offset: 0x0013F510
		// (set) Token: 0x06005BA4 RID: 23460 RVA: 0x00141318 File Offset: 0x0013F518
		public IActivator Activator
		{
			[SecurityCritical]
			get
			{
				return this._activator;
			}
			[SecurityCritical]
			set
			{
				this._activator = value;
			}
		}

		// Token: 0x04002973 RID: 10611
		internal Type _activationType;

		// Token: 0x04002974 RID: 10612
		internal string _activationTypeName;

		// Token: 0x04002975 RID: 10613
		internal IList _contextProperties;

		// Token: 0x04002976 RID: 10614
		internal object[] _callSiteActivationAttributes;

		// Token: 0x04002977 RID: 10615
		internal IActivator _activator;
	}
}
